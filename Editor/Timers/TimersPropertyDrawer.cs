using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.General.Timers
{
    [CustomPropertyDrawer(typeof(Timer))]
    public class TimersPropertyDrawer : PropertyDrawer
    {
        #region Seralized Properties

        private SerializedProperty property;

        private SerializedProperty timeProp;
        private SerializedProperty remProp;
        private SerializedProperty statusProp;
        
        #endregion
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            this.property = property;
            Foldout root = new(){text = property.displayName};

            BuildToolbar(root);
            
            // Serialized fields
            remProp = property.FindPropertyRelative("remaining");
            timeProp = property.FindPropertyRelative("time");
            statusProp = property.FindPropertyRelative("status");
            
            // Time Group
            #region Time Group
            
            VisualElement timeGroup = new(){style = { flexDirection = FlexDirection.Row}};
            root.Add(timeGroup);
            
            FloatField remField = new("") { style = { flexGrow = 1 } };
            remField.BindProperty(remProp);
            timeGroup.Add(remField);

            Label div = new("   /");
            timeGroup.Add(div);
            
            FloatField timeField = new("") { style = { flexGrow = 1 } };
            timeField.BindProperty(timeProp);
            timeGroup.Add(timeField);
            
            #endregion
            
            #region Status Group
            
            // Status Group
            
            EnumField statusField = new(statusProp.displayName, (TimerStatus)statusProp.enumValueIndex);
            statusField.BindProperty(statusProp);
            root.Add(statusField);
            
            #endregion
            
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

        private void BuildToolbar(VisualElement root)
        {
            #region Toolbar
            // Optional: your toolbar/header
            Toolbar toolbar = new();
            root.Add(toolbar);
            
            #region Buttons
            
            // Play
            ToolbarButton playButton = new()
                                       {
                                           name = "Play",
                                           text = "Play",
                                       };
            
            playButton.clicked += OnPlayButton;
            toolbar.Add(playButton);
            
            // Pause
            ToolbarButton pauseButton = new()
                                        {
                                            name = "Pause",
                                            text = "Pause",
                                        };
            pauseButton.clicked += OnPauseButton;

            toolbar.Add(pauseButton);
            
            // Stop
            ToolbarButton stopButton = new()
                                       {
                                           name = "Stop",
                                           text = "Stop",
                                       };
            stopButton.clicked += OnStopButton;

            toolbar.Add(stopButton);
            
            #endregion
            
            #endregion
        }

        #region Toolbar Button Callbacks

        private void OnPlayButton()
        {
            statusProp.enumValueIndex = (int)TimerStatus.Running;
            property.serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(property.serializedObject.targetObject);
        }

        private void OnPauseButton()
        {
            statusProp.enumValueIndex = (int)TimerStatus.Pause;
            property.serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(property.serializedObject.targetObject);
        }

        private void OnStopButton()
        {
            statusProp.enumValueIndex = (int)TimerStatus.Finished;
            property.serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(property.serializedObject.targetObject);
        }
        
        #endregion
    }
}