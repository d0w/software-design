using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Lab2
{
    public partial class Form1 : Form
    {
        private const int GRID = 40;
        private ArrayList coordinates = new ArrayList();
        private const int CIR_WIDTH = 20;
        private const int CIR_HEIGHT = 20;
        private bool aligned = false;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Derek Xu - Lab 2";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // override OnPaint
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            int x = GRID;
            int y = GRID;
            while (x < ClientRectangle.Width)
            {
                g.DrawLine(Pens.Black, x, 0, x, ClientRectangle.Height);
                x += GRID;
            }
            while (y < ClientRectangle.Height)
            {
                g.DrawLine(Pens.Black, 0, y, ClientRectangle.Width, y);
                y += GRID;
            }

            foreach (Point p in this.coordinates)
            {
                if (!aligned)
                {
                    // if no need to align, just paint at coordinate
                    g.FillEllipse(Brushes.Red, p.X - CIR_WIDTH / 2, p.Y - CIR_WIDTH / 2, CIR_WIDTH, CIR_HEIGHT);
                } else
                {
                    // if aligned, then find how much the coordinate passes the line by using modulo GRID
                    // then, if the remainder is > half the GRID separation, then set the new coordinate to add the GRID - remainder.
                    // if the remainder is < half the GRID separation, then set the new coordinate to the original - remainder.
                    int xPos = p.X % GRID > GRID / 2 ? p.X + (GRID - p.X % GRID) : p.X - p.X % GRID;
                    int yPos = p.Y % GRID > GRID / 2 ? p.Y + (GRID - p.Y % GRID) : p.Y - p.Y % GRID;
                    g.FillEllipse(Brushes.Red, xPos - CIR_WIDTH / 2, yPos - CIR_WIDTH / 2, CIR_WIDTH, CIR_HEIGHT);
                }

            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            // if mouse click, add new point to the ArrayList and invalidate the form to trigger OnPaint again
            if (e.Button == MouseButtons.Left)
            {
                Point p = new Point(e.X, e.Y);
                this.coordinates.Add(p);
                this.Invalidate();
            }
            if (e.Button == MouseButtons.Right)
            {
                // clear ArrayList
                this.coordinates.Clear();
                this.Invalidate();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // toggle alignment variable, set button's text and invalidate
            if (aligned)
            {
                aligned = false;
                button1.Text = "Align";
                this.Invalidate();
            } 
            else
            {
                aligned = true;
                button1.Text = "Original";
                this.Invalidate();
            }
        }
    }
}
