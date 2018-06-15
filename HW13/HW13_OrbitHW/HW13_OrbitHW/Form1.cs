/*******************************************************************************************
* Programmer: Ran Tao
* ID: 11488080
* Class: CptS 321, Spring  2017
* Programming Assignment: PA 13 - Orbiting Planets Animation
* Created: April 18, 2017
* Last Revised: April 21, 2017
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

namespace HW13_OrbitHW
{
    public partial class Form1 : Form
    {
        List<Suns> suns;

        public Form1()
        {
            InitializeComponent();

            numericUpDown1.Value = 120;

            suns = new List<Suns>();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            Timer.Enabled = true;
            Timer.Interval = 20;
            Timer.Start();
        }
        //glo var
        private int mode = 0;

        /*************************************************************
        * Function: gravity_Radiobtn_CheckedChanged(object sender, EventArgs e)
        * Date Created: April 18, 2017
        * Date Last Modified: April 20, 2017
        * Description: when checked gravity, mode become to 0;
        * Return: NONE
        *************************************************************/
        private void gravity_Radiobtn_CheckedChanged(object sender, EventArgs e)
        {
            mode = 0;
        }

        /*************************************************************
        * Function: planet_radiobtn_CheckedChanged(object sender, EventArgs e)
        * Date Created: April 18, 2017
        * Date Last Modified: April 20, 2017
        * Description: when checked plante, mode become to 0;
        * Return: NONE
        *************************************************************/
        private void planet_radiobtn_CheckedChanged(object sender, EventArgs e)
        {
            mode = 1;
        }

        /*************************************************************
        * Function: pictureBox1_MouseClick(object sender, EventArgs e)
        * Date Created: April 18, 2017
        * Date Last Modified: April 20, 2017
        * Description: click the picturebox, when start draw and record the data
        * depends on which mode you are in.
        * Return: NONE
        *************************************************************/
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int gravity_redius = (int)numericUpDown1.Value;              

            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                if (mode == 0)
                {
                    if (isTouch(e.X, e.Y, gravity_redius) == false)
                    { 
                        //darw the suns and space
                        using (Brush b = new SolidBrush(ColorTranslator.FromHtml("#EFEFEF")))
                        {
                            g.FillEllipse(b,
                                new RectangleF(e.X - gravity_redius,
                                e.Y - gravity_redius,
                                gravity_redius * 2,
                                gravity_redius * 2));
                        }
                        // add sun into list
                        Suns s = new Suns(e.X, e.Y, gravity_redius);

                        if (s != null)
                        {
                            suns.Add(s);
                        }
                        //draw the block dot
                        using (Brush b = new SolidBrush(Color.Black))
                        {
                            g.FillEllipse(b,
                                new RectangleF(e.X - gravity_redius / 10,
                                e.Y - gravity_redius / 10,
                                gravity_redius / 5,
                                gravity_redius / 5));
                        }
                    }
                }
                else // mode == 1
                {
                    Planets planet = new Planets(e.X, e.Y);

                    if (planet != null)
                    {
                        using (Brush b = new SolidBrush(Color.Red))
                        {
                            //draw plante
                            bool isOutside = true;
                            foreach (Suns sun in suns)
                            {
                                if (Math.Sqrt(((sun.X - planet.x) * (sun.X - planet.x)) + ((sun.Y - planet.y) * (sun.Y - planet.y))) < sun.R - 10)
                                {
                                    //moveable plante
                                    sun.planets.Add(planet);
                                    break;
                                }

                                // is not outside
                                if (Math.Sqrt(((sun.X - planet.x) * (sun.X - planet.x)) + ((sun.Y - planet.y) * (sun.Y - planet.y))) < sun.R + 10)
                                {
                                    isOutside = false;
                                }
                            }

                            //avoid overlap with space
                            if (isOutside == true)
                            {
                                g.FillEllipse(b, new RectangleF(e.X - 10f, e.Y - 10f, 20f, 20f));
                            }
                        }
                    }
                }
                g.Dispose();

                pictureBox1.Invalidate();
            }
        }

        /*************************************************************
        * Function: timer1_Tick(object sender, EventArgs e)
        * Date Created: April 18, 2017
        * Date Last Modified: April 20, 2017
        * Description: using timer to redraw the picturebox
        * Return: NONE
        *************************************************************/
        private void timer1_Tick(object sender, EventArgs e)
        {
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                foreach (Suns s in suns)
                {
                    if (s != null)
                    {
                        //draw space
                        using (Brush b = new SolidBrush(ColorTranslator.FromHtml("#EFEFEF")))
                        {
                            g.FillEllipse(b,
                                new RectangleF((float)(s.X - s.R),
                                (float)(s.Y - s.R),
                                (float)s.R * 2,
                                (float)s.R * 2));
                        }

                        //block dot
                        using (Brush b = new SolidBrush(Color.Black))
                        {
                            g.FillEllipse(b,
                                new RectangleF((float)(s.X - s.R / 10),
                                (float)(s.Y - s.R / 10),
                                (float)s.R / 5,
                                (float)s.R / 5));
                        }
                    }

                    foreach (Planets p in s.planets)
                    {
                        if (p != null)
                        {
                            //calc the xy and radians and cos
                            using (Brush b = new SolidBrush(Color.Red))
                            {
                                double degrees = 1f;
                                double radians = degrees * (Math.PI / 180f);
                                double radius = Math.Sqrt((p.x - s.X) * (p.x - s.X) + (p.y - s.Y) * (p.y - s.Y));
                                double cosTheta = Math.Cos(radians);
                                double sinTheta = Math.Sin(radians);
                                double X = (double)(cosTheta * (p.x - s.X) - sinTheta * (p.y - s.Y) + s.X);
                                double Y = (double)(sinTheta * (p.x - s.X) + cosTheta * (p.y - s.Y) + s.Y);

                                p.x = X;
                                p.y = Y;

                                //draw plante
                                g.FillEllipse(b, new RectangleF((float)p.x - 10f, (float)p.y - 10f, 20f, 20f));
                            }
                        }
                    }
                }
                //Dispose the grapics object
                g.Dispose();
                //tell the picturebox that it needs updating
                pictureBox1.Invalidate();
            }
        }

        /*************************************************************
        * Function: isTouch(int x, int y)
        * Date Created: April 18, 2017
        * Date Last Modified: April 20, 2017
        * Description: each gravity can't touch each other.
        * Return: bool
        *************************************************************/
        private bool isTouch(int x, int y, int gravity_redius)
        {
            foreach (Suns sun in suns)
            {   
                if (Math.Sqrt(((sun.X - x) * (sun.X - x)) + ((sun.Y - y) * (sun.Y - y))) < (sun.R + gravity_redius))
                    return true;
            }
            return false;
        }
    }

    //sun class
    public class Suns
    {
        private double x;
        private double y;
        private double r;

        public double X
        {
            get { return x; }
            set { x = value; }
        }
        public double Y
        {
            get { return y; }
            set { y = value; }
        }
        public double R
        {
            get { return r; }
            set { r = value; }
        }

        public List<Planets> planets;

        public Suns(double x, double y, double r)
        {
            X = x;
            Y = y;
            R = r;
            planets = new List<Planets>();
        }
    }

    //plante class.
    public class Planets
    {
        public double x;
        public double y;
        public Planets(double X, double Y)
        {
            x = X;
            y = Y;
        }
    }
}