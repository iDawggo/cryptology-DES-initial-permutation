using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lesson_24
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public String asciiInput = "";
        public String binaryConvert = "";

        private void scrllAsciiIn_TextChanged(object sender, TextChangedEventArgs e)
        {
            scrllPairityOut.Text = "";
            scrllBinaryOut.Text = "";
            asciiInput = "";
            binaryConvert = "";
            errorsOut.Text = "";

            asciiInput = scrllAsciiIn.Text;

            //Converting ASCII input into binary, restricting to seven characters
            foreach (char c in asciiInput)
            {
                //Character restriction
                if ((asciiInput.Length > 7) && asciiInput != String.Empty)
                {
                    scrllAsciiIn.Text = asciiInput.Remove(asciiInput.Length - 1, 1);
                    scrllAsciiIn.SelectionStart = scrllAsciiIn.Text.Length;
                }
                else if (asciiInput == String.Empty)
                {
                    errorsOut.Text = "Please input a seven-character string in the input!";
                    return;
                }

                //Binary conversion
                binaryConvert += Convert.ToString(c, 2).PadLeft(8, '0');
            }
        }

        private void calculate_Click(object sender, RoutedEventArgs e)
        {
            scrllPairityOut.Text = "";
            scrllBinaryOut.Text = "";
            errorsOut.Text = "";

            if (asciiInput.Length < 7 || asciiInput == String.Empty)
            {
                errorsOut.Text = "Please input a seven-character string in the input!";
                return;
            }

            int count = 0;
            String[,] oddParities = new String[8, 8];
            for (int i = 0; i < asciiInput.Length + 1; i++)
            {
                //Using the odd parity method on the binary string for the ascii character
                String tempParity = oddParity(binaryConvert.Substring(i * 7, 7));

                for (int j = 0; j < 8; j++)
                {
                    //Placing binary string with odd parity into the array
                    oddParities[i, j] = tempParity.Substring(j, 1);
                    count++;
                }
            }

            String parities = "";
            for (int i = 0; i < asciiInput.Length + 1; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    scrllPairityOut.Text += oddParities[i, j];
                    parities += oddParities[i, j];
                }
                scrllPairityOut.Text += "\n";
            }

            //Creating a 2D array for permuted choice 1
            int[,] permutedChoice =
            {
                {57, 49, 41, 33, 25, 17, 9 },
                {1 , 58, 50, 42, 34, 26, 18},
                {10, 2 , 59, 51, 43, 35, 2 },
                {19, 11, 3 , 60, 52, 44, 36},
                {63, 55, 47, 39, 31, 23, 15},
                {7 , 62, 54, 46, 38, 30, 22},
                {14, 6 , 61, 53, 45, 37, 29},
                {21, 13, 5 , 28, 20, 12, 4 },
            };


            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    int choiceIndex = (permutedChoice[i, j]) - 1;
                    scrllBinaryOut.Text += parities[choiceIndex];
                }
                scrllBinaryOut.Text += "\n";
            }
        }

        String oddParity(String input)
        {
            //Counting the number of 1s in the string
            int count = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '1')
                {
                    count++;
                }
            }

            if (count % 2 == 0)
            {
                //If even, add a 1
                input += "1";
            }
            else
            {
                //If odd, add a 0
                input += "0";
            }

            return input;
        }
    }
}
