/*******************************************************************************************
* Programmer: Ran Tao
* ID: 11488080
* Class: CptS 321, Spring  2017
* Programming Assignment: PA 14 - Trie_Prefix_Auto_Complete
* Created: April 23, 2017
* Last Revised: April 23, 2017
* Cited: MSDN documentation
*******************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW14_Trie_Prefix_Auto_Complete
{
    public partial class Form1 : Form
    {
        Trie Trie = new Trie();

        public Form1()
        {
            InitializeComponent();
            
            try
            {
                // using Application.StartupPath.Substring(0, Application.StartupPath.Length - 9)
                // in order to find the wordsEn.txt in the Project folder, not debug folder
                using (StreamReader sr = File.OpenText(Application.StartupPath.Substring(0, Application.StartupPath.Length - 9) + "wordsEn.txt"))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Trie.Addstring(s);
                    }
                }
                return;
            }
            catch
            {
                MessageBox.Show("Error: can't not load wordsEn.txt in HW14_Trie_Prefix_Auto-Complete project folder");
                MessageBox.Show("trying in ~/bin/debug/ folder");
            }

            try
            {
                using (StreamReader sr = File.OpenText("wordsEn.txt"))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Trie.Addstring(s);
                    }
                }
                return;
            }
            catch
            {
                MessageBox.Show("Error: can't not load wordsEn.txt in ~/bin/debug/");
                MessageBox.Show("Error: Done");
                textBox1.Text = "can't not load wordsEn.txt in any folder, please copy one manually.";
            }
        }

        /*************************************************************
        * Function: textBox1_TextChanged(object sender, EventArgs e)
        * Date Created: April 23, 2017
        * Date Last Modified: April 23, 2017
        * Description: input a words
        * Return: NONE
        *************************************************************/
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            string input_string = "";
            input_string = textBox1.Text;

            List<string> words = new List<string>();

            if (string.IsNullOrEmpty(input_string))
            {
                return;
            }
            
            search(input_string, words);

            listBox1.Items.AddRange(words.ToArray());
        }

        /*************************************************************
        * Function: search(string input_prefix_string, List<string> words)
        * Date Created: April 23, 2017
        * Date Last Modified: April 23, 2017
        * Description: search the input word.
        * Return: NONE
        *************************************************************/
        public void search(string input_prefix_string, List<string> words)
        {
            if ((string.IsNullOrEmpty(input_prefix_string))) return;

            char[] input_prefix_char = input_prefix_string.ToArray();

            Trie_Node cur = Trie.m_root;
            Trie_Node child = null;

            foreach(char c in input_prefix_char)
            {
                if(cur.children.Exists(x => x.c == c))
                {
                    child = cur.AddOrGetChild(c);
                    cur = child;
                }
                else
                {
                    words.Add("can't find the word");
                    return;
                }
            }
            //words.Add("success");

            insertString_rec(cur, input_prefix_string.Substring(0,input_prefix_string.Length-1), words);
        }

        /*************************************************************
        * Function: insertString_rec(Trie_Node node, string sub_string, List<string> words)
        * Date Created: April 23, 2017
        * Date Last Modified: April 23, 2017
        * Description: using rec to process the children node.
        * Return: NONE
        *************************************************************/
        private void insertString_rec(Trie_Node node, string sub_string, List<string> words)
        {
            if (node == null)
            {
                return;
            }

            sub_string = sub_string + node.c;

            if (node.c == '\0')
            {
                words.Add(sub_string);
            }

            foreach (Trie_Node n in node.children)
            {
                insertString_rec(n, sub_string, words);
            }
        }
    }

    //node class
    public class Trie_Node
    {
        public char c;
        public List<Trie_Node> children;

        public Trie_Node(char cc)
        {
            c = cc;
            children = new List<Trie_Node>();
        }

        public Trie_Node AddOrGetChild(char cc)
        {
            foreach(Trie_Node n in children)
            {
                if (n.c == cc) return n;
            }

            Trie_Node node = new Trie_Node(cc);
            children.Add(node);
            return node;
        }
    }

    //tree class
    public class Trie
    {
        public Trie_Node m_root = new Trie_Node('\0');

        public void Addstring(string s)
        {
            Trie_Node n = m_root;
            foreach(char c in s)
            {
                n = n.AddOrGetChild(c);
            }
            n = n.AddOrGetChild('\0');
        }
    }
}
