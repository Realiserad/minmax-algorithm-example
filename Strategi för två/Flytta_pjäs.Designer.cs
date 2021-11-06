namespace Strategi_för_två
{
    partial class Flytta_pjäs
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Drag:";
            // 
            // textBox1
            // 
            this.textBox1.AutoCompleteCustomSource.AddRange(new string[] {
            "a1-a2",
            "a2-a3",
            "a3-a4",
            "a4-a5",
            "a5-a4",
            "a4-a3",
            "a3-a2",
            "a2-a1",
            "b1-b2",
            "b2-b3",
            "b3-b4",
            "b4-b5",
            "b5-b4",
            "b4-b3",
            "b3-b2",
            "b2-b1",
            "c1-c2",
            "c2-c3",
            "c3-c4",
            "c4-c5",
            "c5-c4",
            "c4-c3",
            "c3-c2",
            "c2-c1",
            "d1-d2",
            "d2-d3",
            "d3-d4",
            "d4-d5",
            "d5-d4",
            "d4-d3",
            "d3-d2",
            "d2-d1",
            "a1-b1",
            "b1-c1",
            "c1-d1",
            "d1-c1",
            "c1-b1",
            "b1-a1",
            "a2-b2",
            "b2-c2",
            "c2-d2",
            "d2-c2",
            "c2-b2",
            "b2-a2",
            "a3-b3",
            "b3-c3",
            "c3-d3",
            "d3-c3",
            "c3-b3",
            "b3-a3",
            "a4-b4",
            "b4-c4",
            "c4-d4",
            "d4-c4",
            "c4-b4",
            "b4-a4",
            "a5-b5",
            "b5-c5",
            "c5-d5",
            "d5-c5",
            "c5-b5",
            "b5-a5"});
            this.textBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(51, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(12, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 25);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Flytta_pjäs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(172, 88);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Flytta_pjäs";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Flytta pjäs...";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}