using Fsi.General.RunningLock;
using UnityEngine;

namespace Fsi.General
{
	public abstract class MbSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		[RunningLock]
		public bool dontDestroyOnLoad;
		
		public static T Instance { get; private set; }

		protected virtual void Awake()
		{
			if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);

			if (Instance == null)
				Instance = this as T;
			else
				Destroy(gameObject);
		}
	}
}