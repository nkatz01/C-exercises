using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HackerRank
{
    class FrogRiverOne
    {


          public static int solution(int X, int[] A)
        {
            SortedDictionary<int, int> dict = new SortedDictionary<int, int>();
            for (int i = 0; i < A.Length; i++)
            {
                dict.TryAdd(A[i], i);//puts the content as the key and the index as the value.
                if (A[i] == X)//max of each element is X as that covers all positions across the river.
                    break;
            }

          
            if (dict.Last().Key < X)//We acheive that by using a sorted dictionary
                return -1;
            else
                return dict.Last().Value;

        }  

        public static void Main()
        {
            Console.WriteLine(solution(5,new int[] { 1, 3, 1, 4, 2, 3, 5, 4 }));
        }
    }
}
