 using System.Collections.Generic;
 
using System.Linq;
 


namespace PrisonBreak
{
    class Program
    {
        static void Main(string[] args)
        {
             Prison(7, 8, new List<int>() { 1, 3, 4, 5, 7, 8, 9, 10 }, new List<int>() { 1, 3, 4, 5, 7, 8, 9, 10 });
        }

        //unfinished problem
        //what it does do: Finds all the subarrays in an array that are increasing by 1 strictly. Two ways of doing it.
        public static long Prison(int n, int m, List<int> h, List<int> v)
        {
            var hOrdered = h.OrderBy(i => i); 
            var vOrdered = v.OrderBy(i => i);
              var hIncrementingBy1 = hOrdered.SelectMany(i =>  hOrdered.Where(j => j - i  == 1 )).Union(hOrdered.SelectMany(j => hOrdered.Where(i => j - i == 1))).OrderBy(i => i);
               var hIncrementingBy2 = hOrdered.Zip( hOrdered.Skip(1) ,(first,second) => second == first + 1 ? first    :  -1 ).Union(hOrdered.Zip(hOrdered.Skip(1), (first, second) => second == first + 1 ?  second : -1)).OrderBy(i => i);
 
            // Console.Write(string.Join(",", hIncrementingBy1));
            // Console.Write(string.Join(",", hIncrementingBy2));
             return 0; 
        }
    }
}
