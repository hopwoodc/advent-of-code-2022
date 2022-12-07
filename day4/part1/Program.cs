using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace day4
{
    class Solution
    {
        static void Main(string[] args)
        {
            try
            {
                /* For this problem, we are asked if one of the ranges contains the other.
                They are given to us like this:
                1-2,6-8
                3-4,4-4
                Each range is inclusive of their ends.
                For each line, we must check if the first contains the second or the second contains the first.
                We must return the total count of ranges where the above statement is true.
                */
                int count = 0;
                foreach (string line in File.ReadLines(@"./input.txt"))
                {
                    var trimmed = line.Trim();
                    var ranges = trimmed.Split(',');
                    var range0 = new Range(ranges[0]);
                    var range1 = new Range(ranges[1]);

                    if (range0.Contains(range1) || range1.Contains(range0))
                    {
                        count++;
                    }
                }
                Console.WriteLine($"Answer for part 1: {count}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }

    class Range
    {
        /*
        Represents a range of numbers. Range is inclusive of both start and stop.
        */
        public int start;
        public int stop;

        public Range(int start, int stop)
        {
            ConstructorHelper(start, stop);
        }

        public Range(string range)
        {
            /*
            Constructor that can process ranges given like "1-2"
            */
            var items = range.Split('-');
            int start;
            int stop;
            if (!int.TryParse(items[0], out start))
            {
                throw new Exception("Failed to parse line");
            }
            if (!int.TryParse(items[1], out stop))
            {
                throw new Exception("Failed to parse line");
            }
            ConstructorHelper(start, stop);
        }

        private void ConstructorHelper(int start, int stop)
        {
            /*
            Move common error checking code for different constructors here.
            */
            Debug.Assert(stop >= start, "Invalid Range");
            Debug.Assert(start >= 0, "Ranges cannot be negative");
            this.start = start;
            this.stop = stop;
        }

        public bool Contains(Range other)
        {
            /*
            Check if this range contains the other completely
            */
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            return start <= other.start && stop >= other.stop;
        }
    }
}