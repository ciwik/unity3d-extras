using UnityEngine;

namespace Extras.Extensions
{
    public static class GameObjectExtensions
    {
        public static T CopyComponentTo<T>(
            this GameObject source,
            GameObject destination) where T : Component
        {
            var component = source.GetComponent<T>();
            var copy = destination.GetComponent<T>();
            if (copy.IsNull())
            {
                copy = destination.AddComponent<T>();
            }

            var type = component.GetType();
            foreach (var field in type.GetFields())
            {
                var value = field.GetValue(component);
                field.SetValue(copy, value);
            }
            foreach (var property in type.GetProperties())
            {
                if (property.CanWrite)
                {
                    var value = property.GetValue(component);
                    property.SetValue(copy, value);
                }
            }

            return copy;
        }
    }
}

// Example
// obj1.CopyComponentTo<Animator>(obj2);
