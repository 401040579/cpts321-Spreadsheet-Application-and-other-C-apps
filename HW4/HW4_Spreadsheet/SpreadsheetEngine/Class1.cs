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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    public class Class1
    {
    }

    public abstract class Cell : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private readonly int m_RowIndex, m_ColumnIndex;
        protected string m_Text = "", m_Value = "";

        /*************************************************************
        * Function: Cell(int RowIndex, int ColumnIndex)
        * Date Created: Feb 8, 2017
        * Date Last Modified: Feb 9, 2017
        * Description: Cell
        * Return: NONE
        *************************************************************/
        public Cell(int RowIndex, int ColumnIndex)
        {
            m_RowIndex = RowIndex;
            m_ColumnIndex = ColumnIndex;
        }

        public int RowIndex
        {
            get
            {
                return m_RowIndex;
            }
        }

        public int ColumnIndex
        {
            get
            {
                return m_ColumnIndex;
            }
        }
        public string Value
        {
            get
            {
                return m_Value;
            }
        }
        public string Text
        {
            get
            {
                return m_Text;
            }
            set
            {
                if(m_Text == value)
                {
                    return;
                }
                m_Text = value;

                PropertyChanged(this, new PropertyChangedEventArgs("Text"));
            }
        }
    }

    public class Spreadsheet
    {
        /*come up with a design here that actually allows the spreadsheet 
         * to create cells*/
        private class createACell : Cell
        {
            public createACell(int RowIndex, int ColumnIndex) : base(RowIndex, ColumnIndex) { }
            
            /*Value property is a getter only and you’ll have to implement a 
            * way to allow the spreadsheet class to set the value, but no 
            * other class can.*/
            public void SetValue(string value)
            {
                m_Value = value;
            }
        }

        public event PropertyChangedEventHandler CellPropertyChanged;

        /*************************************************************
        * Function: Eval(Cell cell)
        * Date Created: Feb 8, 2017
        * Date Last Modified: Feb 9, 2017
        * Description: eval
        * Return: void
        *************************************************************/
        private void Eval(Cell cell)
        {
            createACell m_Cell = cell as createACell;

            if(string.IsNullOrEmpty(m_Cell.Text))
            {
                m_Cell.SetValue("");
                CellPropertyChanged(cell, new PropertyChangedEventArgs("Value"));

            }
            else if(m_Cell.Text[0] == '=' && m_Cell.Text.Length >= 3)
            {
                string letter = m_Cell.Text.Substring(1);
                string number = letter.Substring(1);
                string lettersAZ = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                int col = 0, row = int.Parse(number) - 1;

                for(int i = 0;i<26;i++)
                {
                    if (lettersAZ[i] == letter[0])
                    {
                        col = i;
                        break;
                    }
                }
                
                m_Cell.SetValue(GetCell(row, col).Text);
                CellPropertyChanged(cell, new PropertyChangedEventArgs("Value"));
            }
            else
            {
                m_Cell.SetValue(m_Cell.Text);
                CellPropertyChanged(cell, new PropertyChangedEventArgs("Value"));
            }
        }

        
        public Cell[,] array;

        /*************************************************************
        * Function: Spreadsheet(int RowIndex, int ColumnIndex)
        * Date Created: Feb 8, 2017
        * Date Last Modified: Feb 9, 2017
        * Description: Spreadsheet cons
        * Return: NONE
        *************************************************************/
        public Spreadsheet(int RowIndex, int ColumnIndex)
        {
            array = new Cell[RowIndex, ColumnIndex];
            for (int x = 0; x < RowIndex; x++)
            {
                for (int y = 0; y < ColumnIndex; y++)
                {
                    array[x, y] = new createACell(x, y);
                    array[x, y].PropertyChanged += OnPropertyChanged;
                }
            }
        }

        /*************************************************************
        * Function: OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        * Date Created: Feb 8, 2017
        * Date Last Modified: Feb 9, 2017
        * Description: OnPropertyChanged
        * Return: void
        *************************************************************/
        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                createACell tmpCell = sender as createACell;
                
                Eval(sender as Cell);
            }
        }
        public Cell GetCell(int RowIndex, int ColumnIndex)
        {
            return array[RowIndex, ColumnIndex];
        }
        public int RowCount
        {
            get
            {
                return array.GetLength(0);
            }
        }

        public int ColumnCount
        {
            get
            {
                return array.GetLength(1);
            }
        }

    }
}
