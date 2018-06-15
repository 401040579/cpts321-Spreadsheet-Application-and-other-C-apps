/*******************************************************************************************
* Programmer: Ran Tao
* ID: 11488080
* Class: CptS 321, Spring  2017
* Programming Assignment: PA 3 - WinForms “Notepad” Application / Fibonacci BigInt Text Reader
* Created: Feb 1, 2017
* Last Revised: Feb 1, 2017
* Cited: MSDN documentation
*******************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW3_Notepad_fib
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /*************************************************************
        * Function: exitToolStripMenuItem_Click(object sender, EventArgs e)
        * Date Created: Feb 1, 2017
        * Date Last Modified: Feb 1, 2017
        * Description: exit the app
        * Return: void
        *************************************************************/
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //use OpenFileDialog/SaveFileDialog for selecting files to open/save
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        //use OpenFileDialog/SaveFileDialog for selecting files to open/save
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
        /*************************************************************
        * Function: saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        * Date Created: Feb 1, 2017
        * Date Last Modified: Feb 1, 2017
        * Description: application needs to provide the ability to save 
        * the text to a text file. Provide a button or menu option that 
        * brings up a SaveFileDialog, prompting the user for a file name. 
        * If the user clicks OK in that dialog, which you’ll determine by 
        * checking the return value of the SaveFileDialog.ShowDialog 
        * function, then save the text in the text box to that file. 
        * You can save it using streams, text-writers, or any other 
        * method you see fit (it’s the loading that’s going to be more 
        * specific).
        * Return: void
        *************************************************************/
        private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter myStream = null;
            //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                myStream = new StreamWriter(saveFileDialog1.FileName);
                myStream.Write(textBox1.Text);
                myStream.Close();
            }
        }
        /*************************************************************
        * Function: loadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        * Date Created: Feb 1, 2017
        * Date Last Modified: Feb 1, 2017
        * Description: Add the option to load text from a file. You can 
        * use a stream reader to open the file and pass it to your 
        * loading function. Remember that StreamReader inherits from 
        * TextReader and has a constructor that takes a file name as a parameter.
        * Return: void
        *************************************************************/
        private void loadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamReader myStream = null;
            //OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                myStream = new StreamReader(openFileDialog1.FileName);
                LoadText(myStream);
                myStream.Close();
            }
        }
        /*************************************************************
        * Function: LoadText(TextReader sr)
        * Date Created: Feb 1, 2017
        * Date Last Modified: Feb 1, 2017
        * Description: Use either ReadToEnd or ReadLine (in a loop) to 
        * load all text from the reader and put it in the text box. 
        * This should replace any content already in the text box.
        * Return: void
        *************************************************************/
        private void LoadText(TextReader sr)
        {
            textBox1.Text = sr.ReadToEnd();
        }
        /*************************************************************
        * Class: FibonacciTextReader
        * Date Created: Feb 1, 2017
        * Date Last Modified: Feb 1, 2017
        * Description: Write a class named FibonacciTextReader that 
        * inherits from the System.IO.TextReader.
        *************************************************************/
        class FibonacciTextReader : TextReader //Inherit from the TextReader class
        {
            int _maxNumber = 0, count = 0; //count is for print first 2's fib numbers.
            BigInteger firstFib = 0, SecondFib = 1;
            
           /* Make it have a constructor that takes an integer as a parameter indicating the maximum
            * number of lines available (the maximum number of numbers in the sequence that you
            * can generate).*/
            public FibonacciTextReader(int maxNumber)
            {
                _maxNumber = maxNumber;
            }

           /* Override the ReadLine method which delivers the next number (as a string) in the
            * Fibonaci sequence. You need logic in this function to handle the first two numbers as
            * special cases. You must return null after the nth call, where n is the integer value passed
            * to the constructor.*/
            public override string ReadLine()
            {
                BigInteger number;

                if(count == 0)
                {
                    count++;
                    return "0";
                }
                else if(count == 1)
                {
                    count++;
                    return "1";
                }
                else
                {
                    number = firstFib + SecondFib;
                    firstFib = SecondFib;
                    SecondFib = number;
                    return number.ToString(); //after the nth call, we don't need return anything.
                }
            }
            /* Implement it such that it repeatedly calls ReadLine and 
             * concatenates all the lines together.*/
            public override string ReadToEnd()
            {
                StringBuilder sb = new StringBuilder();

                for(int i = 0; i < _maxNumber;i++)
                {
                    sb.Append(i + 1).Append(": ").AppendLine(ReadLine());
                }

                return sb.ToString();
            }
        }

        //load the first 50 numbers of the Fibonacci sequence
        private void loadFibonacciNumbersfirst50ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FibonacciTextReader sr = new FibonacciTextReader(50);
            textBox1.Text = sr.ReadToEnd();
        }

        //load the first 100 numbers of the Fibonacci sequence
        private void loadFibonacciNumbersfirst100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FibonacciTextReader sr = new FibonacciTextReader(100);
            textBox1.Text = sr.ReadToEnd();
        }
    }
}
