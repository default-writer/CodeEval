#region

using System;
using System.Collections.Generic;

#endregion

namespace Challenges
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
            int palindrome = 1;
            while (prime < 1000)
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
                    if (!isprime1 &&
                        !isprime2)
                    {
                        break;
                    }
                }
                if (isprime1)
                {
                    primes.Add(prime);
                    if (prime > 100)
                    {
                        if (prime % 10 == prime / 100 &&
                            prime > palindrome)
                        {
                            palindrome = prime;
                        }
                    }
                }
                if (isprime2)
                {
                    primes.Add(prime + 2);
                    if ((prime + 2) > 100)
                    {
                        if ((prime + 2) % 10 == (prime + 2) / 100 &&
                            (prime + 2) > palindrome)
                        {
                            palindrome = prime;
                        }
                    }
                }
                if (isprime1 || isprime2)
                {
                    primesSqrt = (int) Math.Ceiling(Math.Sqrt(prime + 2));
                }
                prime += 6;
            }
            Console.WriteLine(palindrome);
        }
    }
}