#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.General.RunningLock
{
    [CustomPropertyDrawer(typeof(RunningLockAttribute))]
    public class RunningLockDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            // Create default field
            PropertyField field = new(property);

            // Apply initial state
            field.SetEnabled(!Application.isPlaying);

            // Update when playmode changes
            EditorApplication.playModeStateChanged += state =>
                                                      {
                                                          // Update enabled state when entering/exiting playmode
                                                          field.SetEnabled(!Application.isPlaying);
                                                      };

            return field;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginDisabledGroup(Application.isPlaying);
            EditorGUI.PropertyField(position, property, label);
            EditorGUI.EndDisabledGroup();
        }
    }
}
#endif