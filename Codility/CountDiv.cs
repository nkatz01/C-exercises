using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codility
{
    class CountDiv
    {

        public static void Main()
        {
            Console.WriteLine(solution(6, 11, 2));
            Console.WriteLine(solution(0, 1, 1));//->2
            Console.WriteLine(solution(0, 0, 1));//->1
            Console.WriteLine(solution(0, 0, 11));//->1
            Console.WriteLine(solution(0, 14, 2));//-> 8
            Console.WriteLine(solution(10, 10, 5));//->1
            Console.WriteLine(solution(11, 13, 2));//->1
            Console.WriteLine(solution(0, 2000000000, 2000000000));//->1
        }

        public static int solution(int A, int B, int K)
        {

            if (K > B && A > 0)
                return 0;
            else
                if (K > B)//return 1 for 0 as explained below
                return 1;
            long count = 0;//we need to handle when B is 2 billion
            for (int i = B; i >= A; i--)//start of from the highest number
            {
                if (i % K == 0)//else it maybe a prime
                {

                    count = (((long)(i - A)) + K) / K; //if A is 0, add one multiple of K for 0 is divisible by all numbers. if not, still add one multiple of K for the number you’re subtracting (A). 
                    break;
                }
            }

            return (int)count;
        }


    }
}


