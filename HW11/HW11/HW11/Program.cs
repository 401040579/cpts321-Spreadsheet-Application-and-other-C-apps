/*******************************************************************************************
* Programmer: Ran Tao
* ID: 11488080
* Class: CptS 321, Spring  2017
* Programming Assignment: PA 11 - In-Order BST Traversal
* Created: Feb 8, 2017
* Last Revised: March 3, 2017
* Cited: MSDN documentation
*******************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW11
{
    class Program
    {
        /*************************************************************
        * Class: BSTNode
        * Date Created: April 4, 2017
        * Date Last Modified: April 5, 2017
        * Description: a node class of a bst tree
        *************************************************************/
        class BSTNode
        {
            public int data;
            public BSTNode left;
            public BSTNode right;

            public BSTNode(int val)
            {
                data = val;
                left = null;
                right = null;
            }
        }

        /*************************************************************
        * Class: BinartSearchTree
        * Date Created: April 4, 2017
        * Date Last Modified: April 5, 2017
        * Description: a bst tree class with some function
        *************************************************************/
        class BinartSearchTree
        {
            public BSTNode root;
            public static int count;

            public BinartSearchTree()
            {
                root = null;
            }

            public BSTNode addNode(int val)
            {
                BSTNode temp = new BSTNode(val);
                if (root == null)
                {
                    root = temp;
                }
                count++;

                return temp;
            }

            public void insert(BSTNode root, BSTNode newNode)
            {
                while (root != null)
                {
                    if (newNode.data > root.data)
                    {
                        if (root.right == null)
                        {
                            root.right = newNode;
                            break;
                        }
                        root = root.right;
                    }
                    else
                    {
                        if (root.left == null)
                        {
                            root.left = newNode;
                            break;
                        }
                        root = root.left;
                    }
                }
            }

            public bool search(BSTNode root, int x)
            {
                if (root == null)           
                {
                    return false;
                }
                else if (root.data == x)    
                {
                    return true;
                }
                else if (root.data > x)
                {   
                    return search(root.left, x);
                }
                else                        
                    return search(root.right, x);
            }


            /*************************************************************
            * Function: In_Order_Traversal_rec(BSTNode root)
            * Date Created: April 4, 2017
            * Date Last Modified: April 5, 2017
            * Description: in order traversal by using recursion
            * Return: NONE
            *************************************************************/
            public void In_Order_Traversal_rec(BSTNode root)
            {
                if (root != null)
                {
                    In_Order_Traversal_rec(root.left);
                    Console.Write(root.data + " ");
                    In_Order_Traversal_rec(root.right);
                }
            }

            /*************************************************************
            * Function: In_Order_Traversal_rec(BSTNode root)
            * Date Created: April 4, 2017
            * Date Last Modified: April 5, 2017
            * Description: In-order traversal without using recursion 
            *   and instead using a single Stack<T> data structure
            * Return: NONE
            *************************************************************/
            public void In_Order_Traversal_Stack(BSTNode root)
            {
                var nodes = new Stack<BSTNode>();
                BSTNode node = root;
                bool iAmLeftest = false;
                while (true)
                {
                    if (node == null)
                    {
                        if (nodes.Count == 0) { break; }
                        node = nodes.Pop();
                        iAmLeftest = true;
                    }

                    if (node.left == null || iAmLeftest == true)
                    {
                        iAmLeftest = false;
                        Console.Write(node.data + " ");
                        node = node.right;
                    }
                    else
                    {
                        nodes.Push(node);
                        node = node.left;
                    }
                }
            }

            /*************************************************************
            * Function: In_Order_Traversal_No_Stack(BSTNode root)
            * Date Created: April 4, 2017
            * Date Last Modified: April 5, 2017
            * Description: In-order traversal without using recursion 
            * and without using a stack. with O(1) space and O(n) time
            * Return: NONE
            *************************************************************/
            public void In_Order_Traversal_No_Stack(BSTNode root)
            {
                BSTNode current_node = root;
                BSTNode pre_node = null;

                while (current_node != null)
                {
                    if (current_node.left == null)
                    {
                        Console.Write(current_node.data + " ");
                        current_node = current_node.right;
                    }
                    else
                    {
                        pre_node = current_node.left;
                        while (pre_node.right != null && pre_node.right != current_node)
                        {
                            pre_node = pre_node.right;
                        }

                        if (pre_node.right == null)
                        {
                            pre_node.right = current_node;
                            current_node = current_node.left;
                        }
                        else
                        {
                            pre_node.right = null;
                            Console.Write(current_node.data + " ");
                            current_node = current_node.right;
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            int i = 0;

            string input = "";

            while (true)
            {
                Random num = new Random();
                BinartSearchTree bst = new BinartSearchTree();

                while (i < 25)
                {
                    int val = num.Next(0, 100);

                    if (!bst.search(bst.root, val))
                    {
                        bst.insert(bst.root, bst.addNode(val));
                        i++;
                    }
                }

                Console.Write("\nTraversal of the tree with NO stack and NO recursion: \n");
                bst.In_Order_Traversal_No_Stack(bst.root);

                Console.Write("\nTraversal of the tree using a stack but no recursion: \n");
                bst.In_Order_Traversal_Stack(bst.root);

                Console.Write("\nTraversal of tree using recursion: \n");
                bst.In_Order_Traversal_rec(bst.root);    

                Console.WriteLine("\nAggain (y/n)?");
                input = Console.ReadLine();               

                while (input != "n" && input != "y")
                {
                    Console.WriteLine("Invalid input");
                    Console.WriteLine("Aggain (y/n)?");
                    input = Console.ReadLine();
                }

                if (input == "n") break;

                bst = null;
                i = 0;
            }
        }
    }
}