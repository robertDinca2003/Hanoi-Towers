using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Hanoi_Tower
{
    public partial class Form1 : Form
    {

        Dictionary<int, Color> pieceColor = new Dictionary<int, Color>();

        bool GameStarted = false;

        int[,] towers = new int[8, 4];

        int[] t = { 0, 7, 1, 1 };

        int nrMoves = 0;
        int moves = 0;
        int first, second;

        //Start Game
        private void button1_Click(object sender, EventArgs e)
        {
            if (GameStarted)
            {
                MessageBox.Show("You have to complete the game or press END GAME !");
            }
            else
            {

                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;

                label2.Text = "";
                label1.BackColor = Color.White;

                int maxSize = Convert.ToInt32(numericUpDown1.Value);

                if (maxSize < 3 || maxSize > 7) {
                    MessageBox.Show("Tower size out of range ( 3 - 7 )");
                    return;
                }

                GameStarted = true;
                numericUpDown1.Enabled = false;

                t[1] = maxSize;
                t[2] = 0;
                t[3] = 0;

                for (int i = maxSize; i >= 1; i--) { towers[i, 1] = maxSize - i + 1; towers[i, 2] = 0; towers[i, 3] = 0; }

                Form1_Load(sender,e); 
            }
        }

       

        public Form1()
        {
            InitializeComponent();
            pieceColor.Add(7, ColorTranslator.FromHtml("#336600"));
            pieceColor.Add(6, ColorTranslator.FromHtml("#669900"));
            pieceColor.Add(5, ColorTranslator.FromHtml("#cc0000"));
            pieceColor.Add(4, ColorTranslator.FromHtml("#e600e6"));
            pieceColor.Add(3, ColorTranslator.FromHtml("#cc33ff"));
            pieceColor.Add(2, ColorTranslator.FromHtml("#3377ff"));
            pieceColor.Add(1, ColorTranslator.FromHtml("#4ddbff"));

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button3.BackColor = Color.White;
            button4.BackColor = Color.White;
            button5.BackColor = Color.White;

            Graphics g = this.CreateGraphics();
            SolidBrush myBrush = new SolidBrush(Color.Red);
            SolidBrush secondBrush = new SolidBrush(Color.Brown);

            if (checkWinner())
            {
                g.Clear(Color.Yellow);
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;

                label1.BackColor = Color.Yellow;
                label2.Text = "YOU WON!";
            }
            else
            g.Clear(Color.White);

            int maxSize = Convert.ToInt32(numericUpDown1.Value);

            int x = 150 , y = 120 + (7-maxSize) * 30, h = 20 , w = 30;


            if(GameStarted == true)
            {
                g.FillRectangle(secondBrush, new Rectangle(x+10, y, 10, maxSize*30+10));
                g.FillRectangle(secondBrush, new Rectangle(x-70, y+maxSize*30-10, 170, 20));

                g.FillRectangle(secondBrush, new Rectangle(x + 255, y, 10, maxSize * 30 + 10));
                g.FillRectangle(secondBrush, new Rectangle(x + 175, y + maxSize * 30 - 10, 170, 20));

                g.FillRectangle(secondBrush, new Rectangle(x + 500, y, 10, maxSize * 30 + 10));
                g.FillRectangle(secondBrush, new Rectangle(x + 420, y + maxSize * 30 - 10, 170, 20));

            }
            


            for (int i = maxSize; i >= 1; i--)
            {
                if(towers[i,1] != 0)
                {
                    g.FillRectangle(new SolidBrush(pieceColor[towers[i,1]]), new Rectangle(x-(towers[i,1]-1)*10,y,w+(towers[i,1]-1)*20,h));
                    
                }

                if (towers[i, 2] != 0)
                {
                    g.FillRectangle(new SolidBrush(pieceColor[towers[i, 2]]), new Rectangle(x+245 - (towers[i,2] - 1) * 10, y, w + (towers[i,2] - 1) * 20, h));
                    
                }
                if (towers[i, 3] != 0)
                {
                    g.FillRectangle(new SolidBrush(pieceColor[towers[i, 3]]), new Rectangle(x+490 - (towers[i,3] - 1) * 10, y, w + (towers[i,3] - 1) * 20, h));
                    
                }
                y += 30;
            }

            

            g.Dispose();
            myBrush.Dispose();
        }

        // End Game
        private void button2_Click(object sender, EventArgs e)
        {


            GameStarted = false;
            numericUpDown1.Value = 0;
            numericUpDown1.Enabled = true;

            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = true;

            label1.BackColor = Color.White;
            label2.Text = "";

            for (int i = 7; i >= 1; i--) { towers[i, 1] = 0; towers[i, 2] = 0; towers[i, 3] = 0; }

            Form1_Load(sender, e);
        }

        //1
        private void button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.Gray;
            moves++;
            if(moves%2 == 1)
            {
                first = 1;
            }
            else
            {
                second = 1;
                makeMove(first,second,sender,e);
            }
        }

        //2
        private void button4_Click(object sender, EventArgs e)
        {
            button4.BackColor = Color.Gray;
            moves++;
            if (moves % 2 == 1)
            {
                first = 2;
            }
            else
            {
                second = 2;
                makeMove(first, second, sender, e);
            }
        }

        //3
        private void button5_Click(object sender, EventArgs e)
        {
            button5.BackColor = Color.Gray;
            moves++;
            if (moves % 2 == 1)
            {
                first = 3;
            }
            else
            {
                second = 3;
                makeMove(first, second, sender, e);
            }
        }

        private bool checkWinner()
        {
            if (GameStarted == false) return false;
            int maxSize = Convert.ToInt32(numericUpDown1.Value);

            int ok = 1; 
            for (int i = 1; i <= maxSize; i++) if (towers[i, 2] == 0) ok = 0;
            if (ok == 1) return true;
            ok = 1;
            for (int i = 1; i <= maxSize; i++) if (towers[i, 3] == 0) ok = 0;
            if (ok == 1) return true;
            return false;
        }

        private bool validMove(int from, int to)
        {
            //MessageBox.Show($"{towers[t[from], from]} --- {towers[t[to], to]}" );
            if (from == to) return false;
            if (towers[t[from], from] == 0) return false;
            if (towers[t[to], to] == 0) return true;
            if (towers[t[from], from] < towers[t[to], to]) return true;
            else return false;
        }

        

        private void makeMove(int from, int to, object sender, EventArgs e)
        {
            button6.Enabled = false;
            if (!validMove(from, to)) { MessageBox.Show("Invalid Move!"); Form1_Load(sender,e); return; }
            towers[t[to]+1, to] = towers[t[from], from];
            t[to]++;
            towers[t[from], from] = 0;
            t[from]--; 
            Form1_Load(sender, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!GameStarted)
            {
                MessageBox.Show("Game is not started!");
                return; 
            }
            int maxSize = Convert.ToInt32(numericUpDown1.Value);


            if(maxSize % 2 == 1)
            {
                for(int i = 1; i < Math.Pow(2,maxSize); i++)
                {
                    System.Threading.Thread.Sleep(500);
                    if (i % 3 == 1)
                    {
                        if (validMove(1,3)) makeMove(1,3,sender,e);
                        else makeMove(3,1,sender,e);
                    }
                    if(i % 3 == 2)
                    {
                        if (validMove(1,2)) makeMove(1,2,sender,e);
                        else makeMove(2,1,sender,e);
                    }
                    if (i % 3 == 0)
                    {
                        if (validMove(2,3)) makeMove(2,3,sender,e);
                        else makeMove(3,2,sender,e);
                    }

                }

            }
            else
            {
                for(int i = 1; i < Math.Pow(2, maxSize); i++)
                {
                    System.Threading.Thread.Sleep(500);
                    if (i % 3 == 1)
                    {
                        if (validMove(1, 2)) makeMove(1, 2, sender, e);
                        else makeMove(2, 1, sender, e);
                    }
                    if (i % 3 == 2)
                    {
                        if (validMove(1, 3)) makeMove(1, 3, sender, e);
                        else makeMove(3, 1, sender, e);
                    }
                    if (i % 3 == 0)
                    {
                        if (validMove(2, 3)) makeMove(2, 3, sender, e);
                        else makeMove(3, 2, sender, e);
                    }

                }
            }

        }
    }
}
