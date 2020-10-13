
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility
{
    class obj
    {
       public int Key;
        public int Count;
    }
    class Program
    {
        //Problem: Array with arbitrary numbers in it, possibly repeated, has to be divided exactly 50/50. 
        //It needs to be devided in a way that gives one half as much variety, in terms of different numbers, as possible 
        //while not taking more than half of all the array elements. 
        //A function needs to be written that returns n = the (maximum) number of variable different numbers this half will be 
        //able to take, for a given array of elements. What's the most efficent way to go about dividing it and figuring out what n is?
        //Assumptions: The total array elements is always even.

        
        static void Main(string[] args)
        {
              int[] arr = { 80, 9 ,4, 4, 4, 4,6,4};

              Console.WriteLine(Solution(arr));
             Console.WriteLine(solution1(arr));
            Console.WriteLine(solution2(arr));

        }
        //proper solution
        public static int solution2(int[] A)
        {
            HashSet<int> hs = new HashSet<int>();
            foreach(int i in A)
            {
                hs.Add(i);
            }
            return Math.Min(hs.Count(), A.Length / 2);
        }
        public static int solution1(int[] A)
        {
            var groupedVariations = A.GroupBy(i => i, (content, candies) => new { Key = content, Count = candies.Count() }); //creates anonymous object where content = the key = the value of the grouped element and candies is the grouped object.
            var filteredMoreThanOne = groupedVariations.Where(i => i.Count > 1); // filter all elements whose group count is more than 1
            var halfN = (filteredMoreThanOne.Sum(i => i.Count) - filteredMoreThanOne.Count()); //aggregate values for Miri's brother, leaving left over just one of each type.
            var variations = halfN < A.Count() / 2 ? groupedVariations.Count() - ((A.Count() / 2) - halfN) : groupedVariations.Count();//if we've reached halfN, stop. Else, deduct from type count as much needed to fill deficit.
            return variations;
        }


        public static int Solution(int[] A)
        {
            var ls = A.GroupBy(i => i, (content, candies) => new obj { Key = content, Count = candies.Count()  });
             
             List<obj> Counts = new List<obj>();
            int halfN = 0;
           
             foreach(obj item in ls)
            {
               
                if (item.Count > 1 && halfN < (A.Count()/2)  )
                {
                   if ((halfN + (item.Count - 1)) <= (A.Count() / 2))
                    { 
                    halfN +=   (item.Count -1) ;
                     item.Count = 1;
                     
                     }
                  
               
                else  
                    { 
                    item.Count-= (A.Count() / 2) -halfN ;
                    halfN +=  (A.Count() / 2) - halfN;

                }
                  
                    
                }

                Counts.Add(item);

            }
          
           
            int variations = halfN < A.Count()/2 ? ls.Count() - ((A.Count() / 2) - halfN) : ls.Count();
            
            
            return variations;

        }


    }
}
