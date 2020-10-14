using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StringManipulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(run("ThIs is p"));
        }


        public static string run(String p)
        {
            /*
            * Write your code below; return type and arguments should be according to the problem's requirements
            */
            const string DEL = "::";
            string combined_queries = "";
            char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
            char[] consonants = "bcdfghjklmnpqrstvwxyz".ToCharArray();
            string vows = p.ToLower().Count(i => vowels.Any(c => c.Equals(i))).ToString();
            string consos = p.ToLower().Count(i => consonants.Any(c => c.Equals(i))).ToString();
            combined_queries += vows + " " + consos + DEL;



            string[] splitted = p.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            int j = 0;
            string reversed = "";
            foreach (string s in splitted)
            {
                string caseSwap = "";
                for (int c = 0; c < s.Length; c++)
                {

                    caseSwap += Char.IsLower(s[c]) ? Char.ToUpper(s[c]) : Char.ToLower(s[c]);


                }
                splitted[j] = caseSwap;
                j++;



            }
            splitted = splitted.Reverse().ToArray();
            reversed = string.Join(" ", splitted);

            combined_queries += reversed + DEL;


            combined_queries += p.Replace(" ", "-") + DEL;

            string[] split = Regex.Split(p, @"(a)|(e)|(i)|(o)|(u)", RegexOptions.IgnoreCase).Where(x => !string.IsNullOrEmpty(x)).ToArray();
            foreach (string s in split)
            {
                if (s.ToLower().ToCharArray().Any(e => vowels.Any(i => i.Equals(e))))
                {
                    combined_queries += "pv";
                }
                combined_queries += s;
            }


            return combined_queries;
        }
    }
}



