using UnityEngine;

namespace Fsi.Gameplay.Extensions
{
	public static class Vector3Extensions
	{
		public static Vector3 FlattenDirection(this Vector3 vector)
		{
			Vector3 direction = vector.normalized;
			float magnitude = vector.magnitude;
			direction.y = 0;
			direction.Normalize();

			return magnitude * direction;
		}
	}
}