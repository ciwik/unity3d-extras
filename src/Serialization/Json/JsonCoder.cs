using System.Collections.Generic;
using UnityEngine;

namespace Extras.Serialization.Json
{
    public static class JsonCoder
    {
        // Returns object from JSON string
        public static T FromJson<T>(string json) => JsonUtility.FromJson<T>(json);

        public static Dictionary<string, object> FromJson(string json)
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
