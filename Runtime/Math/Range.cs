using System;
using UnityEngine;

namespace Fsi.General.Math
{
	[Serializable]
	public class Range<T>
	{
		[SerializeField]
		private T min;
		public T Min => min;
		
		[SerializeField]
		private T max;
		public T Max => max;
		
		public Range()
		{
			min = max = default;
		}

		public Range(T min, T max)
		{
			this.min = min;
			this.max = max;
		}

		public T Evaluate(float t)
		{
			return typeof(T) switch
			       {
				       { } type when type == typeof(float) => (T)(object)Mathf.Lerp((float)(object)min, (float)(object)max, t),
				       { } type when type == typeof(int) => (T)(object)Mathf.RoundToInt(Mathf.Lerp((int)(object)min, (int)(object)max, t)),
				       { } type when type == typeof(Vector2) => (T)(object)Vector2.Lerp((Vector2)(object)min, (Vector2)(object)max, t),
				       { } type when type == typeof(Vector3) => (T)(object)Vector3.Lerp((Vector3)(object)min, (Vector3)(object)max, t),
				       { } type when type == typeof(Color) => (T)(object)Color.Lerp((Color)(object)min, (Color)(object)max, t),
				       _ => throw new NotSupportedException($"Range<{typeof(T).Name}> does not support Evaluate.")
			       };
		}

		public override string ToString() => $"{min} - {max}";
	}
}