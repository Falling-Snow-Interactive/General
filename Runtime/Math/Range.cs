using System;
using Unity.Properties;
using UnityEngine;

namespace Fsi.Gameplay
{
	[GeneratePropertyBag]
	[Serializable]
	public abstract class Range<T>
	{
		public T min;
		public T max;
		
		public abstract T Center { get; }

		protected Range()
		{
			min = max = default;
		}

		protected Range(T min, T max)
		{
			this.min = min;
			this.max = max;
		}

		public abstract T Random();
	}

	[Serializable]
	public class RangeInt : Range<int>
	{
		public RangeInt(int min, int max) : base(min, max)
		{
		}

		public override int Center => (min + max) / 2;

		public override int Random()
		{
			return UnityEngine.Random.Range(min, max);
		}
	}
	
	[GeneratePropertyBag]
	[Serializable]
	public class RangeFloat : Range<float>
	{
		public RangeFloat(float min, float max) : base(min, max)
		{
		}
		
		public override float Center => (min + max) / 2;

		public override float Random()
		{
			return UnityEngine.Random.Range(min, max);
		}

		public float Lerp(float t)
		{
			return Mathf.Lerp(min, max, t);
		}
	}

	[Serializable]
	public class RangeVector3 : Range<Vector3>
	{
		public override Vector3 Center => (min + max) / 2;
		
		public RangeVector3(Vector3 min, Vector3 max) : base(min, max)
		{
		}

		public override Vector3 Random()
		{
			float x = UnityEngine.Random.Range(min.x, max.x);
			float y = UnityEngine.Random.Range(min.y, max.y);
			float z = UnityEngine.Random.Range(min.z, max.z);
			return new Vector3(x, y, z);
		}
	}
}