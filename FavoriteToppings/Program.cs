using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FavoriteToppings
{
    class Program
    {
        static void Main(string[] args)
        {
            // read a file from a disk
            var json = File.ReadAllText("./toppings.json");
            // deserialize from json to C#
            //property names and json in C# need to match
            var toppingSelections = JsonConvert.DeserializeObject<List<PizzaSelection>>(json);
            // built in but a little less forgiving... faster though
            //var toppingSelections = System.Text.Json.JsonSerializer.Deserialize<List<PizzaSelection>>(json);

           

            Dictionary<string, double> mosttoppings = new Dictionary<string, double>();

            foreach (var topping in toppingSelections)
            {
                topping.Toppings.ForEach(item => {
                    if (mosttoppings.ContainsKey(item))
                    {   
                        mosttoppings[item]++;
                    } else
                    {
                        mosttoppings[item] = 1;
                    }
                    
                    
                });
               
                // Console.WriteLine("------");
            }


        // This is the printout for most toppings:

          /*  foreach (var item in mosttoppings)
            {
                Console.WriteLine(item);
            }*/

            
            // Dictionary to keep the config# -- with the list values
            Dictionary<int, PizzaSelection> toppingsConfiguration = new Dictionary<int, PizzaSelection>();
            // Dictionary to keep the config# -- with count of times used...
            Dictionary<string,int> toppingsConfigCount = new Dictionary<string, int>();

            int count = 0;
            foreach (var toppings in toppingSelections)
            {
                var myPizza = new PizzaSelection();
                toppings.Toppings.ForEach(item =>
                {
                    myPizza.Toppings.Add(item); /// adding topping to Pizza

                });
               
                myPizza.Toppings.Sort(); // Sort so everything will be the same
                
                var result = String.Join(", ", myPizza.Toppings.ToArray());
                if(toppingsConfigCount.ContainsKey(result)) {
                    toppingsConfigCount[result] = toppingsConfigCount[result] + 1;
                } else
                {
                    toppingsConfigCount.Add(result, 1);
                }
                
                toppingsConfiguration.Add(count, myPizza);
                count++; // increment counter

            }

            var sortedDict = from entry in toppingsConfigCount orderby entry.Value descending select entry;

            /*
             * This is the printout for the top 25 combinations:
             */

            foreach (var top in sortedDict.Take(25))
            {
                Console.WriteLine(top);
            }

        }
    }
}
