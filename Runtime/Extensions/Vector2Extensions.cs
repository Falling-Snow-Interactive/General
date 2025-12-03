using UnityEngine;

namespace Fsi.Gameplay.Extensions
{
	public static class Vector2Extensions
	{
		public static Vector3 FlattenToVector3(this Vector2 vector)
		{
			var v = new Vector3(vector.x, 0, vector.y);
			return v;
		}
	}
}