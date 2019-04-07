using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Extras.Extensions
{
    public static class CollectionExtensions
    {
        // Checks that a collection is null or empty
        public static bool IsEmpty(this ICollection collection) => collection == null || collection.Count == 0;
        public static bool IsEmpty<T>(this ICollection<T> collection) => collection == null || collection.Count == 0;
        public static bool IsEmpty<T>(this T[] array) => array == null || array.Length == 0;

        // Checks that a collection is not null and not empty
        public static bool IsNotEmpty(this ICollection collection) => collection != null && collection.Count != 0;
        public static bool IsNotEmpty<T>(this ICollection<T> collection) => collection != null && collection.Count != 0;
        public static bool IsNotEmpty<T>(this T[] array) => array != null && array.Length != 0;

        // Returns a read-only copy of list
        public static IReadOnlyCollection<T> ToReadOnly<T>(this IList<T> list) => new ReadOnlyCollection<T>(list);
    }
}
