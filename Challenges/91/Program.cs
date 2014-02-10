#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
            char[] delims = { ' ', '\r', '\n', '\t' };
            while (stringsLength < strings.Length)
            {
                List<double> words = strings[stringsLength++].Split(delims).Select(OnFunc).ToList();
                int wordsLength = words.Count;
                if (wordsLength > 0)
                {
                    words.Sort();
                    Quicksort(words.ToArray(), 0, wordsLength - 1, Comparer<double>.Default);
                    Console.Write("{0}", words[0].ToString(CultureInfo.InvariantCulture));
                    for (int i = 1; i < wordsLength; i++)
                    {
                        Console.Write(" {0}", words[i].ToString(CultureInfo.InvariantCulture));
                    }
                }
                Console.WriteLine();
            }
        }

        double OnFunc(string s)
        {
            return double.Parse(s, CultureInfo.InvariantCulture);
        }


        void Quicksort<T>(T[] array, int left, int right, IComparer<T> comparer)
        {
            if (left < right)
            {
                int index = Partition(array, left, (left + right) / 2, right, comparer);
                Quicksort(array, left, index - 1, comparer);
                Quicksort(array, index + 1, right, comparer);
            }
        }

        int Partition<T>(T[] array, int left, int pivot, int right, IComparer<T> comparer)
        {
            T value = array[pivot];
            Swap(array, pivot, right);
            for (int i = left; i < right; i++)
            {
                if (comparer.Compare(array[i], value) <= 0)
                {
                    Swap(array, i, left++);
                }
            }
            Swap(array, left, right);
            return left;
        }

        void Swap<T>(T[] array, int p1, int p2)
        {
            if (p1 != p2)
            {
                T temp = array[p1];
                array[p1] = array[p2];
                array[p2] = temp;
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