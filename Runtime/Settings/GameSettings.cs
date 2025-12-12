using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Fsi.General.Settings
{
    public class GameSettings : ScriptableObject
    {
        private const string ResourcePath = "Settings/GameSettings";
        private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

        private static GameSettings settings;
        private static GameSettings Settings => settings ??= GetOrCreateSettings();

        [Header("Launch")]

        [SerializeField]
        private List<GameObject> launchObjects;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnGameLaunch()
        {
            foreach (GameObject obj in Settings.launchObjects)
            {
                GameObject go = Instantiate(obj);
                go.name = obj.name;
            }
        }
    
        #region Settings

        private static GameSettings GetOrCreateSettings()
        {
            GameSettings s = Resources.Load<GameSettings>(ResourcePath);

            #if UNITY_EDITOR
            if (!s)
            {
                if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                {
                    AssetDatabase.CreateFolder("Assets", "Resources");
                }

                if (!AssetDatabase.IsValidFolder("Assets/Resources/Settings"))
                {
                    AssetDatabase.CreateFolder("Assets/Resources", "Settings");
                }

                s = CreateInstance<GameSettings>();
                AssetDatabase.CreateAsset(s, FullPath);
                AssetDatabase.SaveAssets();
            }
            #endif

            return s;
        }

        #if UNITY_EDITOR
        public static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }
        #endif

        #endregion
    }
}