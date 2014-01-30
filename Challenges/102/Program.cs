using System.Linq;

#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#endregion

// ReSharper disable CheckNamespace

namespace Challenges
    // ReSharper restore CheckNamespace
{
    internal interface IChallenge
    {
        void Main(string[] args);
    }

    public static class Json
    {
        private static string _input;
        private static int _position = -1;

        // grammar:

        // elements =  (element){1,} 
        // element = "{" name : ("[" (( element ){1,} | null) "]" | element | value) "}"

        public static IElement ParseEelement(string text)
        {
            _input = text;
            _position = -1;
            if (!string.IsNullOrWhiteSpace(text))
            {
                var element = new JsonElement();
                if (element.TryParse(_position))
                {
                    if (text.Length == _position + 1)
                    {
                        return element;
                    }
                }
            }
            return null;
        }

        private abstract class Base : IElement
        {
            public abstract string Text { get; }

            public override string ToString()
            {
                return Text;
            }

            public abstract bool Parse();

            #region  static methods

            internal static string ToString(char startElement, char endElement, IList<IElement> elements)
            {
                var sb = new StringBuilder();
                sb.Append(startElement);
                sb.AppendFormat("{0}", elements[0].Text);
                for (int i = 1; i < elements.Count; i++)
                {
                    sb.AppendFormat(", {0}", elements[i].Text);
                }
                sb.Append(endElement);
                return sb.ToString();
            }

            internal bool TryParse(int current)
            {
                int previous = _position;
                _position = current;
                if (_input.Length > current + 1)
                {
                    // trial
                    if (Parse())
                    {
                        // advance
                        return true;
                    }
                    //error
                }
                // rollback
                _position = previous;
                return false;
            }

            private static int Next(int current)
            {
                while (current < _input.Length)
                {
                    if (!Any(current, ' ', '\r', '\n', '\t') || current + 1 == _input.Length)
                    {
                        break;
                    }
                    current++;
                }
                return current;
            }

            internal static int Next(int current, char ch)
            {
                int previous = current;
                while (current < _input.Length)
                {
                    if (Is(current, ch))
                    {
                        break;
                    }
                    if (current + 1 == _input.Length)
                    {
                        return previous;
                    }
                    current++;
                }
                return current;
            }

            internal static int Advance(int offset)
            {
                _position += offset;
                int current = Next(_position);
                _position = current;
                return current;
            }

            internal static bool Any(int current, string text)
            {
                return text.Any(t => _input[current] == t);
            }

/*
            internal static bool Any(int current, params char[] chars)
            {
                for (var i = 0; i < chars.Length; i++)
                {
                    if (_input[current] == chars[i]) return true;
                }
                return false;
            }
*/

            internal static bool Is(int current, char ch1)
            {
                return _input[current] == ch1;
            }

/*
            internal static bool Any(int current, char ch1, char ch2)
            {
                return _input[current] == ch1 || _input[current] == ch2;
            }
*/

/*
            internal static bool Any(int current, char ch1, char ch2, char ch3)
            {
                return _input[current] == ch1 || _input[current] == ch2 || _input[current] == ch3;
            }
*/


            private static bool Any(int current, char ch1, char ch2, char ch3, char ch4)
            {
                return _input[current] == ch1 || _input[current] == ch2 || _input[current] == ch3 || _input[current] == ch4;
            }


/*
            internal static bool Not(int current, string text)
            {
                for (var i = 0; i < text.Length; i++)
                {
                    if (_input[current + i] == text[i]) return false;
                }
                return true;
            }
*/

/*
            internal static bool Not(int current, params char[] chars)
            {
                for (var i = 0; i < chars.Length; i++)
                {
                    if (_input[current] == chars[i]) return false;
                }
                return true;
            }
*/

/*
            internal static bool Not(int current, char ch1)
            {
                return _input[current] != ch1;
            }
*/

/*
            internal static bool Not(int current, char ch1, char ch2)
            {
                return _input[current] != ch1 && _input[current] != ch2;
            }
*/

/*
            internal static bool Not(int current, char ch1, char ch2, char ch3)
            {
                return _input[current] != ch1 && _input[current] != ch2 && _input[current] != ch3;
            }
*/

/*
            internal static bool Not(int current, char ch1, char ch2, char ch3, char ch4)
            {
                return _input[current] != ch1 && _input[current] != ch2 && _input[current] != ch3 && _input[current] != ch4;
            }
*/

            #endregion
        }

        public interface IElement
        {
            string Text { get; }
        }


        public interface INamedElement : IElement
        {
            IElement Name { get; }
            IElement Value { get; }
        }

        private class JsonArray : JsonList
        {
            public JsonArray() : base('[', ']') {}

            public override bool Parse()
            {
                int current = Advance(1);
                if (Is(_position, StartElement))
                {
                    while (true)
                    {
                        var element = new JsonElement();
                        if (_position + 1 >= _input.Length)
                        {
                            return false;
                        }
                        if (element.TryParse(current))
                        {
                            Elements.Add(element);
                            if (_position + 1 >= _input.Length)
                            {
                                return false;
                            }
                            if (Is(_position + 1, EndElement))
                            {
                                ++_position;
                                return true;
                            }
                            if (!Is(_position + 1, ','))
                            {
                                return false;
                            }
                            current = Advance(1);
                            continue;
                        }
                        if (_position + 1 >= _input.Length)
                        {
                            return false;
                        }
                        current = Advance(1);
                        if (Any(current, "null"))
                        {
                            Elements.Add(new JsonValue());
                            _position += 3;
                            if (_position + 1 >= _input.Length)
                            {
                                return false;
                            }
                            if (Is(_position + 1, EndElement))
                            {
                                ++_position;
                                return true;
                            }
                            if (_position + 1 >= _input.Length)
                            {
                                return false;
                            }
                            if (!Is(_position + 1, ','))
                            {
                                return false;
                            }
                            current = Advance(1);
                            continue;
                        }
                        return false;
                    }
                }
                return false;
            }
        }

        private class JsonElement : JsonList
        {
            public JsonElement() : base('{', '}') {}

            public override bool Parse()
            {
                int current = Advance(1);
                if (Is(_position, StartElement))
                {
                    while (true)
                    {
                        var name = new JsonName();
                        if (!name.TryParse(current))
                        {
                            return false;
                        }
                        if (_position + 1 >= _input.Length)
                        {
                            return false;
                        }
                        if (!Is(_position + 1, ':'))
                        {
                            return false;
                        }
                        var str = new JsonName();
                        if (str.TryParse(_position + 1))
                        {
                            Elements.Add(new JsonNamedElement(name, str));
                            if (_position + 1 >= _input.Length)
                            {
                                return false;
                            }
                            if (Is(_position + 1, EndElement))
                            {
                                ++_position;
                                return true;
                            }
                            if (!Is(_position + 1, ','))
                            {
                                return false;
                            }
                            current = Advance(1);
                            continue;
                        }
                        var value = new JsonValue();
                        if (value.TryParse(_position + 1))
                        {
                            Elements.Add(new JsonNamedElement(name, value));
                            if (_position + 1 >= _input.Length)
                            {
                                return false;
                            }
                            if (Is(_position + 1, EndElement))
                            {
                                ++_position;
                                return true;
                            }
                            if (!Is(_position + 1, ','))
                            {
                                return false;
                            }
                            current = Advance(1);
                            continue;
                        }
                        var element = new JsonElement();
                        if (element.TryParse(_position + 1))
                        {
                            Elements.Add(new JsonNamedElement(name, element));
                            if (_position + 1 >= _input.Length)
                            {
                                return false;
                            }
                            if (Is(_position + 1, EndElement))
                            {
                                ++_position;
                                return true;
                            }
                            if (!Is(_position + 1, ','))
                            {
                                return false;
                            }
                            current = Advance(1);
                            continue;
                        }
                        var array = new JsonArray();
                        if (array.TryParse(_position + 1))
                        {
                            Elements.Add(new JsonNamedElement(name, array));
                            if (_position + 1 >= _input.Length)
                            {
                                return false;
                            }
                            if (Is(_position + 1, EndElement))
                            {
                                ++_position;
                                return true;
                            }
                            if (!Is(_position + 1, ','))
                            {
                                return false;
                            }
                            current = Advance(1);
                            continue;
                        }
                        return false;
                    }
                }
                return false;
            }
        }

        private abstract class JsonList : Base
        {
            protected readonly char EndElement;
            protected readonly char StartElement;

            private readonly List<IElement> _elements = new List<IElement>();

            protected JsonList(char startElement, char endElement)
            {
                StartElement = startElement;
                EndElement = endElement;
            }

            protected ICollection<IElement> Elements
            {
                get { return _elements; }
            }

            public override string Text
            {
                get { return ToString(StartElement, EndElement, _elements); }
            }
        }

        private class JsonName : Base
        {
            private string _content;

            public override string Text
            {
                get { return string.Format("\"{0}\"", _content); }
            }

            public override bool Parse()
            {
                int current = Advance(1);
                if (Is(current, '"'))
                {
                    int next = Next(current + 1, '"');
                    if (next > current + 1)
                    {
                        _content = _input.Substring(current + 1, next - current - 1);
                        _position = next;
                        return true;
                    }
                }
                return false;
            }
        }

        private class JsonNamedElement : INamedElement
        {
            private readonly JsonName _name;
            private readonly IElement _value;

            public JsonNamedElement(JsonName name, IElement value)
            {
                _name = name;
                _value = value;
            }

            #region Члены INamedElement

            public IElement Name
            {
                get { return _name; }
            }

            public IElement Value
            {
                get { return _value; }
            }

            #endregion

            #region Члены IElement

            public string Text
            {
                get { return string.Format("{0}: {1}", _name.Text, _value.Text); }
            }

            public override string ToString()
            {
                return Text;
            }

            #endregion
        }

        private class JsonValue : Base
        {
            private string _content;

            public override string Text
            {
                get
                {
                    if (_content == null)
                    {
                        return "null";
                    }
                    return string.Format("{0}", _content);
                }
            }

            public override bool Parse()
            {
                int current = Advance(1);
                int next = _position;
                if (next >= _input.Length)
                {
                    return false;
                }
                while (Any(next, "0123456789"))
                {
                    ++next;
                    if (next >= _input.Length)
                    {
                        return false;
                    }
                }
                if (next > current)
                {
                    _content = _input.Substring(current, next - current);
                    _position = next - 1;
                    return true;
                }
                return false;
            }
        }
    }

    internal class Challenge : IChallenge
    {
        public void Main(string[] args)
        {
            //var strings = File.ReadAllText(args[0]);
        }
    }

    internal class Program
    {
        private static int Main(string[] args)
        {
            return Run<Challenge>(args);
        }

        private static int Run<T>(string[] args) where T : IChallenge, new()
        {
            try
            {
                if (args == null)
                {
                    throw new Exception("args == null");
                }
                if (args.Length == 0)
                {
                    throw new Exception("args.Length == 0");
                }
                if (args.Length > 1)
                {
                    throw new Exception("args.Length > 1");
                }
                if (!File.Exists(args[0]))
                {
                    throw new Exception("!File.Exists(args[0])");
                }
                var t = new T();
                t.Main(args);
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                while (exception != null)
                {
                    Console.Error.WriteLine(ex.Message);
                    Console.Error.WriteLine(ex.StackTrace);
                    exception = ex.InnerException;
                }
                return -1;
            }
            return 0;
        }
    }
}