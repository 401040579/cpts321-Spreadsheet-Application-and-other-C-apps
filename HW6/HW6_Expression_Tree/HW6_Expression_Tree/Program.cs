/*******************************************************************************************
* Programmer: Ran Tao
* ID: 11488080
* Class: CptS 321, Spring  2017
* Programming Assignment: PA 6 - Arithmetic Expression Trees (Part 1)
* Created: Feb 15, 2017
* Last Revised: Feb 21, 2017
* Cited: MSDN documentation, and lecture notes
*******************************************************************************************/
using CptS321;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW6_Expression_Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            ExpTree et = new ExpTree();
            string expression = "";
            while (true)
            {
                Console.WriteLine("Menu (current expression=\"" + expression + "\")");
                Console.WriteLine("  1 = Enter a new expression");
                Console.WriteLine("  2 = Set a variable value");
                Console.WriteLine("  3 = Evaluate tree");
                Console.WriteLine("  4 = Quit");
                Console.Write("Enter a number: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.Write("Enter new expression: ");
                        expression = Console.ReadLine();
                        et = new ExpTree(expression);
                        break;
                    case "2":
                        Console.Write("Enter variable name: ");
                        string varName = Console.ReadLine();
                        Console.Write("Enter variable value: ");
                        string varStr = Console.ReadLine();
                        double varValue = Convert.ToDouble(varStr);
                        et.SetVar(varName, varValue);
                        break;
                    case "3":
                        Console.WriteLine("Answer = " + et.Eval());
                        break;
                    case "4":
                        Console.WriteLine("Done");
                        System.Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
