using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Аpproximation
{
    public partial class Form1 : Form
    {
        private List<float> x;
        private List<float> y;
        private List<float> y1;
        private List<float> x1;
        private float SumXY, SumX2, SumX, SumY;
        private float a, b;

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int width = ClientSize.Width - dataGridView1.Width;
            int height = ClientSize.Height;
            g.DrawLine(new Pen(Color.Black), dataGridView1.Width + 20, height / 2 + dataGridView1.Height / 2, dataGridView1.Width - 20 + width, height / 2 + dataGridView1.Height / 2);
            g.DrawLine(new Pen(Color.Black), 30 + dataGridView1.Width, 20, 30 + dataGridView1.Width, height - 20);
            for (int i = 0; i < x.Count; i++)
            {
                g.FillEllipse(new SolidBrush(Color.Black), x[i] + dataGridView1.Width + 30-2, - y[i] + height / 2 + dataGridView1.Height / 2 - 2, 4, 4);
            }
            g.DrawLine(new Pen(Color.Red), dataGridView1.Width+20,height-20-b,dataGridView1.Width+20+dataGridView1.Width-20,height-20-a*(dataGridView1.Width + 20 + dataGridView1.Width - 20)-b);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            x.Clear(); y.Clear();
            x1.Clear(); y1.Clear();
            dataGridView1.Columns.Clear(); dataGridView1.ColumnCount = 2;
            chart1.Series[0].Points.DataBindXY(x, y);
            chart1.Series[1].Points.DataBindXY(x1, y1);
            label1.Text = null;
            textBox1.Text = "0";
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            x = new List<float>();
            y = new List<float>();
            x1 = new List<float>();
            y1 = new List<float>();
            dataGridView1.ColumnCount = 2;
            label1.Text = null;
            textBox1.Text = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            x.Clear();y.Clear();
            x1.Clear();y1.Clear();
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                x.Add((float)Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value));
                y.Add((float)Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value));
            }
            Appoximation();
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            if (textBox1.Text == "0")
                chart1.ChartAreas[0].AxisX.Maximum = 100;
            else
                chart1.ChartAreas[0].AxisX.Maximum = Convert.ToInt32(textBox1.Text);
            x1.Add(0);x1.Add(Convert.ToInt32(chart1.ChartAreas[0].AxisX.Maximum));
            y1.Add(Convert.ToInt32(b));y1.Add(Convert.ToInt32(a * x1[1] + b));
            chart1.Series[0].Points.DataBindXY(x, y);
            chart1.Series[1].Points.DataBindXY(x1, y1);
            if (b < 0)
                label1.Text = "y = " + a + "x - " + Math.Abs(b);
            else
                label1.Text = "y = " + a + "x + " + b;


        }
        private void Appoximation()
        {
            SumX = 0;SumY = 0;SumXY = 0;SumX2 = 0;
            for (int i = 0; i < x.Count; i++)
            {
                SumX += x[i];
                SumY += y[i];
                SumXY += x[i] * y[i];
                SumX2 += x[i] * x[i];
            }
            a = (float)(x.Count * SumXY - SumX * SumY) / (float)(x.Count * SumX2 - SumX * SumX);
            b = (float)(SumY - a * SumX) / (float)x.Count;                               
        }
    }
}
