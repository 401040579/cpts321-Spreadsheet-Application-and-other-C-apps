namespace HW12_Threading_WinForm
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
            this.sorting_btn = new System.Windows.Forms.Button();
            this.sorting_textbox = new System.Windows.Forms.TextBox();
            this.URL_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.download_btn = new System.Windows.Forms.Button();
            this.download_textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // sorting_btn
            // 
            this.sorting_btn.Location = new System.Drawing.Point(13, 13);
            this.sorting_btn.Name = "sorting_btn";
            this.sorting_btn.Size = new System.Drawing.Size(409, 23);
            this.sorting_btn.TabIndex = 0;
            this.sorting_btn.Text = "Go (sorting)";
            this.sorting_btn.UseVisualStyleBackColor = true;
            this.sorting_btn.Click += new System.EventHandler(this.sorting_btn_Click);
            // 
            // sorting_textbox
            // 
            this.sorting_textbox.Location = new System.Drawing.Point(13, 43);
            this.sorting_textbox.Multiline = true;
            this.sorting_textbox.Name = "sorting_textbox";
            this.sorting_textbox.Size = new System.Drawing.Size(412, 574);
            this.sorting_textbox.TabIndex = 1;
            // 
            // URL_textBox
            // 
            this.URL_textBox.Location = new System.Drawing.Point(431, 25);
            this.URL_textBox.Name = "URL_textBox";
            this.URL_textBox.Size = new System.Drawing.Size(558, 20);
            this.URL_textBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(428, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "URL";
            // 
            // download_btn
            // 
            this.download_btn.Location = new System.Drawing.Point(431, 51);
            this.download_btn.Name = "download_btn";
            this.download_btn.Size = new System.Drawing.Size(558, 23);
            this.download_btn.TabIndex = 4;
            this.download_btn.Text = "Go (download string from URL)";
            this.download_btn.UseVisualStyleBackColor = true;
            this.download_btn.Click += new System.EventHandler(this.download_btn_Click);
            // 
            // download_textBox
            // 
            this.download_textBox.Location = new System.Drawing.Point(431, 80);
            this.download_textBox.Multiline = true;
            this.download_textBox.Name = "download_textBox";
            this.download_textBox.Size = new System.Drawing.Size(558, 537);
            this.download_textBox.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 629);
            this.Controls.Add(this.download_textBox);
            this.Controls.Add(this.download_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.URL_textBox);
            this.Controls.Add(this.sorting_textbox);
            this.Controls.Add(this.sorting_btn);
            this.Name = "Form1";
            this.Text = "Ran Tao 11488080";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sorting_btn;
        private System.Windows.Forms.TextBox sorting_textbox;
        private System.Windows.Forms.TextBox URL_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button download_btn;
        private System.Windows.Forms.TextBox download_textBox;
    }
}

