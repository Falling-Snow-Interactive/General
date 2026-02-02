using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Fsi.General.Buckets
{
	[Serializable]
	public abstract class BucketEntry<T> : ISerializationCallbackReceiver
	{
		[HideInInspector, UsedImplicitly]
		[SerializeField]
		private string name;

		public abstract T Value { get; }
		public abstract int Weight { get; }

		public void OnBeforeSerialize()
		{
			name = ToString();
		}

		public void OnAfterDeserialize() { }

		public override string ToString()
		{
			string v = Value != null ? Value.ToString() : "No value";
			string w = Weight.ToString();
			return $"{v} - {w}";
		}
	}
}