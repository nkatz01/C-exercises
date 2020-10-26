using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSignal
{
    class Program
    {
        static void Main(string[] args)
        {
            //  duplicateCharacters("Aasdefsgh!!!"); program that returns number of characters that have duplicates - 1 for each group of duplicates (i.e.  !!! = ! = 1)
           //Console.WriteLine( secondHighestDigit("234367"));//program that returns second highest digit in a string, ignoring non-numeric characters. it returns -1 if only one or none digit was given.
           //each number is treated individualy. So, if the highest number has a duplicate, then this number is retruned as the second highest.
           Console.WriteLine( flr("FLLF"));//(Test with: RF -> 3,FLLF -> 0,LFRFRFR -> 1)//program that returns minimum number of commands (steps and head turns) for a robat to retrun to original starting point on an (x,y) plain. 
          //Parameter: a string of commands, ignoring lower letters and anything not L, R or F. L = turn head left, R = turne head right, F = make one step forward. 
        }

      
        public static int flr(string directions)
        {
        
            var direction = 0;
             (int, int) coordinates =  (0,0); 

        var striplower = directions.Where(i => Char.IsUpper(i));

            var commands = striplower.Where(i => i.Equals('F') || i.Equals('L') | i.Equals('R'));
            foreach(var c in commands)
            {
               
                if (c == 'L') {
                    direction -= 1;
                    direction = direction % 4 < 0 ?( direction % 4) + 4 : direction % 4;

                }
                else if (c == 'R') {
                    direction += 1;
                    direction = direction % 4 < 0 ? (direction % 4) + 4 : direction % 4;
                }
                else if (c == 'F')
                {
                    switch (direction)
                    {
                        case 0:
                            coordinates.Item2++;
                            break;
                        case 2:
                            coordinates.Item2--;
                            break;
                        case 1:
                            coordinates.Item1++;
                            break;
                        case 3:
                            coordinates.Item1--;
                            break;





                    }
                }
            }
            int minCommands = 0;
            minCommands = Math.Abs(coordinates.Item1) + Math.Abs(coordinates.Item2);
            var x = coordinates.Item1;
            var y = coordinates.Item2;
            if ((x > 0 && direction == 1) || (x < 0 && direction == 3))
               return minCommands += 2;
            else if ((y > 0 && direction == 0) || (y < 0 && direction == 2))
               return minCommands += 2;
            else if ((x == 0 && y > 0 && direction == 2) || (x == 0 && y < 0 && direction == 0) || (y == 0 && x > 0 && direction == 3) || (y == 0 && x < 0 && direction == 1)  || (x == 0 && y == 0))
                return minCommands;
            else
                return 1; 







            //if (x == 0 && y == 0)
            //    return minCommands;
            //else if (x == 0 || y == 0)
            //{

            //    if (x > 0)
            //    {
            //        if (direction == 0 || direction == 2)
            //            minCommands++;
            //        else if (direction == 1)
            //            minCommands += 2;
            //        else;//dir = 3
            //    }
            //    else if (y > 0)
            //    {
            //        if (direction == 1 || direction == 3)
            //            minCommands++;
            //        else if (direction == 0)
            //            minCommands += 2;
            //        else;//dir = 2
            //    }
            //    else if (x < 0)
            //        if (direction == 0 || direction == 2)
            //            minCommands++;
            //        else if (direction == 3)
            //            minCommands += 2;
            //        else;//dir = 1
            //    else // y<0 && x == 0
            //    {
            //        if (direction == 3 || direction == 1)
            //            minCommands++;
            //        else if (direction == 2)
            //            direction += 2;
            //        else; //dir = 0
            //    }

            //}
            //else   {// neither of them (x nor y) are 0
            //    if (x > 0 && y > 0)
            //    {
            //        if (direction == 0 || direction == 1)
            //            minCommands += 2;
            //        else
            //            minCommands++;
            //    }
            //    else if (x < 0 && y > 0)
            //    {
            //        if (direction == 0 || direction == 3)
            //            minCommands += 2;
            //        else
            //            minCommands++;
            //    }
            //    else if (x < 0 && y < 0)
            //        if (direction == 3 || direction == 2)
            //            minCommands += 2;
            //        else
            //            minCommands++;
            //    else //y is negative and x is positive
            //        if (direction == 1 || direction == 2)
            //        minCommands += 2;
            //    else
            //        minCommands++;

            //}

            //return minCommands;



            //Pseudocode:
            //(Note: 0 = north, 3 = west)
            //if either x or y is 0, than the number of commands is the abs value of the one (x xor y) that isn't 0, plus:
            //if x > 0
            //if dir (direction) is 0 or 2, then 1 (i.e. plus 1)
            //else if dir is 1, then 2
            //else 0
            //if y > 0 
            //if dir is 1 or 3, then 1
            //else if dir is 0, then 2
            //else 0
            //if x < 0
            //if dir is 0 or 2, then 1
            //else if dir is 3, then 2
            //else 0
            //if y < 0
            //if dir is 3 or 1, then 1
            //else if dir is 2, then 2
            //else 0
            //else if neither of them are 0, than it's the abs value of the sum of both (x and y), plus:
            //if x and y are positive 
            //if dir is 0 or 1, then 2
            //else 1
            //else if only x is negative
            // if dir is 0 or 3, then 2
            //else 1
            //else if both are negative
            //if dir is 3 or 2, then 2
            //else 1
            //else (y is negative and x is positive)
            //if dir is 1 or 2, then 2
            //else 1

            


        }

        public static  int secondHighestDigit(string input)
        {
            var stringNumbers = input.ToCharArray().Where(i => Char.IsNumber(i));
            var maybeMinusOne = stringNumbers.Count() <= 1 ? "-".ToCharArray() : stringNumbers;
           
            var  secondHighest = maybeMinusOne.Count() > 1 ? Array.ConvertAll<Char, int>(stringNumbers.ToArray(), c => (int)Char.GetNumericValue(c)).ToList().OrderByDescending(i => i).Skip(1).First() : -1 ;
             
           
            return secondHighest;
        }
        public static int duplicateCharacters(string input)
        {

            var distincts = input.ToCharArray().GroupBy(i => i).Where(i => i.Count() >1);
            
             return distincts.Count();
        }
    }
}
