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
                foreach (string line in File.ReadLines(@"./input.txt")){
                    var trimmed = line.Trim();
                    int len = trimmed.Length;
                    var compA = new HashSet<char>();
                    var compB = new HashSet<char>();
                    for (int idx = 0; idx < len; idx++){
                        if (idx < len/2){
                            compA.Add(trimmed[idx]);
                        } else{
                            compB.Add(trimmed[idx]);
                        }
                    }
                    var intersection = compA.Intersect(compB).ToArray();
                    Debug.Assert(intersection.Length==1);
                    sum += GetPriority(intersection[0]);
                }
                Console.WriteLine($"Answer for part 1 is: {sum}");
                
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