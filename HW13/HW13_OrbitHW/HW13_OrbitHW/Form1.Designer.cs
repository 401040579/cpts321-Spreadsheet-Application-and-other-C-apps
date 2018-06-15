using System;

namespace HW13_OrbitHW
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.planet_radiobtn = new System.Windows.Forms.RadioButton();
            this.gravity_Radiobtn = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(13, 109);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(631, 546);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // planet_radiobtn
            // 
            this.planet_radiobtn.AutoSize = true;
            this.planet_radiobtn.Location = new System.Drawing.Point(36, 86);
            this.planet_radiobtn.Name = "planet_radiobtn";
            this.planet_radiobtn.Size = new System.Drawing.Size(125, 17);
            this.planet_radiobtn.TabIndex = 2;
            this.planet_radiobtn.TabStop = true;
            this.planet_radiobtn.Text = "Click to create planet";
            this.planet_radiobtn.UseVisualStyleBackColor = true;
            this.planet_radiobtn.CheckedChanged += new System.EventHandler(this.planet_radiobtn_CheckedChanged);
            // 
            // gravity_Radiobtn
            // 
            this.gravity_Radiobtn.AutoSize = true;
            this.gravity_Radiobtn.Location = new System.Drawing.Point(36, 63);
            this.gravity_Radiobtn.Name = "gravity_Radiobtn";
            this.gravity_Radiobtn.Size = new System.Drawing.Size(235, 17);
            this.gravity_Radiobtn.TabIndex = 1;
            this.gravity_Radiobtn.TabStop = true;
            this.gravity_Radiobtn.Text = "Click to create center of gravity (No Overlap)";
            this.gravity_Radiobtn.UseVisualStyleBackColor = true;
            this.gravity_Radiobtn.CheckedChanged += new System.EventHandler(this.gravity_Radiobtn_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Center of gravity radius (pixels):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Simulation Parameters";
            // 
            // Timer
            // 
            this.Timer.Enabled = true;
            this.Timer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(201, 31);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(441, 20);
            this.numericUpDown1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 667);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gravity_Radiobtn);
            this.Controls.Add(this.planet_radiobtn);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Ran Tao 11488080";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton planet_radiobtn;
        private System.Windows.Forms.RadioButton gravity_Radiobtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer Timer;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}

