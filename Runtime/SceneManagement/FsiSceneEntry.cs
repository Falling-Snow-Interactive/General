using System;
using UnityEditor;
using UnityEngine;

namespace Fsi.General.SceneManagement
{
	[Serializable]
	public class FsiSceneEntry : ISerializationCallbackReceiver
	{
		[SerializeField]
		private string name;
		public string Name => name;

		public void OnBeforeSerialize()
		{
			#if UNITY_EDITOR
			if (scene != null) name = scene.name;
			#endif
		}

		public void OnAfterDeserialize()
		{
		}

		#if UNITY_EDITOR
		[SerializeField]
		private SceneAsset scene;
		public SceneAsset Scene => scene;
		#endif
	}
}