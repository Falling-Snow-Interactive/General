using System;
using UnityEngine;

namespace Fsi.General.Timers
{
    /// <summary>
    /// Represents a simple countdown timer that can be started, paused, stopped,
    /// and updated externally through <see cref="Tick"/>. Provides notifications
    /// when the timer ticks and when it completes.
    /// </summary>
    [Serializable]
    public class Timer : ISerializationCallbackReceiver
    {
        [HideInInspector]
        [SerializeField]
        private string name;
        
        #region Events
        
        /// <summary>
        /// Invoked when the timer reaches zero.
        /// </summary>
        private event Action Completed;
        
        /// <summary>
        /// Invoked every time the timer is updated through <see cref="Tick"/>,
        /// providing the delta time that was applied.
        /// </summary>
        private event Action<float> Ticked;
        
        #endregion
        
        #region Public Properties
        
        /// <summary>
        /// Whether the timer is actively running or paused.
        /// </summary>
        public bool Active => Status is TimerStatus.Running or TimerStatus.Pause;
        
        /// <summary>
        /// Current state of the timer (Running, Paused, Finished, etc.).
        /// </summary>
        public TimerStatus Status => status;

        /// <summary>
        /// The total duration of the timer in seconds.
        /// </summary>
        public float Time => time;
        
        /// <summary>
        /// The amount of time left before the timer completes, in seconds.
        /// </summary>
        public float Remaining => remaining;
        
        /// <summary>
        /// Returns how far the timer has progressed, from 0 to 1.
        /// </summary>
        public float Normalized => 1f - (remaining / time);
        
        #endregion
        
        #region Inspector Fields

        [Tooltip("Current state of the timer (Running, Paused, Finished, etc.).")]
        [SerializeField]
        private TimerStatus status;
        
        [Tooltip("Total duration of the timer, in seconds, before completion.")]
        [Min(0)]
        [SerializeField]
        private float time;
        
        [Tooltip("Time left before the timer completes, in seconds.")]
        [SerializeField]
        private float remaining = 0;

        #endregion
        
        /// <summary>
        /// Creates a new timer with the specified duration.
        /// </summary>
        /// <param name="time">The total countdown time in seconds.</param>
        public Timer(float time)
        {
            this.time = time;
            remaining = time;
            status = TimerStatus.Uninitialized;
        }
        
        /// <summary>
        /// Starts the timer and registers it with the <see cref="TimerManager"/>.
        /// Resets the remaining time back to the full duration.
        /// </summary>
        /// <returns>This timer instance.</returns>
        public Timer Start()
        {
            if (!TimerManager.Instance)
            {
                Debug.LogError("Timer | Cannot find TimerManager. Please add the TimerManager component to the scene.");
            }
            
            TimerManager.Instance.Add(this);
            
            status = TimerStatus.Running;
            remaining = time;
            return this;
        }

        /// <summary>
        /// Stops the timer immediately. Optionally invokes the completion event.
        /// </summary>
        /// <param name="notify">Whether to invoke the completion callback.</param>
        /// <returns>This timer instance.</returns>
        public Timer Stop(bool notify = true)
        {
            status = TimerStatus.Finished;
            remaining = 0;
            
            if (notify)
            {
                Completed?.Invoke();
            }
            return this;
        }

        /// <summary>
        /// Pauses the timer without resetting remaining time.
        /// </summary>
        /// <returns>This timer instance.</returns>
        public Timer Pause()
        {
            status = TimerStatus.Pause;
            return this;
        }
        
        /// <summary>
        /// Updates the timer by subtracting the provided delta time. If the timer reaches
        /// zero, it completes and triggers its completion callback.
        /// </summary>
        /// <param name="deltaTime">Time elapsed since the last tick.</param>
        public void Tick(float deltaTime)
        {
            if (status != TimerStatus.Running)
            {
                return;
            }
            
            remaining -= deltaTime;
            if (remaining <= 0)
            {
                status = TimerStatus.Finished;
                Completed?.Invoke();
            }
            
            Ticked?.Invoke(deltaTime);
        }

        /// <summary>
        /// Registers a callback to be invoked when the timer completes.
        /// </summary>
        /// <param name="onComplete">The callback to invoke.</param>
        /// <returns>This timer instance.</returns>
        public Timer OnComplete(Action onComplete)
        {
            Completed += onComplete;
            return this;
        }

        /// <summary>
        /// Registers a callback to be invoked every time the timer ticks.
        /// </summary>
        /// <param name="onTick">Callback receiving the delta time per tick.</param>
        /// <returns>This timer instance.</returns>
        public Timer OnTick(Action<float> onTick)
        {
            Ticked += onTick;
            return this;
        }

        /// <summary>
        /// Returns a readable description of the timer’s progress and state.
        /// </summary>
        public override string ToString() => $"Timer - {remaining:0.00}s/{time}s ({Normalized * 100:00}%) - {status}";

        #region Serialization
        #if UNITY_EDITOR
        
        public void OnBeforeSerialize()
        {
            name = ToString();
        }

        public void OnAfterDeserialize() { }
        
        #endif
        #endregion
    }
}