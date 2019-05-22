using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Extras.Serialization
{
    public static class JsonCoder
    {
        // Returns object from JSON string
        public static T FromJson<T>(string json, JsonSerializerSettings settings = null) =>
            (T) JsonConvert.DeserializeObject(json, typeof(T), settings ?? new JsonSerializerSettings());
        public static T FromCamelCaseJson<T>(string json) =>
            (T) JsonConvert.DeserializeObject(json, typeof(T), _camelCaseSettings);
        public static T FromSnakeCaseJson<T>(string json) =>
            (T) JsonConvert.DeserializeObject(json, typeof(T), _snakeCaseSettings);

        // Returns JSON string from object
        public static string ToJson<T>(T obj, JsonSerializerSettings settings = null, Formatting formatting = Formatting.Indented) =>
            JsonConvert.SerializeObject(obj, formatting, settings ?? new JsonSerializerSettings());
        public static string ToCamelCaseJson<T>(T obj, Formatting formatting = Formatting.Indented) =>
            JsonConvert.SerializeObject(obj, formatting, _camelCaseSettings);
        public static string ToSnakeCaseJson<T>(T obj, Formatting formatting = Formatting.Indented) =>
            JsonConvert.SerializeObject(obj, formatting, _snakeCaseSettings);

// Settings
        private static readonly JsonSerializerSettings _camelCaseSettings =
            new JsonSerializerSettings {
                ContractResolver = new DefaultContractResolver {
                    NamingStrategy = new CamelCaseNamingStrategy {
                        ProcessDictionaryKeys = true,
                        OverrideSpecifiedNames = true
                    }
                },
                DateParseHandling = DateParseHandling.None,
                NullValueHandling = NullValueHandling.Ignore
            };

        private static readonly JsonSerializerSettings _snakeCaseSettings =
            new JsonSerializerSettings {
                ContractResolver = new DefaultContractResolver {
                    NamingStrategy = new SnakeCaseNamingStrategy {
                        ProcessDictionaryKeys = true,
                        OverrideSpecifiedNames = true
                    }
                },
                DateParseHandling = DateParseHandling.None,
                NullValueHandling = NullValueHandling.Ignore
            };
    }
}
