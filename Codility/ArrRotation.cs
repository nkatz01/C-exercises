using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codility
{
    class ArrRotation
    {

        public static int[] Solution(int[] A, int K)
        {
            if (A.Count() == 0)
                return A;
            for (var j =0; j<K; j++) {
                var pivot = A[A.Length-1];
                var i = A.Length - 1;
                while (i >0 ) 
          
                {
                    A[i ] = A[ i-1];
                    i--;
            }
                 
              
                 
            
                
                A[0] = pivot;

            }

          
            return A;
        }


 
        public static void Main()
        {
           
            Solution(new int[] { 1, 2, 3, 4, 5 }, 1).ToList().ForEach(i => Console.Write(i + " "));
            //Solution(new int[] { 1, 2, 3, 4, 5 }, 2).ToList().ForEach(i => Console.Write(i + " "));
            //Solution(new int[] { 1, 2, 3, 4, 5 }, 3).ToList().ForEach(i => Console.Write(i + " "));
            //Solution(new int[] { 1, 2, 3, 4, 5 }, 4).ToList().ForEach(i => Console.Write(i + " "));
            //Solution(new int[] { 1, 2, 3, 4, 5 }, 5).ToList().ForEach(i => Console.Write(i + " "));
            //Solution(new int[] { 3, 4, 5, 1, 2 }, 2).ToList().ForEach(i => Console.Write(i + " "));
            //({ 1, 2, 3, 4, 5 }, 1)            ->        {5, 1, 2, 3, 4}
            //({ 1, 2, 3, 4, 5 }, 2)            ->         {4, 5, 1, 2, 3}
            //({ 1, 2, 3, 4, 5 }, 3)            ->         {3, 4, 5, 1, 2}
            //({ 1, 2, 3, 4, 5 }, 4)            ->         {2, 3, 4, 5, 1}
            //({ 1, 2, 3, 4, 5 }, 5)            ->         {1, 2, 3, 4, 5}
            //({ 3, 4, 5, 1, 2 }, 2)            ->         {1, 2, 3, 4, 5}
        }
    }
}
