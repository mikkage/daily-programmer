using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace PBMViewer
{
    public partial class Form1 : Form
    {
        String[] imageData;                 //Holds the bitmap of the image
        System.Drawing.Graphics graphics;
        Color primary, secondary;           //Primary and secondary colors of the image

        int pixelSize = 5;                  //Size of a bit in pixels

        bool imageLoaded = false;           //Keeps track of if an image has been loaded or not

        public Form1()
        {
            InitializeComponent();
            graphics = this.CreateGraphics();
            comboBox1.SelectedIndex = 0;        //Set up default combo box values
            comboBox2.SelectedIndex = 1;
            primary = Color.White;              //Default colors
            secondary = Color.Black;
        }

        private void option1ToolStripMenuItem_Click(object sender, EventArgs e)
        //Callback for when the 'Load' is clicked in the main menu
        {
            pixelSize = Convert.ToInt16(textBox1.Text);         //Get pixel size from textbox

            Stream myStream = null;

            //Open file dialog to load the image.
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PBM files (*.pbm)|*pbm";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            StreamReader streamReader = new StreamReader(myStream);
                            String fileData = streamReader.ReadToEnd();

                            imageData = fileData.Split('\n');           //Store data loaded in as an array of strings
                            renderImage();                              //Draw the image
                            imageLoaded = true;                         //Image data has been loaded
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        //Callback for when the 'Exit' button is clicked in the main menu
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                System.Environment.Exit(1);
            }
        }

        public void drawSquare(Graphics g, Color c, int x, int y)
        //Draws a square of pixelSize at (x,y) of color c
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(c);
            g.FillRectangle(myBrush, new Rectangle(x, y, pixelSize, pixelSize));
            myBrush.Dispose();
        }

        public void renderImage()
        //Renders the bitmap on screen
        {
            graphics.Clear(this.BackColor);     //Clear screen

            int x = 10;             //x,y coordinates of the top left of the image
            int y = 50;

            //The first three lines of the image data are information about the image, which has already been loaded.
            //After those, it is onlt the bits for the image
            for(int i = 3; i < 10; i++)
            {
                String s = imageData[i];
                for(int j = 0; j < s.Length -1; j++)
                {
                    if (s[j] == '0')
                        drawSquare(graphics, secondary, x, y);      //If the char loaded is a 0, then draw a square of the secondary color
                    else
                        drawSquare(graphics, primary, x, y);        //If not, then draw a primary color square

                    x += pixelSize;                                 //Go to next block
                }
                y += pixelSize;                                     //Go to next line
                x = 10;                                             //Reset x back to the beginning of the line
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        //Callback for when the pixel size textbox is changed
        {
            
            if (imageLoaded && textBox1.Text != "")     //Only go on if a file has been loaded and the textbox is not empty
            {
                pixelSize = Convert.ToInt16(textBox1.Text);     //Get int value from textbox
                renderImage();                                  //Redraw image in appropriate scale
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        //Callback for when an item is selected in the first color combobox
        {
            if (imageLoaded)    //Make sure an image is loaded before changing color
            {
                String color = comboBox1.Text;
                
                //Set the color to the item selected in the combo box
                switch (color)
                {
                    case "White": primary = Color.White; break;
                    case "Black": primary = Color.Black; break;
                    case "Red": primary = Color.Red; break;
                    case "Green": primary = Color.Green; break;
                    case "Blue": primary = Color.Blue; break;
                }
                renderImage();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        //Works the same as the comboBox1_SelectedIndexChanged function, but changes the secondary color instead of the primary
        {
            if (imageLoaded)
            {
                String color = comboBox2.Text;
                switch (color)
                {
                    case "White": secondary = Color.White; break;
                    case "Black": secondary = Color.Black; break;
                    case "Red": secondary = Color.Red; break;
                    case "Green": secondary = Color.Green; break;
                    case "Blue": secondary = Color.Blue; break;
                }
                renderImage();
            }
        }
    }
}