using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Text;

namespace day5
{
    class Solution
    {
        static void Main(string[] args)
        {
            var cargo = new CargoCrane();
            using (var reader = new StreamReader(@"./input.txt"))
            {
                /*
                When reading the following code, keep this in mind as the format of the file.
                    [D]    
                [N] [C]    
                [Z] [M] [P]
                    1   2   3

                move 1 from 2 to 1
                move 3 from 1 to 3
                move 2 from 2 to 1
                move 1 from 1 to 2
                The first part are columns, then column numbering, which we just use as a queue that it's time to go on to the next part.

                In that next part, we start moving items around in the cargo. See the advent of code for complete details.
                */

                var line = reader.ReadLine();
                while (line != null)
                {
                    if (line[1] == '1')
                    {
                        //One of the "unused lines" mentioned above, starts with " 1".
                        break;
                    }
                    cargo.ParseLine(line);
                    line = reader.ReadLine();
                }
                //Add null coalescing here to ensure we don't throw a null exception...
                if ((reader.ReadLine() ?? " ").Trim() != "")
                {
                    //...because instead we want to say this:
                    throw new Exception("File format error");
                }
                //Start processing the "move" statements
                line = reader.ReadLine();
                while (line != null)
                {
                    cargo.ParseMove(line);
                    line = reader.ReadLine();
                }
            }

            Console.WriteLine($"Answer to part 1: {cargo.PrintTopOfStacks()}");
        }
    }

    class CargoCrane
    {
        /*See the ParseLine for more info. But, basically:
            We need to be able to add/remove off the head and tail easily, so LinkedList is most appropriate (O(1))
            But for accessing those linked lists, we will be doing semi-random access to it, with limited number of adds/removals,
            so use List (O(1) for operations we care about)
        */
        private List<LinkedList<char>> stacks;

        public CargoCrane()
        {
            stacks = new List<LinkedList<char>>();
        }

        public void ParseLine(string line)
        {
            /*
                We need to process inputs like this, line by line:
                    [D]    
                [N] [C]    
                [Z] [M] [P]
                 1   2   3
                This function will receive one of those lines and sort the items into the corresponding stack

                In the ASCII art above, each column is a separate "stack of items"
            */
            //str_idx will count the position in the string. Since the input is pretty regular(item every 4), we'll just add by that every time in the loop below
            var str_idx = 1;
            // stack_idx will keep track of which stack an item will go into if the item is not an empty space
            var stack_idx = 0;
            while (str_idx < line.Length)
            {
                char item = line[str_idx];
                if (item != ' ')
                {
                    /*We don't know how many stacks wide the input will be before start (with single pass, anyway),
                    so just keep adding to the stack until the index is valid.

                    ...technically the above isn't true, the Main function could figure it out with the string lengths,
                    but doing it this way this seems easy enough.
                    */
                    while (stacks.Count <= stack_idx)
                    {
                        stacks.Add(new LinkedList<char>());
                    }
                    //Add the item to the end of the list.
                    stacks[stack_idx].AddLast(item);
                }
                //keep track of our indices. 4 comes from the spacing between letters in the docstring
                str_idx += 4;
                //As str_idx moves to the right, our stack_idx does also.
                stack_idx += 1;
            }
        }

        public void ParseMove(string line)
        {
            /*
            This function parses lines like:
            "move 1 from 2 to 1"
            and operates on its stacks.
            We move X items from the top of Y stack to Z stack, one at a time.
            */
            string[] items = line.Split(' ');
            /*
            Now items should be like:
            ["move", "1", "from", "2", "to", "1"]
            */
            int count, source, dest;
            try
            {
                count = int.Parse(items[1]);
                //-1 because the text is 1-indexed, but we are 0-indexed
                source = int.Parse(items[3]) - 1;
                dest = int.Parse(items[5]) - 1;
            }
            catch (FormatException)
            {
                throw new Exception("File Format Exception");
            }

            for (int idx = 0; idx < count; idx++)
            {
                stacks[dest].AddFirst(stacks[source].First());
                stacks[source].RemoveFirst();
            }
        }

        public string PrintTopOfStacks()
        {
            /*
            This function gets the answer that advent of code wants:
            What crate is on the top of each stack?
            */

            var answer = new StringBuilder("", stacks.Count());
            foreach (var stack in stacks)
            {
                answer.Append(stack.First());
            }
            return answer.ToString();
        }
    }
}