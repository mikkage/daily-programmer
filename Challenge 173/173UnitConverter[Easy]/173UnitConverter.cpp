// Michael Parker
// Daily programmer 7/28/14
// Unit Converter
// http://www.reddit.com/r/dailyprogrammer/comments/2bxntq/7282014_challenge_173_easy_unit_calculator/

// Visual Studio 2013

#pragma once
#include <iostream>
#include <vector>
#include <string>
#include <sstream>
#include "UnitConverter.h"

using namespace std;

// String splitting functions - Splits a string into a vector of strings by a delimiter
// From http://stackoverflow.com/questions/236129/how-to-split-a-string-in-c
std::vector<std::string> &split(const std::string &s, char delim, std::vector<std::string> &elems) {
	std::stringstream ss(s);
	std::string item;
	while (std::getline(ss, item, delim)) {
		elems.push_back(item);
	}
	return elems;
}
vector<string> split(const string &s, char delim) {
	vector<string> elems;
	split(s, delim, elems);
	return elems;
}


int main()
{
	cout << "To convert between units, type \"<value> <units> to <units>\"" << endl;
	cout << "For example, \"1 cm to mm\"" << endl;
	cout << "For lengths, the supported units are: mm, cm, m, km, in, yd, mi, and atp" << endl;
	cout << "For masses, the supported units are: g, kg, oz, and lb" << endl;
	cout << "To exit, enter nothing" << endl;

	UnitConverter unitConv;
	string input = "a";
	while (true)
	{
		cout << "Enter units to convert:";
		getline(cin, input);		// Get input from user: input format is: "<value> <unit1> to <unit2>"
		if (input.empty())
			return 0;

		vector<string> splitInput = split(input, ' ');		// Split the user input to get the data needed
		if (splitInput.size() == 4)							// Make sure the correct number of items need has been entered.
		{
			double value = std::stod(splitInput[0]);
			string unitFrom = splitInput[1];
			string unitTo = splitInput[3];

			cout << unitConv.convertUnit(unitFrom, unitTo, value) << " " << unitTo << endl;
		}
		else
			cout << "Invalid input. Try again." << endl;
	}

	cin.get();
	return 0;
}