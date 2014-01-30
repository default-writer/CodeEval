#region

using System;
using System.IO;

#endregion

// ReSharper disable CheckNamespace

namespace Challenges
// ReSharper restore CheckNamespace
{
    internal interface IChallenge
    {
        void Main(string[] args);
    }

    internal class Challenge : IChallenge
    {
        public void Main(string[] args)
        {
            if (args == null) throw new Exception("args == null");
            if (args.Length == 0) throw new Exception("args.Length == 0");
            if (args.Length > 1) throw new Exception("args.Length > 1");
            if (!File.Exists(args[0])) throw new Exception("!File.Exists(args[0])");
            var strings = File.ReadAllLines(args[0]);
            var stringsLength = 0;
            char[] delims = {' ', '\t'};
            while (stringsLength < strings.Length)
            {
                var words = strings[stringsLength++].Split(delims);
                var wordsLength = words.Length;
                if (--wordsLength > 0)
                {
                    Console.WriteLine(words[--wordsLength]);
                }
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