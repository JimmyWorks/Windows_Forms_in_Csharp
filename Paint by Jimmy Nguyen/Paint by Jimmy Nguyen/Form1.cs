using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_by_Jimmy_Nguyen
{
    public partial class MainPanel : Form
    {
        #region Members

        int brushSize = 1;
        Graphics g;
        Pen p;
        bool isPainting;
        int? initialX, initialY;
        drawMode mode;

        enum drawMode
        {
            BRUSH,
            CIRCLE,
            SQUARE,
            RECTANGLE
        };

        #endregion
        public MainPanel()
        {
            InitializeComponent();
            intialize();
        }

        internal void intialize()
        {
            canvasPanel.BackColor = Color.White;
            brushColorButton.BackColor = Color.Black;
            brushSizeButton.Text = "1";
            shapeSizeButton.Text = "20";
            isPainting = false;
            initialX = initialY = null;
            mode = drawMode.BRUSH;
            g = canvasPanel.CreateGraphics();
        }

        private void canvasColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if(cd.ShowDialog() == DialogResult.OK)
            {
                canvasPanel.BackColor = cd.Color;
                canvasColorButton.BackColor = cd.Color;
            }
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            isPainting = true;
            if (isPainting)
            {
                int size = int.Parse(shapeSizeButton.Text);

                switch (mode)
                {
                    case drawMode.BRUSH:
                        initialX = e.X;
                        initialY = e.Y;
                        break;
                    case drawMode.CIRCLE:
                        SolidBrush sb = new SolidBrush(brushColorButton.BackColor);
                        g.FillEllipse(sb, e.X-size/2, e.Y-size/2, size, size);
                        turnOffTool(mode);
                        break;
                    case drawMode.RECTANGLE:
                        sb = new SolidBrush(brushColorButton.BackColor);
                        g.FillRectangle(sb, e.X-size, e.Y-size/2, size*2, size);
                        turnOffTool(mode);
                        break;
                    case drawMode.SQUARE:
                        sb = new SolidBrush(brushColorButton.BackColor);
                        g.FillRectangle(sb, e.X-size/2, e.Y-size/2, size, size);
                        turnOffTool(mode);
                        break;
                    default:
                        Console.WriteLine("Draw mode not supported.");
                        break;
                }
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            initialX = initialY = null;
            isPainting = false;
            if (mode != drawMode.BRUSH)
                mode = drawMode.BRUSH;
        }

        private void brushColorButton_backColorChanged(object sender, EventArgs e)
        {
            if(brushColorButton.BackColor == Color.Black)
            {
                brushColorButton.ForeColor = Color.White;
            }
            else
            {
                brushColorButton.ForeColor = Color.Black;
            }
        }

        private void brushColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if(cd.ShowDialog() == DialogResult.OK)
            {
                brushColorButton.BackColor = cd.Color;

            }
        }

        private void canvasColorButton_backColorChanged(object sender, EventArgs e)
        {
            if (canvasColorButton.BackColor == Color.Black)
            {
                canvasColorButton.ForeColor = Color.White;
            }
            else
            {
                canvasColorButton.ForeColor = Color.Black;
            }
        }

        private void createCircleButton_Click(object sender, EventArgs e)
        {
            if (mode == drawMode.CIRCLE)
            {
                turnOffTool(mode);
            }
            else
            {
                turnOffTool(mode);
                mode = drawMode.CIRCLE;
                createCircleButton.Checked = true;
            }

        }

        private void createSquareButton_Click(object sender, EventArgs e)
        {
            if (mode == drawMode.SQUARE)
            {
                turnOffTool(mode);
            }
            else
            {
                turnOffTool(mode);
                mode = drawMode.SQUARE;
                createSquareButton.Checked = true;
            }
        }

        private void mainPanel_Resized(object sender, EventArgs e)
        {
            canvasPanel.Size = new Size(0, 0);
            canvasPanel.AutoSize = true;
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPainting && mode == drawMode.BRUSH)
            {
                Pen p = new Pen(brushColorButton.BackColor, float.Parse(brushSizeButton.Text));
                //Drawing the line.  
                g.DrawLine(p, new Point(initialX ?? e.X, initialY ?? e.Y), new Point(e.X, e.Y));
                initialX = e.X;
                initialY = e.Y;
            }
        }

        private void createRectangle_Click(object sender, EventArgs e)
        {
            if (mode == drawMode.RECTANGLE)
            {
                turnOffTool(mode);
            }
            else
            {
                turnOffTool(mode);
                mode = drawMode.RECTANGLE;
                createRectangle.Checked = true;
            }


        }

        private void turnOffTool(drawMode currentMode)
        {
            mode = drawMode.BRUSH;

            switch (currentMode)
            {
                case drawMode.CIRCLE:
                    createCircleButton.Checked = false;
                    break;
                case drawMode.RECTANGLE:
                    createRectangle.Checked = false;
                    break;
                case drawMode.SQUARE:
                    createSquareButton.Checked = false;
                    break;
                default:
                    break;
                    
            }
            
        }
    }
}
