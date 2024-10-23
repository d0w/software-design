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
            this.Text = "Derek Xu";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(500, 500);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Size cs = ClientSize;
            g.TranslateTransform(cs.Width / 2.0f, cs.Height/2.0f);
            int scale = Math.Min(cs.Width, cs.Height);
            g.ScaleTransform(scale / 400.0f, scale / 800.0f);

            //g.DrawRectangle(Pens.Black, 0,0, 800, 400);
            g.DrawLine(Pens.Black, 0, -200, 0, 200);
            g.DrawLine(Pens.Black, -200, 0, 200, 0);
            //for (int i = 0; i < 6; ++i)
            //{
            //    g.FillEllipse(Brushes.Black, -50, -250, 100,
            //    100);
            //    g.RotateTransform(60.0f);
            //}


        }
    }
}
