using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeSignal
{
    class ArithmeticMeans
    {
        public static void Main()
        {
            //  Console.WriteLine(countArithmeticMeans(new int[] { 2, 4, 6, 6, 3 }));//returns how many of the elements have their right and left 
            //neighbours' sum equal twice their own value. For first and last element, their right and left neighbours respectively are 0.


            //  Console.WriteLine(concatSwaps( "descognail", new int[] { 3,2,3,1,1 }));//receives an array of integers values which when
            //summed denotes the total lenght of the string passed
            //The goal is to split the string, swap all pairwise neihgbours, concatenate back the bits and return the resulting string.
            //This example should return "codesignal"


            string[,] arr = {
            {"a","c","a" },
            {"b","b", "a"},
            {"c","c", "d"}

            };


            Console.WriteLine(String.Join(",", diagonalsArranging(arr)));
            //receives a 2D array of size n*n
            //concatenates all diagnoles, starting from buttom left, assigning a number i to each of them, starting from 1 to n*2-1
            //re-orders them accordign to lexiographic ordering, leaving i the same value as before but possibly in a new position
            //returns an array showing all i's where they're placed in the new ordering.
            //eg. the input 'arr' above should return -> 
        }

        static int[] diagonalsArranging(string[,] a)
        {
            int diagonals = (a.GetLength(0) * 2) - 1;//GetLength(0) gets the length of rows (GetLength(1) the length of columns and so on for each dimension)
            Dictionary<int, string> concatenatedStrs = new Dictionary<int, string>();//we're using a dictionary where the index of each diagonal is the key

            for (int r = a.GetLength(0) - 1; r >= 0; r--)//starts off with row equaling to bottom left corner, decreasing row (until row = 0) after every inner traversal of columns (to the right)
            {

                string concat = "";
                int row = r;
                for (int c = 0; c < a.GetLength(0) - r; c++)//number of columns to traverse to the right gets ever so more  as we decrease row (or is proportional to row reduction)
                {

                    concat += a[row, c];
                    row++;
                }
                concatenatedStrs.Add((a.GetLength(0) * 2) - diagonals, concat);//the first index should be 1; not 0. Since we're not on row 1 (nor col 1) we have to do this trick)
                diagonals--;


            }

            for (int c = 1; c < a.GetLength(1); c++)//now we do the second half diagonals (top right corner)
            {

                string concat = "";
                int col = c;
                for (int r = 0; r < a.GetLength(0) - c; r++)//number of rows left to traverse to the right gets ever so diminished as we increase col number (or is proportional to column increase)

                {

                    concat += a[r, col];
                    col++;
                }
                concatenatedStrs.Add((a.GetLength(0) * 2) - diagonals, concat);
                diagonals--;


            }




            var ordered = concatenatedStrs.OrderBy(i => i.Value);//re-order

            return ordered.Select(i => i.Key).ToArray();//a map function to extract keys only from dictionary and transform to array which is the returned value
        }



        public static int countArithmeticMeans(int[] a)
        {
            int count = 0;
            int prev = 0;
            int i;
            for (i = 0; i < a.Length - 1; i++)
            {

                if (a[i] * 2 == prev + a[i + 1])
                    count++;
                prev = a[i];
            }
            if (a[i] * 2 == prev)
                count++;

            return count;
        }

        static string concatSwaps(string s, int[] sizes)
        {
            var strtInd = 0;
            string[] splitStrngs = new string[sizes.Length];
            for (var i = 0; i < sizes.Length; i++)
            {
                splitStrngs[i] = s.Substring(strtInd, sizes[i]);
                strtInd += sizes[i];
            }
            var temp = "";
            for (var i = 0; i < sizes.Length - 1; i += 2)
                if (i + 1 < sizes.Length)
                {
                    temp = splitStrngs[i];
                    splitStrngs[i] = splitStrngs[i + 1];
                    splitStrngs[i + 1] = temp;
                }


            string concatenated = "";
            foreach (var st in splitStrngs)
                concatenated += st;
            return concatenated;
        }


    }
}
