using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Codility
{
    class PassingCars
    {
        public static void Main()
        {
            Console.WriteLine(solution(new int[] { 0, 1, 0, 1, 1 }));
        }
        //efficiant solution
        public static int solution(int[] A)
        {
            int numOfOs = A.Count(i => i == 0);
            int count = 0;
            var zerosFound = 0;
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] == 0)
                {
                    zerosFound++;//record every zero found


                    count += (A.Length - (i + 1)) - (numOfOs - zerosFound);//the number of permutations is: for every zero found, the sums of all the subsequent 1s added together
                                                                           //or, the number of 1s multiplied by the number of 0s that come strictly before (leftwards) themselves. 
                                                                           //So each time we encounter a 0, the number of 1s left to find is the number of indices left, minus the number of 0s left to find.


                    if (count > 1000000000)
                        return -1;
                }

            }
            return count;
        }
        public static int solution0(int[] A)
        {
            int count = 0;

            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] == 0)
                {
                    for (int j = i + 1; j < A.Length; j++)
                    {

                        if (A[j] == 1)
                            count++;
                    }

                    if (count > 1000000000)
                        return -1;
                }
            }
            return count;
        }
    }
}
