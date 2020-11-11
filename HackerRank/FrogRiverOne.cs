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
            Dictionary<int, int> dict = new Dictionary<int, int>();//A dictionary only holds unique keys
            for (int i = 0; i < A.Length; i++)
            {
                dict.TryAdd(A[i], i);//puts the content as the key and the index as the value. If the key is already there, the item (key & value) is discarded and not added.
                if (dict.Count() == X)//max of each element is X as that covers all positions across the river.
                    break;
            }
            if (dict.Count() < X)
                return -1;
            else
                return dict.Values.Max();

        }

        //public static int solution(int x, int[] a)
        //{
        //    Dictionary<int, int> dict = new Dictionary<int, int>();
        //    for (int i = 0; i < a.Length; i++)
        //    {
        //        try
        //        {
        //            dict.Add(a[i], i);//puts the content as the key and the index as the value.
        //        }
        //        catch (ArgumentException)
        //        {

        //        }
        //        if (dict.Count() == x)//max of each element is x as that covers all positions across the river.
        //            break;
        //    }
        //    // var values = dict.values.toarray().orderby(i => i);

        //    if (dict.Count() < x)//we acheive that by using a sorted dictionary
        //        return -1;
        //    else
        //        return dict.Values.Max();
        //       // return values.last();

        //}

        public static void Main()
        {
            //   Console.WriteLine(solution(5,new int[] { 1, 3, 1, 4, 2, 3, 5, 4 }));
            Console.WriteLine(solution(3, new int[] { 1, 3, 1, 3, 2, 1, 3 }));

        }
    }
}

