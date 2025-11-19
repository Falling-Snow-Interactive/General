using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.General.Sprites.Preview
{
    [CustomPropertyDrawer(typeof(SpritePreviewAttribute))]
    public class SpritePreviewAttributeDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var attr = (SpritePreviewAttribute)attribute;
            float size = attr.Size;

            // Root container for label + preview + field
            VisualElement root = new()
                                 {
                                     style =
                                     {
                                         flexDirection = FlexDirection.Row,
                                         alignItems = Align.Center,
                                     },
                                 };

            // Preview properties
            const float margin = 4;
            const float borderWidth = 1;
            Color borderColor = new(0, 0, 0, 0.3f);
            
            // Preview
            VisualElement preview = new()
                                    {
                                        style =
                                        {
                                            width = size,
                                            height = size,
                                            
                                            marginRight = margin,
                                            
                                            borderTopLeftRadius = borderWidth,
                                            borderTopRightRadius = borderWidth,
                                            borderBottomLeftRadius = borderWidth,
                                            borderBottomRightRadius = borderWidth,
                                            
                                            borderLeftWidth = borderWidth,
                                            borderTopWidth = borderWidth,
                                            borderRightWidth = borderWidth,
                                            borderBottomWidth = borderWidth,
                                            
                                            borderTopColor = borderColor,
                                            borderRightColor = borderColor,
                                            borderBottomColor = borderColor,
                                            borderLeftColor = borderColor,
                                            
                                            backgroundColor = borderColor,
                                            
                                            // Set background layout behaviour
                                            backgroundPositionX = new BackgroundPosition(BackgroundPositionKeyword.Center), // BackgroundPositionX.Center;
                                            backgroundPositionY = new BackgroundPosition(BackgroundPositionKeyword.Center), // BackgroundPositionY.Center;
                                            backgroundRepeat = new BackgroundRepeat(Repeat.NoRepeat, Repeat.NoRepeat), // BackgroundRepeat.NoRepeat;
                                            backgroundSize = new BackgroundSize(BackgroundSizeType.Contain),
                                        },
                                    };

            root.Add(preview);
            
            PropertyField spriteProp = new(property)
                                       {
                                           dataSourceType = typeof(Sprite),
                                           style =
                                           {
                                               flexGrow = 1,
                                           },
                                       };
            spriteProp.BindProperty(property);
            root.Add(spriteProp);

            // Initial preview setup
            UpdatePreview(preview, property);

            spriteProp.RegisterValueChangeCallback(evt => UpdatePreview(preview, property));

            return root;
        }

        private void UpdatePreview(VisualElement preview, SerializedProperty property)
        {
            Sprite sprite = property.objectReferenceValue as Sprite;

            if (sprite != null)
            {
                Texture2D tex = sprite.texture;
                if (tex != null)
                {
                    preview.style.backgroundImage = new StyleBackground(tex);
                    preview.tooltip = sprite.name;
                    return;
                }
            }

            // If no sprite or no texture, clear preview
            preview.style.backgroundImage = new StyleBackground(StyleKeyword.None); // StyleBackground.None();
            preview.tooltip = "No Sprite";
        }
    }
}