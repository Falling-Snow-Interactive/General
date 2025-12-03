using System;

namespace Fsi.General.GridArrays
{
    public class EnumGrid<T> : GridArray<T> where T : Enum
    {
        public EnumGrid(int width, int height) : base(width, height)
        {
        }
    }
}