using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graph_Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //AllocConsole();
            InitializeComponent();
            this.MouseWheel += PictureBox_MouseWheel;
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        double units = 10;
        float horizontalOffset = 0;
        float verticalOffset = 0;
        float mousePositionX = 0;
        float mousePositionY = 0;
        Graphics bigG;
        Pen mainAxisPen = new Pen(Color.Red, 2);
        Pen secondaryAxisPen = new Pen(Color.LightGray, .01f);
        Equation eq = new Equation();
        ToolTip tt = new ToolTip();


        private void PictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0 && units < 100)
            {
                units++;
                canvasPictBox.Refresh();
            }
            if (e.Delta <= 100 && units > 5)
            {
                units--;
                canvasPictBox.Refresh();
            }
        }

        private void canvasPictBox_Paint(object sender, PaintEventArgs e)
        {
            initializeComponent(sender, e);

        }
        public void initializeComponent(object sender, PaintEventArgs e)
        {
            float width = canvasPictBox.Width;
            float height = canvasPictBox.Height;

            bigG = e.Graphics;


            int numVerticalLines = (int)Math.Round((width * 2) / units, 0);
            int numHorizontalLines = (int)Math.Round((height * 2) / units, 0);

            for (int i = 1; i <= numVerticalLines; i++)
            {

                bigG.DrawLine(
                    secondaryAxisPen,
                    (float)((width / 2) + (units * i)) +horizontalOffset,
                    0,
                    (float)((width / 2) + (units * i)) + horizontalOffset,
                    height
                    );

                bigG.DrawLine(
                    secondaryAxisPen,
                    (float)((width / 2) - (units * i))  + horizontalOffset,
                    0 ,
                  (float)((width / 2) - (units * i)) + horizontalOffset,
                  height
                  );
            }
            for (int i = 1; i <= numHorizontalLines; i++)
            {
                bigG.DrawLine(
                    secondaryAxisPen,
                    0,
                    (float)((height / 2) + (units * i)) + verticalOffset,
                    width,
                    (float)((height / 2) + (units * i)) +verticalOffset
                    );
                bigG.DrawLine(
                    secondaryAxisPen,
                    0,
                    (float)((height / 2) - (units * i)) + verticalOffset,
                    width,
                    (float)((height / 2) - (units * i)) + verticalOffset
                    );
            }

            //vertical line
            bigG.DrawLine(
                mainAxisPen,
                width / 2 + horizontalOffset,
                0,
                width / 2 + horizontalOffset,
                height
                );

            //horizontal line
            bigG.DrawLine(
                mainAxisPen,
                0,
                height / 2 + verticalOffset,
                width,
                height / 2 + verticalOffset
                );

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            eq.text = eq1txt.Text;
            //Console.WriteLine(eq.text);

        }
        private void canvasPictBox_MouseDown(object sender, MouseEventArgs e)
        {
            mousePositionX = e.X;
            mousePositionY = e.Y;
            //Console.WriteLine($"X: {mousePositionX} Y: {mousePositionY}");
        }

        private void canvasPictBox_MouseUp(object sender, MouseEventArgs e)
        {
            horizontalOffset += e.X - mousePositionX;
            verticalOffset += e.Y - mousePositionY;
            //Console.WriteLine($"X offset: {horizontalOffset} Y offset: {verticalOffset}");
            canvasPictBox.Refresh();
        }



        //TODO create a tooltip that refreshes more often
        private void canvasPictBox_MouseHover(object sender, EventArgs e)
        {
            string position = $"X: {MousePosition.X} Y: {MousePosition.Y}";
            tt.SetToolTip(canvasPictBox, position);
        }
    }
    class Equation
    {
        public string text { get; set; }
    }
    class Point
    {
        public float X { get; set; }
        public float Y { get; set; }
    }
}
