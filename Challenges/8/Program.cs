#region

using System;
using System.Collections;
using System.IO;

#endregion

namespace Challenges
{
    internal interface IChallenge
    {
        void Main(string[] args);
    }

    internal class Challenge : IChallenge
    {
        public void Main(string[] args)
        {
            string[] strings = File.ReadAllLines(args[0]);
            int stringsLength = 0;
            char[] delims = { ' ', '\t' };
            while (stringsLength < strings.Length)
            {
                string[] words = strings[stringsLength++].Split(delims, StringSplitOptions.RemoveEmptyEntries);
                int wordsLength = words.Length;
                if (wordsLength > 0)
                {
                    for (int i = 0; i < wordsLength; i++)
                    {
                        Console.Write("{0} ", words[wordsLength - 1 - i]);
                    }
                }
                Console.WriteLine();
            }
        }
    }

    internal class Program
    {
        private static int Main(string[] args) { return Run<Challenge>(args); }

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
                T t = new T();
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