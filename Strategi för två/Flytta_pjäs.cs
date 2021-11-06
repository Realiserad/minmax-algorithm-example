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
    public partial class Flytta_pjäs : Form
    {
        public Flytta_pjäs()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string move = textBox1.Text;
            if (move != "" && ValidMove(move) || move == "korvfest") // om drag är giltigt
            {
                Form1.move = move; // passa drag till Form1
                this.Close();
            }
            else
            {
                textBox1.Text = ""; // töm textBox
                MessageBox.Show("Draget är ogiltigt.", "Ogiltigt drag");
            }
        }

        bool ValidMove(string move) // kontrollerar om det inmatade draget är giltigt
        {
            try
            {
                int color = 1;
                if (Form1.spadesToMove)
                    color = 2;

                int keyTo;
                if (move[3] == 'a')
                    keyTo = 0;
                else if (move[3] == 'b')
                    keyTo = 1;
                else if (move[3] == 'c')
                    keyTo = 2;
                else if (move[3] == 'd')
                    keyTo = 3;
                else
                    return false;
                int keyFrom;
                if (move[0] == 'a')
                    keyFrom = 0;
                else if (move[0] == 'b')
                    keyFrom = 1;
                else if (move[0] == 'c')
                    keyFrom = 2;
                else if (move[0] == 'd')
                    keyFrom = 3;
                else
                    return false;

                foreach (string item in textBox1.AutoCompleteCustomSource)
                {
                    Form1 frm = (Form1)Application.OpenForms["Form1"];
                    if (item == move && frm.board[keyTo, 
                        Math.Abs((int)Char.GetNumericValue(move[4]) - 5)] == 0 
                        && frm.board[keyFrom, Math.Abs((int)Char.GetNumericValue
                        (move[1]) - 5)] == color)
                        return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
