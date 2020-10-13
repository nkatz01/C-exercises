using System;
using static System.Math;
using System.Linq;
 namespace hackAJob1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Run(new int[] { 80, 9, 4, 4, 4, 4, 6, 4, 80 }));

           



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