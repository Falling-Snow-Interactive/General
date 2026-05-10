using System.Collections.Generic;

namespace Fsi.General.Extensions
{
    public static class ListExtensions
    {
        public static string Print<T>(this List<T> list)
        {
            string s = "";
            for (int i = 0; i < list.Count; i++)
            {
                T l = list[i];
                s += $"{l}";
                if (i < list.Count - 1)
                {
                    s += ", ";
                }
            }

            // s += ")";
            return s;
        }
    }
}