using CptS321;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    public class RestoreBGColor : IUndoRedoCmd
    {
        private int m_Color;
        private string m_Name;

        /*************************************************************
        * constructer: RestoreBackColor(uint cellColor, string cellName)
        * Date Created: March 8, 2017
        * Date Last Modified: March 8, 2017
        * Description: Getter color and name
        *************************************************************/
        public RestoreBGColor(int cellColor, string cellName)
        {
            m_Color = cellColor;
            m_Name = cellName;
        }

        /*************************************************************
        * Name: Exec(Spreadsheet ssheet)
        * Date Created: March 8, 2017
        * Date Last Modified: March 8, 2017
        * Description: Restores old color for a cell
        *************************************************************/
        public IUndoRedoCmd Exec(Spreadsheet ssheet)
        {
            Cell cell = ssheet.GetCell(m_Name);

            int old = (int)cell.BGColor;

            cell.BGColor = (uint) m_Color;

            RestoreBGColor oldBGClass = new RestoreBGColor(old, m_Name);

            return oldBGClass;
        }
    }
}
