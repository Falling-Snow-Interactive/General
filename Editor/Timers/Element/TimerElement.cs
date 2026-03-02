using UnityEditor;
using UnityEngine.UIElements;

namespace Fsi.General.Timers.Element
{
    [UxmlElement]
    public partial class TimerElement : VisualElement
    {
        private const string Path = "Packages/com.fallingsnowinteractive.general/Editor/Timers/Element/TimerElement.uxml";

        [UxmlAttribute]
        private int seconds;
        
        private readonly Label remainingLabel;
        private readonly Label timeLabel;

        // ReSharper disable once MemberCanBePrivate.Global
        public TimerElement()
        {
            VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(Path);

            VisualElement root = new();
            visualTree.CloneTree(root);

            remainingLabel = root.Q<Label>("remaining_label");
            timeLabel = root.Q<Label>("time_label");

            remainingLabel.text = seconds.ToString();
        }

        public TimerElement(Timer timer) : this()
        {
            remainingLabel.text = "Rem";
            timeLabel.text = "Time";
        }
    }
}
