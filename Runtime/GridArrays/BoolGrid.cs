using System;

namespace Fsi.General.GridArrays
{
    [Serializable]
    public class BoolGrid : GridArray<bool>
    {
        public BoolGrid(int width, int height) : base(width, height)
        {
            data = new bool[width * height];
        }
    }
}