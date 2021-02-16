using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codility
{
    class MinAvgTwoSlice
    {

        public static void Main()
        {
            Console.WriteLine();             //->1  0 4 6 8 13 14 19 27    0, 2, 4, 9, 10, 15, 23

            Console.WriteLine(solution(new int[] { 4, 2, 2, 5, 1, 5, 8 }));//->1  0 4 6 8 13 14 19 27
            Console.WriteLine(solution(new int[] { 10, 10, -1, 2, 4, -1, 2, -1 }));//->5    10, 10, -1, 3, 4,5, -3, 4    0 10 20 19 21 25 24 26 25
            Console.WriteLine(solution(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, -7 }));//->8  0 1 3 6 10 15 21 28 36 45 55
            Console.WriteLine(solution(new int[] { 4, 3, 2, 1, 2, 1, 4 }));//->3  0 4 7 9 10 12 13 17
            Console.WriteLine(solution(new int[] { -3, -3, 10, -10, 10, -10, 10, -10, 10, -3, -3, 100, -11 }));//->3  0 -3 -6 4 -6 4 -6 4 -6 4 1 -2 98 87
                                                                                                               //                                         |                                                     2 3  4 5  6 7  8 9 10 11 12 13
                                                                                                               //0 -3 -6 4 -6 4 -6 4 -6 4 1 -2 98 87
                                                                                                               //      2 3  4 5  6 7  8 9 10 11 12 13
                                                                                                               //  solution(new int[] {1,1,1,2});  1 1.6 1.75
                                                                                                               //B = {0, 4, 6, 8, 13, 14, 19, 27 }
        }

        public static int solution(int[] A)
        {

            int[] B = new int[A.Length + 1];
            int i = 0;
            int j = 1;
            int copiedVal = A[1];
            A[1] = A[1] + A[0];
            double min = (double)A[1] / 2;
            int pos = 0;
            while (j < A.Length - 1 || (j < A.Length && i == 0))
            {//The way we do this is by first realizing that for each element in the array, you only need to check
             //all two and three sub arrays of the original array and not more, and keep comparing, althwile resetting 
             //the smallest subarray to the samllest found so far. The reason being, as soon as you find 
             //a sub arrr
                B[i + 1] = B[i] + A[i + j];

                if ((double)B[i + 1] / (i + 2) < min)
                {
                    min = (double)B[i + 1] / (i + 2);
                    pos = j - 1;
                }

                if (i == 1)
                {
                    A[j] = copiedVal;
                    j++;
                    copiedVal = A[j];
                    A[j] = A[j] + A[(j - 1)];
                    i = -1;

                }
                i++;

            }

            return pos;
        }



        //public static int solution3(int[] A)
        //{
        //    // A.ToList().ForEach(p => Console.Write(p + " "));
        //    // Console.WriteLine("\n");

        //    //int[,] B = new int[A.Length - 1, A.Length + 1];
        //    //for (int r = 0; r < B.GetLength(0); r++)
        //    //{
        //    //    for (int e = r + 1; e < B.GetLength(1); e++)
        //    //    {
        //    //        B[r, e] = B[r, e - 1] + A[e - 1];
        //    //        Console.Write(B[r, e] + " ");
        //    //    }
        //    //    Console.WriteLine();


        //    //}


        //    int[] B = new int[A.Length + 1];
        //    //for (int e = 0; e < A.Length-1; e++)
        //    //    B[e + 1] = B[e] + A[e];
        //    int i = 0;
        //    int j = 1;
        //    int copiedVal = A[1];
        //    A[1] = A[1] + A[0];
        //    int minVal = A[1];
        //    bool foundLower = false;
        //    double min = (double)A[1] / 2;
        //    int pos = 0;
        //   // int divisor = 2;

        //    while (j < A.Length)//until j = 6
        //    {


        //        B[i + 1] = B[i] + A[i + j];
        //      //   Console.Write(B[i + 1] + " ");

        //        if (((double)B[i + 1] < ((double)min * (i+2))))//B[i + 1] < minVal || 
        //        {

        //            min = (double)B[i + 1] / (i + 2);
        //          //Console.Write($"({i + 2}, {Math.Round(min, 2)}, { minVal} ) ");
        //            //   //  Console.WriteLine($"dividend: {B[i + 1]} , divisor: {i + 2}, min: {min}");
        //            pos = j - 1;
        //          //  divisor = (i + 2);
        //            //if (B[i + 1] < minVal)
        //            //minVal = B[i + 1];
        //              foundLower = true;
        //        }

        //        //else
        //        //{

        //        //    goingUp = true;

        //        //}




        //        if ((i + j) == A.Length - 1 || foundLower)//
        //        {


        //            if (j == A.Length - 1)
        //                break;
        //            A[j] = copiedVal;
        //            j++;
        //            copiedVal = A[j];

        //            A[j] = A[j] + A[(j - 1)];

        //            i = -1;
        //            // B[i - 1] = 0;
        //            foundLower = false;
        //            //  Console.WriteLine();
        //        }
        //        i++;

        //    }

        //    return pos;
        //}

        //public static int solution2(int[] A)
        //{
        //   // A.ToList().ForEach(p => Console.Write(p + " "));
        //   // Console.WriteLine("\n");

        //    //int[,] B = new int[A.Length - 1, A.Length + 1];
        //    //for (int r = 0; r < B.GetLength(0); r++)
        //    //{
        //    //    for (int e = r + 1; e < B.GetLength(1); e++)
        //    //    {
        //    //        B[r, e] = B[r, e - 1] + A[e - 1];
        //    //        Console.Write(B[r, e] + " ");
        //    //    }
        //    //    Console.WriteLine();


        //    //}


        //    int[] B = new int[A.Length + 1];
        //    //for (int e = 0; e < A.Length-1; e++)
        //    //    B[e + 1] = B[e] + A[e];
        //    int i = 0;
        //    int j = 1;
        //    int copiedVal = A[1];
        //    A[1] = A[1] + A[0];
        //    bool foundLower = false;
        //    double min = (double)A[1] / 2;
        //    int  pos = 0; 
        //    while (j < A.Length  )//until j = 6
        //    {


        //        B[i + 1] = B[i] + A[i + j];
        //         Console.Write(B[i + 1] + " ");

        //        if ((double)B[i + 1] / (i + 2) < min)
        //        {
        //            min = (double)B[i + 1] / (i + 2);
        //            Console.Write($"({Math.Round(min,2)})");
        //            //   //  Console.WriteLine($"dividend: {B[i + 1]} , divisor: {i + 2}, min: {min}");
        //                pos = j - 1;
        //           // foundLower = true;
        //            }

        //            //else
        //            //{

        //            //    goingUp = true;

        //            //}




        //            if ((i + j) == A.Length-1 || foundLower)//
        //        {


        //            if (j == A.Length - 1)
        //                break;
        //            A[j] = copiedVal;
        //            j++;
        //            copiedVal = A[j];

        //            A[j] =  A[j] + A[(j - 1)];

        //            i = -1;
        //            // B[i - 1] = 0;
        //            foundLower = false;
        //             Console.WriteLine();
        //        }
        //        i++;

        //    }

        // B.ToList().ForEach(e => Console.Write(e + " "));
        //{ 10, 10, -1, 2, 4, -1, 2, -1 }
        //   0  10  20  19 21 25  24  26 25


        //Console.WriteLine();
        //    Console.WriteLine();
        //    Console.Write($"({Math.Round(min, 2)})");
        //    return pos;
        //}





        public static int solution1(int[] A)
        {
            var minStrtPos = 0;
            int[] B = new int[A.Length + 1];
            for (int i = 0; i < A.Length; i++)
                B[i + 1] = B[i] + A[i];
            B.ToList().ForEach(i => Console.Write(i + " "));
            double min = (double)(B[B.Length - 1] - B[B.Length - 3]) / (B.Length - (B.Length - 2));
            minStrtPos = B.Length - 3;
            //  Console.WriteLine($" jsum = {B[B.Length - 1]}, subtractor = {B[B.Length - 3]}, = divisor = {B.Length - (B.Length - 2)}");

            //  Console.WriteLine(minStrtPos);
            // Console.WriteLine(min);
            var jsum = 0;
            var subtractor = 0;
            var divisor = 0;
            for (int j = B.Length - 1; j >= 2; j--)
            {
                for (int i = j - 2; i >= 0; i--)
                {
                    if ((double)(B[j] - B[i]) / (j - i) <= min)
                    {
                        jsum = B[j];
                        subtractor = B[i];
                        divisor = j - i;
                        min = (double)(B[j] - B[i]) / (j - i);
                        minStrtPos = i;
                        Console.WriteLine($" jsum = {jsum}, subtractor = {subtractor}, = divisor = {divisor}, results = {(double)(B[j] - B[i]) / (j - i)}");

                    }

                    // Console.WriteLine((B[B.Length - 1] - B[i]) / (B.Length - (i + 1)));

                    //if (i == 6)
                    //    Console.WriteLine(minStrtPos);
                }
                Console.WriteLine($" jsom = {B[j]}, entireSub = {(B[j] - B[0]) / j}");
            }
            // Console.WriteLine( );
            //Console.WriteLine($" jsum = {jsum}, subtractor = {subtractor}, = divisor = {divisor}");
            //Console.WriteLine(min);
            // Console.WriteLine(minStrtPos);
            return minStrtPos;
        }
    }
}

