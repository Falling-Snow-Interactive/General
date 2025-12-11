using UnityEngine;

namespace Fsi.General.Extensions
{
	public static class Vector2Extensions
	{
		public static Vector3 FlattenToVector3(this Vector2 vector)
		{
			Vector3 v = new Vector3(vector.x, 0, vector.y);
			return v;
		}
	}
}