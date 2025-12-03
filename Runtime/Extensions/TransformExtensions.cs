using UnityEngine;

namespace Fsi.General.Extensions
{
    public static class TransformExtensions
    {
        public static void ClearChildren(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Transform child = transform.GetChild(i);
                Object.Destroy(child.gameObject);
            }
        }
    }
}