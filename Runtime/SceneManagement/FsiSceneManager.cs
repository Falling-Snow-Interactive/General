using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fsi.General.SceneManagement
{
	public class FsiSceneManager<T> : MbSingleton<T>
		where T : MonoBehaviour
	{
		private static bool DebugLog => FsiSceneManagerUtility.Settings.DebugLog;

		public void LoadScene(string sceneName, LoadSceneMode mode)
		{
			SceneManager.LoadScene(sceneName, mode);
			if (DebugLog) Debug.Log($"SCENE - Loaded {sceneName}");
		}

		public void LoadSceneAsync(string sceneName, LoadSceneMode mode, Action onComplete = null)
		{
			if (DebugLog) Debug.Log($"SCENE: Loading {sceneName}");

			AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(sceneName, mode);
			if (sceneAsync != null)
			{
				if (DebugLog) Debug.Log($"SCENE: Loaded {sceneName}");

				sceneAsync.completed += _ => onComplete?.Invoke();
			}
		}

		public void UnloadScene(string sceneName, Action onComplete = null)
		{
			if (DebugLog) Debug.Log($"SCENE: Unloading {sceneName}");

			AsyncOperation sceneAsync = SceneManager.UnloadSceneAsync(sceneName);
			if (sceneAsync != null)
			{
				if (DebugLog) Debug.Log($"SCENE: Unloaded {sceneName}");

				sceneAsync.completed += _ => onComplete?.Invoke();
			}
		}
	}
}