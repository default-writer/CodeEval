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
    public interface IElement
    {
        string Text { get; }
    }

    internal interface IChallenge
    {
        void Main(string[] args);
    }


    public interface INamedElement : IElement
    {
        IElement Name { get; }
        IElement Value { get; }
    }
    public class Json
    {
        private static string _input;
        private static int _position = -1;

        public static IElement Parse(string text)
        {
            _input = text;
            _position = 0;
            if (!string.IsNullOrWhiteSpace(text))
            {
                var element = new JsonElement();
                var current = Advance(0);
                if (element.TryParse(current))
                {
                    current = Advance(1);
                    if (current == _input.Length)
                    {
                        return element;
                    }
                }
            }
            return null;
        }

        internal static int Next(int current)
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

        internal static int Advance(int offset)
        {
            _position += offset;
            var current = Next(_position);
            _position = current;
            return current;
        }

        internal static bool End(int current)
        {
            return !(current < _input.Length);
        }

        internal static bool Any(int current, string text)
        {
            for (var i = 0; i < text.Length; i++)
            {
                if (_input[current] == text[i])
                {
                    return true;
                }
            }
            return false;
        }

        internal static bool Is(int current, string text)
        {
            for (var i = 0; i < text.Length; i++)
            {
                if (End(current + i))
                {
                    return false;
                }
                if (_input[current + i] != text[i])
                {
                    return false;
                }
            }
            return true;
        }

        internal static string Substring(int startIndex, int count)
        {
            return _input.Substring(startIndex, count);
        }

        internal static int Next(int current, char ch)
        {
            var previous = current;
            while (current < _input.Length)
            {
                if (End(current))
                {
                    return previous;
                }               
                if (Is(current, ch))
                {
                    break;
                }
                current++;
            }
            return current;
        }

        internal static bool Is(int current, char ch1)
        {
            return _input[current] == ch1;
        }

        private static bool Any(int current, char ch1, char ch2, char ch3, char ch4)
        {
            return _input[current] == ch1 || _input[current] == ch2 || _input[current] == ch3 || _input[current] == ch4;
        }


        public abstract class Element : IElement
        {
            public abstract string Text { get; }

            public override string ToString()
            {
                return Text;
            }

            public abstract bool Parse();

            internal bool TryParse(int current)
            {
                var previous = _position;
                _position = current;
                if (!End(current))
                {
                    if (Parse())
                    {
                        return true;
                    }
                }
                _position = previous;
                return false;
            }
        }

        class JsonArray : JsonList
        {
            public JsonArray() : base('[', ']') { }

            public override bool Parse()
            {
                var current = Advance(0);
                if (End(current))
                {
                    return false;
                }
                if (!Is(current, StartElement))
                {
                    return false;
                }
                current = Advance(1);
                while (true)
                {
                    var value = new JsonValue();
                    if (value.TryParse(current))
                    {
                        current = Advance(1);
                        Elements.Add(value);
                        if (End(current))
                        {
                            return false;
                        }
                        if (Is(current, EndElement))
                        {
                            return true;
                        }
                        if (Is(current, ','))
                        {
                            current = Advance(1);
                            continue;
                        }
                        return false;
                    }
                    var name = new JsonName();
                    if (name.TryParse(current))
                    {
                        current = Advance(1);
                        Elements.Add(name);
                        if (End(current))
                        {
                            return false;
                        }
                        if (Is(current, EndElement))
                        {
                            return true;
                        }
                        if (Is(current, ','))
                        {
                            current = Advance(1);
                            continue;
                        }
                        return false;
                    }
                    var element = new JsonElement();
                    if (element.TryParse(current))
                    {
                        current = Advance(1);
                        Elements.Add(element);
                        if (End(current))
                        {
                            return false;
                        }
                        if (Is(current, EndElement))
                        {
                            return true;
                        }
                        if (Is(current, ','))
                        {
                            current = Advance(1);
                            continue;
                        }
                        return false;
                    }
                    var constant = new JsonConstant("null");
                    if (constant.TryParse(current))
                    {
                        current = Advance(1);
                        Elements.Add(constant);
                        if (End(current))
                        {
                            return false;
                        }
                        if (Is(current, EndElement))
                        {
                            return true;
                        }
                        if (Is(current, ','))
                        {
                            current = Advance(1);
                            continue;
                        }
                        return false;
                    }
                    return false;
                }
            }
        }

        class JsonConstant : Element
        {
            private readonly string _content;

            public JsonConstant(string content)
            {
                _content = content;
            }

            public override string Text
            {
                get { return string.Format("{0}", _content); }
            }

            public override bool Parse()
            {
                var current = Advance(0);
                var start = current;  
                if (End(current))
                {
                    return false;
                }
                if (Is(current, _content))
                {
                    current = Advance(_content.Length - 1);
                    if (End(current))
                    {
                        return false;
                    }
                }
                if (current > start)
                {
                    return true;
                }
                return false;
            }
        }

        class JsonElement : JsonList
        {
            public JsonElement() : base('{', '}') { }

            public override bool Parse()
            {
                var current = Advance(0);
                if (End(current))
                {
                    return false;
                }
                if (!Is(current, StartElement))
                {
                    return false;
                }
                current = Advance(1);
                while (true)
                {
                    var name = new JsonName();
                    if (!name.TryParse(current))
                    {
                        return false;
                    }
                    current = Advance(1);
                    if (End(current))
                    {
                        return false;
                    }
                    if (Is(current, ':'))
                    {
                        current = Advance(1);
                        var value = new JsonValue();
                        if (value.TryParse(current))
                        {
                            current = Advance(1);
                            Elements.Add(new JsonNamedElement(name, value));
                            if (End(current))
                            {
                                return false;
                            }
                            if (Is(current, EndElement))
                            {
                                return true;
                            }
                            if (Is(current, ','))
                            {
                                current = Advance(1);
                                continue;
                            }
                            return false;
                        }
                        var str = new JsonName();
                        if (str.TryParse(current))
                        {
                            current = Advance(1);
                            Elements.Add(new JsonNamedElement(name, str));
                            if (End(current))
                            {
                                return false;
                            }
                            if (Is(current, EndElement))
                            {
                                return true;
                            }
                            if (Is(current, ','))
                            {
                                current = Advance(1);
                                continue;
                            }
                            return false;
                        }
                        var array = new JsonArray();
                        if (array.TryParse(current))
                        {
                            current = Advance(1);
                            Elements.Add(new JsonNamedElement(name, array));
                            if (End(current))
                            {
                                return false;
                            }
                            if (Is(current, EndElement))
                            {
                                return true;
                            }
                            if (Is(current, ','))
                            {
                                current = Advance(1);
                                continue;
                            }
                            return false;
                        }
                        var element = new JsonElement();
                        if (element.TryParse(current))
                        {
                            current = Advance(1);
                            Elements.Add(new JsonNamedElement(name, element));
                            if (End(current))
                            {
                                return false;
                            }
                            if (Is(current, EndElement))
                            {
                                return true;
                            }
                            if (Is(current, ','))
                            {
                                current = Advance(1);
                                continue;
                            }
                            return false;
                        }
                        return false;
                    }
                    return false;
                }
            }
        }

        abstract class JsonList : Element
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
                get
                {
                    if (_elements != null)
                    {
                        var sb = new StringBuilder();
                        sb.Append(StartElement);
                        if (_elements.Count > 0)
                        {
                            sb.AppendFormat("{0}", _elements[0].Text);
                            for (var i = 1; i < _elements.Count; i++)
                            {
                                sb.AppendFormat(", {0}", _elements[i].Text);
                            }
                            sb.Append(EndElement);
                            return sb.ToString();
                        }
                    }
                    return null;
                }
            }
        }

        class JsonName : Element
        {
            private string _content;

            public override string Text
            {
                get { return string.Format("\"{0}\"", _content); }
            }

            public override bool Parse()
            {
                var current = Advance(0);
                if (End(current))
                {
                    return false;
                }
                if (Is(current, '"'))
                {
                    var next = Next(current + 1, '"') - 1;
                    if (next > current)
                    {
                        _content = Substring(current + 1, next - current);
                        Advance(next - current + 1);
                        return true;
                    }
                }
                return false;
            }
        }

        class JsonNamedElement : INamedElement
        {
            private readonly IElement _name;
            private readonly IElement _value;

            public JsonNamedElement(IElement name, IElement value)
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

            #endregion
        }

        class JsonValue : Element
        {
            private string _content;

            public override string Text
            {
                get { return string.Format("{0}", _content); }
            }

            public override bool Parse()
            {
                var current = Advance(0);
                var start = current;
                if (End(current))
                {
                    return false;
                }
                while (Any(current, "0123456789"))
                {
                    current++;
                    if (End(current))
                    {
                        return false;
                    }
                }
                if (current > start)
                {
                    _content = Substring(start, current - start);
                    Advance(current - start - 1);
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
                var exception = ex;
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