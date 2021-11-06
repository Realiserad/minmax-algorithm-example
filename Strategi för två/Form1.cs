using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Strategi_för_två
{
    public partial class Form1 : Form
    {
        public Moves m;

        // 0 = tom ruta, 1 = hjärta, 2 = spade
        public int[,] board = new int[4, 5] { { 1, 0, 0, 0, 2 }, { 2, 0, 0, 0, 1 }, 
                                            { 1, 0, 0, 0, 2 }, { 2, 0, 0, 0, 1 } };
        int x_old, y_old;                                      // kommer ihåg var pjäsen befann sig
        object piece;                                          // håller reda på vilken pjäs som skall flyttas
        public static string move;                             // håller ett drag i textformat
        bool selected = false;
        public static bool spadesToMove = true;                // håller reda på vem som är i draget
        bool boardLocked = false;
        bool saved = false; 
        int maxDepth = 8;                                      // antal drag som datorn skall "titta framåt"
        const int INF = 100000000;
        string bestMove;                                       // håller datorns bästa drag
        public List<string> gameMoves = new List<string>();    // sparar dragföljden
        int currMove = -1;                                     // index för senaste drag på brädet

        public Form1()
        {
            InitializeComponent();
            this.Text = "Strategi för två (spader i draget)";
        }

        private void SelectPiece(object sender, EventArgs e) // när spelaren klickar på en pjäs
        {
            if (låsUppBrädetToolStripMenuItem.Checked)
            {
                RemoveMarker();
                selected = true;
                piece = sender;
                x_old = ((PictureBox)sender).Location.X / 100; // notera pos
                y_old = (((PictureBox)sender).Location.Y - 24) / 100;
                ((PictureBox)sender).BackgroundImage = Strategi_för_två.Properties.Resources.marker;
            }
            else
            {
                if (!spadesToMove && !Spades(sender) && !boardLocked) // hjärter är i draget
                {
                    if (!selected)
                    {
                        selected = true;
                        piece = sender; // notera vilken pjäs användaren vill flytta

                        ((PictureBox)sender).BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                        x_old = ((PictureBox)sender).Location.X / 100; // notera pos
                        y_old = (((PictureBox)sender).Location.Y - 24) / 100;
                    }
                    else // en ruta är redan markerad
                    {
                        if (sender != piece) // användaren har markerat en annan pjäs tidigare
                        {
                            selected = true;
                            ((PictureBox)piece).BackgroundImage = null; // avmarkera gammal ruta

                            ((PictureBox)sender).BackgroundImage = Strategi_för_två.Properties.
                                    Resources.marker; // markera ny ruta

                            x_old = ((PictureBox)sender).Location.X / 100; // notera pos
                            y_old = (((PictureBox)sender).Location.Y - 24) / 100;

                            piece = sender; // notera ny pjäs att flytta
                        }
                        else
                        {
                            selected = false;
                            ((PictureBox)sender).BackgroundImage = null; // avmarkera rätt ruta
                            piece = null; // användaren vill inte flytta någon pjäs
                        }
                    }
                }
                else if (spadesToMove && Spades(sender) && !boardLocked) // spader i draget
                {
                    if (!selected)
                    {
                        selected = true;
                        piece = sender; // notera vilken pjäs användaren vill flytta
                        ((PictureBox)sender).BackgroundImage = Strategi_för_två.Properties.
                                    Resources.marker;
                        x_old = ((PictureBox)sender).Location.X / 100;
                        y_old = (((PictureBox)sender).Location.Y - 24) / 100;
                    }
                    else
                    {
                        if (sender != piece)
                        {
                            selected = true;
                            ((PictureBox)piece).BackgroundImage = null;

                            ((PictureBox)sender).BackgroundImage = Strategi_för_två.Properties.
                                    Resources.marker;
                            x_old = ((PictureBox)sender).Location.X / 100;
                            y_old = (((PictureBox)sender).Location.Y - 24) / 100;
                            piece = sender;
                        }
                        else
                        {
                            selected = false;
                            ((PictureBox)sender).BackgroundImage = null;
                            piece = null;
                        }
                    }
                }
            }
        }

        bool Spades(object obj) // kollar om obj innehåller en spader
        {
            if (obj == spades1 || obj == spades2 || obj == spades3 || obj == spades4)
                return true;
            else
                return false;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e) // flytta pjäs
        {
            if (medEnVänToolStripMenuItem.Checked)
                bakåtToolStripMenuItem.Enabled = true;

            if (låsUppBrädetToolStripMenuItem.Checked && selected)
            {
                ((PictureBox)piece).Location = new Point(RoundX(e.X), RoundY(e.Y) + 24);
                PerformMove(e.X / 100, (e.Y - 24) / 100); // Uppdatera spelställningen
                saved = false;
                x_old = ((PictureBox)piece).Location.X / 100; // notera pos
                y_old = (((PictureBox)piece).Location.Y - 24) / 100;
                gameMoves.Clear();
                currMove = -1;
                try
                {
                    m.ClearMoves();
                }
                catch
                {

                }
            }
            else if (selected && !boardLocked && ValidMove(e.X, e.Y))
            { // Om en pjäs är markerad, brädet inte är låst och draget är giltigt
                if (currMove < gameMoves.Count - 1) // användaren har backat spelet
                {
                    framåtToolStripMenuItem.Enabled = false;
                    // Kasta alla drag som ligger efter nuvarande drag i gameMoves
                    for (int i = gameMoves.Count - 1; i > currMove; i--)
                        gameMoves.RemoveAt(i);
                    // Kasta drag i Moves-fönstret
                    try
                    {
                        m.ClearMoves();
                        // Läs in nya drag
                        foreach (string gameMove in gameMoves)
                            m.AddMove(gameMove);
                    }
                    catch
                    {

                    }
                }
                if (spadesToMove && Spades(piece)) // om spader är i draget, flytta en spader
                {
                    ((PictureBox)piece).Location = new Point(RoundX(e.X), RoundY(e.Y) + 24);
                    spadesToMove = false;
                    PerformMove(e.X / 100, (e.Y - 24) / 100); // Uppdatera spelställningen
                    boardLocked = GameOver(board);
                    saved = false;
                    ///////////////////////////////////////////////////////////
                    //             Lägg till drag                            //
                    ///////////////////////////////////////////////////////////
                    try
                    {
                        string nextMove = Convert.ToString(x_old) + Convert.ToString(y_old) + "-" + 
                            Convert.ToString(e.X / 100) + Convert.ToString(e.Y / 100);
                        nextMove = Parse(nextMove);
                        gameMoves.Add(nextMove);
                        currMove++;
                        m.AddMove(nextMove);
                    }
                    catch
                    {

                    }
                    ///////////////////////////////////////////////////////////
                    x_old = ((PictureBox)piece).Location.X / 100; // notera pos
                    y_old = (((PictureBox)piece).Location.Y - 24) / 100;

                    if (boardLocked) // någon har vunnit
                    {
                        if (spelaMotDatornToolStripMenuItem.Checked) // du vann över datorn
                            MessageBox.Show("Du vann!");
                        else
                        {
                            if (Spades(piece)) // spader har vunnit
                                MessageBox.Show("Spader vann!");
                            else
                                MessageBox.Show("Hjärter vann!");
                        }
                    }
                    if (spelaMotDatornToolStripMenuItem.Checked) // om användaren spelar mot datorn
                    {
                        this.Text = "Strategi för två (datorn i draget)";
                        ComputeMove();
                    }
                    else
                        this.Text = "Strategi för två (hjärter i draget)";
                }
                else if (!spadesToMove && !Spades(piece))
                {
                    ((PictureBox)piece).Location = new Point(RoundX(e.X), RoundY(e.Y) + 24);
                    spadesToMove = true;
                    PerformMove(e.X / 100, (e.Y - 24) / 100); // Uppdatera spelställningen
                    boardLocked = GameOver(board);
                    saved = false;
                    ///////////////////////////////////////////////////////////
                    //             Lägg till drag                            //
                    ///////////////////////////////////////////////////////////
                    try
                    {
                        string nextMove = Convert.ToString(x_old) + Convert.ToString(y_old) + "-" +
                            Convert.ToString(e.X / 100) + Convert.ToString(e.Y / 100);
                        nextMove = Parse(nextMove);
                        gameMoves.Add(nextMove);
                        currMove++;
                        m.AddMove(nextMove);
                    }
                    catch
                    {

                    }
                    ///////////////////////////////////////////////////////////
                    x_old = ((PictureBox)piece).Location.X / 100; // notera pos
                    y_old = (((PictureBox)piece).Location.Y - 24) / 100;
                    if (boardLocked) // någon har vunnit
                    {
                        if (Spades(piece)) // spader har vunnit
                            MessageBox.Show("Spader vann!");
                        else
                            MessageBox.Show("Hjärter vann!");
                    }
                    if (spelaMotDatornToolStripMenuItem.Checked) // om användaren spelar mot datorn
                    {
                        this.Text = "Strategi för två (datorn i draget)";
                        ComputeMove();
                    }
                    else
                        this.Text = "Strategi för två (spader i draget)";
                }
            }
        }

        void PerformMove(int x, int y) // flytta pjäs i matrisen board
        {
            int key;
            if (!Spades(piece))
                key = 1;
            else
                key = 2;
            board[x, y] = key;
            board[x_old, y_old] = 0;
        }

        void PerformMove(string move, int[,] board)
        {
            int key = 1;
            if (spadesToMove)
                key = 2;

            int keyTo = 0;
            if (move[3] == 'b')
                keyTo = 1;
            else if (move[3] == 'c')
                keyTo = 2;
            else if (move[3] == 'd')
                keyTo = 3;

            int keyFrom = 0;
            if (move[0] == 'b')
                keyFrom = 1;
            else if (move[0] == 'c')
                keyFrom = 2;
            else if (move[0] == 'd')
                keyFrom = 3;

            board[keyTo, Math.Abs((int)Char.GetNumericValue(move[4]) - 5)] = key;
            board[keyFrom, Math.Abs((int)Char.GetNumericValue(move[1]) - 5)] = 0;
        }

        void RedrawBoard(string moveToMake)
        {
            if (moveToMake != "")
            {
                int keyTo = 0;
                if (moveToMake[3] == 'b')
                    keyTo = 1;
                else if (moveToMake[3] == 'c')
                    keyTo = 2;
                else if (moveToMake[3] == 'd')
                    keyTo = 3;

                int keyFrom = 0;
                if (moveToMake[0] == 'b')
                    keyFrom = 1;
                else if (moveToMake[0] == 'c')
                    keyFrom = 2;
                else if (moveToMake[0] == 'd')
                    keyFrom = 3;

                RemoveMarker(); // ta bort markör på gammal pjäs

                // Räkna ut vilken pjäs som befinner sig på positionen keyFrom, move[1]
                // och flytta pjäsen till keyTo, move[4]
                Point pFrom = new Point(keyFrom * 100,
                    Math.Abs((int)Char.GetNumericValue(moveToMake[1]) - 5) * 100 + 24);
                Point pTo = new Point(keyTo * 100,
                    Math.Abs((int)Char.GetNumericValue(moveToMake[4]) - 5) * 100 + 24);

                if (hearts1.Location == pFrom)
                {
                    hearts1.Location = pTo;
                    hearts1.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                    piece = hearts1;
                }
                else if (hearts2.Location == pFrom)
                {
                    hearts2.Location = pTo;
                    hearts2.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                    piece = hearts2;
                }
                else if (hearts3.Location == pFrom)
                {
                    hearts3.Location = pTo;
                    hearts3.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                    piece = hearts3;
                }
                else if (hearts4.Location == pFrom)
                {
                    hearts4.Location = pTo;
                    hearts4.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                    piece = hearts4;
                }
                else if (spades1.Location == pFrom)
                {
                    spades1.Location = pTo;
                    spades1.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                    piece = spades1;
                }
                else if (spades2.Location == pFrom)
                {
                    spades2.Location = pTo;
                    spades2.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                    piece = spades2;
                }
                else if (spades3.Location == pFrom)
                {
                    spades3.Location = pTo;
                    spades3.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                    piece = spades3;
                }
                else if (spades4.Location == pFrom)
                {
                    spades4.Location = pTo;
                    spades4.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                    piece = spades4;
                }

                move = ""; // undvik att markören plockas bort på piece om fönstret Flytta_pjäs
                           // stängs med krysset
            }
        }

        bool GameOver(int[,] b)
        {
            for (int i = 0; i < 3; i++) // leta tre i rad vertikalt
            {
                for (int j = 0; j < 4; j++)
                {
                    if (b[j, i] == 1 && b[j, i + 1] == 1 && b[j, i + 2] == 1
                        || b[j, i] == 2 && b[j, i + 1] == 2 && b[j, i + 2] == 2)
                        return true;
                }
            }

            for (int i = 0; i < 5; i++) // leta tre i rad horisontellt
            {
                for (int j = 0; j < 2; j++)
                {
                    if (b[j, i] == 1 && b[j + 1, i] == 1 && b[j + 2, i] == 1
                        || b[j, i] == 2 && b[j + 1, i] == 2 && b[j + 2, i] == 2)
                        return true;
                }
            }

            for (int i = 0; i < 3; i++) // leta tre i rad diagonalt från vänster till höger
            {
                for (int j = 0; j < 2; j++)
                {
                    if (b[j, i] == 1 && b[j + 1, i + 1] == 1
                        && b[j + 2, i + 2] == 1 || b[j, i] == 2 &&
                        b[j + 1, i + 1] == 2 && b[j + 2, i + 2] == 2)
                        return true;
                }
            }

            for (int i = 2; i >= 0; i--) // leta tre i rad diagonalt från höger till vänster
            {
                for (int j = 3; j >= 2; j--)
                {
                    if (b[j, i] == 1 && b[j - 1, i + 1] == 1
                        && b[j - 2, i + 2] == 1 || b[j, i] == 2 
                        && b[j - 1, i + 1] == 2 && b[j - 2, i + 2] == 2)
                        return true;
                }
            }

            return false;
        }

        bool ValidMove(int x, int y) // kontrollera om ett drag är giltigt
        {
            Point p = new Point();
            p = ((PictureBox)piece).Location;

            if (Math.Abs(p.X / 100 - x / 100) == 1 && Math.Abs(p.Y / 100 - (y - 24) / 100) == 0
                || Math.Abs(p.Y / 100 - (y - 24) / 100) == 1 && Math.Abs(p.X / 100 - x / 100) == 0)
                return true; // Draget är giltigt
            else
                return false;
        }

        void RemoveMarker() // tar bort markören på senast flyttade pjäs
        {
            try
            {
                ((PictureBox)piece).BackgroundImage = null;
            }
            catch
            {

            }
        }

        int RoundX(int point) // avrunda x-koordinat nedåt till närmaste 100-tal
        {
            return point / 100 * 100;
        }

        int RoundY(int point) 
        {
            return (point - 24) / 100 * 100;
        }

        private void hearts2_MouseDoubleClick(object sender, MouseEventArgs e) // fix för dubbelklick
        {
            selected = false;
            ((PictureBox)sender).BackgroundImage = null; // Avmarkera rätt ruta

            piece = null; // användaren vill inte flytta någon pjäs
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (!saved)
            {
                if (MessageBox.Show("Partiet har inte sparats. Vill du spara partiet nu?", "Spara",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                    sparaToolStripMenuItem_Click(sender, e);
            }
            try
            {
                StreamReader sr = new StreamReader(s[0]);

                char bit = Convert.ToChar(sr.Read());
                bool h1 = false, h2 = false, h3 = false, h4 = false;
                bool s1 = false, s2 = false, s3 = false, s4 = false;

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        board[i, j] = (int)Char.GetNumericValue(bit);
                        if (board[i, j] == 1) // hjärter på pos [i, j]
                        {
                            if (!h1) // om h1 inte är på sin rätta position
                            {
                                hearts1.Location = new Point(i * 100, j * 100 + 24);
                                h1 = true; // h1 är klar
                            }
                            else if (!h2)
                            {
                                hearts2.Location = new Point(i * 100, j * 100 + 24);
                                h2 = true;
                            }
                            else if (!h3)
                            {
                                hearts3.Location = new Point(i * 100, j * 100 + 24);
                                h3 = true;
                            }
                            else if (!h4)
                            {
                                hearts4.Location = new Point(i * 100, j * 100 + 24);
                                h4 = true;
                            }
                        }
                        if (board[i, j] == 2)
                        {
                            if (!s1)
                            {
                                spades1.Location = new Point(i * 100, j * 100 + 24);
                                s1 = true;
                            }
                            else if (!s2)
                            {
                                spades2.Location = new Point(i * 100, j * 100 + 24);
                                s2 = true;
                            }
                            else if (!s3)
                            {
                                spades3.Location = new Point(i * 100, j * 100 + 24);
                                s3 = true;
                            }
                            else if (!s4)
                            {
                                spades4.Location = new Point(i * 100, j * 100 + 24);
                                s4 = true;
                            }
                        }
                        bit = Convert.ToChar(sr.Read());
                    }
                }
                // Brädet är klart
                string line = sr.ReadLine();
                line = sr.ReadLine();
                gameMoves.Clear();
                while (line != "/")
                {
                    gameMoves.Add(line);
                    line = sr.ReadLine();
                }
                RemoveMarker();
                line = sr.ReadLine();
                if (line == "null")
                {
                    piece = null;
                    selected = false;
                }
                else
                {
                    int pY = Convert.ToInt32(Char.GetNumericValue(line[1])) * 100 + 24;
                    int pX = Convert.ToInt32(Char.GetNumericValue(line[0])) * 100;
                    Point p = new Point(pX, pY);
                    if (p == hearts1.Location) hearts1.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                    if (p == hearts2.Location) hearts2.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                    if (p == hearts3.Location) hearts3.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                    if (p == hearts4.Location) hearts4.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                    if (p == spades1.Location) spades1.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                    if (p == spades2.Location) spades2.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                    if (p == spades3.Location) spades3.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                    if (p == spades4.Location) spades4.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                    if (p == hearts1.Location) piece = hearts1;
                    if (p == hearts2.Location) piece = hearts2;
                    if (p == hearts3.Location) piece = hearts3;
                    if (p == hearts4.Location) piece = hearts4;
                    if (p == spades1.Location) piece = spades1;
                    if (p == spades2.Location) piece = spades2;
                    if (p == spades3.Location) piece = spades3;
                    if (p == spades4.Location) piece = spades4;
                    selected = true;
                }
                boardLocked = GameOver(board);
                spadesToMove = Convert.ToBoolean(sr.ReadLine());
                saved = true;
                sr.Close(); // data har lästs in, stäng filen
                if (medEnVänToolStripMenuItem.Checked && gameMoves.Count > -1)
                    bakåtToolStripMenuItem.Checked = true;
                try
                {
                    currMove = gameMoves.Count - 1;
                    foreach (string gameMove in gameMoves)
                        m.AddMove(gameMove);
                }
                catch
                {

                }
            }
            catch
            {
                MessageBox.Show("Filen kan inte öppnas.");
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!saved)
            {
                if (MessageBox.Show("Partiet har inte sparats. Vill du spara partiet nu?", "Spara",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                    sparaToolStripMenuItem_Click(sender, e);
            }
        }

        /************************************************************************
         * 
         *                               Spelmotor
         * 
         ************************************************************************/
        List<int[,]> boards = new List<int[,]>(); // Innehåller alla bräden
        List<string[]> legalMoves = new List<string[]>(); // Innehåller alla giltiga drag för brädena i boards
        Random r = new Random(); // slumpgenerator

        void ComputeMove()
        {
            if (!boardLocked)
            {
                Thread engine = new Thread(new ThreadStart(Minimax));
                engine.Start();
                engine.Join();
                string an = Parse(bestMove);
                try
                {
                    gameMoves.Add(an);
                    currMove++;
                    m.AddMove(an);
                }
                catch
                {

                }
                PerformMove(an, board);
                RedrawBoard(an);
                saved = false;
                selected = true;
                boardLocked = GameOver(board);
                spadesToMove = !spadesToMove;
                this.Text = "Strategi för två (du i draget)";
                if (boardLocked)
                    MessageBox.Show("Du förlorade!");
            }
        }

        void Minimax()
        {
            if (!spadesToMove) // hjärter är i draget, kör Minimaxalgoritmen med hjärter som maximizer
            {
                int maxScore = -INF;
                string[] currentMoves = GetLegalMoves(board, false);
                legalMoves.Add(currentMoves);
                bestMove = legalMoves[0][r.Next(0, legalMoves[0].Length)];
                foreach (string legalMove in legalMoves[0])
                {
                    int[,] target = new int[4, 5];
                    Array.Copy(board, target, 20);
                    boards.Add(target);
                    MakeMove(legalMove, boards[boards.Count - 1], false); // Gör drag i target
                    int nextScore = SpadesMin(boards[boards.Count - 1], 1, -INF, INF);
                    if (nextScore > maxScore)
                    {
                        maxScore = nextScore;
                        bestMove = legalMove;
                    }
                    if (nextScore == maxScore && r.Next(0, 2) == 1)
                    {
                        maxScore = nextScore;
                        bestMove = legalMove;
                    }
                }
            }
            else // spader är i draget, kör Minimaxalgoritmen med hjärter som minimizer
            {

                int maxScore = -INF;
                string[] currentMoves = GetLegalMoves(board, true);
                legalMoves.Add(currentMoves);
                Random r = new Random();
                bestMove = legalMoves[0][r.Next(0, legalMoves[0].Length)];
                foreach (string legalMove in legalMoves[0])
                {
                    int[,] target = new int[4, 5];
                    Array.Copy(board, target, 20);
                    boards.Add(target);
                    MakeMove(legalMove, boards[boards.Count - 1], true); // Gör drag i target
                    int nextScore = HeartsMin(boards[boards.Count - 1], 1, -INF, INF);
                    if (nextScore > maxScore)
                    {
                        maxScore = nextScore;
                        bestMove = legalMove;
                    }
                    if (nextScore == maxScore && r.Next(0, 2) == 1)
                    {
                        maxScore = nextScore;
                        bestMove = legalMove;
                    }
                }
            }
            boards.Clear(); // Rensa gamla bräden
            legalMoves.Clear(); // Rensa gamla drag
        }

        int SpadesMin(int[,] b, int depth, int alpha, int beta)
        {
            if (GameOver(b) || depth >= maxDepth) return Evaluate(b, false);
            int minScore = INF;
            string[] currentMoves = GetLegalMoves(b, true);
            legalMoves.Add(currentMoves);
            int tempIndex = legalMoves.Count - 1;
            foreach (string legalMove in legalMoves[tempIndex])
            {
                int[,] target = new int[4, 5];
                Array.Copy(b, target, 20);
                boards.Add(target);
                MakeMove(legalMove, boards[boards.Count - 1], true); // Gör ett legalMove i det nya brädet
                int nextScore = HeartsMax(boards[boards.Count - 1], depth + 1, alpha, beta);
                if (nextScore < minScore) minScore = nextScore;
                if (nextScore == minScore && r.Next(0, 2) == 1) minScore = nextScore;
                if (nextScore < beta) beta = nextScore;
                if (alpha >= beta) return beta;
            }
            return minScore;
        }

        int SpadesMax(int[,] b, int depth, int alpha, int beta)
        {
            if (GameOver(b) || depth >= maxDepth) return Evaluate(b, true);
            int maxScore = -INF;
            string[] currentMoves = GetLegalMoves(b, true);
            legalMoves.Add(currentMoves);
            int tempIndex = legalMoves.Count - 1;
            foreach (string legalMove in legalMoves[tempIndex])
            {
                int[,] target = new int[4, 5];
                Array.Copy(b, target, 20);
                boards.Add(target);
                MakeMove(legalMove, boards[boards.Count - 1], true); // Gör ett legalMove i det nya brädet
                int nextScore = HeartsMin(boards[boards.Count - 1], depth + 1, alpha, beta);
                if (nextScore > maxScore) maxScore = nextScore;
                if (nextScore == maxScore && r.Next(0, 2) == 1) maxScore = nextScore;
                if (nextScore > alpha) alpha = nextScore;
                if (alpha >= beta) return alpha;
            }
            return maxScore;
        }

        int HeartsMax(int[,] b, int depth, int alpha, int beta)
        {
            if (GameOver(b) || depth >= maxDepth) return Evaluate(b, false);
            int maxScore = -INF;
            string[] currentMoves = GetLegalMoves(b, false);
            legalMoves.Add(currentMoves);
            int tempIndex = legalMoves.Count - 1;
            foreach (string legalMove in legalMoves[tempIndex])
            {
                int[,] target = new int[4, 5];
                Array.Copy(b, target, 20);
                boards.Add(target);
                MakeMove(legalMove, boards[boards.Count - 1], false); // Gör ett legalMove i det nya brädet
                int nextScore = SpadesMin(boards[boards.Count - 1], depth + 1, alpha, beta);
                if (nextScore > maxScore) maxScore = nextScore;
                if (nextScore == maxScore && r.Next(0, 2) == 1) maxScore = nextScore;
                if (nextScore > alpha) alpha = nextScore;
                if (alpha >= beta) return alpha;
            }
            return maxScore;
        }

        int HeartsMin(int[,] b, int depth, int alpha, int beta)
        {
            if (GameOver(b) || depth >= maxDepth) return Evaluate(b, true);
            int minScore = INF;
            string[] currentMoves = GetLegalMoves(b, false);
            legalMoves.Add(currentMoves);
            int tempIndex = legalMoves.Count - 1;
            foreach (string legalMove in legalMoves[tempIndex])
            {
                int[,] target = new int[4, 5];
                Array.Copy(b, target, 20);
                boards.Add(target);
                MakeMove(legalMove, boards[boards.Count - 1], false); // Gör ett legalMove i det nya brädet
                int nextScore = SpadesMax(boards[boards.Count - 1], depth + 1, alpha, beta);
                if (nextScore < minScore) minScore = nextScore;
                if (nextScore == minScore && r.Next(0, 2) == 1) minScore = nextScore;
                if (nextScore < beta) beta = nextScore;
                if (alpha >= beta) return beta;
            }
            return minScore;
        }

        int Evaluate(int[,] b, bool spadesAsMaximizer) // Utvärdera ställningen på brädet b 
        {
            /////////////////////////////////////////////////////////////////////
            //                    LETA EFTER VINNARE                           //
            /////////////////////////////////////////////////////////////////////
            for (int i = 0; i < 3; i++) // leta tre i rad vertikalt
            {
                for (int j = 0; j < 4; j++)
                {
                    if (b[j, i] == 1 && b[j, i + 1] == 1 && b[j, i + 2] == 1)
                    {
                        if (spadesAsMaximizer)
                            return -INF;
                        else
                            return INF;
                    }
                }
            }

            for (int i = 0; i < 5; i++) // leta tre i rad horisontellt
            {
                for (int j = 0; j < 2; j++)
                {
                    if (b[j, i] == 1 && b[j + 1, i] == 1 && b[j + 2, i] == 1)
                    {
                        if (spadesAsMaximizer)
                            return -INF;
                        else
                            return INF;
                    }
                }
            }

            for (int i = 0; i < 3; i++) // leta tre i rad diagonalt från vänster till höger
            {
                for (int j = 0; j < 2; j++)
                {
                    if (b[j, i] == 1 && b[j + 1, i + 1] == 1 && b[j + 2, i + 2] == 1)
                    {
                        if (spadesAsMaximizer)
                            return -INF;
                        else
                            return INF;
                    }
                }
            }

            for (int i = 2; i >= 0; i--) // leta tre i rad diagonalt från höger till vänster
            {
                for (int j = 3; j >= 2; j--)
                {
                    if (b[j, i] == 1 && b[j - 1, i + 1] == 1 && b[j - 2, i + 2] == 1)
                    {
                        if (spadesAsMaximizer)
                            return -INF;
                        else
                            return INF;
                    }
                }
            }

            for (int i = 0; i < 3; i++) // leta tre i rad vertikalt
            {
                for (int j = 0; j < 4; j++)
                {
                    if (b[j, i] == 2 && b[j, i + 1] == 2 && b[j, i + 2] == 2)
                    {
                        if (spadesAsMaximizer)
                            return INF;
                        else
                            return -INF;
                    }
                }
            }

            for (int i = 0; i < 5; i++) // leta tre i rad horisontellt
            {
                for (int j = 0; j < 2; j++)
                {
                    if (b[j, i] == 2 && b[j + 1, i] == 2 && b[j + 2, i] == 2)
                    {
                        if (spadesAsMaximizer)
                            return INF;
                        else
                            return -INF;
                    }
                }
            }

            for (int i = 0; i < 3; i++) // leta tre i rad diagonalt från vänster till höger
            {
                for (int j = 0; j < 2; j++)
                {
                    if (b[j, i] == 2 && b[j + 1, i + 1] == 2 && b[j + 2, i + 2] == 2)
                    {
                        if (spadesAsMaximizer)
                            return INF;
                        else
                            return -INF;
                    }
                }
            }

            for (int i = 2; i >= 0; i--) // leta tre i rad diagonalt från höger till vänster
            {
                for (int j = 3; j >= 2; j--)
                {
                    if (b[j, i] == 2 && b[j - 1, i + 1] == 2 && b[j - 2, i + 2] == 2)
                    {
                        if (spadesAsMaximizer)
                            return INF;
                        else
                            return -INF;
                    }
                }
            }
            /////////////////////////////////////////////////////////////////////
            //                        LETA PJÄSER                              //
            /////////////////////////////////////////////////////////////////////
            int[] lookupTable = new int[20] { 10, 20, 20, 10, 30, 50, 50, 30, 40, 80, 80, 40, 30, 50, 50, 30, 10, 20, 20, 10 };
            int heartsScore = 0, spadesScore = 0, pos = 0;
            for (int y = 0; y < 5; y++)
                for (int x = 0; x < 4; x++)
                {
                    if (b[x, y] == 1) heartsScore += lookupTable[pos];
                    if (b[x, y] == 2) spadesScore += lookupTable[pos];
                    pos++;
                }

            ////////////////////////////////////////////////////////////////////
            //                    RETURNERA RESULTAT                          //
            ////////////////////////////////////////////////////////////////////
            if (spadesAsMaximizer)
                return spadesScore - heartsScore;
            else
                return heartsScore - spadesScore;
        }

        string[] GetLegalMoves(int[,] b, bool asSpades) // hitta alla giltiga drag på brädet b
        {
            // OBS! för enkelhetens skull kommer dragen i listan inte vara AN-noterade
            // utan endast bestå av koordinater och skiljetecken. T ex 00-00 motsvarar
            // AN-notationen a5-b5.
            int key = 1;
            if (asSpades)
                key = 2;
            List<string> moves = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (b[i, j] == key) // om det finns en pjäs på rutan [i, j] som tillhör
                                        // den spelare som kör algoritmen
                    {
                        if (i - 1 >= 0 && b[i - 1, j] == 0) // vänster
                            moves.Add(Convert.ToString(i) + Convert.ToString(j) +
                                "-" + Convert.ToString(i - 1) + Convert.ToString(j));
                        if (j - 1 >= 0 && b[i, j - 1] == 0) // framåt
                            moves.Add(Convert.ToString(i) + Convert.ToString(j) +
                                "-" + Convert.ToString(i) + Convert.ToString(j - 1));
                        if (j + 1 < 5 && b[i, j + 1] == 0) // bakåt
                            moves.Add(Convert.ToString(i) + Convert.ToString(j) +
                                "-" + Convert.ToString(i) + Convert.ToString(j + 1));
                        if (i + 1 < 4 && b[i + 1, j] == 0) // höger
                            moves.Add(Convert.ToString(i) + Convert.ToString(j) +
                                "-" + Convert.ToString(i + 1) + Convert.ToString(j));
                    }
                }
            }
            return moves.ToArray();
        }

        void MakeMove(string moveToMake, int[,] b, bool asSpades)
        {
            int key = 1;
            if (asSpades)
                key = 2;
            b[(int)Char.GetNumericValue(moveToMake[0]), 
                (int)Char.GetNumericValue(moveToMake[1])] = 0;
            b[(int)Char.GetNumericValue(moveToMake[3]),
                (int)Char.GetNumericValue(moveToMake[4])] = key;
        }

        string Parse(string moveToParse) // omvandla ett drag till AN-notation
        {
            // t ex 00-10 -> a5-b5
            char keyTo = 'a';
            if (moveToParse[3] == '1')
                keyTo = 'b';
            else if (moveToParse[3] == '2')
                keyTo = 'c';
            else if (moveToParse[3] == '3')
                keyTo = 'd';
            char keyFrom = 'a';
            if (moveToParse[0] == '1')
                keyFrom = 'b';
            else if (moveToParse[0] == '2')
                keyFrom = 'c';
            else if (moveToParse[0] == '3')
                keyFrom = 'd';

            return Convert.ToString(keyFrom) +
                Convert.ToString(((int)Char.GetNumericValue(moveToParse[1]) - 5) / -1) +
                "-" + Convert.ToString(keyTo) +
                Convert.ToString(((int)Char.GetNumericValue(moveToParse[4]) - 5) / -1);
        }

        /************************************************************************
         *
         *                                  Meny
         *                      
         ************************************************************************/
        private void avslutaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void newGame_Click(object sender, EventArgs e)
        {
            RemoveMarker();

            selected = false;
            boardLocked = false;
            piece = null;
            spadesToMove = true;

            if (spelaMotDatornToolStripMenuItem.Checked)
                this.Text = "Strategi för två (du i draget)";
            else if (låsUppBrädetToolStripMenuItem.Checked)
                this.Text = "Strategi för två (flytta fritt)";

            gameMoves.Clear();
            currMove = -1;
            try
            {
                m.ClearMoves();
            }
            catch
            {

            }

            board = new int[4, 5] { { 1, 0, 0, 0, 2 }, { 2, 0, 0, 0, 1 }, 
                                  { 1, 0, 0, 0, 2 }, { 2, 0, 0, 0, 1 } };

            spades1.Location = new Point(0, 424);
            spades2.Location = new Point(200, 424);
            spades3.Location = new Point(100, 24);
            spades4.Location = new Point(300, 24);
            hearts1.Location = new Point(100, 424);
            hearts2.Location = new Point(300, 424);
            hearts3.Location = new Point(0, 24);
            hearts4.Location = new Point(200, 24);
        }

        private void omToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

        private void sparaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Spara...";
            saveFileDialog1.Filter = "Textfiler (*.txt)|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string file = saveFileDialog1.FileName;
                    StreamWriter sw = new StreamWriter(file, false);      
                    // Aktuell position
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            sw.Write(board[i, j]);
                        }
                    }
                    // Dragföljd
                    sw.WriteLine();
                    foreach (string gameMove in gameMoves)
                        sw.WriteLine(gameMove);
                    // Stopptecken
                    sw.WriteLine("/");
                    // Extra
                    if (piece != null)
                    {
                        sw.Write(Convert.ToString(((PictureBox)piece).Location.X / 100));
                        sw.Write(Convert.ToString((((PictureBox)piece).Location.Y - 24) / 100));
                        sw.WriteLine();
                    }
                    else
                        sw.WriteLine("null");
                    sw.WriteLine(spadesToMove);
                    sw.Close();
                    saved = true;
                }
                catch
                {
                    MessageBox.Show("Filen kunde inte sparas.");
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!saved)
            {
                if (MessageBox.Show("Partiet har inte sparats. Vill du spara partiet nu?", "Spara",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                    sparaToolStripMenuItem_Click(sender, e);
            }
            openFileDialog1.Title = "Öppna...";
            openFileDialog1.Filter = "Textfiler (*.txt)|*.txt"; // visa endast textfiler
            if (openFileDialog1.ShowDialog() == DialogResult.OK) // om användaren öppnar en fil
            {
                try
                {
                    string file = openFileDialog1.FileName;
                    StreamReader sr = new StreamReader(file);

                    char bit = Convert.ToChar(sr.Read());
                    bool h1 = false, h2 = false, h3 = false, h4 = false;
                    bool s1 = false, s2 = false, s3 = false, s4 = false;

                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            board[i, j] = (int)Char.GetNumericValue(bit);
                            if (board[i, j] == 1) // hjärter på pos [i, j]
                            {
                                if (!h1) // om h1 inte är på sin rätta position
                                {
                                    hearts1.Location = new Point(i * 100, j * 100 + 24);
                                    h1 = true; // h1 är klar
                                }
                                else if (!h2)
                                {
                                    hearts2.Location = new Point(i * 100, j * 100 + 24);
                                    h2 = true;
                                }
                                else if (!h3)
                                {
                                    hearts3.Location = new Point(i * 100, j * 100 + 24);
                                    h3 = true;
                                }
                                else if (!h4)
                                {
                                    hearts4.Location = new Point(i * 100, j * 100 + 24);
                                    h4 = true;
                                }
                            }
                            if (board[i, j] == 2)
                            {
                                if (!s1)
                                {
                                    spades1.Location = new Point(i * 100, j * 100 + 24);
                                    s1 = true;
                                }
                                else if (!s2)
                                {
                                    spades2.Location = new Point(i * 100, j * 100 + 24);
                                    s2 = true;
                                }
                                else if (!s3)
                                {
                                    spades3.Location = new Point(i * 100, j * 100 + 24);
                                    s3 = true;
                                }
                                else if (!s4)
                                {
                                    spades4.Location = new Point(i * 100, j * 100 + 24);
                                    s4 = true;
                                }
                            }
                            bit = Convert.ToChar(sr.Read());
                        }
                    }
                    // Brädet är klart
                    string line = sr.ReadLine();
                    line = sr.ReadLine();
                    gameMoves.Clear();
                    while (line != "/")
                    {
                        gameMoves.Add(line);
                        line = sr.ReadLine();
                    }
                    RemoveMarker();
                    line = sr.ReadLine();
                    if (line == "null")
                    {
                        piece = null;
                        selected = false;
                    }
                    else
                    {
                        int pY = Convert.ToInt32(Char.GetNumericValue(line[1])) * 100 + 24;
                        int pX = Convert.ToInt32(Char.GetNumericValue(line[0])) * 100;
                        Point p = new Point(pX, pY);
                        if (p == hearts1.Location) hearts1.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                        if (p == hearts2.Location) hearts2.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                        if (p == hearts3.Location) hearts3.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                        if (p == hearts4.Location) hearts4.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                        if (p == spades1.Location) spades1.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                        if (p == spades2.Location) spades2.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                        if (p == spades3.Location) spades3.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                        if (p == spades4.Location) spades4.BackgroundImage = Strategi_för_två.Properties.Resources.marker;
                        if (p == hearts1.Location) piece = hearts1;
                        if (p == hearts2.Location) piece = hearts2;
                        if (p == hearts3.Location) piece = hearts3;
                        if (p == hearts4.Location) piece = hearts4;
                        if (p == spades1.Location) piece = spades1;
                        if (p == spades2.Location) piece = spades2;
                        if (p == spades3.Location) piece = spades3;
                        if (p == spades4.Location) piece = spades4;
                        selected = true;
                    }
                    boardLocked = GameOver(board);
                    spadesToMove = Convert.ToBoolean(sr.ReadLine());
                    saved = true;
                    sr.Close(); // data har lästs in, stäng filen
                    if (medEnVänToolStripMenuItem.Checked && gameMoves.Count > -1)
                        bakåtToolStripMenuItem.Checked = true;
                    try
                    {
                        currMove = gameMoves.Count - 1;
                        foreach (string gameMove in gameMoves)
                            m.AddMove(gameMove);
                    }
                    catch
                    {

                    }
                }
                catch
                {
                    MessageBox.Show("Filen kan inte öppnas.");
                }
            }
        }

        private void spelaSjälvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bakåtToolStripMenuItem.Enabled = false;
            if (låsUppBrädetToolStripMenuItem.Checked)
                boardLocked = GameOver(board);
            spelaMotDatornToolStripMenuItem.Checked = false;
            medEnVänToolStripMenuItem.Checked = false;
            låsUppBrädetToolStripMenuItem.Checked = false;
        }

        private void flyttaPjäsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Flytta_pjäs().ShowDialog(); // showDialog gör att programmet väntar
                                            // med att köra nästa kodrad tills fönstret
                                            // Flytta_pjäs har stängts
            int[,] backup = board;
            try
            {
                if (move == "korvfest") // påskägg
                {
                    hearts1.Image = Strategi_för_två.Properties.Resources.skull;
                    hearts2.Image = Strategi_för_två.Properties.Resources.skull;
                    hearts3.Image = Strategi_för_två.Properties.Resources.skull;
                    hearts4.Image = Strategi_för_två.Properties.Resources.skull;
                    move = "";
                }
                else if (!boardLocked)
                {
                    PerformMove(move, board);
                    RedrawBoard(move);
                    boardLocked = GameOver(board);
                    saved = false;
                    spadesToMove = !spadesToMove;
                    try
                    {
                        m.AddMove(move);
                    }
                    catch
                    {

                    }
                    gameMoves.Add(move);
                    currMove++;
                    bakåtToolStripMenuItem.Enabled = true;
                    x_old = Convert.ToInt32(Char.GetNumericValue(move[3]));
                    y_old = Convert.ToInt32(Char.GetNumericValue(move[4]));
                    if (boardLocked) // någon har vunnit
                    {
                        if (Spades(piece)) // spader har vunnit
                            MessageBox.Show("Spader vann!");
                        else
                            MessageBox.Show("Hjärter vann!");
                    }
                    if (medEnVänToolStripMenuItem.Checked)
                    {
                        if (spadesToMove)
                            this.Text = "Strategi för två (spader i draget)";
                        else
                            this.Text = "Strategi för två (hjärter i draget)";
                    }
                    else if (låsUppBrädetToolStripMenuItem.Checked)
                        this.Text = "Strategi för två (flytta fritt)";
                    else if (spelaMotDatornToolStripMenuItem.Checked)
                    {
                        this.Text = "Strategi för två (datorn i draget)";
                        ComputeMove();
                    }
                }
            }
            catch
            {
                // Något gick fel, återställ brädet
                board = backup;
            }
        }

        private void medEnVänToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bakåtToolStripMenuItem.Enabled = true;
            if (låsUppBrädetToolStripMenuItem.Checked)
                boardLocked = GameOver(board);
            spelaMotDatornToolStripMenuItem.Checked = false;
            medEnVänToolStripMenuItem.Checked = true;
            låsUppBrädetToolStripMenuItem.Checked = false;
            if (spadesToMove)
                this.Text = "Strategi för två (spader i draget)";
            else
                this.Text = "Strategi för två (hjärter i draget)";
        }

        private void spelaMotDatornToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bakåtToolStripMenuItem.Enabled = false;
            if (låsUppBrädetToolStripMenuItem.Checked)
                boardLocked = GameOver(board);
            spelaMotDatornToolStripMenuItem.Checked = true;
            medEnVänToolStripMenuItem.Checked = false;
            låsUppBrädetToolStripMenuItem.Checked = false;

            if (spadesToMove)
                this.Text = "Strategi för två (du i draget)";
            else // datorn är i draget
            {
                this.Text = "Strategi för två (datorn i draget)";
                ComputeMove();
            }
        }

        private void bytBrädeToolStripMenuItem_Click(object sender, EventArgs e) // ändra bakgrundsbilden
        {
            new Byt_bräde().ShowDialog();
        }

        private void ledtrådToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (boardLocked)
            {
                MessageBox.Show("Inga giltiga drag hittades.");
            }
            else // ge ledtråd
            {
                Thread engine = new Thread(new ThreadStart(Minimax));
                engine.Start();
                engine.Join();
                MessageBox.Show(Parse(bestMove));
            }
        }

        private void låsUppBrädetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bakåtToolStripMenuItem.Enabled = true;
            boardLocked = false;
            spelaMotDatornToolStripMenuItem.Checked = false;
            medEnVänToolStripMenuItem.Checked = false;
            låsUppBrädetToolStripMenuItem.Checked = true;

            this.Text = "Strategi för två (flytta fritt)";
        }

        private void visaDragföljdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m = new Moves();
            m.Show();
        }

        private void bakåtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Gå bakåt ett steg
            if (currMove >= 0)
            {
                spadesToMove = !spadesToMove;
                string backMove = gameMoves[currMove].Substring(3, 2) + "-" + gameMoves[currMove].Substring(0, 2);
                RedrawBoard(backMove);
                PerformMove(backMove, board);
                boardLocked = GameOver(board);
                currMove--;
                framåtToolStripMenuItem.Enabled = true;
                if (currMove == -1) // startpositionen är nådd
                    bakåtToolStripMenuItem.Enabled = false;
            }
        }

        private void framåtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Flytta framåt ett steg
            if (currMove < gameMoves.Count - 1)
            {
                currMove++;
                RedrawBoard(gameMoves[currMove]);
                PerformMove(gameMoves[currMove], board);
                boardLocked = GameOver(board);
                spadesToMove = !spadesToMove;
                bakåtToolStripMenuItem.Enabled = true;
                if (currMove == gameMoves.Count - 1) // slutpositionen är nådd
                    framåtToolStripMenuItem.Enabled = false;
            }
        }

        private void bytSidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (spelaMotDatornToolStripMenuItem.Checked)
            {
                this.Text = "Strategi för två (datorn i draget)";
                ComputeMove();
            }
        }
    }
}