using UnityEditor;
using UnityEngine;

namespace Fsi.General.SceneManagement.Settings
{
	public class FsiSceneManagerSettings : ScriptableObject
	{
		private const string ResourcePath = "Settings/FsiSceneManagerSettings";
		private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

		[Header("Debugging")]
		[SerializeField]
		private bool debugLog;

		public bool DebugLog => debugLog;

		public static FsiSceneManagerSettings GetOrCreateSettings()
		{
			FsiSceneManagerSettings settings = Resources.Load<FsiSceneManagerSettings>(ResourcePath);

			#if UNITY_EDITOR
			if (!settings)
			{
				if (!AssetDatabase.IsValidFolder("Assets/Resources")) AssetDatabase.CreateFolder("Assets", "Resources");

				if (!AssetDatabase.IsValidFolder("Assets/Resources/Settings"))
					AssetDatabase.CreateFolder("Assets/Resources", "Settings");

				settings = CreateInstance<FsiSceneManagerSettings>();
				AssetDatabase.CreateAsset(settings, FullPath);
				AssetDatabase.SaveAssets();
			}
			#endif

			return settings;
		}

		#if UNITY_EDITOR
		public static SerializedObject GetSerializedSettings()
		{
			return new SerializedObject(GetOrCreateSettings());
		}
		#endif
	}
}