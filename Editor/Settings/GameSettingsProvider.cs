using Fsi.Settings;
using UnityEditor;
using UnityEngine.UIElements;

namespace Fsi.General.Settings
{
    public static class GameSettingsProvider
    {
        private const string Name = "Game";
        
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return SettingsEditorUtility.CreateSettingsProvider(Name, "Falling Snow Interactive/" + Name, OnActivate);
        }

        private static void OnActivate(string searchContext, VisualElement root)
        {
            SerializedObject prop = GameSettings.GetSerializedSettings();
            VisualElement settings = SettingsEditorUtility.CreateSettingsPage(prop, Name);
            root.Add(settings);
        }
    }
}