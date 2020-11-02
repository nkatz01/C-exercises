using System;
using System.Collections.Generic;
using System.Text;

namespace Codility
{
    public class FrogJmp
    {

        public static int Solution(int X, int Y, int D)
        {

            double jumps = (double)(Y-X)/D;
           
            if (jumps % 1 > 0)
                return ((Y - X) / D) + 1;
            else
                 return (Y - X) / D;

        }

        public static void Main(string[] args)
        {
            Console.WriteLine(Solution(10, 85, 30));
        }
    }
}
