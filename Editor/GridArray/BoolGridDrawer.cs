#if UNITY_EDITOR
using Fsi.General.GridArrays;
using UnityEditor;
using UnityEngine.UIElements;

namespace Fsi.General.GridArray
{
    [CustomPropertyDrawer(typeof(BoolGrid))]
    public class BoolGridDrawer : GridArrayDrawer<bool>
    {
        protected override string StyleSheetPath => "Packages/com.fallingsnowinteractive.general/Editor/GridArray/BoolGridDrawer.uss";
        protected override float CellWidth => 16f;

        const float CellSize = 18f; // nice small square
        
        protected override VisualElement CreateCell(SerializedProperty elementProp)
        {
            Button cellButton = new()
                                {
                                    text = string.Empty,
                                    style =
                                    {
                                        width = CellSize,
                                        height = CellSize,
                                    },
                                };

            // Base USS class for all cells
            cellButton.AddToClassList("bool-grid__cell");

            // Local method to refresh visuals based on value via USS classes
            void RefreshCellVisual(bool value)
            {
                cellButton.EnableInClassList("bool-grid__cell--on", value);
            }

            bool currentValue = elementProp.boolValue;
            RefreshCellVisual(currentValue);

            cellButton.clicked += () =>
                                  {
                                      currentValue = !currentValue;
                                      elementProp.boolValue = currentValue;
                                      elementProp.serializedObject.ApplyModifiedProperties();
                                      RefreshCellVisual(currentValue);
                                  };

            return cellButton;
        }
    }
}
#endif