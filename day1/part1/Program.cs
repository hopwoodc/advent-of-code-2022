using System;
using System.Collections.Generic;
using System.IO;

namespace day1
{
    class Solution
    {
        static void Main(string[] args)
        {
            try
            {
                //For each elf, track how many calories they hold so far
                int currentCalories = 0;
                //Track the elf with the most calories here
                int mostCalories = 0;
                foreach (string line in File.ReadLines(@"./input.txt")){
                    var trimmed = line.Trim();
                    //Elves are separated by empty lines
                    if (trimmed.Length == 0){
                        if (mostCalories < currentCalories){
                            mostCalories = currentCalories;
                        }
                        currentCalories = 0;
                        continue;
                    }
                    //Add the elve's "snacks" to the total calories
                    int parsed;
                    if (int.TryParse(trimmed, out parsed)){
                        currentCalories += parsed;
                    }else{
                        Console.WriteLine($"Error parsing '{line}'");
                    }
                }
                
                //Check in case the list of calories didn't include a new line at the end.
                if (currentCalories>mostCalories){
                    mostCalories = currentCalories;
                }

                Console.WriteLine($"The answer is {mostCalories}");
            } catch (Exception e){
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}