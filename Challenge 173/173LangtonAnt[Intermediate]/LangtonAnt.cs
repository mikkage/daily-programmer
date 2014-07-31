//Michael Parker
//7/30/14
//Advanced Langton's Ant
//http://www.reddit.com/r/dailyprogrammer/comments/2c4ka3/7302014_challenge_173_intermediate_advanced/

//Visual Studio 2013
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Diagnostics;

namespace langtonAnt
{
    class Ant
    {
        //Class members
        private List<List<char>> grid;          //The 2d grid that the ant moves on. The char describes the color of the square
        private char direction = 'u';           //Direction that the ant is facing.
        private int x, y, width, height;        //X and Y coordinates of the ant, and the height and width of the grid
        private int maxColors = 10;             //Maximum number of colors that can be displayed on the grid
        private char[] colorChars = new char[]  //Char notation for the colors that are stored in the grid
        {
            'w', 'b', 'r', 'g', 't', 'c', 'm', 'y', 'a', 'n'
        };
        Color[] colors = new Color[]            //Colors for creating the bitmap
        {
            Color.White,
            Color.Black,
            Color.Red,
            Color.Lime,
            Color.Blue,
            Color.Yellow,
            Color.Cyan,
            Color.Magenta,
            Color.Gray,
            Color.Green
        };
        private List<char> colorsList;                          //Will store a list representation of colorChars[] to get access to some List functions on the data set
        private List<char> colorDirections = new List<char>();  //Will store the list of which way the ant must turn on certain colors, as defined by the user.


        //Class methods
        public Ant(int size, string colorTurns)
        {
            colorsList = colorChars.ToList<char>();             //Converts the colorChars[] array to a list
            this.width = size;                                  //Set height and width of the grid, keeping it a square for now.
            this.height = size;

            x = y = size/2;                         //Put ant in center of grid
            maxColors = colorTurns.Length - 1;      //Maximum colors is determined from the length of the string that describes how the ant moves at each color
                                                    //EX: "LLRRL" means that there is a maximum of five colors
                                                    //One is subtracted from the length to allow it to index into the colorsList array


            // Initialize grid to all 'w', or white
            Console.Write("Initializing grid...");
            grid = new List<List<char>>(size);
            for(int i = 0; i < size; i++)
            {
                List<char> col = new List<char>(size);
                for(int j = 0; j < size; j++)
                {
                    col.Add('w');
                }
                grid.Add(col);
            }
            Console.WriteLine("Done");

            //Copy the string that describes how the ant moves on each color into a list ex: "LLRRLL", and make sure it is all uppercase
            colorTurns = colorTurns.ToUpper();
            for (int i = 0; i < colorTurns.Length; i++)
            {
                this.colorDirections.Add(colorTurns[i]);
            }

        }

        //Changes the ant's direction by making it turn 90 degrees to the left.
        public void turnLeft()
        {
            switch (direction)
            {
                case 'u': direction = 'l'; break;
                case 'l': direction = 'd'; break;
                case 'd': direction = 'r'; break;
                case 'r': direction = 'u'; break;
            }
        }

        //Changes the ant's direction by making it turn 90 degrees to the right.
        public void turnRight()
        {
            switch (direction)
            {
                case 'u': direction = 'r'; break;
                case 'l': direction = 'u'; break;
                case 'd': direction = 'l'; break;
                case 'r': direction = 'd'; break;
            }
        }

        //Makes the ant move forward, if possible by:
        // 1. Rotate the ant
        // 2. Update the color of the square the ant it currently on
        // 3. Move the ant in the correct direction
        public void move()
        {
            if (x < width && y < height)                                //Make sure that moving won't make the ant go out of bounds.
            {
                char currentColor = grid[y][x];                         //Get color at ant's current location
                int colorIndex = colorsList.IndexOf(currentColor);      //Get the index of the color

                //Decide whether to turn left or right, then make that turn.
                if (colorDirections[colorIndex] == 'L')
                    turnLeft();
                else turnRight();

                if (colorIndex == maxColors)                            //If the color is the last in the sequence, start over
                    colorIndex = 0;
                else
                    colorIndex++;                                       //Otherwise, go to next color

                grid[y][x] = colorsList[colorIndex];                    //Change location's color to the new color


                //Move forward
                switch (direction)
                {
                    case 'u': if (y > 0) y--; break;
                    case 'l': if (x > 0) x--; break;
                    case 'd': if (y < height) y++; break;
                    case 'r': if (x < width) x++; break;
                }
            }
        }

        //Runs the ant through a given number of generations
        public void runGeneration(int generations)
        {
            Console.Write("Running generations...");
            for(int i = 0; i < generations; i++)
            {
                move();
            }
            Console.WriteLine("Done");
        }

        //Saves the bitmap to file
        public void printGridToFile(string fileName)
        {
            //Make sure the filename has the correct extention(.bmp)
            string ext = Path.GetExtension(fileName);
            if (ext != ".bmp")
                fileName += ".bmp";

            Console.Write("Writing to file " + fileName + "...");

            //Go through the grid and add each bit the bitmap
            System.Drawing.Bitmap bmp = new Bitmap(this.width, this.height);
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    int index = colorsList.IndexOf(grid[j][i]);
                    bmp.SetPixel(i, j, colors[index]);
                }
            }
            //Save the file
            bmp.Save(fileName);
            Console.WriteLine("Done");
            Process.Start(fileName);    //Open the file in viewer
        }

    }

    class LangtonAnt
    {
        static void Main(string[] args)
        {
            int size;
            Console.Write("Enter Size of Grid: ");
            size = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter LR pattern: ");
            string pattern = Console.ReadLine();

            Ant ant = new Ant(size, pattern);
            Console.Write("Enter number of generations: ");
            int gens = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter name of file to save: ");
            string fileName = Console.ReadLine();

            ant.runGeneration(gens);
            ant.printGridToFile(fileName);

            Console.ReadLine();
        }
    }
}
