// Copyright Falling Snow Interactive 2025

using UnityEngine;

namespace Fsi.General
{
    public static class FsiUtilities
    {
        public static void DestroyChildren(GameObject obj)
        {
            if (!obj)
            {
                return;
            }

            for (int i = obj.transform.childCount - 1; i >= 0; i--)
            {
                GameObject child = obj.transform.GetChild(i).gameObject;

                if (Application.isPlaying)
                {
                    Object.Destroy(child);
                }
                else
                {
                    Object.DestroyImmediate(child);
                }
            }
        }

        public static void DestroyChildren<T>(T component) where T : Component
        {
            DestroyChildren(component.gameObject);
        }
    }
}