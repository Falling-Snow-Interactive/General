using System;
using UnityEngine;

namespace Fsi.General.Sprites.Preview
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SpritePreviewAttribute : PropertyAttribute
    {
        public float Size { get; }

        public SpritePreviewAttribute(float size = 64f)
        {
            Size = size;
        }
    }
}
