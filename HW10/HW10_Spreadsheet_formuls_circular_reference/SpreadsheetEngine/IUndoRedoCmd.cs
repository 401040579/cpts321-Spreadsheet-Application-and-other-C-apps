using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    public interface IUndoRedoCmd
    {
        IUndoRedoCmd Exec(Spreadsheet ssheet);
    }

    public class multiCmds
    {
        private IUndoRedoCmd[] m_cmds;
        public string m_name;


        /*************************************************************
        * Constructor: multiCmds()
        * Date Created: March 3, 2017
        * Date Last Modified: March 3, 2017
        * Description: Constructor
        *************************************************************/
        public multiCmds() { }

        /*************************************************************
        * Constructor: multiCmds(IUndoRedoCmd[] commandObjects, string name)
        * Date Created: March 3, 2017
        * Date Last Modified: March 3, 2017
        * Description: Constructor with IUndoRedoCmd[] interface
        *************************************************************/
        public multiCmds(IUndoRedoCmd[] cmds, string name)
        {
            m_cmds = cmds; m_name = name;
        }

        /*************************************************************
        * Constructor: multiCmds(List<IUndoRedoCmd> commandObjects, string name)
        * Date Created: March 3, 2017
        * Date Last Modified: March 3, 2017
        * Description: Constructor with IUndoRedoCmd[] list
        *************************************************************/
        public multiCmds(List<IUndoRedoCmd> cmds, string name)
        {
            m_cmds = cmds.ToArray(); m_name = name;
        }

        /*************************************************************
        * Function: Exec(Spreadsheet sheet)
        * Date Created: March 3, 2017
        * Date Last Modified: March 3, 2017
        * Description: Calls each one in the multiCmds
        * Return: Class multiCmds
        *************************************************************/
        public multiCmds Exec(Spreadsheet sheet)
        {
            List<IUndoRedoCmd> cmd_list = new List<IUndoRedoCmd>();

            foreach (IUndoRedoCmd cmd in m_cmds)
            {
                IUndoRedoCmd doCmd = cmd.Exec(sheet);

                cmd_list.Add(doCmd);
            }

            multiCmds mulcmds = new multiCmds(cmd_list.ToArray(), m_name);

            return mulcmds;
        }
    }
    
    public class UndoRedoClass
    {
        private Stack<multiCmds> m_undos = new Stack<multiCmds>();  

        private Stack<multiCmds> m_redos = new Stack<multiCmds>();
        
        /*************************************************************
        * property: CanUndo
        * Date Created: March 3, 2017
        * Date Last Modified: March 3, 2017
        * Description: if undo stack empty, return false.
        *************************************************************/
        public bool CanUndo
        {
            get
            {
                return m_undos.Count != 0;
            }
        }

        /*************************************************************
        * property: CanRedo
        * Date Created: March 3, 2017
        * Date Last Modified: March 3, 2017
        * Description: if redo stack empty, return false.
        *************************************************************/
        public bool CanRedo
        {
            get
            {
                return m_redos.Count != 0;
            }
        }

        /*************************************************************
        * property: UndoNext
        * Date Created: March 3, 2017
        * Date Last Modified: March 3, 2017
        * Description: if canundo, return the action of this undo
        *************************************************************/
        public string UndoNext
        {
            get
            {
                if (CanUndo)    // Check if the undo stack is empty
                {
                    return m_undos.Peek().m_name;
                }
                return "";
            }
        }

        /*************************************************************
        * property: RedoNext
        * Date Created: March 3, 2017
        * Date Last Modified: March 3, 2017
        * Description: if canredo, return the action of this redo
        *************************************************************/
        public string RedoNext
        {
            get
            {
                if (CanRedo)    // Check if the redo stack is empty
                {
                    return m_redos.Peek().m_name;
                }
                return "";
            }
        }

        /*************************************************************
        * Function: AddUndos(multiCmds undos)
        * Date Created: March 3, 2017
        * Date Last Modified: March 3, 2017
        * Description: Adds an undo event to the undo stack
        * Return: NONE
        *************************************************************/
        public void AddUndos(multiCmds undo)
        {
            m_undos.Push(undo); m_redos.Clear();
        }

        /*************************************************************
        * Function: Undo(Spreadsheet ssheet)
        * Date Created: March 3, 2017
        * Date Last Modified: March 3, 2017
        * Description: do undo push in redo stack
        * Return: NONE
        *************************************************************/
        public void Undo(Spreadsheet ssheet)
        {
            m_redos.Push(m_undos.Pop().Exec(ssheet));
        }


        /*************************************************************
        * Function: Redo(Spreadsheet ssheet)
        * Date Created: March 3, 2017
        * Date Last Modified: March 3, 2017
        * Description: do redo push in undo stack
        * Return: NONE
        *************************************************************/
        public void Redo(Spreadsheet ssheet)
        {
            m_undos.Push(m_redos.Pop().Exec(ssheet));
        }

        /*************************************************************
        * Function: Clear()
        * Date Created: March 3, 2017
        * Date Last Modified: March 3, 2017
        * Description: clear redo undo stack
        * Return: NONE
        *************************************************************/
        public void Clear()
        {
            m_undos.Clear(); m_redos.Clear();
        }
    }
}
