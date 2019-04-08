using Extras.Extensions;
using System;
using System.Collections;
using UnityEngine;

namespace Extras.Async
{
    public abstract class BaseAsyncTask :
        IEnumerator,
        IAsyncTask
    {
// Properties
        object IEnumerator.Current => _current;

        public bool IsRunning   { get; private set; }
        public bool IsPaused    { get; private set; }
        public bool IsCompleted { get; private set; }
        public bool IsCancelled { get; private set; }
        public bool IsFaulted   { get; private set; }

// Events
        public event Action<IAsyncTask> Started;
        public event Action<IAsyncTask> Paused;
        public event Action<IAsyncTask> Resumed;
        public event Action<IAsyncTask> Completed;
        public event Action<IAsyncTask> Cancelled;
        public event Action<IAsyncTask> Faulted;

// Construction
        protected BaseAsyncTask(MonoBehaviour parent)
        {
            _parent = parent;
        }

// Methods: IEnumerator
        bool IEnumerator.MoveNext()
        {
            var result = false;
            if (this.IsCancelled)
            {
                (this as IEnumerator).Reset();
            }
            else
            {
                if (!this.IsRunning)
                {
                    this.IsRunning = true;
                    _coroutine = new object();

                    OnStarted();
                    Started?.Invoke(this);
                }

                if (_current.IsNotNull())
                {
                    result = true;
                }
                else if (this.IsPaused)
                {
                    result = true;
                }
                else
                {
                    if (!Update())
                    {
                        OnCompleted();
                        Completed?.Invoke(this);
                        this.IsCompleted = true;
                    }
                    else
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        void IEnumerator.Reset()
        {
            this.IsRunning = false;
            this.IsPaused = false;
            this.IsCompleted = false;
            this.IsCancelled = false;
            this.IsFaulted = false;

            _coroutine = null;
        }

// Methods: IAsyncTask
        public IAsyncTask Execute()
        {
            if (_current.IsNull())
            {
                if (!this.IsRunning)
                {
                    this.IsRunning = true;
                    _coroutine = _parent.StartCoroutine(this);
                }
            }

            return this;
        }

        public void Pause()
        {
            if (this.IsRunning && !this.IsPaused)
            {
                this.IsPaused = true;
                OnPaused();
                Paused?.Invoke(this);
            }
        }

        public void Resume()
        {
            if (this.IsRunning)
            {
                IsPaused = false;
                OnResumed();
            }
        }

        public void Cancel()
        {
            if (Stop())
            {
                OnCancelled();
                Cancelled?.Invoke(this);
            }
        }

// Base methods
        protected abstract bool Update();

        protected virtual void OnStarted() {}
        protected virtual void OnPaused() {}
        protected virtual void OnResumed() {}
        protected virtual void OnCompleted() {}
        protected virtual void OnCancelled() {}
        // TODO
        protected virtual void OnFaulted() {}

// Private methods
        private bool Stop()
        {
            if (this.IsRunning)
            {
                var coroutine = _coroutine as Coroutine;
                if (coroutine.IsNotNull())
                    _parent.StopCoroutine(coroutine);

                (this as IEnumerator).Reset();

                return this.IsCancelled = true;
            }

            return false;
        }

// Variables
        private IAsyncTask    _current;
        private object        _coroutine;
        private MonoBehaviour _parent;
    }
}
