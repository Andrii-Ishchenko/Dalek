using Dalek.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Dalek
{
    public partial class Form1 : Form
    {
        private Game game;
        private int startX, startY, endX, endY;
        private int fieldSizeX;
        private int fieldSizeY;

        private int cellSize = 36;

        public Form1(Game game)
        {
            InitializeComponent();

            this.SetStyle(
                  ControlStyles.UserPaint |
                  ControlStyles.AllPaintingInWmPaint |
                  ControlStyles.DoubleBuffer, true);

            this.game = game;
        }

        private void setSize()
        {
            var minFormSizeX = (int)(fieldSizeX * 1.2);
            var minFormSizeY = (int)(fieldSizeY * 1.2);

            if (panel1.Width < minFormSizeX)
            {
                var delta = minFormSizeX - panel1.Width;
                Width += delta;
            }


            if (panel1.Height < minFormSizeY)
            {
                var delta = minFormSizeY - panel1.Height;
                Height += delta;
            }



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fieldSizeX = cellSize * game.MX;
            fieldSizeY = cellSize * game.MY;
            setSize();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
           // Debug.WriteLine(e.KeyData);
            bool ret = false;
            switch (e.KeyCode)
            {
                case Keys.Left:
                    game.Move(-1,0);
                    break;
                case Keys.Right:
                    game.Move(1,0);
                    break;
                case Keys.Up:
                    game.Move(0,-1);
                    break;
                case Keys.Down:
                    game.Move(0,1);
                    break;
                case Keys.Space:
                    game.Blink();
                    break;
                default:
                    return;

            }

            panel1.Invalidate();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            panel1.Invalidate();

            startX = (panel1.Width - fieldSizeX) / 2;
            startY = (panel1.Height - fieldSizeY) / 2;

            endX = startX + fieldSizeX;
            endY = startY + fieldSizeY;
        }

        private void Draw()
        {
            using(Graphics g = this.panel1.CreateGraphics())
            {
                lbLevel.Text = game.Level.ToString();
                lbScore.Text = game.Score.ToString();

                DrawField(g);
                DrawHero(g);
                DrawDaleks(g);
            }

        }

        private void DrawDaleks(Graphics g)
        {
           
        }

        private void DrawHero(Graphics g)
        {
            var rectStartX = startX + game.Hero.X * cellSize + cellSize / 6;
            var rectStartY = startY + game.Hero.Y * cellSize + cellSize / 6;
          
            Brush brush = new SolidBrush(Color.DarkRed);

            g.FillRectangle(brush, rectStartX, rectStartY, cellSize*2/3, cellSize*2/3);
        }

        private void DrawField(Graphics g)
        {
            Pen borderPen = new Pen(Color.SandyBrown, 1);
            Brush backgroundBrush = new SolidBrush(Color.Lavender);

            g.FillRectangle(backgroundBrush, startX, startY, fieldSizeX, fieldSizeY);



            for (int i = 0; i <= game.MX; i++)
            {
                g.DrawLine(borderPen, startX + i * cellSize, startY, startX + i * cellSize, endY);
                //Debug.WriteLine("[{0},{1}]->[{2},{3}]", startX + i * cellSize, startY, startX + i * cellSize, endY);
            }



            for (int i = 0; i <= game.MY; i++)
            {
                g.DrawLine(borderPen, startX, startY + i * cellSize, endX, startY + i * cellSize);
               // Debug.WriteLine("[{0},{1}]->[{2},{3}]", startX, startY + i * cellSize, endX, startY + i * cellSize);
            }

            backgroundBrush.Dispose();
        }
    }
       
}
