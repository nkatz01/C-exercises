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
          fizzBuzz(15);
        }

        static public string Run(int N, int M)//Does it for specified range
        {
           
            var range = Enumerable.Range(N, (M - N )+1);
            var strs = range.Select(i => i % 3 == 0 && i % 5 == 0 ? "FizzBuzz" : (i % 3 == 0 ? "Fizz" : (i % 5 == 0 ? "Buzz"  : i.ToString()) )).ToList();
           string sequence = string.Join("," ,strs);
            
            
            return sequence;
        }

        public static void fizzBuzz(int n)
        {
            var range = Enumerable.Range(1, n);//Does it from 1 upto n
            var strs = range.Select(i => i % 3 == 0 && i % 5 == 0 ? "FizzBuzz" : (i % 3 == 0 ? "Fizz" : (i % 5 == 0 ? "Buzz" : i.ToString()))).ToList();

            strs.ToList().ForEach(i => Console.WriteLine(i));


        }
    }
}
