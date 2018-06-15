/*******************************************************************************************
* Programmer: Ran Tao
* ID: 11488080
* Class: CptS 321, Spring  2017
* Programming Assignment: PA 12 - Threading
* Created: April 12, 2017
* Last Revised: April 14, 2017
* Cited: MSDN documentation
*******************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW12_Threading_WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            URL_textBox.Text = "http://www.google.com";
        }

        // globe vars
        string url, result;
        List<List<int>> single_thread_list = new List<List<int>>();
        List<List<int>> mutil_thread_list = new List<List<int>>();
        public delegate void DoDelegate(object o);

        /*************************************************************
        * Function: download_btn_Click(object sender, EventArgs e)
        * Date Created: April 12, 2017
        * Date Last Modified: April 13, 2017
        * Description: Click the download button
        * Return: NONE
        *************************************************************/
        private void download_btn_Click(object sender, EventArgs e)
        {
            download_textBox.Clear();
            download_textBox.Text = "[NOTICE] IF THE WEB STRING IS TOOOOO LONG, SETTING THE TEXT WILL CAUSE LAGGING.\r\n\r\n";
            url = URL_textBox.Text;

            isEnabled_download(false);

            Thread download = new Thread(download_Web);
            download.Start();
        }

        /*************************************************************
        * Function: download_Web()
        * Date Created: April 12, 2017
        * Date Last Modified: April 13, 2017
        * Description: do the downloading thread
        * Return: NONE
        *************************************************************/
        void download_Web()
        {
            if (url.Length <= 7)
            {
                result = "Please enter http:// at the front.";
            }
            else if (url.Substring(0, 7) == "http://")
            {
                using (var webClient = new System.Net.WebClient())     
                {
                    try
                    {
                        result = webClient.DownloadString(url);
                    }
                    catch
                    {
                        result = "The website is not responding, plz re-enter url.";
                    }
                }
            }
            else
            {
                result = "Please enter a valid URL - enter http:// at the front.";
            }
            
            downloadOutput(result);
            isEnabled_download(true);
        }

        /*************************************************************
        * Function: isEnabled_download(bool bo)
        * Date Created: April 12, 2017
        * Date Last Modified: April 13, 2017
        * Description: set the display depend the downloading finshing or not
        * Return: NONE
        *************************************************************/
        private void isEnabled_download(bool bo)
        {
            if (download_btn.InvokeRequired || URL_textBox.InvokeRequired || download_textBox.InvokeRequired)
            {
                download_btn.Invoke(new MethodInvoker(() => isEnabled_download(bo)));
                URL_textBox.Invoke(new MethodInvoker(() => isEnabled_download(bo)));
                download_textBox.Invoke(new MethodInvoker(() => isEnabled_download(bo)));
            }
            else
            {
                download_btn.Enabled = bo;
                URL_textBox.Enabled = bo;
                download_textBox.Enabled = bo;
            }
        }

        /*************************************************************
        * Function: downloadOutput(object obj)
        * Date Created: April 12, 2017
        * Date Last Modified: April 13, 2017
        * Description: invoke the message
        * Return: NONE
        *************************************************************/
        private void downloadOutput(object obj)
        {
            if (InvokeRequired)
            {
                DoDelegate method = new DoDelegate(downloadOutput);
                Invoke(method, obj);
                return;
            }

            string text = (string)obj;
            download_textBox.Text += text + "\r\n";
        }

        /*************************************************************
        * Function: sorting_btn_Click(object sender, EventArgs e)
        * Date Created: April 12, 2017
        * Date Last Modified: April 13, 2017
        * Description: click the sort button
        * Return: NONE
        *************************************************************/
        private void sorting_btn_Click(object sender, EventArgs e)
        {
            sorting_textbox.Clear();
            isEnabled_sort(false);
            single_thread_list = new List<List<int>>();
            mutil_thread_list = new List<List<int>>();

            Thread sort = new Thread(sortingList);
            sort.Start();
        }

        /*************************************************************
        * Function: sortingList(object o)
        * Date Created: April 12, 2017
        * Date Last Modified: April 14, 2017
        * Description: do the sorting thread
        * Return: NONE
        *************************************************************/
        void sortingList(object o)
        {
            DoDelegate sort = new DoDelegate(sortingOutput);

            Random randnum = new Random();

            for (int x = 0; x < 8; x++)
            {
                List<int> sublist = new List<int>();
                List<int> sublist2 = new List<int>();
                for (int y = 0; y < 1000000; y++)
                {
                    sublist.Add(randnum.Next(0, 1000000));
                    sublist2.Add(randnum.Next(0, 1000000));
                }

                single_thread_list.Add(sublist);
                mutil_thread_list.Add(sublist2);
            }

            DateTime start = DateTime.Now;

            foreach (var sub in single_thread_list)
            {
                sub.Sort();
            }
            DateTime end = DateTime.Now;

            string singleThread = "Single-threaded time: " + (int)timeDiff(start, end) + " ms";
            sort.Invoke(singleThread);

            Thread multisorting = new Thread(multiSort);
            multisorting.Start();
        }

        /*************************************************************
        * Function: multiSort()
        * Date Created: April 12, 2017
        * Date Last Modified: April 14, 2017
        * Description: multisorting thread
        * Return: NONE
        *************************************************************/
        private void multiSort()
        {
            DoDelegate multisorting = new DoDelegate(sortingOutput);
            List<Thread> list_threads = new List<Thread>();

            DateTime start = DateTime.Now;
            
            for (int i = 0; i < 8; i++)
            {
                // using ParameterizedThreadStart is a way to reliably get a sorted list
                Thread t = new Thread(new ParameterizedThreadStart(sorti));
                list_threads.Add(t);
                t.Start(i);
            }

            //if put end here is roughly 4 (4 core cpu) time faster than the single.
            //making interface more reasonable. and it turely sort all list.
            DateTime end = DateTime.Now;
            
            //it reliably do all 8 threads.
            foreach (Thread t in list_threads)
            {
                t.Join();
            }

            //DateTime end = DateTime.Now;
            //but if put end here the time will increase, since join will pause the main thread.
            //make sure all thread are done. but it will cause more time to do. and only less item may
            //unsorted at this point. we may assume the list already sorted.

            //determine when 8 threads complete
            int count = 0;
            foreach (var sub in mutil_thread_list)
            {
                count++;
                for (int i = 0; i < sub.Count - 1; i++)
                {
                    if (sub[i] > sub[i + 1])
                    {
                        multisorting.Invoke("list " + count);
                        multisorting.Invoke("not sorted " + sub[i] + " -> " + sub[i+1]);
                    }
                }
            }

            string MultiThread = "Multi-threaded time: " + (int)timeDiff(start, end) + " ms";
            multisorting.Invoke(MultiThread);
            isEnabled_sort(true);
        }
        
        /*************************************************************
        * Function: sorti(int i)
        * Date Created: April 12, 2017
        * Date Last Modified: April 13, 2017
        * Description: sort one list per thread
        * Return: NONE
        *************************************************************/
        void sorti(object i)
        {
            mutil_thread_list[(int)i].Sort();
        }

        /*************************************************************
        * Function: timeDiff(DateTime d1, DateTime d2)
        * Date Created: April 12, 2017
        * Date Last Modified: April 13, 2017
        * Description: cal the diff time
        * Return: double
        *************************************************************/
        private double timeDiff(DateTime d1, DateTime d2)
        {
            double timeDiff = 0.0;
            TimeSpan ts1 = new TimeSpan(d1.Ticks);
            TimeSpan ts2 = new TimeSpan(d2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            timeDiff = ts.TotalMilliseconds;
            return timeDiff;
        }      

        /*************************************************************
        * Function: sortingOutput(object obj)
        * Date Created: April 12, 2017
        * Date Last Modified: April 13, 2017
        * Description: invoke sorting output
        * Return: NONE
        *************************************************************/
        private void sortingOutput(object obj)
        {
            if (InvokeRequired)
            {
                DoDelegate method = new DoDelegate(sortingOutput);

                Invoke(method, obj);

                return;
            }

            string text = obj.ToString();
            sorting_textbox.Text += text + "\r\n";
        }

        /*************************************************************
        * Function: isEnabled_sort(bool bo)
        * Date Created: April 12, 2017
        * Date Last Modified: April 13, 2017
        * Description: set the display depend the sorting finshing or not
        * Return: NONE
        *************************************************************/
        private void isEnabled_sort(bool bo)
        {
            if (sorting_btn.InvokeRequired || sorting_textbox.InvokeRequired)
            {
                sorting_btn.Invoke(new MethodInvoker(() => isEnabled_sort(bo)));
                sorting_textbox.Invoke(new MethodInvoker(() => isEnabled_sort(bo)));
            }
            else
            {
                sorting_btn.Enabled = bo;
                sorting_textbox.Enabled = bo;
            }
        }
    }
}