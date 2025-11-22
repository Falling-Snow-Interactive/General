using System;
using UnityEngine;

namespace Fsi.General.Timers
{
    /// <summary>
    /// Represents a simple countdown timer that can be started, paused, stopped,
    /// and updated in TimerManager<see cref="TimerManager"/> through <see cref="Tick"/>.
    /// Provides notifications when the timer ticks and when it completes.
    /// </summary>
    [Serializable]
    public class Timer : ISerializationCallbackReceiver
    {
        [HideInInspector]
        [SerializeField]
        private string name;
        
        #region Events
        
        /// <summary>
        /// Invoked every time the timer is updated through <see cref="Tick"/>,
        /// providing the delta time that was applied. Set via event setter <see cref="OnTick"/>.
        /// </summary>
        private event Action<float> Ticked;
        
        /// <summary>
        /// Invoked when the timer reaches zero. Set via event setter <see cref="OnComplete"/>.
        /// </summary>
        private event Action Completed;
        
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
        public float Progress => 1f - remaining / time;
        
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
        
        #region Constructors

        /// <summary>
        /// Creates a new timer with the specified duration.
        /// </summary>
        /// <param name="time">The total countdown time in seconds.</param>
        /// <param name="status">Starting status of the timer.</param>
        public Timer(float time, TimerStatus status = TimerStatus.Uninitialized)
        {
            this.time = time;
            remaining = time;
            this.status = status;
        }
        
        #endregion
        
        #region Timer Control
        
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
                remaining = 0;
                status = TimerStatus.Finished;
                Completed?.Invoke();
            }
            
            Ticked?.Invoke(deltaTime);
        }
        
        #endregion
        
        #region Callback Setters

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
        
        #endregion 
        
        #region Object Overrides

        /// <summary>
        /// Returns a readable description of the timer’s progress and state.
        /// </summary>
        public override string ToString() => $"Timer - {Remaining:0.00}s/{Time}s - {Status}";
        
        #endregion

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