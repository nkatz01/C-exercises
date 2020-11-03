using System;
using System.Linq;
namespace DayOfWeek
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(Run("23-10"));
        }

		static public string Run(string birthday_date)
		{
			//
			//A program that given a day (of the month) and month returns all the years in which that day will fall on either a Fri, Sat or Sun. It calculates this from 2016, for the next 50 years.
			//
			string[] splitDate = birthday_date.Split('-');
			int d = Int32.Parse(splitDate[0]);
			int m = Int32.Parse(splitDate[1]);
			string accum = "";
			const int c = 0; 
				  int[] lapsedDaysForMon = { 0, 3, 2, 5, 0, 3, 5, 1, 4, 6, 2, 4 };
			var counter = m < 3 ? Enumerable.Range(2015, 50) : Enumerable.Range(2016, 50); 
				foreach (int y in  counter) { 
				var wkday = (y + y / 4 - y / 100 + y / 400 + lapsedDaysForMon[m - 1] + d + c) % 7;
			 
				if (wkday == 5)
					accum += "Fry-";
				else if (wkday == 6)
					accum += "Sat-";
				else if (wkday == 0)
					accum += "Sun-";
				if (wkday == 6 || wkday == 5 || wkday == 0)
					accum += y + " "; 

			}
			 
			return accum;
		}
	}
}
