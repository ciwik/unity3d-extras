using Extras.Async;
using System;
using UnityEngine;

namespace Extras.Extensions
{
    public static class FuncExtensions
    {
        public static AsyncTask AsCoroutine(this Func<bool> func, MonoBehaviour parent) => new AsyncTask(parent, func);
    }
}
