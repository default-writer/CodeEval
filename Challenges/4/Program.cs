using System.Linq;
using NUnit.Framework;

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

    internal class Challenge : IChallenge
    {
        public void Main(string[] args)
        {
            List<int> primes = new List<int>();
            List<int> primes_sqrt = new List<int>();
            primes.Add(2);
            primes.Add(3);
            int prime = 5;
            while (primes.Count < 1000)
            {
                bool isprime = true;
                bool isprimeplus2 = true;
                foreach (var i in primes_sqrt)
                {
                    if (isprime && (prime%i) == 0)
                    {
                        isprime = false;
                    }
                    if (isprimeplus2 && ((prime + 2)%i) == 0)
                    {
                        isprimeplus2 = false;
                    }
                    if (!isprime && !isprimeplus2)
                    {
                        break;
                    }
                }
                if (isprime)
                {
                    primes.Add(prime);
                }
                if (isprimeplus2)
                {
                    primes.Add(prime + 2);
                }
                if (isprime || isprimeplus2)
                {
                    int sqrt = (int) Math.Floor(Math.Sqrt(prime));
                    if (primes_sqrt[primes_sqrt.Count - 1] != sqrt)
                        primes_sqrt.Add(sqrt);
                }
                prime += 6;
            }
            Console.WriteLine(primes.Sum());
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