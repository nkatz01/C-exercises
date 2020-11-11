using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codility
{
    class Permutation
    {

        public static void Main()
        {
            Console.WriteLine(solution(new int[] { 4, 1, 2, 3 }));
            Console.WriteLine(solution(new int[] { 4,1,3 }));
        }

        public static int solution(int[] A)
        {
            HashSet<int> hashSet = new HashSet<int>();
            foreach (var e in A)
                hashSet.Add(e);

            if (hashSet.Count() < A.Count())
                return 0; 

           return A.Except(Enumerable.Range(1, A.Length)).Count()==0 ? 1 : 0; 
        }
    }
}
