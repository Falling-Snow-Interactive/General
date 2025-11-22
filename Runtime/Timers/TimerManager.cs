using System.Collections.Generic;
using UnityEngine;

namespace Fsi.General.Timers
{
    public class TimerManager : MbSingleton<TimerManager>
    {
        [SerializeField]
        private List<Timer> timers = new();
        
        private void FixedUpdate()
        {
            foreach (Timer t in timers)
            {
                t.Tick(Time.fixedDeltaTime);
            }
        }

        public void Add(Timer timer)
        {
            if (timers.Contains(timer))
            {
                return;
            }
            
            timers.Add(timer);
        }

        public void Remove(Timer timer)
        {
            timers.Remove(timer);
        }
    }
}