//Michael Parker
//6/18/2014
//Final Grades
//http://www.reddit.com/r/dailyprogrammer/comments/28gq9b/6182014_challenge_167_intermediate_final_grades/

//Visual Studio 2013
//Microsoft office must be installed to generate the spreadsheet

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace FinalGrades
{
    class Student
    {
        private string fName, lName;    //First and last names of the student
        private int[] grades;           //Array of exam grades for the student, out of 100
        private double avgGrade;        //Average of exam scores
        private string letterGrade;     //Letter grade assigned from the average of exam scores

        public Student(string fName, string lName, int g1, int g2, int g3, int g4, int g5)
        {
            this.fName = fName;
            this.lName = lName;
            grades = new int[5];
            this.grades[0] = g1;
            this.grades[1] = g2;
            this.grades[2] = g3;
            this.grades[3] = g4;
            this.grades[4] = g5;

            Array.Sort(this.grades);    //Exam scores must be printed from lowest to highest

            avgGrade = this.grades.Sum() / 5.0;     //Get average exam score(out of 100)

            avgGrade = Math.Round(avgGrade, 0);     //Round the average score to the nearest whole number

            //Assign letter grades
            if (avgGrade >= 90)
                letterGrade = "A";
            else if (avgGrade < 90 && avgGrade >= 80)
                letterGrade = "B";
            else if (avgGrade < 80 && avgGrade >= 70)
                letterGrade = "C";
            else if (avgGrade < 70 && avgGrade >= 60)
                letterGrade = "D";
            else letterGrade = "F";

            //Assign + or - to the letter grades, if the grade is higher than F
            if (letterGrade != "F")
            {
                double firstDigit = avgGrade % 10;          //Gets the digits from the ones of the average grade.
                if (firstDigit >= 0 && firstDigit <= 3)     //X0 - X3 is in the - category
                    letterGrade += "-";
                if (firstDigit >= 7 && firstDigit <= 10)    //X7 - X0 is in the + category
                    letterGrade += "+";
            }
        }

        //Returns a string with all of the student's data in the following format:
        //Last Name, First Name (Average Score) (Letter Grade) [list of all exam scores]
        public string toString()
        {
            string grade = "";
            for (int i = 0; i < 5; i++)
                grade += grades[i].ToString() + " ";
            return lName + ", " + fName + " (" + avgGrade + ") (" + letterGrade +") "+ grade;
        }

        public double getAvg()
        {
            return avgGrade;
        }

        public string getfName()
        {
            return fName;
        }
        public string getlName()
        {
            return lName;
        }
        public string getLetterGrade()
        {
            return letterGrade;
        }
        public int[] getGrades()
        {
            return grades;
        }
    }

    class FinalGrades
    {
        static void Main(string[] args)
        {
            List<Student> studentList = new List<Student>();
            string input = "";

            //Main input loop
            do
            {
                input = Console.ReadLine();
                if (input != "")
                {
                    string[] studentInfo = input.Split(' ');        //Split the line into separate strings on each blank space
                    List<string> studentInfoList = studentInfo.Where(str => !string.IsNullOrWhiteSpace(str)).ToList();      //Remove all strings that are only blank space, and save in a new list

                    int index = 0;

                    //Parse first name(can be multiple separate words such as Billy Bob
                    string fName = "";
                    while(studentInfoList[index] != ",")    //First name and last name are separated by a comma
                    {
                        fName += studentInfoList[index] + " ";
                        index++;
                    }
                    fName = fName.Trim();
                    index++;

                    //Parse last name(can also be separate words)
                    string lName = "";
                    bool lNameDone = false;
                    while (!lNameDone)
                    {
                        //Try to parse the string as an int
                        try
                        {
                            //If it is an int, then we have reached the end of the last name
                            Convert.ToInt32(studentInfoList[index]);
                            lNameDone = true;
                        }
                        catch (FormatException e)
                        {
                            //If it is not an int, it is part of the last name, so append it to the last name string and go to the next string in the list
                            lName += studentInfoList[index] + " ";
                            index++;
                        }
                    }

                    //Last 5 strings after the last name are the five test scores
                    int grade1 = Convert.ToInt32(studentInfoList[index].Trim());
                    int grade2 = Convert.ToInt32(studentInfoList[index + 1].Trim());
                    int grade3 = Convert.ToInt32(studentInfoList[index + 2].Trim());
                    int grade4 = Convert.ToInt32(studentInfoList[index + 3].Trim());
                    int grade5 = Convert.ToInt32(studentInfoList[index + 4].Trim());

                    //Create the student object and add it to the list of students.
                    Student s = new Student(fName, lName, grade1, grade2, grade3, grade4, grade5);
                    studentList.Add(s);
                }
            } while (input != "");

            //Reorder the list of students by average test score(highest first)
            studentList = studentList.OrderByDescending(x => x.getAvg()).ToList();

            //Print student information for the entire list
            for(int i = 0; i < studentList.Count(); i++)
            {
                Console.WriteLine(studentList[i].toString());
            }

            //Create Excel spreadsheet with the data
            Excel.Application xlApp = new Excel.Application();
            if(xlApp == null)
            {
                Console.WriteLine("Error starting Excel. Make sure it is installed correctly.");
                Console.ReadLine();
                return;
            }
            Excel.Workbook workbook = xlApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet worksheet = workbook.Sheets.Add();

            //Name columns
            worksheet.Cells[1, "A"].Value2 = "First Name";
            worksheet.Cells[1, "B"].Value2 = "Last Name";
            worksheet.Cells[1, "C"].Value2 = "Percentage";
            worksheet.Cells[1, "D"].Value2 = "Letter Grade";
            worksheet.Cells[1, "E"].Value2 = "Score 1";
            worksheet.Cells[1, "F"].Value2 = "Score 2";
            worksheet.Cells[1, "G"].Value2 = "Score 3";
            worksheet.Cells[1, "H"].Value2 = "Score 4";
            worksheet.Cells[1, "I"].Value2 = "Score 5";

            //Insert data from each student in the appropriate columns
            for (int i = 0; i < studentList.Count; i++)
            {
                worksheet.Cells[i + 2, "A"].Value2 = studentList[i].getfName();
                worksheet.Cells[i + 2, "B"].Value2 = studentList[i].getlName();
                worksheet.Cells[i + 2, "C"].Value2 = studentList[i].getAvg().ToString();
                worksheet.Cells[i + 2, "D"].Value2 = studentList[i].getLetterGrade();

                int[] grades = studentList[i].getGrades();
                worksheet.Cells[i + 2, "E"].Value2 = grades[0].ToString();
                worksheet.Cells[i + 2, "F"].Value2 = grades[1].ToString();
                worksheet.Cells[i + 2, "G"].Value2 = grades[2].ToString();
                worksheet.Cells[i + 2, "H"].Value2 = grades[3].ToString();
                worksheet.Cells[i + 2, "I"].Value2 = grades[4].ToString();
            }

            workbook.Close(true);
            xlApp.Quit();

            Console.ReadLine();
            return;
        }
    }
}
