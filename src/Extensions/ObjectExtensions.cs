using System;

namespace Extras.Extensions
{
    public static class ObjectExtensions
    {
        // Applies some actions for object and returns it
        public static T Tap<T>(this T obj, Action<T> action)
        {
            action(obj);
            return obj;
        }

        // Checks that an object is null
        public static bool IsNull<T>(this T obj) => obj == null;

        // Checks that an object is not null
        public static bool IsNotNull<T>(this T obj) => obj != null;
    }
}
