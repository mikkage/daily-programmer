//Michael Parker
//Daily programmer 6/14/14
//Planetary Gravity Calculator
//http://www.reddit.com/r/dailyprogrammer/comments/284mep/6142014_challenge_166b_easy_planetary_gravity/
//This program takes the mass of an object, the number of planets, and the information about those planets(name, radius, and density), then calculates the
//the weight(in netwons) of the object as if it were on the surface of each planet entered.

//Visual Studio 2013

#include <iostream>
#include <vector>
#include <string>
#include <sstream>
#include <fstream>

using namespace std;

//String splitting functions - Splits a string into a vector of strings by a delimiter
//From http://stackoverflow.com/questions/236129/how-to-split-a-string-in-c
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

class planet
{
private:
	string name;
	double mass;
	double volume;
	double density;
	double radius;
	const double G = 6.67e-11;

public:
	//Constructor - Creates a planet object and calculates volume and mass
	planet(double r, double d, string n)
	{
		name = n;
		density = d;
		radius = r;

		volume = (4.0 / 3.0) * 3.14159 * (radius * radius * radius);
		mass = volume * density;
	}
	//Returns the force of gravity, given the mass of the second object
	double getGravityForce(double otherMass)
	{
		return (G * ((mass * otherMass) / (radius * radius)));
	}
	//Returns planet's name
	string getName()
	{
		return name;
	}
};


int main()
{
	vector<planet> planets;

	string planetInput;
	int mass;
	int numPlanets;

	cout << "Enter mass of object:";
	cin >> mass;

	cout << "Enter the number of planets:";
	cin >> numPlanets;

	//Get planet information
	for (int i = 0; i < numPlanets; i++)
	{
		getline(cin, planetInput);
		if (planetInput.empty())	//If input is empty, do not use it and try again
		{
			i--;
		}
		else
		{
			vector<string> v = split(planetInput, ',');	//Split the input on every comma
			planet p(stod(v[1]), stod(v[2]), v[0]);		//Create new planet object with provided information
			planets.push_back(p);						//Add planet to the list of planets
		}
	}
	
	//Display the names and force for every planet entered
	for (int i = 0; i < planets.size(); i++)
	{
		cout << planets[i].getName() << ": ";
		cout << planets[i].getGravityForce(mass) << endl;
	}

	cin.get();
	return 0;
}
