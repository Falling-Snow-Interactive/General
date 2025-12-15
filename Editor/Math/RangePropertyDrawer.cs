using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.General.Math
{
    // [CustomPropertyDrawer(typeof(Range<>))]
    public class RangePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new();

            SerializedProperty minProp = property.FindPropertyRelative("min");
            SerializedProperty maxProp = property.FindPropertyRelative("max");

            PropertyField minField = new(minProp)
                                     {
                                         label = $"\t\t{minProp.displayName}",
                                         style = { height = EditorGUIUtility.singleLineHeight },
                                     };
            
            PropertyField maxField = new(maxProp)
                                     {
                                         label = $"\t\t{maxProp.displayName}",
                                         style = { height = EditorGUIUtility.singleLineHeight },
                                     };
            
            root.Add(new Label($"{property.displayName}"));
            root.Add(minField);
            root.Add(maxField);
            
            return root;
        }
    }
}