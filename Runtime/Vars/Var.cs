using System;
using UnityEngine;

namespace Fsi.General.Vars
{
    [Serializable]
    public class Var<T>
    {
        public event Action Updated;

        [SerializeField]
        private T value;
        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                Updated?.Invoke();
            }
        }
        
        public Var(T t)
        {
            value = t;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}