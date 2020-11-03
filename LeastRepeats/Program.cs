using System;
using static System.Math;
using System.Linq;
using System.Collections.Generic;

namespace hackAJob1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Run(new int[] { 80, 9, 4, 4, 4, 6, 6, 9 }));
            Console.WriteLine(Run1(new int[] { 80, 9, 4, 4, 4, 6, 6, 9 }));




        }
        //Proper solution
        static public int Run1(int[] student_list)
        {
            HashSet<int> set = new HashSet<int>();
            foreach(int i in student_list)
            {
                if (set.Contains(i))
                    set.Remove(i);
                else
                    set.Add(i);
            }
            return set.First();
        }
        static public int Run(int[] student_list)
        {

            int[] arr = new int[10000];

            if (student_list.Length > 10000)
                Array.Copy(student_list, 0, arr, 0, 10000);
            else
                Array.Copy(student_list, 0, arr, 0, student_list.Length);

            var newArr = arr.Where(i => i != 0).GroupBy(i => i);
            //int single_student_number = newArr.OrderBy(i => i.Count()).First().Key;
            int single_student_number = newArr.Where(j => j.Count() == newArr.Min(i => i.Count())).First().Key;
            return single_student_number;


        }
      
    }
}