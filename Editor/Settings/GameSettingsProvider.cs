using Fsi.Settings;
using UnityEditor;

namespace Fsi.General.Settings
{
    public static class GameSettingsProvider
    {
        private const string Name = "Game";
        
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            SerializedObject prop = GameSettings.GetSerializedSettings();
            return SettingsEditorUtility.CreateSettingsProvider<GameSettings>(Name, "Falling Snow Interactive/" + Name, prop);
        }
    }
}