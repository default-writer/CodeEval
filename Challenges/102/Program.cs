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
    public interface IElement
    {
        string Text { get; }
    }

    public interface IElementCollection : IElement
    {
        IEnumerable<IElement> Elements { get; }
    }

    internal interface IChallenge
    {
        void Main(string[] args);
    }


    public interface INamedElement : IElement
    {
        string Name { get; }
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
                var element = new JsonList();
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
        public static void ParseMany(string text, IList<IElement> items)
        {
            _input = text;
            _position = 0;
            if (!string.IsNullOrWhiteSpace(text))
            {
                while (true)
                {
                    var element = new JsonList();
                    var current = Advance(0);
                    if (element.TryParse(current))
                    {
                        current = Advance(1);
                        items.Add(element);
                        if (End(current))
                        {
                            return;
                        }
                        continue;
                    }
                    return;
                }
            }
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
            return current < _input.Length && text.Any(t => _input[current] == t);
        }

        internal static bool Is(int current, string text)
        {
            return !text.Where((t, i) => Not(current + i, t)).Any();
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
                if (Is(current, ch))
                {
                    return current;
                }
                current++;
            }
            return previous;
        }

        internal static bool Is(int current, char ch1)
        {
            return current < _input.Length && _input[current] == ch1;
        }

        internal static bool Not(int current, char ch1)
        {
            return current < _input.Length && _input[current] != ch1;
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

        class JsonArray : Collection
        {
            public JsonArray() : base('[', ']') { }

            public override bool Parse()
            {
                var current = Advance(0);
                if (Not(current, Open))
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
                        Add(value);
                        if (Is(current, ','))
                        {
                            current = Advance(1);
                            continue;
                        }
                        return Is(current, Close);
                    }
                    var name = new JsonName();
                    if (name.TryParse(current))
                    {
                        current = Advance(1);
                        Add(name);
                        if (Is(current, ','))
                        {
                            current = Advance(1);
                            continue;
                        }
                        return Is(current, Close);
                    }
                    var constant = new JsonConstant("null");
                    if (constant.TryParse(current))
                    {
                        current = Advance(1);
                        Add(constant);
                        if (Is(current, ','))
                        {
                            current = Advance(1);
                            continue;
                        }
                        return Is(current, Close);
                    }
                    var element = new JsonList();
                    if (element.TryParse(current))
                    {
                        current = Advance(1);
                        Add(element);
                        if (Is(current, ','))
                        {
                            current = Advance(1);
                            continue;
                        }
                        return Is(current, Close);
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
                if (Is(current, _content))
                {
                    Advance(_content.Length - 1);
                    return true;
                }
                return false;
            }
        }

        class JsonList : Collection
        {
            public JsonList() : base('{', '}') { }

            public override bool Parse()
            {
                var current = Advance(0);
                if (Not(current, Open))
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
                    if (Is(current, ':'))
                    {
                        current = Advance(1);
                        var value = new JsonValue();
                        if (value.TryParse(current))
                        {
                            current = Advance(1);
                            Add(new JsonNamedElement(name, value));
                            if (Is(current, ','))
                            {
                                current = Advance(1);
                                continue;
                            }
                            return Is(current, Close);
                        }
                        var str = new JsonName();
                        if (str.TryParse(current))
                        {
                            current = Advance(1);
                            Add(new JsonNamedElement(name, str));
                            if (Is(current, ','))
                            {
                                current = Advance(1);
                                continue;
                            }
                            return Is(current, Close);
                        }
                        var array = new JsonArray();
                        if (array.TryParse(current))
                        {
                            current = Advance(1);
                            Add(new JsonNamedElement(name, array));
                            if (Is(current, ','))
                            {
                                current = Advance(1);
                                continue;
                            }
                            return Is(current, Close);
                        }
                        var element = new JsonList();
                        if (element.TryParse(current))
                        {
                            current = Advance(1);
                            Add(new JsonNamedElement(name, element));
                            if (Is(current, ','))
                            {
                                current = Advance(1);
                                continue;
                            }
                            return Is(current, Close);
                        }
                        return false;
                    }
                    return false;
                }
            }
        }

        abstract class Collection : Element, IElementCollection
        {
            protected readonly char Close;
            protected readonly char Open;

            private readonly List<IElement> _elements = new List<IElement>();

            protected Collection(char open, char close)
            {
                Open = open;
                Close = close;
            }

            public IEnumerable<IElement> Elements
            {
                get { return _elements; }
            }

            protected void Add(IElement element)
            {
                _elements.Add(element);
            }

            public override string Text
            {
                get
                {
                    var sb = new StringBuilder();
                    sb.Append(Open);
                    if (_elements.Count > 0)
                    {
                        sb.AppendFormat("{0}", _elements[0].Text);
                        for (var i = 1; i < _elements.Count; i++)
                        {
                            sb.AppendFormat(", {0}", _elements[i].Text);
                        }
                        sb.Append(Close);
                    }
                    return sb.ToString();
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

            public string Name
            {
                get { return _name.Text; }
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
            var strings = File.ReadAllText(args[0]);
            List<IElement> elements = new List<IElement>();
            Json.ParseMany(strings, elements);
            foreach (var e in elements)
            {
                var query = e as IElementCollection;
                if (query == null)
                {
                    continue;
                }
                int sum = 0;
                foreach (var element in query.Elements)
                {
                    var n = element as INamedElement;
                    if (n == null)
                    {
                        continue;
                    }
                    var collection1 = n.Value as IElementCollection;
                    if (collection1 == null)
                    {
                        continue;
                    }
                    foreach (var element2 in collection1.Elements)
                    {
                        var items = element2 as INamedElement;
                        if (items == null || items.Name != "\"items\"")
                        {
                            continue;
                        }
                        var colleciton2 = items.Value as IElementCollection;
                        if (colleciton2 == null)
                        {
                            continue;
                        }
                        foreach (var element3 in colleciton2.Elements)
                        {
                            IElementCollection collection3 = element3 as IElementCollection;
                            if (collection3 == null)
                            {
                                continue;
                            }
                            int localsum = 0;
                            bool found = false;
                            foreach (var element4 in collection3.Elements)
                            {
                                INamedElement id = element4 as INamedElement;
                                if (id == null)
                                {
                                    continue;
                                }
                                if (id.Name == "\"id\"")
                                {
                                    int value;
                                    if (int.TryParse(id.Value.Text, out value))
                                    {
                                        localsum += value;
                                    }
                                }
                                if (id.Name == "\"label\"")
                                {
                                    found = true;
                                }
                            }
                            if (found)
                            {
                                sum += localsum;
                            }
                        }
                    }
                }
                Console.WriteLine(sum);
            }
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