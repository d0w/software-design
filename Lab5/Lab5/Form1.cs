using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    /*
     * Rules:
     * 1. Any live cell < two live neighbors dies
     * 2. Any live cell with 2 or 3 live neighbors lives to next generation
     * 3. Any live cell with more than 3 live neighbors dies
     * 4. Any dead cell with 3 live neighbors becomes live cell
     */
    public partial class Form1 : Form
    {
        private const int cols = 50;
        private const int rows = 25;
        private const int cellSize = 20;
        private const int yoffset = 50;
        private const int xoffset = 10;
        private Timer timer;
        private Grid currentGrid;
        public Form1()
        {
            InitializeComponent();

            this.Text = "Conway's Game of Life by Derek Xu";
            this.ClientSize = new Size(cols * cellSize + 50, rows * cellSize + 80);
            this.StartPosition = FormStartPosition.CenterScreen;

            // create buttons
            // used the following link to create them programatically
            // https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/how-to-add-to-a-form?view=netdesktop-9.0
            Button generateButton = new Button
            {
                Text = "Next Generation",
                Location = new Point(10, 10),
                Size = new Size(100, 20)
            };
            generateButton.Click += generateClick;

            Button clearButton = new Button
            {
                Text = "Clear",
                Location = new Point(150, 10),
                Size = new Size(80, 20)
            };

            clearButton.Click += clearClick;

            Button startButton = new Button
            {
                Text = "Start",
                Location = new Point(350, 10),
                Size = new Size(80, 20)
            };
            startButton.Click += startClick;

            Button stopButton = new Button
            {
                Text = "Stop",
                Location = new Point(500, 10),
                Size = new Size(80, 20)
            };
            stopButton.Click += stopClick;
            

            Controls.Add(generateButton);
            Controls.Add(clearButton);
            Controls.Add(startButton);
            Controls.Add(stopButton);


            // setup timer
            // https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.timer?view=windowsdesktop-8.0
            timer = new Timer
            {
                Interval = 500
            };
            timer.Tick += timerTick;


            // grid init
            currentGrid = new Grid(cols, rows);

        }

        private void generateClick(object sender, EventArgs e)
        {
            nextGeneration();
        }

        private void clearClick(object sender, EventArgs e)
        {
            currentGrid = new Grid(cols, rows);
            Invalidate();
        }

        private void timerTick(object sender, EventArgs e)
        {
            nextGeneration();
        }

        private void nextGeneration()
        {
            // create new grid, generate based off the current grid, set current grid to new grid and redraw form
            Grid newGrid = new Grid(cols, rows);
            currentGrid.Generate(newGrid);
            currentGrid = newGrid;
            Invalidate();
        }

        private void startClick(object sender, EventArgs e)
        {
            // start timer
            timer.Start();
        }

        private void stopClick(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            // get cell x and y coordinate based on mouse click
            int x = (e.X - xoffset) / cellSize;
            int y = (e.Y - yoffset) / cellSize;

            if (x >= 0 && y >= 0 && x < cols && y < rows) 
            {
                if (e.Button == MouseButtons.Left)
                {
                    // add if left click
                    currentGrid[x, y] = true;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    currentGrid[x, y] = false;
                }
            }

            // redraw form
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // for every cell, check draw an outlined rectangle
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    g.DrawRectangle(Pens.Black, x * cellSize + xoffset, y * cellSize + yoffset, cellSize, cellSize);

                    // fill rectangle if grid cell is set to true
                    if (currentGrid[x, y])
                    {
                        g.FillRectangle(Brushes.Black, x * cellSize + 1 + xoffset, y * cellSize + 1 + yoffset, cellSize - 1, cellSize - 1);
                    }
                }
            }
        }

    }

    public class Grid
    {
        private bool[,] cells;
        private int cols;
        private int rows;

        public Grid(int cols, int rows)
        {
            this.cols = cols;
            this.rows = rows;
            cells = new bool[cols, rows];
        }

        public bool this[int x, int y]
        {
            get
            {
                // return false for out of bounds
                if (x < 0 || y < 0 || x >= cols || y>= rows)
                {
                    return false;
                }
                return cells[x, y];
            }
            set
            {
                if (x < 0 || y < 0 || x >= cols || y >= rows)
                {
                    return;
                }
                cells[x, y] = value;
            }
        }

        public void Generate(Grid newGrid)
        {
            // for every cell 
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    // count the neighbors to determine what to do next
                    int neighbors = countNeighbors(x, y);

                    // if 3 neighbors and does not exist yet, spawn or keep alive
                    if (!this[x, y] && neighbors == 3)
                    {
                        newGrid[x, y] = true;
                    }
                    // if 2-3 neighbors and already exists, lives
                    else if (this[x, y] && (neighbors == 2 || neighbors == 3))
                    {
                        newGrid[x, y] = true;
                    }
                    // if less than 2 or more than 3 neighbors, die
                    else
                    {
                        newGrid[x, y] = false;
                    }
                }
            }
        }
        private int countNeighbors(int x, int y)
        {
            int count = 0;
            // check all 8 squares above/below/left/right
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue; // skip self
                    }

                    if (this[x + i, y + j])
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }


}
