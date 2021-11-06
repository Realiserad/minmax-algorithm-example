namespace Strategi_för_två
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sparaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.avslutaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGame = new System.Windows.Forms.ToolStripMenuItem();
            this.låsUppBrädetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spelaMotDatornToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medEnVänToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.funktionerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bakåtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.framåtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bytBrädeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flyttaPjäsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visaDragföljdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ledtrådToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.omToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hearts4 = new System.Windows.Forms.PictureBox();
            this.hearts3 = new System.Windows.Forms.PictureBox();
            this.hearts2 = new System.Windows.Forms.PictureBox();
            this.hearts1 = new System.Windows.Forms.PictureBox();
            this.spades4 = new System.Windows.Forms.PictureBox();
            this.spades3 = new System.Windows.Forms.PictureBox();
            this.spades2 = new System.Windows.Forms.PictureBox();
            this.spades1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.bytSidaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hearts4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hearts3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hearts2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hearts1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spades4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spades3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spades2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spades1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.gameToolStripMenuItem,
            this.funktionerToolStripMenuItem,
            this.helpToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(400, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.sparaToolStripMenuItem,
            this.avslutaToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.fileToolStripMenuItem.Text = "Arkiv";
            this.fileToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Öppna...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // sparaToolStripMenuItem
            // 
            this.sparaToolStripMenuItem.Name = "sparaToolStripMenuItem";
            this.sparaToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sparaToolStripMenuItem.Text = "Spara...";
            this.sparaToolStripMenuItem.Click += new System.EventHandler(this.sparaToolStripMenuItem_Click);
            // 
            // avslutaToolStripMenuItem
            // 
            this.avslutaToolStripMenuItem.Name = "avslutaToolStripMenuItem";
            this.avslutaToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.avslutaToolStripMenuItem.Text = "Avsluta";
            this.avslutaToolStripMenuItem.Click += new System.EventHandler(this.avslutaToolStripMenuItem_Click);
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGame,
            this.låsUppBrädetToolStripMenuItem,
            this.spelaMotDatornToolStripMenuItem,
            this.medEnVänToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.gameToolStripMenuItem.Text = "Spela";
            // 
            // newGame
            // 
            this.newGame.Name = "newGame";
            this.newGame.Size = new System.Drawing.Size(152, 22);
            this.newGame.Text = "Nytt spel";
            this.newGame.Click += new System.EventHandler(this.newGame_Click);
            // 
            // låsUppBrädetToolStripMenuItem
            // 
            this.låsUppBrädetToolStripMenuItem.Name = "låsUppBrädetToolStripMenuItem";
            this.låsUppBrädetToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.låsUppBrädetToolStripMenuItem.Text = "Lås upp brädet";
            this.låsUppBrädetToolStripMenuItem.Click += new System.EventHandler(this.låsUppBrädetToolStripMenuItem_Click);
            // 
            // spelaMotDatornToolStripMenuItem
            // 
            this.spelaMotDatornToolStripMenuItem.CheckOnClick = true;
            this.spelaMotDatornToolStripMenuItem.Name = "spelaMotDatornToolStripMenuItem";
            this.spelaMotDatornToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.spelaMotDatornToolStripMenuItem.Text = "Mot datorn";
            this.spelaMotDatornToolStripMenuItem.Click += new System.EventHandler(this.spelaMotDatornToolStripMenuItem_Click);
            // 
            // medEnVänToolStripMenuItem
            // 
            this.medEnVänToolStripMenuItem.Checked = true;
            this.medEnVänToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.medEnVänToolStripMenuItem.Name = "medEnVänToolStripMenuItem";
            this.medEnVänToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.medEnVänToolStripMenuItem.Text = "Med en vän";
            this.medEnVänToolStripMenuItem.Click += new System.EventHandler(this.medEnVänToolStripMenuItem_Click);
            // 
            // funktionerToolStripMenuItem
            // 
            this.funktionerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bakåtToolStripMenuItem,
            this.framåtToolStripMenuItem,
            this.bytSidaToolStripMenuItem,
            this.bytBrädeToolStripMenuItem,
            this.flyttaPjäsToolStripMenuItem,
            this.visaDragföljdToolStripMenuItem});
            this.funktionerToolStripMenuItem.Name = "funktionerToolStripMenuItem";
            this.funktionerToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.funktionerToolStripMenuItem.Text = "Funktioner";
            // 
            // bakåtToolStripMenuItem
            // 
            this.bakåtToolStripMenuItem.Enabled = false;
            this.bakåtToolStripMenuItem.Name = "bakåtToolStripMenuItem";
            this.bakåtToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.bakåtToolStripMenuItem.Text = "Bakåt";
            this.bakåtToolStripMenuItem.Click += new System.EventHandler(this.bakåtToolStripMenuItem_Click);
            // 
            // framåtToolStripMenuItem
            // 
            this.framåtToolStripMenuItem.Enabled = false;
            this.framåtToolStripMenuItem.Name = "framåtToolStripMenuItem";
            this.framåtToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.framåtToolStripMenuItem.Text = "Framåt";
            this.framåtToolStripMenuItem.Click += new System.EventHandler(this.framåtToolStripMenuItem_Click);
            // 
            // bytBrädeToolStripMenuItem
            // 
            this.bytBrädeToolStripMenuItem.Name = "bytBrädeToolStripMenuItem";
            this.bytBrädeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.bytBrädeToolStripMenuItem.Text = "Byt bräde...";
            this.bytBrädeToolStripMenuItem.Click += new System.EventHandler(this.bytBrädeToolStripMenuItem_Click);
            // 
            // flyttaPjäsToolStripMenuItem
            // 
            this.flyttaPjäsToolStripMenuItem.Name = "flyttaPjäsToolStripMenuItem";
            this.flyttaPjäsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.flyttaPjäsToolStripMenuItem.Text = "Flytta pjäs...";
            this.flyttaPjäsToolStripMenuItem.Click += new System.EventHandler(this.flyttaPjäsToolStripMenuItem_Click);
            // 
            // visaDragföljdToolStripMenuItem
            // 
            this.visaDragföljdToolStripMenuItem.Name = "visaDragföljdToolStripMenuItem";
            this.visaDragföljdToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.visaDragföljdToolStripMenuItem.Text = "Visa dragföljd";
            this.visaDragföljdToolStripMenuItem.Click += new System.EventHandler(this.visaDragföljdToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ledtrådToolStripMenuItem,
            this.omToolStripMenuItem});
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(43, 20);
            this.helpToolStripMenuItem1.Text = "Hjälp";
            // 
            // ledtrådToolStripMenuItem
            // 
            this.ledtrådToolStripMenuItem.Name = "ledtrådToolStripMenuItem";
            this.ledtrådToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ledtrådToolStripMenuItem.Text = "Ledtråd";
            this.ledtrådToolStripMenuItem.Click += new System.EventHandler(this.ledtrådToolStripMenuItem_Click);
            // 
            // omToolStripMenuItem
            // 
            this.omToolStripMenuItem.Name = "omToolStripMenuItem";
            this.omToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.omToolStripMenuItem.Text = "Om...";
            this.omToolStripMenuItem.Click += new System.EventHandler(this.omToolStripMenuItem_Click);
            // 
            // hearts4
            // 
            this.hearts4.BackColor = System.Drawing.Color.Transparent;
            this.hearts4.Image = global::Strategi_för_två.Properties.Resources.hearts;
            this.hearts4.Location = new System.Drawing.Point(200, 24);
            this.hearts4.Name = "hearts4";
            this.hearts4.Size = new System.Drawing.Size(100, 100);
            this.hearts4.TabIndex = 8;
            this.hearts4.TabStop = false;
            this.hearts4.Click += new System.EventHandler(this.SelectPiece);
            // 
            // hearts3
            // 
            this.hearts3.BackColor = System.Drawing.Color.Transparent;
            this.hearts3.Image = global::Strategi_för_två.Properties.Resources.hearts;
            this.hearts3.Location = new System.Drawing.Point(0, 24);
            this.hearts3.Name = "hearts3";
            this.hearts3.Size = new System.Drawing.Size(100, 100);
            this.hearts3.TabIndex = 7;
            this.hearts3.TabStop = false;
            this.hearts3.Click += new System.EventHandler(this.SelectPiece);
            // 
            // hearts2
            // 
            this.hearts2.BackColor = System.Drawing.Color.Transparent;
            this.hearts2.Image = global::Strategi_för_två.Properties.Resources.hearts;
            this.hearts2.Location = new System.Drawing.Point(300, 424);
            this.hearts2.Name = "hearts2";
            this.hearts2.Size = new System.Drawing.Size(100, 100);
            this.hearts2.TabIndex = 6;
            this.hearts2.TabStop = false;
            this.hearts2.Click += new System.EventHandler(this.SelectPiece);
            this.hearts2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.hearts2_MouseDoubleClick);
            // 
            // hearts1
            // 
            this.hearts1.BackColor = System.Drawing.Color.Transparent;
            this.hearts1.Image = global::Strategi_för_två.Properties.Resources.hearts;
            this.hearts1.Location = new System.Drawing.Point(100, 424);
            this.hearts1.Name = "hearts1";
            this.hearts1.Size = new System.Drawing.Size(100, 100);
            this.hearts1.TabIndex = 5;
            this.hearts1.TabStop = false;
            this.hearts1.Click += new System.EventHandler(this.SelectPiece);
            // 
            // spades4
            // 
            this.spades4.BackColor = System.Drawing.Color.Transparent;
            this.spades4.Image = global::Strategi_för_två.Properties.Resources.spades;
            this.spades4.Location = new System.Drawing.Point(300, 24);
            this.spades4.Name = "spades4";
            this.spades4.Size = new System.Drawing.Size(100, 100);
            this.spades4.TabIndex = 4;
            this.spades4.TabStop = false;
            this.spades4.Click += new System.EventHandler(this.SelectPiece);
            // 
            // spades3
            // 
            this.spades3.BackColor = System.Drawing.Color.Transparent;
            this.spades3.Image = global::Strategi_för_två.Properties.Resources.spades;
            this.spades3.Location = new System.Drawing.Point(100, 24);
            this.spades3.Name = "spades3";
            this.spades3.Size = new System.Drawing.Size(100, 100);
            this.spades3.TabIndex = 3;
            this.spades3.TabStop = false;
            this.spades3.Click += new System.EventHandler(this.SelectPiece);
            // 
            // spades2
            // 
            this.spades2.BackColor = System.Drawing.Color.Transparent;
            this.spades2.Image = global::Strategi_för_två.Properties.Resources.spades;
            this.spades2.Location = new System.Drawing.Point(200, 424);
            this.spades2.Name = "spades2";
            this.spades2.Size = new System.Drawing.Size(100, 100);
            this.spades2.TabIndex = 2;
            this.spades2.TabStop = false;
            this.spades2.Click += new System.EventHandler(this.SelectPiece);
            // 
            // spades1
            // 
            this.spades1.BackColor = System.Drawing.Color.Transparent;
            this.spades1.Image = global::Strategi_för_två.Properties.Resources.spades;
            this.spades1.Location = new System.Drawing.Point(0, 424);
            this.spades1.Name = "spades1";
            this.spades1.Size = new System.Drawing.Size(100, 100);
            this.spades1.TabIndex = 1;
            this.spades1.TabStop = false;
            this.spades1.Click += new System.EventHandler(this.SelectPiece);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // bytSidaToolStripMenuItem
            // 
            this.bytSidaToolStripMenuItem.Name = "bytSidaToolStripMenuItem";
            this.bytSidaToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.bytSidaToolStripMenuItem.Text = "Byt sida";
            this.bytSidaToolStripMenuItem.Click += new System.EventHandler(this.bytSidaToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(400, 524);
            this.Controls.Add(this.hearts4);
            this.Controls.Add(this.hearts3);
            this.Controls.Add(this.hearts2);
            this.Controls.Add(this.hearts1);
            this.Controls.Add(this.spades4);
            this.Controls.Add(this.spades3);
            this.Controls.Add(this.spades2);
            this.Controls.Add(this.spades1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Strategi för två";
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hearts4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hearts3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hearts2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hearts1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spades4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spades3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spades2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spades1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox spades1;
        private System.Windows.Forms.PictureBox spades2;
        private System.Windows.Forms.PictureBox spades3;
        private System.Windows.Forms.PictureBox spades4;
        private System.Windows.Forms.PictureBox hearts1;
        private System.Windows.Forms.PictureBox hearts2;
        private System.Windows.Forms.PictureBox hearts3;
        private System.Windows.Forms.PictureBox hearts4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGame;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sparaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spelaMotDatornToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem omToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem avslutaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medEnVänToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem funktionerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flyttaPjäsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bytBrädeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ledtrådToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem låsUppBrädetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visaDragföljdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bakåtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem framåtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bytSidaToolStripMenuItem;
    }
}

