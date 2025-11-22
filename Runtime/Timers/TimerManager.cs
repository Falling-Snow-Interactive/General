using System.Collections.Generic;
using UnityEngine;

namespace Fsi.General.Timers
{
    /// <summary>
    /// Central manager responsible for updating all active <see cref="Timer"/> instances.
    /// The manager ticks each timer once per <see cref="FixedUpdate"/> using Unity's fixed delta time.
    /// </summary>
    public class TimerManager : MbSingleton<TimerManager>
    {
        #region Public Properties
        
        /// <summary>
        /// The list of all timers currently registered to be updated by the manager.
        /// </summary>
        public List<Timer> Timers => timers;
        
        #endregion
        
        #region Inspector fields
        
        [SerializeField]
        private List<Timer> timers = new();
        
        #endregion
        
        #region MonoBehaviour
        
        /// <summary>
        /// Unity callback invoked during the fixed timestep loop.
        /// Iterates through all registered timers and advances them using <see cref="Time.fixedDeltaTime"/>.
        /// </summary>
        private void FixedUpdate()
        {
            foreach (Timer t in timers)
            {
                t.Tick(Time.fixedDeltaTime);
            }
        }
        
        #endregion
        
        #region Timer Control

        /// <summary>
        /// Registers a <see cref="Timer"/> with the manager if it is not already present.
        /// </summary>
        /// <param name="timer">The timer instance to register.</param>
        /// <returns>
        /// <para><c>true</c> if the timer was successfully added;</para>
        /// <para><c>false</c> if it was already registered.</para>
        /// </returns>
        public bool Add(Timer timer)
        {
            if (timers.Contains(timer))
            {
                return false;
            }
            
            timers.Add(timer);
            return true;
        }

        /// <summary>
        /// Unregisters a <see cref="Timer"/> from the manager.
        /// </summary>
        /// <param name="timer">The timer instance to remove.</param>
        /// <returns>
        /// <para><c>true</c> if the timer was removed;</para>
        /// <para><c>false</c> if it was not found in the manager.</para>
        /// </returns>
        public bool Remove(Timer timer)
        {
            return timers.Remove(timer);
        }
        
        #endregion
    }
}