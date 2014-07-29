//Michael Parker
//Daily programmer 6/30/14
//2D Array Rotation
//http://www.reddit.com/r/dailyprogrammer/comments/29i9jw/6302014_challenge_169_easy_90_degree_2d_array/

//Visual Studio 2013

#include <iostream>
#include <vector>

using namespace std;

//Setup and utility functions
void initializeVector(vector<vector<int>>& v);
void print2dVector(vector<vector<int>>& v);

//2D array rotation functions
void rotate2dVector90(vector<vector<int>>& v);
void rotate2dVector270(vector<vector<int>>& v);

//Helper functions to rotate the 2D arrays
void transpose2dVector(vector<vector<int>>& v);
void reverse2dVectorColumns(vector<vector<int>>& v);
void reverse2dVectorRows(vector<vector<int>>& v);

int main()
{
	vector<vector<int>> matrix;

	initializeVector(matrix);
	cout << "Original" << endl;
	print2dVector(matrix);

	cout << "Transposed" << endl;
	transpose2dVector(matrix);
	print2dVector(matrix);

	initializeVector(matrix);
	cout << "Rotated 90" << endl;
	rotate2dVector90(matrix);
	print2dVector(matrix);

	initializeVector(matrix);
	cout << "Rotated 270" << endl;
	rotate2dVector270(matrix);
	print2dVector(matrix);


	cin.get();
	return 0;
}

void initializeVector(vector<vector<int>>& v)
//*******************************************
//Initializes the 2d vector to the following:
// 1 2 3
// 4 5 6
// 7 8 9
//*******************************************
{
	v.clear();
	vector<int> row;
	for (int i = 1; i < 10; i++)
	{
		row.push_back(i);
		i++;
		row.push_back(i);
		i++;
		row.push_back(i);

		v.push_back(row);
		row.clear();
	}
}

void print2dVector(vector<vector<int>>& v)
//Prints the matrix to console
{
	for (vector<vector<int>>::iterator it = v.begin(); it != v.end(); it++)
	{
		for (vector<int>::iterator it1 = it->begin(); it1 != it->end(); it1++)
		{
			cout << *it1 << " ";
		}
		cout << endl;
	}
	cout << endl;
}

void transpose2dVector(vector<vector<int>>& v)
//Transposes the matrix - used as a step in rotating the matrix
{
	vector<int> row;
	vector<int> tempRow;
	vector<vector<int>> matrix;

	for (int i = 0; i < v.size(); i++)
	{
		row = v[i];
		for (int j = 0; j < row.size(); j++)
		{
			tempRow.push_back(v[j][i]);
		}
		matrix.push_back(tempRow);
		tempRow.clear();
	}
	v = matrix;
}

void reverse2dVectorRows(vector<vector<int>>& v)
//Reverses the contents of each row of the 2d vector
{
	vector<int> row;
	vector<vector<int>> matrix;
	for (int i = 0; i < v.size(); i++)
	{
		row = v[i];
		reverse(row.begin(), row.end());
		matrix.push_back(row);
	}
	v = matrix;
}

void reverse2dVectorColumns(vector<vector<int>>& v)
//Reverses the contents of each of the 2d vector's rows
{
	std::reverse(v.begin(), v.end());
}

void rotate2dVector90(vector<vector<int>>& v)
//Rotates all of the values in the 2d vector clockwise by 90 degrees
//by transposing the vector, then reversing the values of the rows
{
	transpose2dVector(v);
	reverse2dVectorRows(v);
}

void rotate2dVector270(vector<vector<int>>& v)
//Rotates all of the values in the 2d vector clockwise by 270 degrees
//by transposing the vector, then reversing the values of the columns
{
	transpose2dVector(v);
	reverse2dVectorColumns(v);
}