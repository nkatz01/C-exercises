using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HackerRank
{
    class MinimumBribe
    {

        static void Main(string[] args)
        {
             int[] arr = new int[] { 1,7,2,3,4,5,6 };//->7
            //  int[] arr = new int[]{1,2,5,3,7,8,6,4 };//->7
            //   int[] arr = new int[] { 2,1,5,3,4 };//-> 3
            //  int[] arr = new int[] { 2,5,1,3,4}; //-> TC
            // int[] arr = new int[] { 5,1,2,3,7,8,6,4};//-> TC
            //  int[] arr = new int[] { 1, 2, 5, 3, 7, 6, 8, 4 };//-> 6
            

            minimumBribes(arr);
        }




        static void minimumBribes(int[] q)
        {
            List<int> numbersFound = new List<int>();
            var count = 0;
            var min = q.OrderBy(i => i).ElementAt(0);

            for (var j = 0; j < q.Length; j++)
            {

                if (q[j] <= j + 1)//if element is equal or less to index number
                {

                    if (q[j] > min)//with every element we find that isn't the next one in the sequence, that signifies another swap. 
                    {
                        count++;

                    }

                }


                else if (q[j] > j + 1 && q[j] <= j + 3)//if element is more than index number - count increases by 1 or two.
                {
                    count += q[j] - (j + 1);
                }

                else
                {
                    count = -1;
                    break;
                }


                numbersFound.Add(q[j]);

                if (q[j] == min)
                    min++;
                while (numbersFound.Contains(min))//meanwhile, the different branches might have found numbers later in the sequence. They will never show up again in q and min needs to account for them.
                {
                    min++;
                }
                //if (numbersFound.Contains(min - 1)) //can removing everything up to min, everytime min gets increased.
                //    numbersFound.OrderBy(i => i).ToList().RemoveRange(0, min-1);


            }


            if (count != -1)
                Console.WriteLine(count);
            else
                Console.WriteLine("Too chaotic");

        }



        //Only half baked solution
        //static void minimumBribes(int[] q)
        //{
        //    var changesOfTwo = 0;
        //    var changesOfOne = 0;
        //    var count = 0;
        //    int rem = 0;
        //    // differences.ToList().ForEach(i => Console.Write(i + " "));

        //    var qOrdered = q.OrderBy(i => i);
        //    var differences = q.Zip(qOrdered, (first, second) => Math.Abs(first - second));
        //    foreach (var el in differences)
        //    {

        //        rem = el % 2;

        //        if (rem == 1)
        //        {
        //            changesOfTwo += el / 2;
        //            changesOfOne++;
        //        }
        //        else

        //            changesOfTwo += el / 2;


        //    }

        //    count =  (changesOfOne/2  )  + (changesOfTwo + changesOfOne);

        //    Console.WriteLine(count);

        //}

    }
}



