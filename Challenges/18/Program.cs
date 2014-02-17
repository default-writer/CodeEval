#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

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

        class BigInteger
        {
            List<byte> numbers;

            public BigInteger(string input)
            {
                numbers = new List<byte> { 0 };
                for (int i = 0; i < input.Length; i++)
                {
                    MultiplyAdd((byte)(input[i] - '0'));
                }
            }

            public static BigInteger Parse(string input)
            {
                return new BigInteger(input);
            }

            private void MultiplyAdd(byte offset)
            {
                // 255 = 0xff
                // 255 * 10 = 2550 = 255*8 + 255*2 = 255 << 3 + 255 << 1
                // 255 << 3 = (0 += 0x7) 0xf8
                // 255 << 1 = (0x7 += 0x1) 0xfe
                // 255 *10 = (0x8 += 0x1) 0xf6

                List<byte> data = new List<byte>();
                
                _shift = offset;
                for (int i = 0; i < numbers.Count; i++)
                {
                    numbers[i] = Add(Shift3(numbers[i]), Shift1(numbers[i]));
                    numbers[i] = Add(numbers[i], _shift);
                }
                if (_shift > 0)
                    numbers.Add(_shift);
            }

            const int Shift3Left = 3;
            const int Shift3Right = 8 - Shift3Left;
            const byte Const3 = byte.MaxValue >> 3;
            const byte NegConst3 = 0x7 << Shift3Right;

            byte _shift;

            private byte Shift3(byte number)
            {
                if (number > Const3)
                {
                    _shift += (byte)((number & NegConst3) >> Shift3Right);
                }
                return (byte)(number << Shift3Left);
            }

            const int Shift1Left = 1;
            const int Shift1Right = 8 - Shift1Left;
            const byte Const1 = byte.MaxValue >> 1;
            const byte NegConst1 = 1 << Shift1Right;

            private byte Shift1(byte number)
            {
                if (number > Const3)
                {
                    _shift += (byte)((number & NegConst1) >> Shift1Right);
                }
                return (byte)(number << Shift1Left);
            }

            const byte ShiftAddLeft = 1;
            const int ShiftAddRight = 8 - ShiftAddLeft;
            const byte ConstAdd = byte.MaxValue >> 1;
            const byte NegConstAdd = 1 << ShiftAddRight;

            private byte Add(byte number1,  byte number2)
            {
                if ((byte)(number1 & ConstAdd) + (byte)(number2 & ConstAdd) > ConstAdd)
                {
                    _shift = (byte)((byte)((number1 & NegConstAdd) >> ShiftAddRight) | (byte)((number2 & NegConstAdd) >> ShiftAddRight));
                }
                else 
                {
                    _shift = 0;
                }
                return (byte)(number1 + number2);
            }
        }

        public void Main(string[] args)
        {
            string[] strings = File.ReadAllLines(args[0]);
            int stringsLength = 0;
            char[] delims = { ',' };
            while (stringsLength < strings.Length)
            {
                string[] words = strings[stringsLength++].Split(delims, StringSplitOptions.RemoveEmptyEntries);
                var n = BigInteger.Parse(words[0].Trim());
                var x = BigInteger.Parse(words[1].Trim());
                //while (x < n)
                //{
                //    x *= 2;
                //}
                Console.WriteLine(x);
            }
        }

        Pair OnFunc(string s) { return new Pair() { data = double.Parse(s, CultureInfo.InvariantCulture), text = s }; }
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