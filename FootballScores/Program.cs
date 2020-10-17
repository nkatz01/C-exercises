using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace FootballScores
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(run("manutd"));
        }
        public static int run(String teamKey)
        {
             
            Task<string> result = GetResponseString();
             var jsonResult = result.Result;

            dynamic dynamicResultObject = JsonConvert.DeserializeObject(jsonResult);
           

            var rounds = dynamicResultObject.rounds;
            int goals = 0;
            int i = 0;
            foreach (var matchday in rounds)//object that includes matches array
              {
                
                var matchesObj = (matchday as JObject).Properties().Where(prop => prop.Name.Equals("matches"));
             
                foreach (var mathcArr in matchesObj)
                {
                     
                  
                    foreach (var entry in mathcArr.Value)
                    {
                        //Console.WriteLine((entry.ElementAt(1) as JProperty).Name);
                        var teamOneAndTwo = entry.SelectTokens("$.['team1', 'team2']").Select(i => (i.First() as JProperty).Value.ToString());

                        goals += teamOneAndTwo.ElementAt(0).Equals("manutd") ? Int32.Parse( entry.SelectToken("score1").ToString()) : teamOneAndTwo.ElementAt(1).Equals("manutd") ? Int32.Parse(entry.SelectToken("score2").ToString())  : 0;
                        
                       

                    }
                 

                }
                
             }
            
           
            return goals;
        }

    

        static public async Task<string> GetResponseString()
        {
            var httpClient = new HttpClient();
            
            
            var response = await httpClient.GetAsync("https://raw.githubusercontent.com/openfootball/football.json/198f60ce50ac427dadc16a35ef4ad65edcea3125/2014-15/en.1.json");
            var contents = await response.Content.ReadAsStringAsync();

            return contents;
        }
    }
}
