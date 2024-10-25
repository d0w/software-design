using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Sine Wave by Derek Xu";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(500, 500);
            ResizeRedraw = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // scaling 800x400 drawing size to client size
            Graphics g = e.Graphics;
            Size cs = ClientSize;
            g.TranslateTransform(cs.Width / 2.0f, cs.Height / 2.0f);

            // scale drawing area to 800x400
            float scale = Math.Min(cs.Width / 800.0f, cs.Height / 400.0f);
            if (scale == 0f) return;
            g.ScaleTransform(scale, scale);

            // draw the axes of 400 units long each centered at the origin
            g.DrawLine(Pens.Black, 0, -200, 0, 200);
            g.DrawLine(Pens.Black, -400, 0, 400, 0);

            // draw sine wave
            for (int x = -360; x <= 360; x++)
            {
                float radians = (float)(x * Math.PI / 180); // Convert degrees to radians
                float y = (float)Math.Sin(radians) * 100.0f; // Scale y to peak amplitude of 100 so that the peak-to-peak amplitude is 200
                // Draw a circle for each degree
                g.FillEllipse(Brushes.Black, x - 1, y - 1, 2, 2); // Centered around (x, y)
            }


        }
    }
}
