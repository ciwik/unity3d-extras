using System;
using System.Collections.Generic;
using UnityEngine;

namespace Extras.Serialization.Json
{
    public static class JsonCoder
    {
        // Returns object from JSON string
        public static T FromJson<T>(string json)
        {
            if (typeof(T) == typeof(Dictionary<string, object>))
            {
                var dict = FromJsonToDictionary(json);
                return (T) Convert.ChangeType(dict, typeof(T));
            }

            return JsonUtility.FromJson<T>(json);
        }

        private static Dictionary<string, object> FromJsonToDictionary(string json)
        {
            using (var parser = new JsonParser(json))
            {
                return parser.ParseDictionary();
            }
        }

        // Returns JSON string from object
        public static string ToJson<T>(T obj) => JsonUtility.ToJson(obj);

        public static string ToJson(Dictionary<string, object> dict)
        {
            using (var serializer = new JsonSerializer())
            {
                return serializer.SerializeDictionary(dict);
            }
        }
    }
}
