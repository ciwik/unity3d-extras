using System;

namespace Extras.Extensions
{
    public static class ObjectExtensions
    {
        // Apply some actions for object and return it
        public static T Tap<T>(this T obj, Action<T> action)
        {
            action(obj);
            return obj;
        }
    }
}
