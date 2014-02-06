using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
            string[] lines = File.ReadAllLines(args[0]);
            if (lines.Length > 20) return;
            foreach (var line in lines)
            {
                string[] input = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (input.Length == 3)
                {
                    int A, B, N;
                    if (!int.TryParse(input[0], out A) || !int.TryParse(input[1], out B) || !int.TryParse(input[2], out N))
                    {
                        continue;
                    }
                    Console.Write("1");
                    for (int i = 2; i <= N; i++)
                    {
                        Console.Write(" ");
                        bool case1 = false;
                        bool case2 = false;
                        if (i%A == 0) case1 = true;
                        if (i%B == 0) case2 = true;
                        if (case1) Console.Write("F");
                        if (case2) Console.Write("B");
                        if (case1 || case2)
                        {
                            continue;
                        }
                        Console.Write(i);
                    }
                }
                Console.WriteLine();
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