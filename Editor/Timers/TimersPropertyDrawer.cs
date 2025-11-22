using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.General.Timers
{
    [CustomPropertyDrawer(typeof(Timer))]
    public class TimersPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            Foldout root = new(){text = property.displayName};

            // Optional: your toolbar/header
            Toolbar toolbar = new();
            root.Add(toolbar);
            
            // Serialized fields

            SerializedProperty statusProp = property.FindPropertyRelative("status");
            EnumField statusField = new(statusProp.displayName, (TimerStatus)statusProp.enumValueIndex);
            statusField.BindProperty(statusProp);
            root.Add(statusField);

            VisualElement timeGroup = new(){style = { flexDirection = FlexDirection.Row}};
            root.Add(timeGroup);
            
            SerializedProperty remProp = property.FindPropertyRelative("remaining");
            FloatField remField = new("") { style = { flexGrow = 1 } };
            remField.BindProperty(remProp);
            timeGroup.Add(remField);

            Label div = new("   /");
            timeGroup.Add(div);
            
            SerializedProperty timeProp = property.FindPropertyRelative("time");
            FloatField timeField = new("") { style = { flexGrow = 1 } };
            timeField.BindProperty(timeProp);
            timeGroup.Add(timeField);
            
            // Build toolbar
            
            // Play
            ToolbarButton playButton = new()
                                       {
                                           name = "Play",
                                           text = "Play",
                                       };
            playButton.clicked += () =>
                                  {
                                      statusProp.enumValueIndex = (int)TimerStatus.Running;
                                      property.serializedObject.ApplyModifiedProperties();
                                      EditorUtility.SetDirty(property.serializedObject.targetObject);
                                  };

            toolbar.Add(playButton);
            
            // Pause
            ToolbarButton pauseButton = new()
                                       {
                                           name = "Pause",
                                           text = "Pause",
                                       };
            pauseButton.clicked += () =>
                                  {
                                      statusProp.enumValueIndex = (int)TimerStatus.Pause;
                                      property.serializedObject.ApplyModifiedProperties();
                                      EditorUtility.SetDirty(property.serializedObject.targetObject);
                                  };

            toolbar.Add(pauseButton);
            
            // Stop
            ToolbarButton stopButton = new()
                                        {
                                            name = "Stop",
                                            text = "Stop",
                                        };
            stopButton.clicked += () =>
                                   {
                                       statusProp.enumValueIndex = (int)TimerStatus.Finished;
                                       property.serializedObject.ApplyModifiedProperties();
                                       EditorUtility.SetDirty(property.serializedObject.targetObject);
                                   };

            toolbar.Add(stopButton);
            
            // Progress Bar
            ProgressBar bar = new();
            root.Add(bar);
            
            timeField.RegisterValueChangedCallback(evt =>
                                                   {
                                                       bar.highValue = evt.newValue;
                                                       property.serializedObject.ApplyModifiedProperties();
                                                       EditorUtility.SetDirty(property.serializedObject.targetObject);
                                                   });
            
            remField.RegisterValueChangedCallback(evt =>
                                                   {
                                                       bar.value = evt.newValue;
                                                       property.serializedObject.ApplyModifiedProperties();
                                                       EditorUtility.SetDirty(property.serializedObject.targetObject);
                                                   });
            
            return root;
        }
    }
}