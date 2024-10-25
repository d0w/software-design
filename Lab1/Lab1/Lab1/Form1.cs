using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        public int paintCount;
        public Form1()
        {
            InitializeComponent();
            paintCount = 0;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            paintCount++;
            Graphics graphics = e.Graphics;
            graphics.DrawString("My name is Derek Xu", Font, Brushes.Black, 10, 10);

            // draw paint count
            string paintEvents = "There have been " + paintCount + " paint events since the program started.";
            graphics.DrawString(paintEvents, Font, Brushes.Black, 10, 30);
        }
    }
}
