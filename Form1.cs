using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {

        private string volcano = "1305-900x600.jpg";
        private int i = 0;
        private Graphics g = null;
        private Bitmap bmp = null;
        private int[] istocnik = { 0, 0 };
        private List<Stone> stone = new List<Stone>();
        private int n = 10;
        
        private class Stone
        {
            public Double a;
            public Double b;
            public int xt;
            public int yt;
            public int v;
            public int r;
            public Stone(Double a1, Double b1, int v1,int r1, int xt1, int yt1)
            {
                a = a1;
                b = b1;
                xt = xt1;
                yt = yt1;
                v = v1;
                r = r1;

            }


        }
 
        public Form1()
        {
            
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width,pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.Black);
            g.DrawImage(
                        new Bitmap(Image.FromFile(volcano)),
                        new RectangleF(Convert.ToInt32 (bmp.Width/2-75), bmp.Height-150, 150, 150),
                        new RectangleF(0, 0, 900, 600),
                        GraphicsUnit.Pixel);
            pictureBox1.Image = bmp;
        }

        private void vivod()
        {
            g.Clear(Color.Black);
             g.DrawImage(
                         new Bitmap(Image.FromFile(volcano)),
                         new RectangleF(Convert.ToInt32(bmp.Width / 2 - 75), bmp.Height - 150, 150, 150),
                         new RectangleF(0, 0, 900, 600),
                         GraphicsUnit.Pixel);
             
            for (int j = 0; j < stone.Count; j++)
            {
                g.DrawEllipse(new Pen(Color.Red, 5), 
                    Convert.ToInt32(bmp.Width / 2 -5)+stone[j].xt,
                    (bmp.Height - 100) - stone[j].yt, stone[j].r, stone[j].r);
               
            }
            
            textBox1.Text = stone.Count.ToString();
            pictureBox1.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Double a,b;
            Double x;
            if (stone.Count < n)
            { 
                CrStone();
                CrStone();
                CrStone();
                CrStone();
            }
            for (int j = 0; j < stone.Count; j++)
            {
                a= stone[j].a;
                b=stone[j].b;
                stone[j].xt = stone[j].xt + stone[j].v;
                x = Convert.ToDouble(stone[j].xt);                
                stone[j].yt = Convert.ToInt32( a*x*x+b*x );
                if (stone[j].yt < -50) stone.RemoveAt(j);
            }
            
            
            vivod();
            i++;
        }

        private void CrStone()
        {
            var n = new Random();
            int x = n.Next(10, 125);
            if (x == 0) x = 1;
            int y = n.Next(10, 400);
            int v = n.Next(-1, 2);
            if (v == 0) v = 1;
            if (v < 0) x = x * (-1);
            int r = n.Next(1, 15);
            

            Double a;
            Double b;

            a = (y / Math.Pow(x, 2)) * (-1);
            b = x * 2 * a * (-1);

            stone.Add(new Stone(a, b, v,r, 0, 0));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            CrStone();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (n<490) n = n + 10;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (n>10) n = n - 10;
        }
    }
}
