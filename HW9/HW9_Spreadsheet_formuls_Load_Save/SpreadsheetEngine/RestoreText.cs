using CptS321;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    public class RestoreText : IUndoRedoCmd
    {
        private string m_Text, m_Name;

        /*************************************************************
        * constructer: RestoreText(string cellText, string cellName)
        * Date Created: March 8, 2017
        * Date Last Modified: March 8, 2017
        * Description: Getter
        *************************************************************/
        public RestoreText(string cellText, string cellName)
        {
            m_Text = cellText; m_Name = cellName;
        }

        /*************************************************************
        * Name: Exec(Spreadsheet ssheet)
        * Date Created: March 8, 2017
        * Date Last Modified: March 8, 2017
        * Description: Restores old text for a cell
        *************************************************************/
        public IUndoRedoCmd Exec(Spreadsheet ssheet)
        {
            Cell cell = ssheet.GetCell(m_Name);

            string old = cell.Text;

            cell.Text = m_Text;

            RestoreText oldTextClass = new RestoreText(old, m_Name);

            return oldTextClass;
        }
    }
}
