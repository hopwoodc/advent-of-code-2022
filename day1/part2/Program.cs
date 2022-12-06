using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;

namespace day1
{
    class Solution
    {
        static void Main(string[] args)
        {
            var queue = new SortedQueue<int>();
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
                        queue.AddNode(currentCalories);
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

                Console.WriteLine($"Part 1 answer is {mostCalories}");
                int part2answer = 0;
                for (int i=0; i < 3; i++){
                    part2answer += queue.Pop();
                }
                Console.WriteLine($"Part 2 answer is {part2answer}");
            } catch (Exception e){
                Console.WriteLine($"Error: {e.Message}");
            }
            
        }
    }


    /* We need to be able to track the top three elves with the most calories now.
    This could be solved with a SortedQueue...
    This solution will be a *little* over the top, but it will be good practice.
    Let's implement the queue using a linked list.
    
    Let's make it a template class so we can compare other things later.
    Technically I could avoid being templated by using IComparable as value's type,
    but I don't want to mix types in this queue.
    */
    class SortedQueue<T> where T : IComparable
    {
        //oh hey we're allowed to make nested classes.
        /* 
        Create a SortedNode class to hold the actual data
        This helps us avoid needing to create separate Head or Tail
        subclases, and avoid having a "valid" flag...
        */
        private class SortedNode
        {
            public T value;
            public SortedNode? nextNode = null;

            public SortedNode(T value){
                this.value = value;
            }
        }

        //In C# variables can be null, which is distinct from 0, unlike how it is in C.
        //Variables have to be marked nullable with a question mark at the end of the type name.
        private SortedNode? head = null;
        
        public void AddNode (T other){
            var newNode = new SortedNode(other);
            //If the queue is empty, simply set head to new node.
            if (head == null){
                head = newNode;
                return;
            }

            //next base case, if new node needs to be before current head.
            if (head.value.CompareTo(other)<0){
                newNode.nextNode = head;
                head = newNode;
                return;
            }
            
            // Now we know there are at least two nodes.
            // So we also now know that we can look ahead.

            // I might recurse, but I already did that in my TrieTree practice.
            // So, instead let's make it iterative.
            // Use index to point to current position in linked list.
            // We need to find the index that is one before the new node
            var index = head;
            while (index.nextNode != null){
                if (index.nextNode.value.CompareTo(other)<0){
                    break;
                }
                index = index.nextNode;
            }
            //the case where we went to the end of the list.
            //nothing special to do, just add our node.
            if (index.nextNode == null){
                index.nextNode = newNode;
                return;
            }
            //the case where we're in the middle of the list.
            //insert ourselves.
            newNode.nextNode = index.nextNode;
            index.nextNode = newNode;
        }

        public void printList(){
            var index = head;
            int iter = 0;
            while (index != null){
                Console.WriteLine(index.value);
                index = index.nextNode;
                iter++;
                if (iter > 10){
                    break;
                }
            }
        }

        public T Pop(){
            if (head != null){
                var retVal = head.value;
                head = head.nextNode;
                return retVal;
            }
            throw new IndexOutOfRangeException();
        }
    }
}

