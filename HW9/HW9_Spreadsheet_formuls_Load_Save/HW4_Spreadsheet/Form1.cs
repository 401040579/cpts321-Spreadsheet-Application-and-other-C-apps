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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CptS321;
using SpreadsheetEngine;
using System.IO;

namespace HW7_Spreadsheet
{
    public partial class Form1 : Form
    {
        private Spreadsheet ssheet = new Spreadsheet(50, 26);
        public UndoRedoClass UnRedo = new UndoRedoClass();

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
            refreshUndoRedoButtons();
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
            if (e.PropertyName == "BGColor" && selectedCell != null)
            {
                dataGridView1.Rows[selectedCell.RowIndex].Cells[selectedCell.ColumnIndex].Style.BackColor = Color.FromArgb((int)selectedCell.BGColor);
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

            Cell tempCell = ssheet.GetCell(row, column);

            dataGridView1.Rows[row].Cells[column].Value = tempCell.Text;
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
            int row = e.RowIndex, column = e.ColumnIndex; string m_Text;

            IUndoRedoCmd[] undos = new IUndoRedoCmd[1];

            Cell tempCell = ssheet.GetCell(row, column);

            undos[0] = new RestoreText(tempCell.Text, tempCell.Name);

            try
            {
                m_Text = dataGridView1.Rows[row].Cells[column].Value.ToString();
            }
            catch (NullReferenceException)
            {
                m_Text = "";
            }

            tempCell.Text = m_Text;
            //get a temp cmd for undo;
            multiCmds tmpcmd = new multiCmds(undos, "cell text change");
            //push in undo stack
            UnRedo.AddUndos(tmpcmd);

            dataGridView1.Rows[row].Cells[column].Value = tempCell.Value;

            refreshUndoRedoButtons();
        }

        /*************************************************************
        * Function: button1_Click(object sender, DataGridViewCellCancelEventArgs e)
        * Date Created: Feb 8, 2017
        * Date Last Modified: Feb 9, 2017
        * Description: button for demo *old stuff for hw4 dome*
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

        /*************************************************************
        * Function: chooseBackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        * Date Created: March 8, 2017
        * Date Last Modified: March 8, 2017
        * Description: button for change bgcolor
        * Return: void
        *************************************************************/
        private void chooseBackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedColor = 0;

            List<IUndoRedoCmd> undos = new List<IUndoRedoCmd>();

            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                selectedColor = colorDialog.Color.ToArgb();

                foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    Cell spreadsheetCell = ssheet.GetCell(cell.RowIndex, cell.ColumnIndex);

                    RestoreBGColor tempBGClass = new RestoreBGColor((int)spreadsheetCell.BGColor, spreadsheetCell.Name);

                    undos.Add(tempBGClass);

                    spreadsheetCell.BGColor = (uint)selectedColor;
                }

                multiCmds tempCmd = new multiCmds(undos, "changing cell background color");

                UnRedo.AddUndos(tempCmd);

                refreshUndoRedoButtons();
            }
        }

        /*************************************************************
        * Function: undoToolStripMenuItem_Click(object sender, EventArgs e)
        * Date Created: March 8, 2017
        * Date Last Modified: March 8, 2017
        * Description: button for undo
        * Return: void
        *************************************************************/
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnRedo.Undo(ssheet);
            refreshUndoRedoButtons();
        }

        /*************************************************************
        * Function: redoToolStripMenuItem_Click(object sender, EventArgs e)
        * Date Created: March 8, 2017
        * Date Last Modified: March 8, 2017
        * Description: button for redo
        * Return: void
        *************************************************************/
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnRedo.Redo(ssheet);
            refreshUndoRedoButtons();
        }

        /*************************************************************
        * Function: refreshUndoRedoButtons()
        * Date Created: March 8, 2017
        * Date Last Modified: March 8, 2017
        * Description: update the menu content
        * Return: void
        *************************************************************/
        private void refreshUndoRedoButtons()
        {
            ToolStripItemCollection tempColl = menuStrip1.Items;
            ToolStripMenuItem tempMenus = tempColl[0] as ToolStripMenuItem;

            //tempMenus.DropDown.Items.Find("Results View", true);
            //bool a = tempMenus.DropDown.Items.GetEnumerator().MoveNext();

            for (int i = 0; i < tempMenus.DropDownItems.Count; i++)
            {
                if (tempMenus.DropDownItems[i].Text[0] == 'U')
                {
                    bool undo = UnRedo.CanUndo;
                    tempMenus.DropDownItems[i].Enabled = undo;
                    tempMenus.DropDownItems[i].Text = "Undo " + UnRedo.UndoNext;
                }
                else if (tempMenus.DropDownItems[i].Text[0] == 'R')
                {
                    bool redo = UnRedo.CanRedo;
                    tempMenus.DropDownItems[i].Enabled = redo;
                    tempMenus.DropDownItems[i].Text = "Redo " + UnRedo.RedoNext;
                }
            }
        }

        /*************************************************************
        * Function: saveToolStripMenuItem_Click(object sender, EventArgs e)
        * Date Created: March 20, 2017
        * Date Last Modified: March 20, 2017
        * Description: button for save file
        * Return: void
        *************************************************************/
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML files (*.xml)|*.xml";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream outfile = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write); // Create with permission to write
                ssheet.Save(outfile);

                outfile.Dispose();
            }
        }

        /*************************************************************
        * Function: loadToolStripMenuItem_Click(object sender, EventArgs e)
        * Date Created: March 20, 2017
        * Date Last Modified: March 20, 2017
        * Description: button for load file
        * Return: void
        *************************************************************/
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Clear();// clear spreadsheet

                //read file
                Stream infile = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                ssheet.Load(infile);
                infile.Dispose();
                UnRedo.Clear(); // clear stack
            }

            refreshUndoRedoButtons();
        }

        public void Clear()
        {
            for (int i = 0; i < ssheet.RowCount; i++)
            {
                for (int j = 0; j < ssheet.ColumnCount; j++)
                {
                    if (ssheet.array[i, j].Text != "" || ssheet.array[i, j].Value != "" || ssheet.array[i, j].BGColor != 4294967295)    // Only changed cells
                    {
                        ssheet.array[i, j].Clear();
                    }
                }
            }
        }
    }
}