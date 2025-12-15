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
	}
}