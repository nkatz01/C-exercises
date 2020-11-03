using System;

namespace RomanNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a decimal year, between 1 and 9999 inclusive, that you wish to convert to roman numerals : ");
            ;
            var n = Int32.Parse(Console.ReadLine());
            Console.WriteLine(convert(n));


        }

        public static string convert(int n)
        {

            if (n >= 1)
            {
                int year = n;
                String romanOnes = RomanDigit(n % 10, "I", "V", "X"); //module assures that only 'place 1's' value is passed as argument
                n = n / 10;
                String romanTens = RomanDigit(n % 10, "X", "L", "C");
                n = n / 10;
                String romanHundreds = RomanDigit(n % 10, "C", "D", "M");
                n = n / 10;

                String romanThousands = "";


                for (int i = n % 10; i > 0; i -= RomanDigit(i, "M", "M", "M").Length)
                {

                    romanThousands += RomanDigit(i, "M", "M", "M");

                }


                return "Year " + year + " converted to Roman numerals is " + romanThousands + romanHundreds + romanTens + romanOnes;
            }

            else
            {
                throw new ArgumentException(String.Format("{0} is not an positive number", n),
                                      "n");
            }
        }
        public static String RomanDigit(int n, String one, String five, String ten)
        {

            String roman = "";

            if (n == 1)
            { roman += one; }
            else if (n == 2)
            { roman += one + one; }
            else if (n == 3)
            { roman += one + one + one; }
            else if (n == 4)
            { roman += one + five; }
            else if (n == 5)
            { roman += five; }
            else if (n == 6)
            { roman += five + one; }
            else if (n == 7)
            { roman += five + one + one; }
            else if (n == 8)
            { roman += five + one + one + one; }
            else if (n == 9)
            { roman += one + ten; }
            else
                ; // if n==0, roman returns empty

            return roman;

        }

    }
}