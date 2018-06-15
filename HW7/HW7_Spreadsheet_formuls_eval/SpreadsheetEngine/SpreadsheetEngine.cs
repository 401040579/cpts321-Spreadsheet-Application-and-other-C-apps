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
                if (m_Text == value)
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
        public event PropertyChangedEventHandler CellPropertyChanged;
        private Dictionary<string, HashSet<string>> depDict;

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

        /*************************************************************
        * Function: setEmpty(createACell m_Cell, Cell cell)
        * Date Created: Feb 8, 2017
        * Date Last Modified: Feb 9, 2017
        * Description: set value to empty
        * Return: void
        *************************************************************/
        private void setEmpty(createACell m_Cell, Cell cell)
        {
            m_Cell.SetValue("");
            CellPropertyChanged(cell, new PropertyChangedEventArgs("Value"));
        }

        /*************************************************************
        * Function: setExp(createACell m_Cell, Cell cell)
        * Date Created: Feb 8, 2017
        * Date Last Modified: Feb 9, 2017
        * Description: set value to exp
        * Return: void
        *************************************************************/
        private void setExp(createACell m_Cell, Cell cell)
        {
            ExpTree exptree = new ExpTree(m_Cell.Text.Substring(1));
            string[] variables = exptree.GetAllVariables();
            foreach (string variableName in variables)
            {
                Cell variableCell = GetCell(variableName);
                double value;

                if (string.IsNullOrEmpty(variableCell.Value))
                    exptree.SetVar(variableCell.Name, 0);

                else if (!double.TryParse(variableCell.Value, out value))
                    exptree.SetVar(variableName, 0);

                else
                    exptree.SetVar(variableName, value);
            }

            //old code from hw4
            //string letter = m_Cell.Text.Substring(1);
            //string number = letter.Substring(1);
            //string lettersAZ = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            //int col = 0, row = int.Parse(number) - 1;
            //for (int i = 0; i < 26; i++)
            //{
            //    if (lettersAZ[i] == letter[0])
            //    {
            //        col = i;
            //        break;
            //    }
            //}
            //m_Cell.SetValue(GetCell(row, col).Value.ToString());

            m_Cell.SetValue(exptree.Eval().ToString());
            CellPropertyChanged(cell, new PropertyChangedEventArgs("Value"));
        }

        /*************************************************************
        * Function: setExp(createACell m_Cell, Cell cell)
        * Date Created: Feb 8, 2017
        * Date Last Modified: Feb 9, 2017
        * Description: set value to exp
        * Return: void
        *************************************************************/
        private void setText(createACell m_Cell, Cell cell)
        {
            m_Cell.SetValue(m_Cell.Text);
            CellPropertyChanged(cell, new PropertyChangedEventArgs("Value"));
        }

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

            if (string.IsNullOrEmpty(m_Cell.Text)) { setEmpty(m_Cell, cell); }
            // if it is an exp
            else if (m_Cell.Text[0] == '=' && m_Cell.Text.Length >= 3) { setExp(m_Cell, cell); }
            else { setText(m_Cell, cell); }

            if (depDict.ContainsKey(m_Cell.Name))
                foreach (var dependentCell in depDict[m_Cell.Name]) Eval(dependentCell);
        }
        /*************************************************************
        * Function: Eval(string exp)
        * Date Created: March 3, 2017
        * Date Last Modified: March 3, 2017
        * Description: eval
        * Return: void
        *************************************************************/
        private void Eval(string exp)
        {
            Eval(GetCell(exp));
        }

        /*************************************************************
        * Function: GetCell(string location)
        * Date Created: March 3, 2017
        * Date Last Modified: March 3, 2017
        * Description: get a cell by using a string
        * Return: Cell
        *************************************************************/
        public Cell GetCell(string exp)
        {
            char letter = exp[0];
            short number;
            Cell result;

            if (char.IsLetter(letter) == false) { return null; }

            if (short.TryParse(exp.Substring(1), out number) == false) { return null; }

            try { result = GetCell(number - 1, letter - 'A'); }
            catch { return null; }

            return result;
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

            depDict = new Dictionary<string, HashSet<string>>();

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
                RemoveDep(tmpCell.Name);

                if (tmpCell.Text != "" && tmpCell.Text[0] == '=')
                {
                    ExpTree exp = new ExpTree(tmpCell.Text.Substring(1));
                    MakeDep(tmpCell.Name, exp.GetAllVariables()); 
                }
                Eval(sender as Cell);
            }
        }

        /*************************************************************
        * Function: RemoveDep(string cellName)
        * Date Created: March 3, 2017
        * Date Last Modified: March 3, 2017
        * Description: Removes all dependencies from the cell name
        * Return: void
        *************************************************************/
        private void RemoveDep(string cellName)
        {
            List<string> dependenciesList = new List<string>();

            foreach (string key in depDict.Keys)
            {
                if (depDict[key].Contains(cellName))
                    dependenciesList.Add(key); 
            }

            foreach (string key in dependenciesList)
            {
                HashSet<string> hashset = depDict[key];
                if (hashset.Contains(cellName))
                    hashset.Remove(cellName); 
            }
        }

        /*************************************************************
        * Function: MakeDep(string cellName, string[] variablesUsed)
        * Date Created: March 3, 2017
        * Date Last Modified: March 3, 2017
        * Description: Removes all dependencies from the cell name
        * Return: void
        *************************************************************/
        private void MakeDep(string cellName, string[] variablesUsed)
        {
            for (int i = 0; i < variablesUsed.Length; i++)
            {
                if (depDict.ContainsKey(variablesUsed[i]) == false)
                {
                    depDict[variablesUsed[i]] = new HashSet<string>();
                }
                depDict[variablesUsed[i]].Add(cellName);
            }
        }

        /*************************************************************
        * Function: GetCell(int RowIndex, int ColumnIndex)
        * Date Created: March 3, 2017
        * Date Last Modified: March 3, 2017
        * Description: get cell from given row and col
        * Return: Cell
        *************************************************************/
        public Cell GetCell(int RowIndex, int ColumnIndex)
        {
            return array[RowIndex, ColumnIndex];
        }

        //getter
        public int RowCount { get { return array.GetLength(0); } }
        public int ColumnCount { get { return array.GetLength(1); } }
    }
}
