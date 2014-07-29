//Michael Parker
//Daily programmer 7/21/14
//Text to PBM
//Visual Studio 2013
//http://www.reddit.com/r/dailyprogrammer/comments/2ba3g3/7212014_challenge_172_easy/

#include <iostream>
#include <fstream>
#include <string>
#include <vector>

using namespace std;

string strToUpper(string);
string strToPBM(string);

vector<vector<string>> letters;				//Stores the 'bitmap' for each letter A-Z

int main()
{
	fstream fin;
	fin.open("font.txt");

	//Load letter 'bitmaps' from file
	while (!fin.eof())
	{
		vector<string> letter;
		for (int i = 0; i < 8; i++)			//Every letter is comprised of 8 lines
		{
			string line;
			getline(fin, line);
			if (i > 0)						//First line is the letter; ignore it
			{
				letter.emplace_back(line);	//Rest of the lines are the bitmap, add them to the vector
			}
		}
		letters.emplace_back(letter);		//Add bitmap to the vector of bitmaps
	}

	vector<string> space;					//Add bitmap for a blank space at the end of the vector
	for (int i = 0; i < 7; i++)
	{
		space.emplace_back("00000");
	}
	letters.emplace_back(space);


	//Get input
	string in;

	cout << "Enter your word(s):";
	getline(cin, in);

	cout << strToUpper(in) << endl;
	in = strToUpper(in);					//Convert the input to all uppercase before converting it into a bitmap
	string output = strToPBM(in);			//output now has the final bitmap

	cout << output;

	//Write output to file
	ofstream fout;
	fout.open(in + ".PBM");
	fout << (output);
	fout.close();

	cin.get();
	return 0;
}

//Makes all characters in a given string uppercase
string strToUpper(string s)
{
	char c;

	for (int i = 0; i < s.size(); i++)
	{
		c = toupper(s[i]);
		s[i] = c;
	}

	return s;
}

//Converts the given string to a string containing the bitmap image.
string strToPBM(string s)
{
	//Set up the seven lines that will make up the bitmap
	vector<string> output;
	for (int i = 0; i < 7; i++)
	{
		output.emplace_back("");
	}

	vector<string> currentLetter;
	//Go through the whole string: for every letter...
	for (int i = 0; i < s.size(); i++)
	{
		if (s[i] == ' ')
			currentLetter = letters[letters.size() - 1];		//If the character is a space, then set currentLetter to the last element in the letters vector(blank space)
		else
			currentLetter = letters[s[i] - 65];					//If not, subtract 65 from the character to get the appropriate index for the letter
																//EX: ASCII value for 'A' is 65, so 65 - 65 = 0, which is the index the bitmap for 'A' is stored in

		//For each line in the output, append the corresponding line from the letter bitmap
		for (int j = 0; j < letters[0].size(); j++)				
		{
			if (i == 0)
				output[j].append(currentLetter[j]);				//If it's the first letter, do not prepend a small space before the next letter
			else
				output[j].append("00" + currentLetter[j]);		//Otherwise prepend a space before the next letter
		}
	}

	s = "P1\n";
	s.append(to_string(output[0].length()) + "\n");
	s.append(to_string(output.size()) + "\n");
	//Create final bitmap string from vector
	for (int i = 0; i < output.size(); i++)
	{
		s.append(output[i] + "\n");
	}

	return s;
}