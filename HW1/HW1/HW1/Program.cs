/*******************************************************************************************
* Programmer: Ran Tao
* ID: 11488080
* Class: CptS 321, Spring  2017
* Programming Assignment: PA 1 - BST Number List in Console/Terminal
* Created: Jan 17, 2017
* Last Revised: Jan 17, 2017
* Cited: https://msdn.microsoft.com/en-us/library/ms379572(v=vs.80).aspx
*******************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    class Program
    {
        // BST's node
        public class Node
        {
            public int data;
            public Node left; // left child
            public Node right; // right child
            public void DisplayData()
            {
                Console.Write(data + " ");
            }
        }
        // BST
        public class BinarySearchTree
        {
            public Node rootNode = null;

            public void insert(int data)
            {
                Node parent;

                //packaging data into the node
                Node newNode = new Node();
                newNode.data = data;

                //root node
                if (rootNode == null)
                {
                    rootNode = newNode;
                }
                //finding the right child node insert
                else
                {
                    Node currentNode = rootNode;
                    while (true)
                    {
                        parent = currentNode;
                        if (newNode.data < currentNode.data)
                        {
                            currentNode = currentNode.left;
                            if (currentNode == null)
                            {
                                parent.left = newNode;
                                break;
                            }
                        }
                        else if (newNode.data > currentNode.data)
                        {
                            currentNode = currentNode.right;
                            if (currentNode == null)
                            {
                                parent.right = newNode;
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            /*************************************************************
            * Function: InOrder(Node theRoot)
            * Date Created: Jan 17, 2017
            * Date Last Modified: Jan 17, 2017
            * Description: Traverse the tree in order to produce this output.
            * Return: None
            *************************************************************/
            public void InOrder(Node theRoot)
            {
                if (theRoot != null)
                {
                    InOrder(theRoot.left);
                    theRoot.DisplayData();
                    InOrder(theRoot.right);
                }
            }

            /*************************************************************
            * Function: public int countItems(Node root)
            * Date Created: Jan 17, 2017
            * Date Last Modified: Jan 17, 2017
            * Description: Number of items (note that this will be less than or equal to the number of items
            * entered by the user, since duplicates won’t be added to the tree). Write a function that
            * determines this from your BST, NOT the array returned from the split. In other words,
            * you must have a Count function in your BST implementation.
            * Return: int
            *************************************************************/
            public int countItems(Node root)
            {
                int left, right;

                if (root == null)
                    return 0;
                else
                {
                    left = countItems(root.left); 
                    right = countItems(root.right); 
                    return right + left + 1;
                }
            }

            /*************************************************************
            * Function: public int calc_tree_level(Node root)
            * Date Created: Jan 17, 2017
            * Date Last Modified: Jan 17, 2017
            * Description: Number of levels in the tree. A tree with no nodes at all has 0 levels. A tree with a single
            * node has 1 level. A tree with 2 nodes has 2 levels. A tree with three nodes could have 2
            * or 3 levels. You should know why this is from your advanced data structures
            * prerequisite course.
            * Return: int
            *************************************************************/
            public int calcTreeLevel(Node root)
            {
                int left, right;

                if (root == null)
                    return 0; // recursive export, empty layer node number is 0
                else
                {
                    left = 1 + calcTreeLevel(root.left); // on his return from the left of the layer
                    right = 1 + calcTreeLevel(root.right); // up from right back layer
                    if (left > right) return left;
                    else return right;
                }
            }
        }

        /*************************************************************
        * Function: run()
        * Date Created: Jan 17, 2017
        * Date Last Modified: Jan 17, 2017
        * Description: run the program
        * Return: int
        *************************************************************/
        public void run()
        {
            Console.WriteLine("Enter a collection of numbers in the range [0, 100], separated by spaces: ");
            string s = Console.ReadLine();
            string[] sArray = s.Split();
            BinarySearchTree bst = new BinarySearchTree();

            foreach (string i in sArray)
            {
                int num = int.Parse(i);
                bst.insert(num);
            }
            Console.Write("Tree contents: ");
            bst.InOrder(bst.rootNode);

            Console.WriteLine();

            Console.WriteLine("Tree statistics:");
            Console.WriteLine("  Number of nodes: " + bst.countItems(bst.rootNode));
            Console.WriteLine("  Tree levels: " + bst.calcTreeLevel(bst.rootNode));

            //Theoretical minimum number of levels that the tree could have given the number of nodes it contains
            Console.WriteLine("  Minimum number of levels that a tree with " + bst.countItems(bst.rootNode) + 
                " nodes could have = " + Math.Ceiling(Math.Log(bst.countItems(bst.rootNode) + 1, 2)));
            Console.WriteLine("Done");

        }
        
        static void Main(string[] args)
        {
            Program p = new Program();
            p.run();
        }
    }
}
