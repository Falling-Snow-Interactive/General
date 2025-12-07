using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Fsi.General.Buckets
{
	public abstract class Bucket<TEntry, TValue> where TEntry : BucketEntry<TValue>
	{
		public abstract List<TEntry> Entries { get; }

		private int GetWeight()
		{
			int weight = 0;
			foreach (TEntry entry in Entries) weight += entry.Weight;

			return weight;
		}

		public TValue GetRandom()
		{
			if (Entries.Count <= 0) throw new IndexOutOfRangeException("No entry in Bucket");

			float roll = Random.Range(0, GetWeight());
			foreach (TEntry entry in Entries)
			{
				roll -= entry.Weight;
				if (roll < 0) return entry.Value;
			}

			return Entries[^1].Value;
		}

		public List<TValue> GetRandom(int count, bool repeats = true)
		{
			List<TValue> result = new();
			for (int i = 0; i < count; i++)
			{
				TValue rand = GetRandom();
				switch (repeats)
				{
					case false when !result.Contains(rand):
					case true:
						result.Add(rand);
						break;
				}
			}

			return result;
		}
	}
}