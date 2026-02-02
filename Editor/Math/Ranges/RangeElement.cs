using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.General.Math.Ranges
{
    public class RangeElement : VisualElement
    {
        private const string USS = "Packages/com.fallingsnowinteractive.general/Editor/Math/Ranges/RangeElement.uss";
        
        #region USS Classes

        private const string RootClass = "range_root";
        private const string LabelClass = "range_label";
        private const string FieldClass = "range_field";
        private const string FieldLabelClass = "range_field_label";
        private const string FieldValueClass = "range_field_value";
        private const string FieldSpacerClass = "range_field_space";

        #endregion

        private readonly Label rootLabel;
        
        /// <summary>
        /// 
        /// </summary>
        public RangeElement(string label, SerializedProperty property)
        {
            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(USS);
            if (styleSheet)
            {
                styleSheets.Add(styleSheet);
            }

            VisualElement root = new();
            root.AddToClassList(RootClass);
            Add(root);

            rootLabel = new Label(label);
            rootLabel.AddToClassList(LabelClass);
            root.Add(rootLabel);

            VisualElement fieldGroup = new();
            fieldGroup.AddToClassList(FieldClass);
            root.Add(fieldGroup);

            Label minLabel = new("Min");
            minLabel.AddToClassList(FieldLabelClass);
            fieldGroup.Add(minLabel);

            SerializedProperty minProp = property.FindPropertyRelative("min");
            PropertyField minField = new(minProp) { label = "" };
            minField.AddToClassList(FieldValueClass);
            fieldGroup.Add(minField);

            VisualElement fieldSpacer = new();
            fieldSpacer.AddToClassList(FieldSpacerClass);
            fieldGroup.Add(fieldSpacer);

            Label maxLabel = new("Max");
            maxLabel.AddToClassList(FieldLabelClass);
            fieldGroup.Add(maxLabel);

            SerializedProperty maxProp = property.FindPropertyRelative("max");
            PropertyField maxField = new(maxProp) { label = "" };
            maxField.AddToClassList(FieldValueClass);
            fieldGroup.Add(maxField);

            // When used inside a MultiColumnListView cell, hide the top label.
            RegisterCallback<AttachToPanelEvent>(_ => UpdateRootLabelVisibility());
        }

        private void UpdateRootLabelVisibility()
        {
            bool hide = IsInMultiColumnListView();
            rootLabel.style.display = hide ? DisplayStyle.None : DisplayStyle.Flex;
        }

        private bool IsInMultiColumnListView()
        {
            for (VisualElement p = this; p != null; p = p.parent)
            {
                if (p is MultiColumnListView)
                    return true;

                // Fallback: Unity's internal root class name for the control.
                if (p.ClassListContains("unity-multi-column-list-view"))
                    return true;
            }

            return false;
        }
    }
}