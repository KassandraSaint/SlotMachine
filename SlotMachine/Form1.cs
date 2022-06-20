using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlotMachine
{
    public partial class Form1 : Form
    {
        // Crearting a SIZE constant
        const int SIZE = 3;
        // Creating a dictionary to hold pictures for slot machine
        private Dictionary<int, string> pics = new Dictionary<int, string>()
        {
            {0, "Apple.bmp"}, 
            {1, "Banana.bmp"},
            {2, "Cherries.bmp"},
            {3, "Grapes.bmp"},
            {4, "Lemon.bmp"},
            {5, "Lime.bmp"},
            {6, "Orange.bmp"},
            {7, "Pear.bmp"},
            {8, "Strawberry.bmp"},
            {9, "Watermelon.bmp"}
        };

        // Initilizing an array to hold randomly selected pictures
        int[] randomPicNums = new int[SIZE];

        // Initializing total variables to account money in and money out
        decimal moneyIn = 0m;
        decimal moneyOut = 0m;

        // Creating Random Object
        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        // Creating a method to generate random picture numbers and fill pictureBox controls with random pictures
        private void picGenerator()
        {
            // Creating an array of pictureBoxes
            PictureBox[] pictureBoxes = new PictureBox[SIZE] { picBox1, picBox2, picBox3 }; 

            // Choosing 3 random pictures and adding them to randomPics array
            for (int i = 0; i < SIZE; i++)
            {
                int pic = rand.Next(pics.Count);
                randomPicNums[i] = pic;

                // Getting elements from the pics dictionary  at selected random positions 
                var picture = pics[randomPicNums[i]];

                // Filling picture Box slots with random pictures 
                pictureBoxes[i].Image = new Bitmap("Resources/" + picture);
            }        
        }

        // Creating a method to count how many pictures matched
        private int matchedCount()
        {
            // Initializing a variable to track number of matched pictures and setting it to 1 as it
            // will always have at least one occurace 
            int numMatched = 1;

            // Creating a dictionary to track number of elements
            Dictionary<int, int> counter = new Dictionary<int, int>();

            // Itterating over array of random pictures, adding them to a dictionary counter
            for (int i = 0; i < SIZE; i++)
            {
                // If counter dictionary doesn't countain an element with the key from randomPicNums array
                if (!counter.ContainsKey(randomPicNums[i]))
                {
                    // Adding an element with this number as a key and setting a value to 1
                    counter[randomPicNums[i]] = 1;
                }
                // Otherwise, if it is already present incrementing value by 1
                else
                {
                    counter[randomPicNums[i]]++;
                    // If key exists in the dictionary and we incresing it's value counter,
                    // we increase numMatched counnter by one as well
                    numMatched++;
                }
            }

            // Returning number of matched pictures
            return numMatched;
        }

        // Creating a method to count how much was won
        private void moneyPerSpin()
        {
            // Initializing money variable to store user inserted money
            decimal money = 0;
            // Calling matchedCount method to get number of matched pictures and store it in matched variable
            int matched = matchedCount();
            // Getting amount of money entered by a user and storing it in money variable
            if (decimal.TryParse(inserted.Text, out money))
            {
                // Updating moneyIn variable by incrementing it by current money value
                moneyIn += money;
                // If 2 or 3 pictures matched multiplying money inserted by 2 or 3 respectively
                if (matched == 3)
                {
                    money *= 3;
                }
                else if (matched == 2)
                {
                    money *= 2;
                }
                // Or setting money to 0
                else
                {
                    money = 0;
                }
                // Updating moneyOut variable by incrementing it by current money value
                moneyOut += money;

                // Creating a message variable formatted with format specifier for curency
                string message = String.Format("You got {0:C} after this spin", money);
                // Showing user how much money he or she got after a spin
                MessageBox.Show(message);
            }
            else
            {
                MessageBox.Show("Wrong input! Try entering amount of inserted money again");
            }
        }
        private void spinBtn_Click(object sender, EventArgs e)
        {
            // Calling picGenerator method to generate images for slot machine
            picGenerator();

            // Calling moneyPerSpin method to calculate how much was won or lost over a slot machine spin
            moneyPerSpin();

        }
        private void exitBtn_Click(object sender, EventArgs e)
        {
            // Showing a message to the user on how much money was won and lost during a game
            string message = String.Format("You inserted {0:C} into a slot machine and won {1:C}", moneyIn, moneyOut);
            MessageBox.Show(message);
            // Closing the form 
            this.Close();
        }
    }
}
