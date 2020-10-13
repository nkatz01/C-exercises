using System;

namespace SemiPrime
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i <= 51; i++)
            {
                if (IsSemiPrime(i))
                    Console.WriteLine($"{i}");
            }
        }
        public static bool IsSemiPrime(int number)
        {
            int q = 0;
            for (int i = 2; i <= number; i++)
            {
                if (number % i == 0)
                {
                    q = number / i;
                    return checkPrime(i) && checkPrime(q);

                }

            }
            return false;
        }

        public static int getNextPrime(int n)
        {
            do
            {
                n++;
            } while (!checkPrime(n));
            return n;
        }

        public static bool checkPrime(int n)
        {

            if (n == 1)
                return false;

            for (int i = 2; i <= Math.Sqrt(n); i = getNextPrime(i))
            {
                if (n % i == 0 && n != 2)
                    return false;
            }
            return true;

        }
    }
}
