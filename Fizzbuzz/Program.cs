using System;
using System.Collections;
using System.Linq;
namespace Fizzbuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Run(1,5));
        }

        static public string Run(int N, int M)
        {
            //
            // Write your code below; return type and arguments should be according to the problem's requirements
            //
            var range = Enumerable.Range(N, (M - N )+1);
            var strs = range.Select(i => i % 3 == 0 && i % 5 == 0 ? "FizzBuzz" : (i % 3 == 0 ? "Fizz" : (i % 5 == 0 ? "Buzz"  : i.ToString()) )).ToList();
           string sequence = string.Join("," ,strs);
            
          

            
            return sequence;
        }
    }
}
