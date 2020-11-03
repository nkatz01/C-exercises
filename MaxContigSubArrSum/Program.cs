using System;
using System.Linq;

namespace MaxContigSubArrSum
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 };
            Console.WriteLine(Run(array));
        }

        public static int Run(int[] A)
        {
           int largestSum = (int)System.Math.Pow(-10,3);
            Console.WriteLine(largestSum);
            int newLargestSum = 0;
            for (int i = 0; i < A.Length; i++) { 
                newLargestSum += A[i];
                if (largestSum < newLargestSum)
                    largestSum = newLargestSum;
                if (newLargestSum < 0)
                    newLargestSum = 0;
            }
           if (largestSum< 1)
                             largestSum = A.Max();
            return largestSum;
        }
    }
}

