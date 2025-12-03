using System;
using UnityEngine;

namespace Fsi.General.GridArrays
{
    [Serializable]
    public class GameObjectGrid : GridArray<GameObject>
    {
        public GameObjectGrid(int width, int height) : base(width, height)
        {
        }
    }
}