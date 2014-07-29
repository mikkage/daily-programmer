// Michael Parker
// Daily programmer 7/28/14
// Unit Converter
// http://www.reddit.com/r/dailyprogrammer/comments/2bxntq/7282014_challenge_173_easy_unit_calculator/

// Visual Studio 2013

#pragma once
#include <iostream>
#include "UnitConverter.h"

// Conversion values

// Length conversion values
// To avoid making an 8x8 grid of conversion values for length, there are two conversion tables: anyhting to km, and km to anything.
// To convert length values, it will first convert the value to km, then that from km to the requested unit.
// The arrays store the conversion values in the following order: [mm, cm, m, km, in, yd, mi, atp]
const extern double lengthToKilometers[8] = { 0.000001, 0.00001, 0.001, 1, 0.0000254, 0.0009144, 1.60934, 0.0000308567758 };
const extern double kilometersToOtherLengths[8] = { 1000000, 100000, 1000, 1, 39370.1, 1093.61, 0.621371, 32407.7929 };

// Mass conversion values
// A 4 x 4 grid that holds the conversion values in the following format:
//	   g  kg  lb  oz
//  g
//  kg
//  lb
//  oz
// The units converted from are selected in by row, and the units converted to are selected by column.
const extern double massConversionFactors[4][4] = { { 1, 0.001, 0.00220462, 0.035274 },
												  { 1000, 1, 2.20462, 35.274 },
												  { 453.592, 0.453592, 1, 16 },
												  { 28.3495, 0.0283495, 0.0625, 1 } };

// Gets the index of the given unit to get the correct conversion factor from lengthToKilometers[] or kilometersToOtherLengths[]
int UnitConverter::getLengthIndex(string unit)
{
	if (unit == "mm")
		return 0;
	if (unit == "cm")
		return 1;
	if (unit == "m")
		return 2;
	if (unit == "km")
		return 3;
	if (unit == "in")
		return 4;
	if (unit == "yd")
		return 5;
	if (unit == "mi")
		return 6;
	if (unit == "atp")
		return 7;
}

// Gets the index of the given unit to get the correct conversion factor from massConversionFactors[]
int UnitConverter::getMassIndex(string unit)
{
	if (unit == "g")
		return 0;
	if (unit == "kg")
		return 1;
	if (unit == "lb")
		return 2;
	if (unit == "oz")
		return 3;
}

// Gets the type of the unit passed in: length, mass, or unknown
char UnitConverter::getUnitType(string unit)
{
	if (unit == "mm" || unit == "cm" || unit == "m" || unit == "km" || unit == "in" || unit == "yd" || unit == "mi" || unit == "atp")
		return 'l';
	if (unit == "g" || unit == "kg" || unit == "lb" || unit == "oz")
		return 'm';
	return '?';			//Bad units input by user
}

// Checks whether the two types of units can be converted between
bool UnitConverter::checkUnits(string unit1, string unit2)
{
	//The two units must have the same type(length or mass) and must be known units(not '?').
	return ((getUnitType(unit1) == getUnitType(unit2)) && getUnitType(unit1) != '?');
}

// Does the unit conversion
double UnitConverter::convertUnit(string fromUnit, string toUnit, double value)
{
	if (checkUnits(fromUnit, toUnit))		// First be sure that the two unit types can be converted between.
	{
		if (getUnitType(fromUnit) == 'l')	// If the units are both length...
		{
			// Convert to km first
			double length = value * lengthToKilometers[getLengthIndex(fromUnit)];

			// Then use that value to convert from km to the requested unit.
			length = length * kilometersToOtherLengths[getLengthIndex(toUnit)];

			return length;
		}
		else
		{
			int fromIndex = getMassIndex(fromUnit);		// Get the index for the mass conversion factor.
			int toIndex = getMassIndex(toUnit);

			double conversionFactor = massConversionFactors[fromIndex][toIndex];	// Get conversion factor from 2d array

			double mass = value * conversionFactor;									// Convert the units.
			return mass;
		}
	}
	else
	{
		// If the values cannot be converted, let the user know and return -1 since mass and length should not be negative anyway.
		std::cout << "Cannot convert " << fromUnit << " to " << toUnit << endl;
		return -1;
	}
}