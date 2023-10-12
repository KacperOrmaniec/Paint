using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class Form1 : Form
    {
        // bitmap 

        Bitmap bitmap;

        // object containing drawing methods 

        Graphics graphics;

        // a field to check if we have the ability to draw. 

        bool canPaint = false;

        // start point and end point for pencil 

        Point startPoint, endPoint;

        // pencil 

        Pen pen = new Pen(Color.Black, 1);

        // eraser 

        Pen eraser = new Pen(Color.White, 10);

        // index for individual tools 

        // e.g., 1 - pencil, 2 - eraser, etc. 

        int index;

        // width and height of the figure 
        int width, height;
        // coordinates of the start and end points for figures 
        int startX, startY, endX, endY;

        // color selection window
        ColorDialog colorDialog = new ColorDialog();


        private void pbDrawing_MouseDown(object sender, MouseEventArgs e)
        {
            // By holding down the mouse button we can draw 
            canPaint = true;
            // As long as we hold down the mouse button, we change  
            // pencil’s end point 
            endPoint = e.Location;

            // Setting the coordinates of the starting point for drawing figures
            startX = e.X;
            startY = e.Y;
        }

        private void btnEllipse_Click(object sender, EventArgs e)
        {
            index = 3;
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            index = 4;
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            index = 5;
        }

        private void pbDrawing_Paint(object sender, PaintEventArgs e)
        {
            {
                Graphics g = e.Graphics;

                if (canPaint)
                {
                    if (index == 3)
                    {
                        g.DrawEllipse(pen, startX, startY, width, height);
                    }

                    else if (index == 4)
                    {
                        g.DrawRectangle(pen, startX, startY, width, height);
                    }

                    else if (index == 5)
                    {
                        g.DrawLine(pen, startX, startY, endX, endY);
                    }
                }
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            colorDialog.ShowDialog();
            pen.Color = colorDialog.Color;
            pbColor.BackColor = colorDialog.Color;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPG images (*.jpg)|*.jpg";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap blank = new Bitmap(pbDrawing.Width, pbDrawing.Height);
                Graphics g = Graphics.FromImage(blank);
                g.Clear(Color.White);
                g.DrawImage(bitmap, 0, 0, pbDrawing.Width, pbDrawing.Height);
                Bitmap btm = new Bitmap(blank);
                btm.Save(saveFileDialog.FileName, ImageFormat.Jpeg);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            graphics.Clear(Color.White);
            pbDrawing.Image = bitmap;
        }

        private void pbDrawing_MouseMove(object sender, MouseEventArgs e)
        {
            if (canPaint)
            {
                if (index == 1)

                {
                    // Setting the start point
                    startPoint = e.Location;
                    //textBox1.Text = startPoint.ToString(); 
                    // Drawing Lines 
                    graphics.DrawLine(pen, startPoint, endPoint);
                    // Setting the end point
                    endPoint = startPoint;
                    //textBox2.Text = endPoint.ToString();
                }
                else if (index == 2)

                {
                    // Setting the start point
                    startPoint = e.Location;
                    //textBox1.Text = startPoint.ToString(); 
                    // Drawing Lines 
                    graphics.DrawLine(eraser, startPoint, endPoint);
                    // Setting the end point
                    endPoint = startPoint;
                    //textBox2.Text = endPoint.ToString();
                }
            }
            pbDrawing.Refresh();
        }

        private void pbDrawing_MouseUp(object sender, MouseEventArgs e)
        {
            endX = e.X;
            endY = e.Y;

            // setting the width and height of the figure 
            width = endX - startX;
            height = endY - startY;

            canPaint = false;
            if (index == 3)

            {
                graphics.DrawEllipse(pen, startX, startY, width, height);
            }

            else if (index == 4)
            {
                graphics.DrawRectangle(pen, startX, startY, width, height);
            }

            else if (index == 5)
            {
                graphics.DrawLine(pen, startX, startY, endX, endY);
            }
        }

        private void btnPencil_Click(object sender, EventArgs e)
        {
            index = 1;
        }

        private void btnEraser_Click(object sender, EventArgs e)
        {
            index = 2;
        }

        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(pbDrawing.Width, pbDrawing.Height);
            graphics = Graphics.FromImage(bitmap);
            pbDrawing.Image = bitmap;
            pbColor.BackColor = Color.Black;
        }
    }
}
