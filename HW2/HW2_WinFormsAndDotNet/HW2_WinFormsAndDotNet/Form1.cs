/*******************************************************************************************
* Programmer: Ran Tao
* ID: 11488080
* Class: CptS 321, Spring  2017
* Programming Assignment: PA 2 - WinForms and .NET
* Created: Jan 25, 2017
* Last Revised: Jan 26, 2017
* Cited: MSDN documentation
*******************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW2_WinFormsAndDotNet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int num = 0;
            StringBuilder sb = new StringBuilder();

            //get random list
            List<int> list = new List<int>();
            Random rand = new Random();
            for(int i = 0; i < 10000; i++)
            {
                num = rand.Next(1, 20000);
                list.Add(num);
            }

            //run function 1
            string func1text = "1. HashSet method: " + function1(list) + " unique numbers";
            string func1text2 = "Time complexity is O(n) because it has a for loop to traverse the array once, and Add() and Count both having O(1) time complexity. And also space complexity is O(n) because it is a hashset.";
            sb.Append(func1text).AppendLine().AppendLine(func1text2).AppendLine();

            //run function 2
            string func2text = "2. O(1)storage method: " + function2(list) + " unique numbers";
            string func2text2 = "Time complexity is O(n^2) because it has a nested loop to traverse the list. And space complexity is O(1) since it doesn't allocate additional lists, arrays, or containers of any sort.";
            sb.Append(func2text).AppendLine().AppendLine(func2text2).AppendLine();

            //run function 3
            string func3text = "3. Sorted method: " + function3(list) + " unique numbers";
            string func3text2 = "Time complexity is O(n) because it has a for loop to traverse the list once and the list is already sorted. And space complexity is O(1) since it is no dynamic memory allocation,";
            sb.Append(func3text).AppendLine().AppendLine(func3text2).AppendLine(); ;
            
            textBox1.Text = sb.ToString();
        }
        /*************************************************************
        * Function: function1(List<int> l)
        * Date Created: Jan 26, 2017
        * Date Last Modified: Jan 26, 2017
        * Description:Do not alter the list in any way and use a hash 
        *  set to determine the number of distinct integers in the list.
        * Return: private int
        *************************************************************/
        private int function1(List<int> l)
        {
            HashSet<int> h = new HashSet<int>();
            for(int i = 0; i < l.Count; i++) // n time @ O(1) each time
            {
                h.Add(l[i]); // n times @ O(1) each time
            }
            return h.Count;
        }
        /*************************************************************
        * Function: function2(List<int> l)
        * Date Created: Jan 26, 2017
        * Date Last Modified: Jan 26, 2017
        * Description:determine the number of distinct items it 
        *  contains while keeping the storage complexity (auxiliary) at O(1).
        * Return: private int
        *************************************************************/
        private int function2(List<int> l)
        {
            int count = 0;
            bool isUnique = false;
            for(int i = 0; i < l.Count; i++) //n times @ O(1) each time
            {
                for(int j = 0; j < i; j++) //n times @ O(n) each time
                {
                    if (l[i] != l[j])
                    {
                        isUnique = true;
                    }
                    else
                    {
                        isUnique = false;
                        break;
                    }
                }
                if (isUnique == true) count++;
                isUnique = false;
            }
            return count+1; //+1 is for adding the first number, since the second loop never touch it.
        }
        /*************************************************************
        * Function: function3(List<int> l)
        * Date Created: Jan 26, 2017
        * Date Last Modified: Jan 26, 2017
        * Description:Sort the list (use built-in sorting functionality)
        *  and then use a new algorithm to determine the number of 
        *  distinct items with O(1) storage, no dynamic memory 
        *  allocation, and O(n) time complexity.
        * Return: private int
        *************************************************************/
        private int function3(List<int> l)
        {
            int count = 0;
            l.Sort();
            for(int i = 0; i<l.Count-1;i++) // n time @ O(1) each time
            {
                if(l[i]!=l[i+1])
                {
                    count++;
                }
            }
            return count+1; // +1 for last itme.
        }
    }
}
