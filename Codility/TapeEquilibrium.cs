using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codility
{
    class TapeEquilibrium
    {
        public static void Main() => Console.WriteLine(solution(new int[] {3,1,2,4,3 }));//-> 1

        public static int solution(int[] arr)
        {
          
            var l = 0;
            var r = arr.Sum();
            var lowest =Math.Abs( arr[0] - (r - arr[0]));
            for(var i = 0; i < arr.Length - 1; i++)
            {
                l += arr[i];
                r = r - arr[i];
                if (Math.Abs(l - r) < lowest)
                    lowest = Math.Abs(l - r);
             }
            
            return lowest;
        }
    }
}
//  Console.WriteLine(r);//(Math.Abs(l - r)
