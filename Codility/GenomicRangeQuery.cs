using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codility
{
    class GenomicRangeQuery
    {

        public static void Main()
        {
            solution("CAGCCTA", new int[] { 2, 5, 0 }, new int[] { 4, 5, 6 }).ToList().ForEach(i => Console.Write(i + " "));
        }

        public static int[] solution(string S, int[] P, int[] Q)
        {
            //Foreach character in S, we're going to add an element/tuple of 3 entries to 'prefixSums'. The elements in 'prefixSums' will indicate how many As, Cs, Gs and Ts we found in the string, 
            // with each entry in the tuple indicating a different letter, A, C and G respectively. (See later what we do about T).
            // So, for 3 of these 4 different characters, A C G and T, we have counters that we'll bump up each time we encounter one. One counter for each character. (0, 1, 2) = (A, C, G).
            // By bumping up the relevant counter for each occurrence of the letter it is associated with, they'll in the end serve as accumulators for how many times we encountered the relevant character in the string.
            // Every letter we move forward, we add a new element to 'prefixSums' and have all the counters in the new element take on (we assign to them) the values of the previous counter at their respective positions,
            // as well as adding a 1 if the letter that's associated with 'this' counter is found at the current position and a 0 if not. 
            //To answer the queries, we find out for a given S[P[K]] to S[Q[K]] the minimal factor within this boundary, by subtracting the counter at element P[K]  in 'prefixSums' 
            // [for the relevant factor/letter - i.e. starting from lowest to highest, that is index (firstIndex, second, third)] 
            // from the counter at the same position in element Q[K] in 'prefixSums', to see if we're left with a number more or equal to 1. This will tell us, that in between these two positions in S, a letter associated with the factor of this value [i.e. 1, 2, or 3] was found
            //For the forth character T (i.e. factor 4), we don't need a separate counter as we can indicate its occurrence by merely adding a new element to 'prefixSums' where all the counters remain unchanged from the previous element in 'prefixSums'.

            List<Tuple<int, int, int>> prefixSums = new List<Tuple<int, int, int>>() { Tuple.Create(0, 0, 0) };
            int[] minimalIF = new int[P.Length];




            var i = 0;
            foreach (var ch in S)

            {
                Console.WriteLine(prefixSums.Count());
                switch (ch)//here the string is traversed, and the counters are set accordingly
                {
                    case 'A':
                        prefixSums.Add(Tuple.Create(1 + prefixSums.ElementAt(i).Item1, prefixSums.ElementAt(i).Item2, prefixSums.ElementAt(i).Item3));
                        break;
                    case 'C':
                        prefixSums.Add(Tuple.Create(prefixSums.ElementAt(i).Item1, 1 + prefixSums.ElementAt(i).Item2, prefixSums.ElementAt(i).Item3));
                        break;
                    case 'G':
                        prefixSums.Add(Tuple.Create(prefixSums.ElementAt(i).Item1, prefixSums.ElementAt(i).Item2, 1 + prefixSums.ElementAt(i).Item3));
                        break;
                    default:
                        prefixSums.Add(Tuple.Create(prefixSums.ElementAt(i).Item1, prefixSums.ElementAt(i).Item2, prefixSums.ElementAt(i).Item3));

                        break;

                }
                i++;
            }

            foreach (var p in prefixSums)
                Console.WriteLine(p);

            for (int j = 0; j < P.Length; j++)//here we answer the queries
            {
                if (prefixSums.ElementAt(Q[j] + 1).Item1 - prefixSums.ElementAt(P[j]).Item1 > 0)
                    minimalIF[j] = 1;
                else if (prefixSums.ElementAt(Q[j] + 1).Item2 - prefixSums.ElementAt(P[j]).Item2 > 0)
                    minimalIF[j] = 2;
                else if (prefixSums.ElementAt(Q[j] + 1).Item3 - prefixSums.ElementAt(P[j]).Item3 > 0)
                    minimalIF[j] = 3;
                else
                    minimalIF[j] = 4;
            }
            return minimalIF;
        }

        public static int[] solution6(string S, int[] P, int[] Q)
        {

            Dictionary<Tuple<int, int>, string> ranges = new Dictionary<Tuple<int, int>, string>();
            int[] minimalIF = new int[P.Length];
            SortedDictionary<char, int> lookUp = new SortedDictionary<char, int>();
            HashSet<char> hs = new HashSet<char>();

            lookUp.Add('A', 1);
            lookUp.Add('C', 2);
            lookUp.Add('G', 3);
            lookUp.Add('T', 4);

            var j = 0;
            for (int i = 0; i < P.Length; i++)

            {
                bool found = false;
                if (ranges.Count > 1)
                {
                    foreach (var pair in ranges)
                    {
                        if (pair.Key.Item1 <= P[i] && pair.Key.Item2 <= Q[i])
                            if (pair.Value.Min() == 'A')
                            {
                                found = true;
                                ranges.Add(Tuple.Create(P[i], Q[i]), "A");
                            }
                    }

                }

                else
                {

                    if (!found)
                    {
                        j = P[i];
                        while (hs.Count < 4 && j <= Q[i])
                        {
                            Console.WriteLine(j);
                            hs.Add(S[j]);
                            j++;
                        }


                        minimalIF[i] = lookUp.First(e => hs.Contains(e.Key)).Value;
                        hs.Clear();
                    }
                }

            }


            return minimalIF;
        }
        public static int[] solution5(string S, int[] P, int[] Q)
        {
            int[] minimalIF = new int[Q.Length];
            SortedDictionary<char, int> ht = new SortedDictionary<char, int>();

            ht.Add('A', 1);
            ht.Add('C', 2);
            ht.Add('G', 3);
            ht.Add('T', 4);

            for (int i = 0; i < P.Length; i++)
            {

                var unique = S.Substring(P[i], Q[i] - P[i] + 1).ToCharArray().Distinct().OrderBy(i => i);

                int v;
                minimalIF[i] = ht.TryGetValue(unique.First(), out v) ? v : 0;
            }

            return minimalIF;
        }
        public static int[] solution4(string S, int[] P, int[] Q)
        {
            int[] minimalIF = new int[Q.Length];
            SortedDictionary<char, int> ht = new SortedDictionary<char, int>();
            HashSet<char> hs = new HashSet<char>();

            ht.Add('A', 1);
            ht.Add('C', 2);
            ht.Add('G', 3);
            ht.Add('T', 4);
            var j = 0;
            for (int i = 0; i < P.Length; i++)
            {
                j = P[i];
                while (hs.Count < 4 && j <= Q[i])
                {
                    Console.WriteLine(j);
                    hs.Add(S[j]);
                    j++;
                }



                int v;
                minimalIF[i] = ht.TryGetValue(hs.Min(), out v) ? v : 0;
                hs.Clear();
            }

            return minimalIF;
        }


        public static int[] solution3(string S, int[] P, int[] Q)
        {
            int[] minimalIF = new int[Q.Length];
            SortedDictionary<char, int> ht = new SortedDictionary<char, int>();
            HashSet<char> hs = new HashSet<char>();

            ht.Add('A', 1);
            ht.Add('C', 2);
            ht.Add('G', 3);
            ht.Add('T', 4);
            var j = 0;
            for (int i = 0; i < P.Length; i++)
            {
                j = P[i];
                while (hs.Count < 4 && j <= Q[i])
                {
                    Console.WriteLine(j);
                    hs.Add(S[j]);
                    j++;
                }


                minimalIF[i] = ht.First(e => hs.Contains(e.Key)).Value;
                hs.Clear();
            }

            return minimalIF;
        }
        public static int[] solution2(string S, int[] P, int[] Q)
        {
            int[] minimalIF = new int[Q.Length];
            SortedDictionary<char, int> ht = new SortedDictionary<char, int>();
            HashSet<char> hs;

            ht.Add('A', 1);
            ht.Add('C', 2);
            ht.Add('G', 3);
            ht.Add('T', 4);

            for (int i = 0; i < P.Length; i++)
            {
                hs = new HashSet<char>(S.Substring(P[i], Q[i] - P[i] + 1).ToCharArray());

                minimalIF[i] = ht.First(j => hs.Contains(j.Key)).Value;
            }

            return minimalIF;
        }
        public static int[] solution1(string S, int[] P, int[] Q)
        {
            int[] minimalIF = new int[Q.Length];
            Dictionary<char, int> ht = new Dictionary<char, int>();
            HashSet<char> hs;

            ht.Add('A', 1);
            ht.Add('C', 2);
            ht.Add('G', 3);
            ht.Add('T', 4);

            for (int i = 0; i < P.Length; i++)
            {

                hs = new HashSet<char>(S.Substring(P[i], Q[i] - P[i] + 1).ToCharArray());

                var j = 0;

                while (!hs.Contains(ht.ElementAt(j).Key) && i <= ht.Count)
                {

                    j++;
                }
                if (j <= ht.Count)
                    minimalIF[i] = ht.ElementAt(j).Value;


            }



            return minimalIF;
        }
    }
}
