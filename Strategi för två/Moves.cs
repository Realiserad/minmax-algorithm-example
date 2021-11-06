using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Strategi_för_två
{
    public partial class Moves : Form
    {
        public Moves()
        {
            InitializeComponent();
        }

        public void ClearMoves()
        {
            richTextBox1.Text = "";
        }

        public void AddMove(string x)
        {
            richTextBox1.Text += x + "\n";
        }

        private void Moves_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            Form1 frm = (Form1)Application.OpenForms["Form1"];
            foreach (string gameMove in frm.gameMoves)
                richTextBox1.Text += gameMove + "\n";
        }
    }
}
