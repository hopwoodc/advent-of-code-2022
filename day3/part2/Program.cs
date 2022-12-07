using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace day3
{
    class Solution
    {
        static void Main(string[] args)
        {
            //some unit tests for the GetPriority function
            Debug.Assert(GetPriority('a')==1);
            Debug.Assert(GetPriority('z')==26);
            Debug.Assert(GetPriority('A')==27);
            Debug.Assert(GetPriority('Z')==52);



            try
            {
                int sum = 0;
                
                var elfGroupIndex = 0; //Elves are in groups of three, so will count mod 3
                //This will hold the intersection of the elves so far.
                HashSet<char>? elfGroupIntersection = null;
                foreach (string line in File.ReadLines(@"./input.txt")){
                    var trimmed = line.Trim();
                    int len = trimmed.Length;
                    var items = new HashSet<char>();
                    for (int idx = 0; idx < len; idx++){
                        items.Add(trimmed[idx]);
                    }
                    switch (elfGroupIndex){
                        case 3: //fall through to implement modulo.
                        case 0:
                            elfGroupIntersection = items;
                            elfGroupIndex = 0;
                            break;
                        case 1:
                            Debug.Assert(elfGroupIntersection != null);
                            elfGroupIntersection = elfGroupIntersection.Intersect(items).ToHashSet();
                            break;
                        case 2:
                            Debug.Assert(elfGroupIntersection != null);
                            elfGroupIntersection = elfGroupIntersection.Intersect(items).ToHashSet();
                            Debug.Assert(elfGroupIntersection.Count()==1);
                            sum += GetPriority(elfGroupIntersection.ToList()[0]);
                            break;
                    }
                    elfGroupIndex++;

                }
                Console.WriteLine($"Answer for part 2 is: {sum}");
                
            } catch (Exception e){
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        static int GetPriority(char item){
            /*
            */

            if (item >= 128){
                throw new Exception("item must be ASCII");
            }

            int priority = item;

            //a-z are 1-26, A-Z are 27-52
            if (priority >= 97){
                priority -= 96;
            } else{
                priority -=65;
                priority +=27;
            }
            return priority;
        }

    }
}