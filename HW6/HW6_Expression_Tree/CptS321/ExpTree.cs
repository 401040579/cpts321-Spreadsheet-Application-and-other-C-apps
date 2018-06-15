/*******************************************************************************************
* Programmer: Ran Tao
* ID: 11488080
* Class: CptS 321, Spring  2017
* Programming Assignment: PA 6 - Arithmetic Expression Trees (Part 1)
* Created: Feb 15, 2017
* Last Revised: Feb 21, 2017
* Cited: MSDN documentation, and lecture notes
*******************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptS321
{
    /*************************************************************
    * Class: ExpTree
    * Date Created: Feb 15, 2017
    * Date Last Modified: Feb 16, 2017
    * Description: Build the expression tree correctly internally
    *************************************************************/
    public class ExpTree
    {
        /*************************************************************
        * Class: Node
        * Date Created: Feb 15, 2017
        * Date Last Modified: Feb 16, 2017
        * Description:  Each node in the tree will be in one of three categories:
        *       o Node representing a constant numerical value
        *       o Node representing a variable
        *       o Node representing a binary operator
        *************************************************************/
        private abstract class Node
        {
            public abstract double Eval();
        }

        /*************************************************************
        * Class: ConstNode
        * Date Created: Feb 15, 2017
        * Date Last Modified: Feb 16, 2017
        * Description:  Only needs value
        *************************************************************/
        private class ConstNode : Node
        {
            private double m_value;

            public ConstNode(double value) { m_value = value; }

            public override double Eval() { return m_value; }
        }

        /*************************************************************
        * Class: OpNode
        * Date Created: Feb 15, 2017
        * Date Last Modified: Feb 16, 2017
        * Description:  OpNode need one char for op, and two Node for
        * left and right children.
        *************************************************************/
        private class OpNode : Node
        {
            private char m_op;
            private Node m_left, m_right;

            public OpNode(char op, Node left, Node right)
            {
                m_op = op;
                m_left = left;
                m_right = right;
            }

            /*************************************************************
            * Function: Eval()
            * Date Created: Feb 15, 2017
            * Date Last Modified: Feb 16, 2017
            * Description:  override Eval() to handle op.
            *************************************************************/
            public override double Eval()
            {
                double left = m_left.Eval();
                double right = m_right.Eval();

                switch (m_op)
                {
                    case '+': return left + right;

                    case '-': return left - right;

                    case '*': return left * right;

                    case '/': return left / right;
                }
                return 0;
            }
        }

        /*************************************************************
        * Class: VarNode
        * Date Created: Feb 15, 2017
        * Date Last Modified: Feb 21, 2017
        * Description:  contant string for varName, and a dictionary for
        * storing var.
        *************************************************************/
        private class VarNode : Node
        {
            private string m_varName;

            private Dictionary<string, double> m_lookup;

            public VarNode(string varNode)
            {
                m_varName = varNode;
                m_vars[m_varName] = 0;
            }

            public override double Eval()
            {
                m_lookup = m_vars;
                if (!m_lookup.ContainsKey(m_varName)) return double.NaN;
                return m_lookup[m_varName];
            }
        }

        private Node m_root;

        // a member dictionary
        private static Dictionary<string, double> m_vars = new Dictionary<string, double>();

        /*************************************************************
        * Function: SetVar(string varName, double varValue)
        * Date Created: Feb 15, 2017
        * Date Last Modified: Feb 16, 2017
        * Description: Sets the specified variable variable within 
        * the ExpTree variables dictionary
        * Return: NONE
        *************************************************************/
        public void SetVar(string varName, double varValue)
        {
            m_vars[varName] = varValue;
        }

        // constructor
        public ExpTree()
        {
        }

        /*************************************************************
        * Constructor: ExpTree(string expression)
        * Date Created: Feb 15, 2017
        * Date Last Modified: Feb 16, 2017
        * Description: Implement this constructor to construct the 
        * tree from the specific expression
        *************************************************************/
        public ExpTree(string expression)
        {
            m_root = Compile(expression);
        }

        /*************************************************************
        * Function: BuildSimple(string term)
        * Date Created: Feb 15, 2017
        * Date Last Modified: Feb 16, 2017
        * Description: string to int, but VarNode
        * Return: Node
        *************************************************************/
        private static Node BuildSimple(string term)
        {
            double num;
            if (double.TryParse(term, out num))
            {
                return new ConstNode(num);
            }
            return new VarNode(term);
        }

        /*************************************************************
        * Function: Compile(string exp)
        * Date Created: Feb 15, 2017
        * Date Last Modified: Feb 16, 2017
        * Description: Parse the expression that the user enters
        * Return: Node
        *************************************************************/
        private static Node Compile(string exp) //cant return a public type 
        {
            /*
            find first operator
            build parent operator node
            parent.left = buildsimple(beforeop char)
            parent.right = compile(after op char)
            return parent;                        
             */

            exp = exp.Replace(" ", "");

            // check for being entirely enclosed in ()
            // ((3+4+5))
            // (3+4)*(5+6)
            // if first char is '(' and last char matching ')', remove parens
            if (exp == "") return null;
            if (exp[0] == '(')
            {
                int counter = 1;
                for (int i = 1; i < exp.Length; i++)
                {
                    if (exp[i] == ')')
                    {
                        counter--;
                        if (counter == 0)
                        {
                            if (i == exp.Length - 1)
                            {
                                return Compile(exp.Substring(1, exp.Length - 2));
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    if (exp[i] == '(')
                    {
                        counter++;
                    }
                }
            }

            // get low op index
            // build op node for char at that index
            int index = GetLowOpIndex(exp);
            if (index != -1)
            {
                return new OpNode
                    (
                    exp[index],
                    Compile(exp.Substring(0, index)),
                    Compile(exp.Substring(index + 1))
                    );
            }
            return BuildSimple(exp);
        }

        /*************************************************************
        * Function: Eval()
        * Date Created: Feb 15, 2017
        * Date Last Modified: Feb 16, 2017
        * Description: Implement this member function with no parameters 
        * that evaluates the expression to a double value
        * Return: double
        *************************************************************/
        public double Eval()//ok
        {
            if (m_root != null) { return m_root.Eval(); }
            else
                return double.NaN;
        }

        /*************************************************************
        * Function: GetLowOpIndex(string exp)
        * Date Created: Feb 15, 2017
        * Date Last Modified: Feb 16, 2017
        * Description: Get Low Op Index
        * Return: int
        *************************************************************/
        private static int GetLowOpIndex(string exp)
        {
            // 3+4*5+6 3*4+5*6 3/4/5/6 3-4*5-6
            int parenCounter = 0;
            int index = -1;
            for (int i = exp.Length - 1; i >= 0; i--)
            {
                switch (exp[i])
                {
                    case ')':
                        parenCounter--;
                        break;
                    case '(':
                        parenCounter++;
                        break;
                    case '+':
                    case '-':
                        if (parenCounter == 0) return i;
                        break;
                    case '*':
                    case '/':
                        if (parenCounter == 0 && index == -1) index = i;
                        break;
                }
            }
            return index;
        }
    }
}
