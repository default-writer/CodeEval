using System.Linq;

#region

using System;
using System.Collections.Generic;

#endregion

// ReSharper disable CheckNamespace

namespace Challenges
// ReSharper restore CheckNamespace
{
    class Challenge
    {
        static void Main()
        {
            List<int> primes = new List<int>();
            primes.Add(2);
            primes.Add(3);
            int primesSqrt = 1;
            int prime = 5;
            while (primes.Count < 1000)
            {
                bool isprime1 = true;
                bool isprime2 = true;
                for (int i = 0; i < primes.Count && primes[i] <= primesSqrt; i++)
                {
                    if (isprime1 && (prime % primes[i]) == 0)
                    {
                        isprime1 = false;
                    }
                    if (isprime2 && ((prime + 2) % primes[i]) == 0)
                    {
                        isprime2 = false;
                    }
                    if (!isprime1 && !isprime2)
                    {
                        break;
                    }
                }
                if (isprime1)
                {
                    primes.Add(prime);
                }
                if (isprime2)
                {
                    primes.Add(prime + 2);
                }
                if (isprime1 || isprime2)
                {
                    primesSqrt = (int)Math.Ceiling(Math.Sqrt(prime + 2));
                }
                prime += 6;
            }
            Console.WriteLine(primes.Sum());
        }
    }
}