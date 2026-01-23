using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fsi.General.SceneManagement
{
	public class FsiSceneManager<T> : MbSingleton<T>
		where T : MonoBehaviour
	{
		private static bool DebugLog => FsiSceneManagerUtility.Settings.DebugLog;

		// ReSharper disable Unity.PerformanceAnalysis
		public void LoadScene(string sceneName, LoadSceneMode mode)
		{
			SceneManager.LoadScene(sceneName, mode);
			if (DebugLog)
			{
				Debug.Log($"SceneManagement | Loaded {sceneName}");
			}
		}

		// ReSharper disable Unity.PerformanceAnalysis
		public void LoadSceneAsync(string sceneName, LoadSceneMode mode, Action onComplete = null)
		{
			if (DebugLog)
			{
				Debug.Log($"SceneManagement | Loading {sceneName}");
			}

			AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(sceneName, mode);
			if (sceneAsync != null)
			{
				if (DebugLog)
				{
					Debug.Log($"SceneManagement | Loaded {sceneName}");
				}

				sceneAsync.completed += _ => onComplete?.Invoke();
			}
		}

		// ReSharper disable Unity.PerformanceAnalysis
		public void UnloadScene(string sceneName, Action onComplete = null)
		{
			if (DebugLog)
			{
				Debug.Log($"SceneManagement | Unloading {sceneName}");
			}

			AsyncOperation sceneAsync = SceneManager.UnloadSceneAsync(sceneName);
			if (sceneAsync != null)
			{
				if (DebugLog)
				{
					Debug.Log($"SceneManagement | Unloaded {sceneName}");
				}

				sceneAsync.completed += _ => onComplete?.Invoke();
			}
		}
	}
}