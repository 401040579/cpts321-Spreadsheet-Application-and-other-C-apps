/*******************************************************************************************
* Programmer: Ran Tao
* ID: 11488080
* Class: CptS 321, Spring  2017
* Programming Assignment: PA 4 - First Steps for your Spreadsheet Application
* Created: Feb 8, 2017
* Last Revised: Feb 9, 2017
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
using SpreadsheetEngine;

namespace HW4_Spreadsheet
{
    public partial class Form1 : Form
    {
        private Spreadsheet ssheet = new Spreadsheet(50, 26);

        public Form1()
        {
            InitializeComponent();
        }

        /*************************************************************
        * Function: Form1_Load(object sender, EventArgs e)
        * Date Created: Feb 8, 2017
        * Date Last Modified: Feb 9, 2017
        * Description: load form
        * Return: void
        *************************************************************/
        private void Form1_Load(object sender, EventArgs e)
        {
            ssheet.CellPropertyChanged += OnCellPropertyChanged;
            dataGridView1.CellBeginEdit += dataGridView1_CellBeginEdit;
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;

            dataGridView1.Columns.Clear();
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i <= 25; i++)
                dataGridView1.Columns.Add(letters[i].ToString(), letters[i].ToString());
            dataGridView1.Rows.Add(50); //add 50 rows
            for (int i = 0; i < 50; i++) //signing name 1-50
            {
                var row = dataGridView1.Rows[i];
                row.HeaderCell.Value = (i + 1).ToString();
            }
        }

        /*************************************************************
        * Function: OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        * Date Created: Feb 8, 2017
        * Date Last Modified: Feb 9, 2017
        * Description: OnCellPropertyChanged
        * Return: void
        *************************************************************/
        private void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell selectedCell = sender as Cell;

            if (e.PropertyName == "Value" && selectedCell != null)
            {
                dataGridView1.Rows[selectedCell.RowIndex].Cells[selectedCell.ColumnIndex].Value = selectedCell.Value;
            }
        }

        /*************************************************************
        * Function: dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        * Date Created: Feb 8, 2017
        * Date Last Modified: Feb 9, 2017
        * Description: dataGridView1_CellBeginEdit
        * Return: void
        *************************************************************/
        void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int row = e.RowIndex, column = e.ColumnIndex;

            Cell spreadsheetCell = ssheet.GetCell(row, column);

            dataGridView1.Rows[row].Cells[column].Value = spreadsheetCell.Text;
        }

        /*************************************************************
        * Function: dataGridView1_CellEndEdit(object sender, DataGridViewCellCancelEventArgs e)
        * Date Created: Feb 8, 2017
        * Date Last Modified: Feb 9, 2017
        * Description: dataGridView1_CellEndEdit
        * Return: void
        *************************************************************/
        void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex, column = e.ColumnIndex;
            string m_Text;

            Cell spreadsheetCell = ssheet.GetCell(row, column);     

            try
            {
                m_Text = dataGridView1.Rows[row].Cells[column].Value.ToString();  
            }
            catch (NullReferenceException)
            {
                m_Text = "";
            }

            spreadsheetCell.Text = m_Text;     

            dataGridView1.Rows[row].Cells[column].Value = spreadsheetCell.Value; 

        }

        /*************************************************************
        * Function: button1_Click(object sender, DataGridViewCellCancelEventArgs e)
        * Date Created: Feb 8, 2017
        * Date Last Modified: Feb 9, 2017
        * Description: button for demo
        * Return: void
        *************************************************************/
        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            /*The demo should set the text in about 50 random cells to a text string of your choice. 
             * “Hello World!” would be fine or some other message would be ok too.*/
            for (int i = 0; i < 50; i++)
            {
                int row = rnd.Next(0, 49);
                int column = rnd.Next(2, 25); // columns start from >2 so nothing conflicts

                ssheet.array[row, column].Text = "Hello World!";
            }
            /*Also, do a loop to set the text in every cell in column B to “This is cell B#”,
             *  where # number is the row number for the cell.*/
            for (int i = 0; i < 50; i++)
            {
                ssheet.array[i, 1].Text = "This is cell B" + (i + 1).ToString();
            }
            /*The result should be that the cells in column A update to have the same values as column B.*/
            for (int i = 0; i < 50; i++) { ssheet.array[i, 0].Text = "=B" + (i + 1).ToString(); }
        }
    }
}
