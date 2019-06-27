using System;
using System.Collections.Generic;
using System.Globalization;
using Extras.Extensions;

namespace Extras.Caches
{
    public sealed class StringCache<T>
    {
        public StringCache(
            string format = null,
            IFormatProvider formatter = null,
            int capacity = 128)
        {
            _format = format.IsBlank() ? "{0}" : format;
            _formatter = formatter ?? NumberFormatInfo.InvariantInfo;
            _cache = new Dictionary<T, string>(capacity);
        }

        public string Get(T key)
        {
            if (!_cache.TryGetValue(key, out var result))
            {
                result = string.Format(_formatter, _format, key);
                _cache[key] = result;
            }

            return result;
        }

        private readonly Dictionary<T, string> _cache;
        private readonly string _format;
        private readonly IFormatProvider _formatter;
    }
}
