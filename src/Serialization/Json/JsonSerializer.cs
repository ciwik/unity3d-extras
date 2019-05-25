using System;
using System.Collections;
using System.Text;

namespace Extras.Serialization.Json
{
    public sealed class JsonSerializer : IDisposable
    {
        public JsonSerializer() => _jsonBuilder = new StringBuilder();

        public string SerializeDictionary(IDictionary obj)
        {
            var first = true;

            _jsonBuilder.Append('{');

            foreach (var e in obj.Keys)
            {
                if (!first)
                {
                    _jsonBuilder.Append(',');
                }

                SerializeString(e.ToString());
                _jsonBuilder.Append(':');

                SerializeValue(obj[e]);

                first = false;
            }

            _jsonBuilder.Append('}');

            return _jsonBuilder.ToString();
        }

        private void SerializeValue(object value)
        {
            IDictionary asDict;
            string asStr;

            if (value == null)
            {
                _jsonBuilder.Append("null");
            }
            else if ((asStr = value as string) != null)
            {
                SerializeString(asStr);
            }
            else if (value is bool b)
            {
                _jsonBuilder.Append(b ? "true" : "false");
            }
            else if ((asDict = value as IDictionary) != null)
            {
                SerializeDictionary(asDict);
            }
            else if (value is char c)
            {
                SerializeString(new string(c, 1));
            }
            else
            {
                SerializeOther(value);
            }
        }

        private void SerializeString(string str)
        {
            _jsonBuilder.Append('\"');

            var charArray = str.ToCharArray();
            foreach (var c in charArray)
            {
                switch (c)
                {
                    case '"':
                        _jsonBuilder.Append("\\\"");
                        break;
                    case '\\':
                        _jsonBuilder.Append("\\\\");
                        break;
                    case '\b':
                        _jsonBuilder.Append("\\b");
                        break;
                    case '\f':
                        _jsonBuilder.Append("\\f");
                        break;
                    case '\n':
                        _jsonBuilder.Append("\\n");
                        break;
                    case '\r':
                        _jsonBuilder.Append("\\r");
                        break;
                    case '\t':
                        _jsonBuilder.Append("\\t");
                        break;
                    default:
                        var codepoint = Convert.ToInt32(c);
                        if (codepoint >= 32 && codepoint <= 126)
                        {
                            _jsonBuilder.Append(c);
                        }
                        else
                        {
                            _jsonBuilder.Append("\\u");
                            _jsonBuilder.Append(codepoint.ToString("x4"));
                        }
                        break;
                }
            }

            _jsonBuilder.Append('\"');
        }

        private void SerializeOther(object value)
        {
            switch (value)
            {
                case float f:
                    _jsonBuilder.Append(f.ToString("R", System.Globalization.CultureInfo.InvariantCulture));
                    break;
                case int _:
                case uint _:
                case long _:
                case sbyte _:
                case byte _:
                case short _:
                case ushort _:
                case ulong _:
                    _jsonBuilder.Append(value);
                    break;
                case double _:
                case decimal _:
                    _jsonBuilder.Append(Convert.ToDouble(value).ToString("R", System.Globalization.CultureInfo.InvariantCulture));
                    break;
                default:
                    _jsonBuilder.Append(JsonCoder.ToJson(value));
                    break;
            }
        }

        public void Dispose() => _jsonBuilder.Clear();

        private readonly StringBuilder _jsonBuilder;
    }
}
