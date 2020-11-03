using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codility
{
    class PermMissingElem
    {

        public static void Main() => Console.WriteLine(solution(new int[] { 2, 3, 1, 5, 4, 7 }));

        public static int solution(int[] arr)
        {
            Array.Sort(arr);
            var all = Enumerable.Range(1, arr.Length + 1);
            foreach (var i in all)
            {
                if (Array.BinarySearch(arr, i) < 0)
                    return i;

            }
            throw new ArgumentException("Array not missing any value");
        }
    }
}
