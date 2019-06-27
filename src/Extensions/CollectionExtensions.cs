using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

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
        public static IReadOnlyCollection<T> AsReadOnly<T>(this IList<T> list) => new ReadOnlyCollection<T>(list);

        // Returns the same list with shuffled items
        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;

            while (n > 1)
            {
                var k = Random.Range(0, n);
                n--;

                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }
    }
}
