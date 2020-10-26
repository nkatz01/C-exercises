using System;
using System.Linq;

namespace HackerRank
{
    class Program
    {
     
        static void Main(string[] args)
        {
               int[,] arr = {
            {1, 1, 1, 0, 0, 0},
            {0, 1 ,0 ,0 ,0, 0},
            {1 ,1, 1, 0, 0 ,0},
            {0, 0 ,2 ,4 ,4 ,0},
            {0 ,0 ,0 ,2 ,0, 0},
            {0 ,0 ,1, 2 ,4 ,0}
            };

            Console.WriteLine(hourglassSum(arr));
        }

     public   static int hourglassSum(int[,] arr)// -> 19
        {
            //for(var r = 0; r<arr.GetLength(0); r++)
            //{
            //    Console.WriteLine();
            //    for (var c = 0; c < arr.GetLength(1) ; c++)
            //        Console.Write(arr[r, c]);
            //}

            int[] sums = new int[(arr.GetLength(0) - 2) * (arr.GetLength(1) - 2)];
            Console.WriteLine(sums.Count());
            var ind = 0;
            for (var r = 0; r < arr.GetLength(0) - 2; r++)// r < arr.GetLength(0) - 2; r++)
            {
                sums = UpperAndLower(r, arr, sums, ind);
                Console.WriteLine();
                sums = Middle(r+1, arr, sums, ind);
                Console.WriteLine();
                sums = UpperAndLower(r + 2, arr, sums, ind);
                //Console.Write(arr[r, c]);
                //   Console.WriteLine(string.Join(" ",resArr));
                Console.WriteLine();
                ind += arr.GetLength(1) - 2;
            }


            return sums.Max();
        }

    public static int[] UpperAndLower(int r,  int[,] arr, int[] sums, int ind)
        {

            for ( var c = 0; c < arr.GetLength(1) - 2; c++) { 
                sums[ind] += arr[r, c] + arr[r,c+1] + arr[r, c+2] ;
              Console.WriteLine(sums[ind]);//3,2,1,0
               
                ind++;
            }
          
            return sums;
        }
    public static int[] Middle(int r,  int[,] arr, int[] sums, int ind)
    {
            for (var c = 1; c < arr.GetLength(1) - 1; c++) { 
                sums[ind] += arr[r, c];
                //Console.WriteLine(arr[r, c]);
               Console.WriteLine(sums[ind]);

                ind++;
            }
            return sums;
    }
    }
}
