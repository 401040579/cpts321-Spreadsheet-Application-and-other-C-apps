/*******************************************************************************************
* Programmer: Ran Tao
* ID: 11488080
* Class: CptS 321, Spring  2017
* Programming Assignment: PA 7 - Spreadsheet Application
* Created: Feb 8, 2017
* Last Revised: March 3, 2017
* Cited: MSDN documentation
*******************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptS321
{
    public class SpreadsheetEngine
    {

    }

    public abstract class Cell : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private readonly int m_RowIndex, m_ColumnIndex;
        protected string m_Text = "", m_Value = "";
        protected int m_BackColor = -1;
        private readonly string m_Name;

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
            m_Name += Convert.ToChar('A' + ColumnIndex);
            m_Name += (RowIndex + 1).ToString();
        }

        //getter
        public int RowIndex { get { return m_RowIndex; } }
        public int ColumnIndex { get { return m_ColumnIndex; } }
        public string Name { get { return m_Name; } }
        public string Value { get { return m_Value; } }
        public string Text
        {
            get { return m_Text; }
            set
            {
                if (m_Text == value) { return; }
                m_Text = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Text"));
            }
        }
        public uint BGColor
        {
            get { return (uint) m_BackColor; }
            set
            {
                if (m_BackColor == value) { return; }
                m_BackColor = (int) value;
                PropertyChanged(this, new PropertyChangedEventArgs("BGColor"));
            }
        }

        public void Clear()
        {
            Text = "";
            BGColor = 0;
        }
    }    
}
