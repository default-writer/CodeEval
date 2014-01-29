using System;
using System.IO;

// ReSharper disable CheckNamespace
namespace Challenges
// ReSharper restore CheckNamespace
{

    interface IChallenge
    {
        void Main(string[] args);
    }

    class Challenge : IChallenge
    {
        public void Main(string[] args)
        {
            if (args == null) throw new Exception("args == null");
            if (args.Length == 0) throw new Exception("args.Length == 0");
            if (args.Length > 1) throw new Exception("args.Length > 1");
            try
            {
                if (!File.Exists(args[0])) throw new Exception("!File.Exists(args[0])");
                string[] strings = File.ReadAllLines(args[0]);
                int stringsLength = 0;
                char[] delims = new char[] {' ', '\t'};
                while (stringsLength < strings.Length)
                {
                    string[] words = strings[stringsLength++].Split(delims);
                    int wordsLength = words.Length;
                    if (--wordsLength > 0)
                    {
                        Console.WriteLine(words[--wordsLength]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
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
                T t= new T();
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
