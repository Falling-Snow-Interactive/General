using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.General.Math.Ranges
{
    [CustomPropertyDrawer(typeof(Range<>))]
    public class RangePropertyDrawer : PropertyDrawer
    {
        private const float FieldLabelWidth = 28f;
        private const float FieldSpacing = 8f;
        private const float MinFieldWidth = 60f;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new RangeElement(property.displayName, property);
        }

        #region IMGUI
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty minProp = property.FindPropertyRelative("min");
            SerializedProperty maxProp = property.FindPropertyRelative("max");

            if (minProp == null || maxProp == null)
            {
                EditorGUI.PropertyField(position, property, label, true);
                return;
            }

            using (new EditorGUI.PropertyScope(position, label, property))
            {
                position.height = EditorGUIUtility.singleLineHeight;

                Rect contentRect = EditorGUI.PrefixLabel(position, label);

                int previousIndent = EditorGUI.indentLevel;
                float previousLabelWidth = EditorGUIUtility.labelWidth;
                EditorGUI.indentLevel = 0;

                float availableWidth = contentRect.width - FieldLabelWidth * 2f - FieldSpacing;
                float valueWidth = Mathf.Max(MinFieldWidth, availableWidth * 0.5f);

                Rect minLabelRect = new(contentRect.x, contentRect.y, FieldLabelWidth, contentRect.height);
                Rect minValueRect = new(minLabelRect.xMax, contentRect.y, valueWidth, contentRect.height);
                Rect maxLabelRect = new(minValueRect.xMax + FieldSpacing, contentRect.y, FieldLabelWidth, contentRect.height);
                Rect maxValueRect = new(maxLabelRect.xMax, contentRect.y, Mathf.Max(MinFieldWidth, contentRect.xMax - maxLabelRect.xMax), contentRect.height);

                EditorGUI.LabelField(minLabelRect, "Min");
                EditorGUI.PropertyField(minValueRect, minProp, GUIContent.none);
                EditorGUI.LabelField(maxLabelRect, "Max");
                EditorGUI.PropertyField(maxValueRect, maxProp, GUIContent.none);

                EditorGUIUtility.labelWidth = previousLabelWidth;
                EditorGUI.indentLevel = previousIndent;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty minProp = property.FindPropertyRelative("min");
            SerializedProperty maxProp = property.FindPropertyRelative("max");

            if (minProp == null || maxProp == null)
                return EditorGUI.GetPropertyHeight(property, label, true);

            return EditorGUIUtility.singleLineHeight;
        }
        
        #endregion
    }
}