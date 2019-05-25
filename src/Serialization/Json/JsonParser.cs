using Extras.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Extras.Serialization.Json
{
    public sealed class JsonParser : IDisposable
    {
        public JsonParser(string json) => _jsonReader = new StringReader(json);

        public Dictionary<string, object> ParseDictionary()
        {
            var table = new Dictionary<string, object>();

            _jsonReader.Read();

            while (true)
            {
                switch (NextToken)
                {
                    case TOKEN.NONE:
                        return null;
                    case TOKEN.COMMA:
                        continue;
                    case TOKEN.CURLY_CLOSE:
                        return table;
                    default:
                        var name = ParseString();
                        if (name.IsNull())
                        {
                            return null;
                        }

                        if (NextToken != TOKEN.COLON)
                        {
                            return null;
                        }
                        _jsonReader.Read();

                        table[name] = ParseValue();
                        break;
                }
            }
        }

        public void Dispose()
        {
            _jsonReader.Dispose();
            _jsonReader = null;
        }

        private object ParseValue()
        {
            var nextToken = NextToken;
            return ParseByToken(nextToken);
        }

        private string ParseString()
        {
            var s = new StringBuilder();
            char c;

            _jsonReader.Read();

            var parsing = true;
            while (parsing)
            {
                if (_jsonReader.Peek() == -1)
                {
                    parsing = false;
                    break;
                }

                c = NextChar;
                switch (c)
                {
                    case '"':
                        parsing = false;
                        break;
                    case '\\':
                        if (_jsonReader.Peek() == -1)
                        {
                            parsing = false;
                            break;
                        }

                        c = NextChar;
                        switch (c)
                        {
                            case '"':
                            case '\\':
                            case '/':
                                s.Append(c);
                                break;
                            case 'b':
                                s.Append('\b');
                                break;
                            case 'f':
                                s.Append('\f');
                                break;
                            case 'n':
                                s.Append('\n');
                                break;
                            case 'r':
                                s.Append('\r');
                                break;
                            case 't':
                                s.Append('\t');
                                break;
                            case 'u':
                                var hex = new char[4];

                            for (var i=0; i< 4; i++)
                            {
                                hex[i] = NextChar;
                            }

                            s.Append((char) Convert.ToInt32(new string(hex), 16));
                            break;
                        }
                        break;
                    default:
                        s.Append(c);
                        break;
                }
            }

            return s.ToString();
        }

        private object ParseByToken(TOKEN token)
        {
            switch (token)
            {
                case TOKEN.STRING:
                    return ParseString();
                case TOKEN.NUMBER:
                    return ParseNumber();
                case TOKEN.CURLY_OPEN:
                    return ParseDictionary();
                case TOKEN.TRUE:
                    return true;
                case TOKEN.FALSE:
                    return false;
                case TOKEN.NULL:
                    return null;
                default:
                    return null;
            }
        }

        private object ParseNumber()
        {
            var number = NextWord;

            if (number.IndexOf('.') == -1)
            {
                long parsedInt;
                Int64.TryParse(number, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out parsedInt);
                return parsedInt;
            }

            double parsedDouble;
            Double.TryParse(number, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out parsedDouble);
            return parsedDouble;
        }

        private void EatWhitespace()
        {
            while (Char.IsWhiteSpace(PeekChar))
            {
                _jsonReader.Read();

                if (_jsonReader.Peek() == -1) {
                    break;
                }
            }
        }

        private char PeekChar => Convert.ToChar(_jsonReader.Peek());

        private char NextChar => Convert.ToChar(_jsonReader.Read());

        private string NextWord {
            get
            {
                var word = new StringBuilder();

                while (!IsWordBreak(PeekChar))
                {
                    word.Append(NextChar);

                    if (_jsonReader.Peek() == -1)
                    {
                        break;
                    }
                }

                return word.ToString();
            }
        }

        private TOKEN NextToken
        {
            get
            {
                EatWhitespace();

                if (_jsonReader.Peek() == -1)
                {
                    return TOKEN.NONE;
                }

                switch (PeekChar)
                {
                    case '{':
                        return TOKEN.CURLY_OPEN;
                    case '}':
                        _jsonReader.Read();
                        return TOKEN.CURLY_CLOSE;
                    case '[':
                        return TOKEN.SQUARED_OPEN;
                    case ']':
                        _jsonReader.Read();
                        return TOKEN.SQUARED_CLOSE;
                    case ',':
                        _jsonReader.Read();
                        return TOKEN.COMMA;
                    case '"':
                        return TOKEN.STRING;
                    case ':':
                        return TOKEN.COLON;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '-':
                        return TOKEN.NUMBER;
                }

                switch (NextWord)
                {
                    case "false":
                        return TOKEN.FALSE;
                    case "true":
                        return TOKEN.TRUE;
                    case "null":
                        return TOKEN.NULL;
                }

                return TOKEN.NONE;
            }
        }

        private bool IsWordBreak(char c) => Char.IsWhiteSpace(c) || WORD_BREAK.IndexOf(c) != -1;

        private StringReader _jsonReader;

        private const string WORD_BREAK = "{}[],:\"";

        private enum TOKEN
        {
            NONE,
            CURLY_OPEN,
            CURLY_CLOSE,
            SQUARED_OPEN,
            SQUARED_CLOSE,
            COLON,
            COMMA,
            STRING,
            NUMBER,
            TRUE,
            FALSE,
            NULL
        }
    }
}
