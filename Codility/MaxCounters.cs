using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Codility
{
    class MaxCounters
    {
        public static void Main()
        {
            Console.WriteLine(string.Join(",", solution(5, new int[] { 3, 4, 4, 6, 1, 4, 4 })));
            // Console.WriteLine(string.Join(",", solution(1, new int[] { 1})));
            // Console.WriteLine(string.Join(",", solution(1, new int[] { 2 })));
            //  Console.WriteLine(string.Join(",", solution(6, new int[] { 3, 4, 4, 7, 1, 4, 4 ,7})));
            //  Console.WriteLine(string.Join(",", solution(6, new int[] { 7,7,7,7,7,7})));
            //  Console.WriteLine(string.Join(",", solution(1, new int[] { 2, 1, 1, 2, 1 })));

        }



        //Anything that's got no instruction for, is going to have the value of the highest indecie up to before the last bumpup.
        public static int[] solution(int N, int[] A)
        {


            var max = 0;
            int[] counters = new int[N];//we're going to store here the results of the Increase(X) (or 'bumpup') operations; ignoring the MaxCounter(all Xs) operations for now.
            Hashtable indexesChanged = new Hashtable();

            if (A.Where(i => i <= counters.Length).Count() < 1)
                return counters;
            else
            {


                for (int i = 0; i < A.Length; i++)
                {
                    if (A[i] <= counters.Length)
                    {
                        counters[A[i] - 1]++;
                        if (!indexesChanged.ContainsKey(A[i] - 1))//stores how many changes (Increase ops) we've made to this index in 'counters' in between MaxCounter operations
                            indexesChanged.Add(A[i] - 1, 1);
                        else
                            indexesChanged[A[i] - 1] = ((int)indexesChanged[A[i] - 1]) + 1;
                    }
                    else
                    {
                        if (indexesChanged.Count > 0)//each time we have an MaxCounter operation, if any bumpups were made in between, we're adding to max 
                                                     //the most biggest new change (since the last MaxCounter operation) we've made to a single index in 'countres'
                        {
                            max += indexesChanged.Values.Cast<int>().Max();
                            indexesChanged.Clear();//we're clearing the hashtable in order to start a new 'bumpup series'
                        }

                    }
                }

                for (int j = 0; j < counters.Length; j++)
                {
                    if (indexesChanged.Contains(j) && counters[j] < max)//if we've changed this index in 'counters' since the last MaxCounter we need this index to be 'max' plus the last changes/bumpups
                        //except if it's the same index that was the max at the last MaxCounter operation in which case it will already have the additional changes as well as max.. 
                        counters[j] = max + (int)indexesChanged[j];
                    else if (counters[j] < max)//if this index had no changes the last time round (or also if the last instruction was a MaxCounter) or it had changes but is already =<max, we don't want to override it with a lower number.
                        counters[j] = max;

                }

                return counters;
            }
        }




        public static int[] solution1(int N, int[] A)//77% 2 failed
        {
            int[] counters = new int[N];
            if (A.Where(i => i <= counters.Length).Count() < 1)
                return counters;
            else
            {
                for (int i = 0; i < A.Length; i++)
                {
                    if (A[i] <= counters.Length)
                    {

                        counters[A[i] - 1]++;
                    }
                    else
                    {
                        var max = counters.Max();
                        for (int j = 0; j < counters.Length; j++)
                            counters[j] = max;
                    }
                }
                return counters;
            }
        }



    }
}
