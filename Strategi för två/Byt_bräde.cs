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
    public partial class Byt_bräde : Form
    {
        // Nytt bräde
        Bitmap bmpBoard = new Bitmap(400, 524);

        public Byt_bräde()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // välj fil
        {
            openFileDialog1.Title = "Välj nytt bräde...";
            openFileDialog1.Filter = "Portable Network Graphics (*.PNG)|*.png|Graphics Interchange Format (*.GIF)|*.gif|Joint Photographic Experts Group (*.JPG;*.JPEG;*.JPE)|*.jpg;*.jpeg;*.jpe|Bitmap (*.BMP)|*.bmp";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.Drawing.Image newBoard = System.Drawing.Image.FromFile(openFileDialog1.FileName);
                if (newBoard.Width != 400 && newBoard.Height != 500)
                {
                    MessageBox.Show("Brädet har inte rätt proportioner. Bilden skall vara 400 x 500 pixlar",
                        "Fel bildstorlek");
                    button2.Enabled = false;
                }
                else // bilden har rätt storlek
                {
                    pictureBox1.BackgroundImage = newBoard;
                    textBox1.Text = openFileDialog1.FileName;
                    // rita 24 px ram
                    for (int i = 0; i < 400; i++)
                    {
                        for (int j = 0; j < 24; j++)
                        {
                            bmpBoard.SetPixel(i, j, Color.White);
                        }
                    }
                    // infoga newBoard från minnet
                    Bitmap newBoard_bmp = new Bitmap(newBoard);
                    for (int i = 0; i < 400; i++)
                    {
                        for (int j = 24; j < 524; j++)
                        {
                            bmpBoard.SetPixel(i, j, newBoard_bmp.GetPixel(i, j - 24));
                        }
                    }
                    button2.Enabled = true;
                }
            }
        }

        private void Byt_bräde_Load(object sender, EventArgs e) // hämta bakgrundsbilden för Form1
        {
            Form1 frm = (Form1)Application.OpenForms["Form1"];
            System.Drawing.Image preview = frm.BackgroundImage;
            preview = Crop(preview, new Rectangle(0, 24, 400, 500));
            pictureBox1.BackgroundImage = preview;
        }

        Image Crop(Image img, Rectangle cropArea) // cropArea är den del av bilden som skall sparas
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return (Image)(bmpCrop);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 frm = (Form1)Application.OpenForms["Form1"];
            frm.BackgroundImage = bmpBoard;
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 frm = (Form1)Application.OpenForms["Form1"];
            frm.BackgroundImage = Strategi_för_två.Properties.Resources.board;
            Close();
        }
    }
}
