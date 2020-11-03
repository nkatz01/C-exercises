using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using static LanguageExt.Prelude;

namespace CalculateShiftsPay

{
    class Program
    {
        //@param rates - a dictionary that has a payRate name as a key and - an amount, a time from and time until (inside a tuple) - as its value.
        //@param shifts - a list of KeyValuePairs (where the keys can be duplicated) where the key is an employee's name and the values (inside a tuple) of a time from and time until they worked. Assumption: A shift doesn't cross over from one day to another (although it will be pointed out later where changes would need to be made in order to accommodate for this.)
        //@return results - a list of KeyValuePairs with the key being an employee's name and the value, a tuple with a payrate name, amount of hours worked for that pay rate and  pay earned for it.

        static List<KeyValuePair<string, (string, string, string)>> CalcPay(Dictionary<string, (double, Option<TimeSpan>, Option<TimeSpan>)> rates, List<KeyValuePair<string, (DateTime, DateTime)>> shifts)
        {
            IEnumerable<IGrouping<string, KeyValuePair<string, (DateTime, DateTime)>>> groupedNames = shifts.GroupBy(i => i.Key);//Group the rows in shifts by name of employee.
           var results = new List<KeyValuePair<string, (string, string, string)>>();
            List<KeyValuePair<string, (double, TimeSpan)>> listOfPaymentsforShiftsMaybRepeated = new List<KeyValuePair<string, (double, TimeSpan)>>();//This collec will be used to receive each return from StartsInPeriod; that is for each call per shift. 
            //Since one shift can include more than one payrate period and also the same period (eg Default) more than once, this collec may hold entries for the same payrate period 'name' (though 'actually' different) repeated.


            foreach (var group in groupedNames)//In this collec, since a dictionary can only have unique keys, entries from the listOfPaymentsforShiftsMaybRepeated collec will be thrown in one after another, updating the previous value at the given payrate 'name'
             //in order to consolidate the repeated payrates appearing in the previous collection. The reason for doing this is because in the end, a collec of pay sums for each payrate period name (only appearing once), for each employee, will have to be returned, regardless of whether 
             //the employee had their default sum made up of two 'Default' periods or just one.

            {
                Dictionary<string, (double, TimeSpan)> allShiftsAccum = new Dictionary<string, (double, TimeSpan)>(){
                 { "Morning", (0, new TimeSpan(0,0,0))},
                 { "Evening", (0, new TimeSpan(0,0,0))},
                 { "Night", (0, new TimeSpan(0,0,0))},
                 { "Default", (0, new TimeSpan(0,0,0))}
              };
                var shiftsPerName = group.ToList().Select(i => new { name = group.Key, startOfShift = i.Value.Item1, endOfShift = i.Value.Item2 }); //Within each group, for each shift, an anonymous object is created with 3 attributes: The employee name (which will be the same since this
                 //was the condition for the groping), the time of the shift-start and the time of the shift-end.

                foreach (var e in shiftsPerName)
                {

                    listOfPaymentsforShiftsMaybRepeated = StartsInPeriod(e.startOfShift, e.endOfShift, rates);//foreach element (shift) in shiftsPerName, send the startOfShift time and endOfShift time to StartsInPeriod() in order to find out how many payrates it is made up, the duration of each and the pay.
                    var aggregatPerShift = listOfPaymentsforShiftsMaybRepeated.GroupBy(i => i.Key, (shiftName, objs) => new { Key = shiftName, Total = objs.Sum(i => i.Value.Item1), Duration = new TimeSpan(objs.Sum(i => i.Value.Item2.Ticks)) });//a groupBy payrate-name together with a function that
                    //projects in addition to the shiftName, also the sum of each payrate period (that is, actual hours worked within a given payrate-name) pay, for that payrate-name/group as well as the duration of hours.
                    foreach (var shiftPay in aggregatPerShift)//While the previous line consolidates the same payrate-names across one shift, the following loop will consolidate the same payrate-names worked across multiple shifts, for the same employee.
                    {
                        allShiftsAccum[shiftPay.Key] = (allShiftsAccum[shiftPay.Key].Item1 + shiftPay.Total, allShiftsAccum[shiftPay.Key].Item2 + shiftPay.Duration);//Updates include the previous values, if there was.
                    }

                }


                foreach (var payrate in allShiftsAccum)//Now, in order to also include the name of that employee as well as format the duration and pay. We copy the values into the 'results' KeyValuePair list. This can then be returned to the caller.
                {
                    if (payrate.Value.Item1 > 0)//Don't include entries that have their (0,0) value left from the declaration of this dictionary.
                        results.Add(new KeyValuePair<string, (string, string, string)>(group.Key, (payrate.Key, payrate.Value.Item2.ToString(@"hh\:mm"), "£" + payrate.Value.Item1.ToString())));

                }


            }


            return results.OrderByDescending(i => i.Key).ThenBy(i => i.Value.Item3).ToList();//A linq OrderBy name and then order by pay, descending.


        }
        //Functions to abstract out nested 'if' statements. The first checks that the times it's received to denote a given shift start and end within the times given to denote a certain payrate period.
        //More specifically, it checks that the startOfShift is after or equal to the start of the payrate and the endOfShift ends before or equal the payrate end.
        //a shift started before rate end, shift started after or equal to rate start and, shift also ended before or equal to rate end

        public static bool Shift_startAndEnd_WithinRate(DateTime startOfShift, DateTime endOfShift, TimeSpan start, TimeSpan end) =>  startOfShift.TimeOfDay.CompareTo(start) >= 0 && endOfShift.TimeOfDay.CompareTo(end) <= 0;
        //Checks whether the shift started within the pay rate but ended after. Specifically, that the startOfShift is before the end of payrate, after the start of the payrate but that the endOfShift is after the end of the payrate.
        public static bool Shift_start_WithinRate_butEndsAftr(DateTime startOfShift, DateTime endOfShift, TimeSpan start, TimeSpan end) => startOfShift.TimeOfDay.CompareTo(end) < 0 && startOfShift.TimeOfDay.CompareTo(start) >= 0 && endOfShift.TimeOfDay.CompareTo(end) > 0;
        //Checks whether the shift started before but ended within the given payrate. Specifically, that the startOfShift is before the start of the pay rate, that the end of the payrate is after the endOfShift but that the endOfShift is after the payrate start.
        public static bool Shift_start_BefrRate_butEndsWithin(DateTime startOfShift, DateTime endOfShift, TimeSpan start, TimeSpan end) => startOfShift.TimeOfDay.CompareTo(start) < 0 && end.CompareTo(endOfShift.TimeOfDay) >= 0 && endOfShift.TimeOfDay.CompareTo(start) > 0;


        public static void Main(string[] args)
        {
            //Problem 2
            //Tuple<int, int> point = Tuple.Create(3, 3);//a tuple is a pair (in this case just 2 but it can have more values) and we can query it with the built in attributes Item1 and Item2 etc.
            //IEnumerable<(int, int)> points = new List<(int, int)>(){//a list of pairs that implements all the additional methods that the IEnumerable interface offers us
            //{   (3, 4) },
            //{   (5, 3) },
            //{   (4, 2) },
            //{   (5, 1) },
            //{   (1, 2) }
            //};

            //var Xmin = points.Min(i => i.Item1);// a linq library function that uses the lambda form. i gets paired with each item in the list subsequently and then we specify that we want Item1 of that element specifically. This will represent the X dimension. 
            //                                    //The Min function selects the lowest value of all the elements' item1s in the list. Same for the next four lines but the difference is whether we're checking X, Y, Min or Max?
            //var Xmax = points.Max(i => i.Item1);
            //var Ymin = points.Min(i => i.Item2);
            //var Ymax = points.Max(i => i.Item2);
            //Boolean isInBetween = (point.Item1 <= Xmax && point.Item1 >= Xmin) && (point.Item2 <= Ymax && point.Item2 >= Ymin);//Here a check is done to see if it's in the boundary. In our case, it is.


            //Problem 2
            //The Rates input collection
            Dictionary<string, (double, Option<TimeSpan>, Option<TimeSpan>)> rates = new Dictionary<string, (double, Option<TimeSpan>, Option<TimeSpan>)>(){//A dictionary cannot have duplicate keys
            { "Morning", ( 15.00,  new TimeSpan( 5, 00, 00), new TimeSpan( 10, 00, 00) ) },//TimeOfDay takes out the time component and leaves the date out.
            { "Evening", ( 18.00,  new TimeSpan( 16, 30, 00) ,  new TimeSpan( 20, 00, 00) ) },
            { "Night", ( 20.00,  new TimeSpan( 20, 00, 00)  , new TimeSpan( 23, 00, 00) ) },
            { "Default", ( 10.00, None, None) }//Equivilant of null in LanguageExt or what is called a Monad. It can either be a TimeSpan or none. It saves us from having to deal with nulls all the time and risking run time null exceptions.
            };



            //The Shifts input collection (from now on collec)
            List<KeyValuePair<string, (DateTime, DateTime)>> shifts = new List<KeyValuePair<string, (DateTime, DateTime)>>(){
             new KeyValuePair<string, (DateTime, DateTime)>("John", (new DateTime(2020, 06, 23, 9, 00, 00), new DateTime(2020, 06, 23, 17, 00, 00))),
             new KeyValuePair<string, (DateTime, DateTime)>("John", (new DateTime(2020, 06, 23, 6, 00, 00), new DateTime(2020, 06, 23, 14, 00, 00))),
             new KeyValuePair<string, (DateTime, DateTime)>("Emma", (new DateTime(2020, 06, 23, 11, 30, 00), new DateTime(2020, 06, 23, 18, 00, 00)))
              };

             Console.WriteLine(string.Join("\n", CalcPay(rates, shifts)));//Calling the CalcPay function which is calling the StartsInPeriod function. Briefly, CalcPay will first group the 'shifts' collec by employee name and then send each shift of each employee 
            //to the StartsInPeriod function which will "slice" that shift up to its pay rate periods, pay and duration for each. It will send back this information to CalcPay which will, for each employee, sum up their pay and duration for each of the four pay rate periods 
            //separately and then return the final calculated results. The final results is a collec with a row for each employee for each pay rate period (across all their shifts), how much total pay they've accumulated and the sum/duration of the hours accountable for that.
           // Console.WriteLine(string.Join(",", StartsInPeriod(new DateTime(2020, 06, 23, 20, 00, 00), new DateTime(2020, 06, 24, 01, 00, 00), rates)));

        }
        //@param startOfShift - the start time of an employee’s shift, extracted from 'shifts'.
        //@param endOfShift - the end time of an employee’s shift, extracted from 'shifts'.
        //@param rates - a dictionary that has a payRate name as a key and - an amount, a time from and time until (inside a tuple) - as its value.
        //@return payments - a list of KeyValuePairs, each containing a payrate-name, the duration of hours making up an actual period worked at this payrate and the amount earned for it.

        public static List<KeyValuePair<string, (double, TimeSpan)>> StartsInPeriod(DateTime startOfShift, DateTime endOfShift, Dictionary<string, (double, Option<TimeSpan>, Option<TimeSpan>)> rates)
        {

            List<KeyValuePair<string, (double, TimeSpan)>> payments = new List<KeyValuePair<string, (double, TimeSpan)>>();//This will be the returned variable.
            TimeSpan end;
            TimeSpan start;
            double pay = 0.00;


            do//The approach we are going to take is the following: A shift (time from - time until) is viewed as a sum of hours and at every iteration, we're subtracting a number of hours, calculating its appropriate worth (based on which payrate it belongs to)
             {   //and checking if the employee has still left unaccounted hours on their balance? 
            
                foreach (var e in rates)//foreach payrate specification, the current shift boundaries (time from - time until) are checked against to see if it starts, ends, both starts and ends or neither starts nor ends, within this payrate's boundaries.
                {

                    if (e.Value.Item2.IsSome)//Because 'rates' can have the 'Default' entry where the time-from and time-until can be None, this condition says that the time (doesn't matter which) cannot be None. 
                    {//period isn't the default
                        start = e.Value.Item2.ToSeq().First();//This is done in order to extract what's inside the Option - i.e. the time (TimeSpan). It's converted to a sequence and then, the first item is taken (This is done in both cases, whether Some or None).
                        end = e.Value.Item3.ToSeq().First();

                         //The shift starts ADN ends within rate period.
                        if (Shift_startAndEnd_WithinRate(startOfShift, endOfShift, start, end) && startOfShift.Day == endOfShift.Day)//This checks if the current shiftStart and ShiftEnd fall both within any of the (due to the foreach loop) payrates in 'rates'.
                           
                            //A check was added to make sure the startOfShift and endOfShift are not on different days to each other. If this is to be accounted for, a separate else needs to handle this as obviously, if for eg the endOfShift finishes the next day
                            //in the middle of the same payrate time frame, for today, the full current payrate (from startOfShift or start of payrate - whichever is later ) still needs to be exhausted. If the check startOfShift.Day == endOfShift.Day was not done,
                            //this would not be obvious and the employee's working hours would be exhausted a day too early.
                        {

                           
                            pay = endOfShift.Subtract(startOfShift).TotalHours * e.Value.Item1;//Subtract start of shift from end of shift to get total hours worked and that's it. All hours are consumed (Of course this is only if startOfShift and endOfShift are on same day as explained above).


                            payments.Add(new KeyValuePair<string, (double, TimeSpan)>(e.Key, (Math.Round(pay), endOfShift - startOfShift)));//The third parametere calculates the duration. In this case, simple.
                            startOfShift = endOfShift;//Resetting the start of shift time so that the do loop is broken out from.
                            break;

                        }
                        else if (Shift_start_WithinRate_butEndsAftr(startOfShift, endOfShift, start, end) && startOfShift.Day == endOfShift.Day)//shift starts within rate period But ends after
                        {
                            pay = end.Subtract(startOfShift.TimeOfDay).TotalHours * e.Value.Item1;//subtract start of shift from 'end of rate end' to get number of hours to calculate on the current payrate (as further on might encoruch on a different or default payrate).

                            payments.Add(new KeyValuePair<string, (double, TimeSpan)>(e.Key, (Math.Round(pay), end - startOfShift.TimeOfDay)));//end is end-time of current payrate.
                            startOfShift += end - startOfShift.TimeOfDay;//We reduce the number of hours left to account for by moving the startOfShift time forward with the number of hours for which we just calculated payment.

                            break;
                        }
                        else if (Shift_start_BefrRate_butEndsWithin(startOfShift, endOfShift, start, end) && startOfShift.Day == endOfShift.Day)//shift starts before BUT ends within rate period
                        {
                            pay = endOfShift.TimeOfDay.Subtract(start).TotalHours * e.Value.Item1;


                            payments.Add(new KeyValuePair<string, (double, TimeSpan)>(e.Key, (Math.Round(pay), endOfShift.TimeOfDay - start)));
                            endOfShift -= endOfShift.TimeOfDay - start;//we adjust the hours left to account for by moving the endOfShift backwards as this might, for eg., be the first time the foreach loop executes and the employee's shift happens to 'start' 
                            //outside of any defined payrate period. I.e. the 'Default'. In such a case, we want those to catch onto the default perhaps, in the next turn. There are other ways this might happen - for example if at the previous iteration, a full payrate
                            //period was exhausted in terms of the employee being paid for it but yet the shift continuous, enters a 'Default' period and draws into a yet another defined period where it stops..


                            break;
                        }
                       // All other cases are where the shift starts AND ends 'after' OR 'before' rate period. We then use the default rate. See below.



                    }

                }
                if (pay == 0)//This well be the case if none of the previous branches were entered.
                {  //To get the start of the next payrate period, we call ReturnEarliestBreak, passing it a Boolean to indicate whether the shift starts in the same day or not, and a list containing, the endOfShift, the start times for all payrate periods (and the startOfShift).
                   //This is done so that, in the case of when startOfShift and endOfShift fall in the same day, ReturnEarliestBreak can return the lowest of them all (among the payrate start times and the endOfShift) on the condition that it is also not lower than startOfShift. 
                   //In the case where the shift spans over two days, ReturnEarliestBreak will return the lowest of the payrate start times that are higher than the startOfShift, if there's one, else ReturnEarliestBreak will return the lowest among the 3 start times and the endOfShift.  
                    var nextPeriodStrt = ReturnEarliestBreak(endOfShift.Day == startOfShift.Day, new List<TimeSpan>() { endOfShift.TimeOfDay, rates["Morning"].Item2.ToSeq().First(), rates["Evening"].Item2.ToSeq().First(), rates["Night"].Item2.ToSeq().First() }, startOfShift.TimeOfDay);
                     var twentyFour = new TimeSpan(24, 00, 00);
                    pay = endOfShift.Day == startOfShift.Day ? nextPeriodStrt.Subtract(startOfShift.TimeOfDay).TotalHours * rates["Default"].Item1 : nextPeriodStrt > startOfShift.TimeOfDay ? nextPeriodStrt.Subtract(startOfShift.TimeOfDay).TotalHours * rates["Default"].Item1
                                              : ((twentyFour - startOfShift.TimeOfDay) + nextPeriodStrt).TotalHours * rates["Default"].Item1;
                   
                    TimeSpan duration = new TimeSpan(0, 0, 0);
                    //If the shift starts and ends on the same day, the hours needed to add in order to bring startOfShift forward (or the duration of the current calculated hours) is simply the received nextPeriodStrt minus the startOfShift. 
                    //Else, if the nextPeriodStrt is before midnight, the same calculation is performed; else, startOfShift is pushed forward the amount of hours left to midnight plus simply the value of  nextPeriodStrt. The pay is calculated similarly. (Although, this part doesn't work properly because
                    //other parts of the program have not been modified likewise to accommodate a shift spanning over more than one day.
                    duration += endOfShift.Day == startOfShift.Day ? nextPeriodStrt - startOfShift.TimeOfDay : nextPeriodStrt > startOfShift.TimeOfDay ? nextPeriodStrt - startOfShift.TimeOfDay : (twentyFour - startOfShift.TimeOfDay) + endOfShift.TimeOfDay;
                    payments.Add(new KeyValuePair<string, (double, TimeSpan)>("Default", (Math.Round(pay), duration)));
                    startOfShift += duration;
 

                }

                pay = 0;//pay is reset for the next iteration.

            } while (endOfShift.Subtract(startOfShift).TotalHours > 0);


            return payments;
        }

        //To get the start of the next payrate period when a 'Default' shift is started, ReturnEarliestBreak is called and is passed a Boolean to indicate whether the shift starts in the same day or not. The second parameter is a list containing, the endOfShift and the start times for all payrate periods.  
        //As a third parameter, the startOfShift is passed. The purpose of this function is explained above. Here i describe what the specific linq functions do. SelectMany and the Where (filter) inside it, together form a linq-style equivalent of a nested foreach loop.
        //This is in order to check every element against every other element to determine the lowest. First(), takes the first element of the sequence whereas Skip(n), skips (leaves out) the first n elements in the sequence. Count() counts how many elements are in the sequence.

        public static TimeSpan ReturnEarliestBreak(bool ShftEndIsSameDay, List<TimeSpan> ls, TimeSpan shiftStart) => ShftEndIsSameDay ? ls.SelectMany(x => ls.Where(i => x.CompareTo(i) <= 0 && i.CompareTo(shiftStart) > 0)).First() :
                                                                                                         ls.Skip(1).Where(i => i.CompareTo(shiftStart) > 0).OrderBy(i => i).Count() > 0 ? ls.Skip(1).Where(i => i.CompareTo(shiftStart) > 0).OrderBy(i => i).First()
                                                                                                                    : ls.SelectMany(x => ls.Where(i => x.CompareTo(i) <= 0)).First();


    }
}








