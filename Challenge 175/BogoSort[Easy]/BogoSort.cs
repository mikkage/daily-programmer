//Michael Parker
//8/11/2014
//String Bogo Sort
//http://www.reddit.com/r/dailyprogrammer/comments/2d8yk5/8112014_challenge_175_easy_bogo/
//http://en.wikipedia.org/wiki/Bogo-sort

//Visual Studio 2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BogoSort
{
    class BogoSortString
    {
        static void Main(string[] args)
        {
            int sum = 0;
            for (int i = 0; i < 100; i++)
            {
                sum += BogoSort("elephatn", "elephant");
            }
            Console.WriteLine("Average iterations to bogo sort: " + sum / 100);
            Console.ReadLine();
        }

        //BogoSort
        //"Sorts" a string by trying random combinations of its characters until it works
        //Args: unsorted - the unsorted string
        //      sorted - what the unsorted string should be when sorted
        //Returns the number of attempts it took to sort the string
        public static int BogoSort(string unsorted, string sorted)
        {
            //Make sure the sorted and unsorted strings are all lowercase
            unsorted = unsorted.ToLower();
            sorted = sorted.ToLower();

            int iterations = 0;             //Total number of iterations taken to sort the string

            Random rnd = new Random();

            List<char> unsortedList;

            bool isSorted = false;
            while(!isSorted)
            {
                unsortedList = unsorted.ToList<char>(); //unsortedList now Contains the characters from the unsorted string

                string randomized = "";     //The randomized string will be built here

                //Randomly allocate all of the characters in the list into the randomized string
                for (int i = 0; i < unsorted.Length; i++)
                {
                    int rand = rnd.Next(0, unsorted.Length - i);    //Get randomized index 
                    randomized += unsortedList[rand];               //Add the character at that index into the randomized string
                    unsortedList.RemoveAt(rand);                    //Remove the used character from the list
                }
                if (sorted == randomized)
                    isSorted = true;        //If the string has been 'sorted', then we are done and can exit the loop
                iterations++;
            }
            return iterations;
        }
    }
}
