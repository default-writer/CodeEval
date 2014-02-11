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
        public class Pair
        {
            public static IComparer<Pair> Comparer = new PairComparer(); 
            public string text;
            public double data;
            class PairComparer : IComparer<Pair>
            {
                public int Compare(Pair x, Pair y) 
                {
                    if (x.data < y.data) 
                        return -1;
                    if (x.data > y.data)
                        return 1;
                    return 0;
                }
            }
        }


        public void Main(string[] args)
        {
            string[] strings = File.ReadAllLines(args[0]);
            int stringsLength = 0;
            char[] delims = { ' ' };
            while (stringsLength < strings.Length)
            {
                Pair[] words = strings[stringsLength++].Split(delims).Select(OnFunc).ToArray();
                int wordsLength = words.Length;
                if (wordsLength > 0)
                {
                    Quicksort(words, Pair.Comparer);
                    Console.Write("{0}", words[0].text);
                    for (int i = 1; i < wordsLength; i++)
                    {
                        Console.Write(" {0}", words[i].text);
                    }
                }
                Console.WriteLine();
            }
        }

        Pair OnFunc(string s) { return new Pair() { data = double.Parse(s, CultureInfo.InvariantCulture), text = s }; }

        class PartitionItem
        {
            public int left;
            public int right;
        }

        void Quicksort<T>(T[] array, IComparer<T> comparer)
        {
            Stack<PartitionItem> list = new Stack<PartitionItem>();
            list.Push(new PartitionItem() { left = 0, right = array.Length - 1 });
            while (list.Count > 0)
            {
                PartitionItem p = list.Pop();
                int left = p.left;
                int right = p.right;
                int index = Partition(array, left, (left + right) / 2, right, comparer);
                if (left < index - 1) list.Push(new PartitionItem() { left = left, right = index - 1 });
                if (index + 1 < right) list.Push(new PartitionItem() { left = index + 1, right = right });
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