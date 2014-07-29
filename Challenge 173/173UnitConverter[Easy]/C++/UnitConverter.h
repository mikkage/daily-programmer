// Michael Parker
// Daily programmer 7/28/14
// Unit Converter
// http://www.reddit.com/r/dailyprogrammer/comments/2bxntq/7282014_challenge_173_easy_unit_calculator/

// Visual Studio 2013

#ifndef GUARD_UNIT_CONV
#define GUARD_UNIT_CONV
#include <string>
using namespace std;

// UnitConverter class
// Can convert lengths and masses.
// Supported length units: mm, cm, m, km, in, yd, mi, and atp
// Supported mass units: g, kg, oz, and lb
class UnitConverter
{
private:
	// Helper methods
	int getLengthIndex(string unit);
	int getMassIndex(string unit);
	char getUnitType(string unit);
	bool checkUnits(string unit1, string unit2);

public:
	// Converts from one unit to another
	double convertUnit(string fromUnit, string toUnit, double value);

};
#endif