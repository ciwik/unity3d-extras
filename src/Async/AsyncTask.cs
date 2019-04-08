using System;
using UnityEngine;

namespace Extras.Async
{
    public sealed class AsyncTask : BaseAsyncTask
    {
        public AsyncTask(MonoBehaviour parent, Func<bool> func) : base(parent)
        {
            _func = func;
        }

        protected override void OnStarted()
        {
            base.OnStarted();
            _func?.Invoke();
        }

        protected override bool Update() => _func();

        private readonly Func<bool> _func;
    }
}

// Example
//
//    Async task declaration:
//
//    bool MoveToPoint(Vector3 target, float speed, float threshold)
//    {
//        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 1f);
//        return Vector3.Distance(transform.position, target) >= 0.05f;
//    }
//
//    Execution as coroutine:
//
//    Func<bool> func = () => MoveToPoint(Vector3.down, 1f, 0.05f);
//    func.AsCoroutine(this).Execute();
