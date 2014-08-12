//Michael Parker
//8/4/2014
//Thue-Morse Sequence
//http://www.reddit.com/r/dailyprogrammer/comments/2cld8m/8042014_challenge_174_easy_thuemorse_sequences/

//More info on the Thue-Morse sequence can be found here http://en.wikipedia.org/wiki/Thue%E2%80%93Morse_sequence

//Visual Studio 2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThueMorseSequence
{
    class ThueMorse
    {
        static void Main(string[] args)
        {
            ThueMorseSeq tms = new ThueMorseSeq();
            ThueMorseSeq tms1 = new ThueMorseSeq();

            for (int i = 17; i > 0; i--)
                Console.WriteLine("Sequence " + i + ": " + tms.getThueMorse(i));

            Console.ReadLine();
        }
    }

    class ThueMorseSeq
    {
        private List<string> sequences;  //List that stores the sequences so that previously calculated sequences can be accessed instantly

        public ThueMorseSeq()
        {
            sequences = new List<string>();
            sequences.Add("0");  //Sequence starts with 0
        }

        //Flips the 'bits' in the string - "01101" returns "10010"
        private string flipBits(string str)
        {
            string flippedStr = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '0')
                    flippedStr += "1";
                else flippedStr += "0";

            }
            return flippedStr;
        }

        //Returns the nth Thue-Morse sequence
        //Trying to find anything above ~17 ends up taking too long
        public string getThueMorse(int n)
        {
            if (n > 0)
            {
                // If the nth sequence has already been calculated, no need to do it again, just return the stored sequence
                if (n <= sequences.Count + 1)
                {
                    return sequences[n - 1];
                }
                //Otherwise, we have to find the nth sequence
                else
                {
                    while (sequences.Count <= n)
                    {
                        string s = sequences[sequences.Count - 1];    //Get previous sequence
                        s += flipBits(s);                           //Add the reverse of the previous sequence to get the new sequence
                        sequences.Add(s);                            //Add the new sequence to the end of the sequences list
                    }
                    return sequences[n - 1];                         //Return nth sequence
                }
            }
            else return "Enter a number 1 or greater.";
        }
    }
}
