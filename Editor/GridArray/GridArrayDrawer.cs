using Fsi.General.GridArrays;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.General.GridArray
{
    [CustomPropertyDrawer(typeof(GridArray<>), true)]
    public abstract class GridArrayDrawer<T> : PropertyDrawer
    {
        private const float GridSpacing = 2f;
        protected abstract string StyleSheetPath { get; }
        protected abstract float CellWidth { get; }
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new()
                                 {
                                     style =
                                     {
                                         flexDirection = FlexDirection.Column,
                                         marginTop = 2f,
                                         marginBottom = 2f,
                                     },
                                 };

            Foldout fold = new() { text = property.displayName, value = false };
            root.Add(fold);

            ScrollView scroll = new();
            fold.Add(scroll);

            if (!string.IsNullOrWhiteSpace(StyleSheetPath))
            {
                StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(StyleSheetPath);
                if (styleSheet != null)
                {
                    root.styleSheets.Add(styleSheet);
                }
            }
            
            // Container for the grid
            VisualElement gridContainer = new()
            {
                style =
                {
                    flexDirection = FlexDirection.Column,

                    paddingTop = GridSpacing,
                    paddingRight = GridSpacing,
                    paddingBottom = GridSpacing,
                    paddingLeft = GridSpacing,
                },
            };
            scroll.Add(gridContainer);
            
            // Properties
            SerializedProperty widthProp = property.FindPropertyRelative("width");
            SerializedProperty heightProp = property.FindPropertyRelative("height");

            Foldout optionFold = new()
                               {
                                   text = "Options",
                                   value = false,
                               };
            fold.Add(optionFold);

            PropertyField widthField = new(widthProp);
            PropertyField heightField = new(heightProp);

            optionFold.Add(widthField);
            optionFold.Add(heightField);

            // Initial build
            RebuildGrid(gridContainer, property);

            // Rebuild grid when width or height change
            widthField.RegisterCallback<ChangeEvent<int>>(_ =>
            {
                RebuildGrid(gridContainer, property);
            });

            heightField.RegisterCallback<ChangeEvent<int>>(_ =>
            {
                RebuildGrid(gridContainer, property);
            });

            return root;
        }

        private void RebuildGrid(VisualElement gridContainer, SerializedProperty property)
        {
            property.serializedObject.Update();

            SerializedProperty widthProp = property.FindPropertyRelative("width");
            SerializedProperty heightProp = property.FindPropertyRelative("height");
            SerializedProperty dataProp = property.FindPropertyRelative("data");

            int width = Mathf.Max(1, widthProp.intValue);
            int height = Mathf.Max(1, heightProp.intValue);

            // Ensure backing array size
            int requiredSize = width * height;
            if (dataProp.arraySize != requiredSize)
            {
                dataProp.arraySize = requiredSize;
                property.serializedObject.ApplyModifiedProperties();
                property.serializedObject.Update();
            }

            gridContainer.Clear();

            for (int row = height-1; row >= 0; row--)
            {
                VisualElement rowContainer = new()
                {
                    style =
                    {
                        flexDirection = FlexDirection.Row,
                        flexGrow = 1,
                        flexShrink = 0,
                        
                        width = new StyleLength(StyleKeyword.Auto),

                        paddingTop = GridSpacing,
                        paddingRight = GridSpacing,
                        paddingBottom = GridSpacing,
                        paddingLeft = GridSpacing,
                    },
                };
                gridContainer.Add(rowContainer);

                Label rowLabel = new($"{row}")
                                 {
                                     style =
                                     {
                                         width = 10, 
                                     },
                                 };
                rowContainer.Add(rowLabel);

                for (int col = 0; col < width; col++)
                {
                    int index = row * width + col;
                    SerializedProperty elementProp = dataProp.GetArrayElementAtIndex(index);

                    VisualElement cell = CreateCell(elementProp);
                    rowContainer.Add(cell);
                }
            }
            
            // Add another one for the y labels
            VisualElement yLabelContainer = new()
                                         {
                                             style =
                                             {
                                                 flexDirection = FlexDirection.Row,
                                                 flexGrow = 1,
                                                 flexShrink = 0,
                        
                                                 width = new StyleLength(StyleKeyword.Auto),

                                                 paddingTop = GridSpacing,
                                                 paddingRight = GridSpacing,
                                                 paddingBottom = GridSpacing,
                                                 paddingLeft = GridSpacing,
                                             },
                                         };
            gridContainer.Add(yLabelContainer);

            VisualElement xLabelSpace = new()
                             {
                                 style =
                                 {
                                     flexGrow = 0,
                                     flexShrink = 0,
                                     width = 10 + GridSpacing * width, 
                                     
                                     unityTextAlign = TextAnchor.MiddleCenter,
                                 },
                             };
            yLabelContainer.Add(xLabelSpace);

            for (int col = 0; col < width; col++)
            {
                Label yLabel = new($"{col}")
                               {
                                   style =
                                   {
                                       flexGrow = 0,
                                       flexShrink = 0,
                                       width = CellWidth,
                                   },
                               };
                yLabelContainer.Add(yLabel);
            }
        }

        protected abstract VisualElement CreateCell(SerializedProperty elementProp);
    }
}