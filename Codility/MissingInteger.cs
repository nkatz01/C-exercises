using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility
{
    class MissingInteger
    {
        public static void Main()
        {
            //  Console.WriteLine(solution(new int[] {  1, -1, 3, 6, 4, 1, 2 }));
            // Console.WriteLine(solution(new int[] { 1, 2, 3 }));
            Console.WriteLine(solution(new int[] { -1, -3 }));
        }


        public static int solution(int[] A)
        {

            HashSet<int> hs = new HashSet<int>();
            var B = A.OrderBy(i => i).ToArray();
            var startInd = Array.IndexOf(B, B.Where(i => i > 0).Count() > 0 ? B.First(i => i > 0) : 1);//if the array contains positive values, get the index of the first and lowest positive item

            if (startInd < 0)//if there are no positive numbers, 1 will also not be found and will set startInd to -1.
                return 1; ;


            for (int j = startInd; j < A.Length; j++)

                hs.Add(B[j]);//deals with dups


            int e = 1;
            while (hs.Contains(e))
            {
                e++;
            }
            return e;



        }
    }
}
