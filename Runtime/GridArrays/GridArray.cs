using System;
using System.Linq;
using UnityEngine;

namespace Fsi.General.GridArrays
{
    [Serializable]
    public class GridArray<T>
    {
        [SerializeField]
        protected T[] data;
        public T[] Data => data;

        [Min(1)]
        [SerializeField]
        private int width = 1;
        public int Width => width;

        [Min(1)]
        [SerializeField]
        private int height = 1;
        public int Height => height;
        
        public GridArray(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        
        public T this[int x, int y]
        {
            get => data[y * Width + x];
            set => data[y * Width + x] = value;
        }

        public override bool Equals(object obj)
        {
            if (obj is not GridArray<T> other)
            {
                return false;
            }

            if (Width != other.Width || Height != other.Height)
            {
                return false;
            }

            if (data == null && other.data == null)
            {
                return true;
            }

            if (data == null || other.data == null)
            {
                return false;
            }

            if (data.Where((t, i) => !t.Equals(other.data[i])).Any())
            {
                return false;
            }

            return data.SequenceEqual(other.data);
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + Width.GetHashCode();
            hash = hash * 31 + Height.GetHashCode();
            
            return hash;
        }

        public static bool operator ==(GridArray<T> a, GridArray<T> b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(GridArray<T> a, GridArray<T> b)
        {
            return !(a == b);
        }
    }
}