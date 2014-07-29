//Michael Parker
//Daily programmer 7/14/14
//Hex to 8 x 8 Bitmap
//http://www.reddit.com/r/dailyprogrammer/comments/2ao99p/7142014_challenge_171_easy_hex_to_8x8_bitmap/

//Visual Studio 2013

#include <iostream>
#include <string>

using namespace std;

string hexCharToBin(char);

int main()
{
	string input = "a";
	while (!input.empty())
	{
		getline(cin, input);
		for (int i = 0; i < input.size(); i++)
		{
			if (input[i] != ' ')					
			{
				cout << hexCharToBin(input[i]);		//If the current character is not a space, print the bitmap pattern.
			}
			else cout << endl;						//If the current character is a space, start a new line
		}
	}
	cin.get();
	return 0;
}

string hexCharToBin(char c)
//Returns the bitmap pattern for the character passed in(hex digit).
{
	switch (c)
	{
		case '0': return "    ";
		case '1': return "   X";
		case '2': return "  X ";
		case '3': return "  XX";
		case '4': return " X  ";
		case '5': return " X X";
		case '6': return " XX ";
		case '7': return " XXX";
		case '8': return "X   ";
		case '9': return "X  X";
		case 'A': return "X X ";
		case 'B': return "X XX";
		case 'C': return "XX  ";
		case 'D': return "XX X";
		case 'E': return "XXX ";
		case 'F': return "XXXX";
		default: return "";
	}
}