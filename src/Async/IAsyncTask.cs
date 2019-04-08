using System;

namespace Extras.Async
{
    public interface IAsyncTask
    {
        bool IsRunning { get; }
        bool IsPaused { get; }
        bool IsCompleted { get; }
        bool IsCancelled { get; }
        bool IsFaulted { get; }

        IAsyncTask Execute();
        void Pause();
        void Resume();
        void Cancel();

        event Action<IAsyncTask> Started;
        event Action<IAsyncTask> Paused;
        event Action<IAsyncTask> Resumed;
        event Action<IAsyncTask> Completed;
        event Action<IAsyncTask> Cancelled;
        event Action<IAsyncTask> Faulted;
    }
}
