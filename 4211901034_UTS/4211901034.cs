using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace _4211901034_UTS
{
    public partial class Form1 : Form
    {
        Bitmap sourceImage, invertImage;

        int image_height, image_width;
        int BIN = 256;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // tombol load image
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sourceImage = (Bitmap)Bitmap.FromFile(openFileDialog1.FileName);
                pictureBox1.Image = sourceImage;
                invertImage = new Bitmap(sourceImage);
                //delete the histogram
                BIN = 255;

                // width and height
                image_width = sourceImage.Width;
                image_height = sourceImage.Height;
            }
        }

        // tombol RGB Channel
        private void button2_Click(object sender, EventArgs e)
        {
            redImageChannel();
            greenImageChannel();
            blueImageChannel();
            
        }
        // tombol RGB Histogram
        private void button3_Click(object sender, EventArgs e)
        {
            redChannelHistogram();
            greenChannelHistogram();
            blueChannelHistogram();
        }

        // tombol invert image
        private void button4_Click(object sender, EventArgs e)
        {
            if (sourceImage == null) return;

            for (int x = 0; x < image_width; x++)
            {
                for (int y = 0; y < image_height; y++)
                {
                    // get rgb value of the pixel at (x, y)
                    Color w = sourceImage.GetPixel(x, y);

                    // invers image
                    int rInverse = 255 - w.R;
                    int gInverse = 255 - w.G;
                    int bInverse = 255 - w.B;

                    Color inverse_color = Color.FromArgb(rInverse, gInverse, bInverse);
                    invertImage.SetPixel(x, y, inverse_color);

                }
            }
            pictureBox8.Image = invertImage;
        }

        // tombol invert RGB
        private void button5_Click(object sender, EventArgs e)
        {
            invertRedChannel();
            invertGreenChannel();
            invertBlueChannel();
        }

        // tombol invert histogram
        private void button6_Click(object sender, EventArgs e)
        {
            invertRedHistogram();
            invertGreenHistogram();
            invertBlueHistogram();
        }


        // my function
        // red channel
        private void redImageChannel()
        {
            if (sourceImage == null) return;
            int pilChannel = 1;
            //displaying Red Channel
            Bitmap redImage = imageConvert(pilChannel);
            pictureBox2.Image = redImage;
            label4.ForeColor = Color.Red;
        }
        // green channel
        private void greenImageChannel()
        {
            if (sourceImage == null) return;
            int pilChannel = 2;
            //displaying Red Channel
            Bitmap greenImage = imageConvert(pilChannel);
            pictureBox3.Image = greenImage;
            label5.ForeColor = Color.Green;
        }
        // blue image
        private void blueImageChannel()
        {
            if (sourceImage == null) return;
            int pilChannel = 3;
            //displaying Red Channel
            Bitmap blueImage = imageConvert(pilChannel);
            pictureBox4.Image = blueImage;
            label6.ForeColor = Color.Blue;
        }
        
        // histogram

        // red channel histogram
        private void redChannelHistogram()
        {
            if (sourceImage == null) return;
            int pilChannel = 1;
            //delete the histogram
            if (chart1.Series.Count > 0)
            {
                chart1.Series.RemoveAt(0);
            }
            //chart init
            chart1.Series.Add("Red Channel Image");
            chart1.Series["Red Channel Image"].Color = Color.Red;
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
            float[] his = new float[BIN];
            his = hitungHistogram(pilChannel);
            for (int i = 0; i < BIN; i++)
            {
                chart1.Series["Red Channel Image"].Points.AddXY(i, his[i]);
            }
            label9.ForeColor = Color.Red;
        }

        private void greenChannelHistogram()
        {
            if (sourceImage == null) return;
            int pilChannel = 2;
            //delete the histogram
            if (chart2.Series.Count > 0)
            {
                chart2.Series.RemoveAt(0);
            }
            //chart init
            chart2.Series.Add("Green Channel Image");
            chart2.Series["Green Channel Image"].Color = Color.Green;
            foreach (var series in chart2.Series)
            {
                series.Points.Clear();
            }
            float[] his = new float[BIN];
            his = hitungHistogram(pilChannel);
            for (int i = 0; i < BIN; i++)
            {
                chart2.Series["Green Channel Image"].Points.AddXY(i, his[i]);
            }
            label8.ForeColor = Color.Green;
        }
        private void blueChannelHistogram()
        {
            if (sourceImage == null) return;
            int pilChannel = 3;
            //delete the histogram
            if (chart3.Series.Count > 0)
            {
                chart3.Series.RemoveAt(0);
            }
            //chart init
            chart3.Series.Add("Blue Channel Image");
            chart3.Series["Blue Channel Image"].Color = Color.Blue;
            foreach (var series in chart3.Series)
            {
                series.Points.Clear();
            }
            float[] his = new float[BIN];
            his = hitungHistogram(pilChannel);
            for (int i = 0; i < BIN; i++)
            {
                chart3.Series["Blue Channel Image"].Points.AddXY(i, his[i]);
            }
            label7.ForeColor = Color.Blue;
        }

        // hitung histogram
        private float[] hitungHistogram(int imageChannel)
        {
            //init of bins
            BIN = 256;
            //initializaation of histogram el
            float[] h = new float[BIN];
            //histogram init
            for (int i = 0; i < BIN; i++)
            {
                h[i] = 0;
            }
            //histogram calculation
            for (int x = 0; x < sourceImage.Width; x++)
                for (int y = 0; y < sourceImage.Height; y++)
                {
                    Color w = sourceImage.GetPixel(x, y);
                    int r = (int)(w.R * BIN / 256);
                    int g = (int)(w.G * BIN / 256);
                    int b = (int)(w.B * BIN / 256);
                    //calculate gray channel
                    int gray = (int)((0.5 * w.R + 0.419 * w.G + 0.081 * w.B) * BIN / 256);
                    //calculate histogram
                    if (imageChannel == 1)
                        h[r] = h[r] + 1;
                    else if (imageChannel == 2)
                        h[g] = h[g] + 1;
                    else if (imageChannel == 3)
                        h[b] = h[b] + 1;
                    else if (imageChannel == 4)
                        h[gray] = h[gray] + 1;
                }
            return h;
        }

        // histogram

        // invert image
        private float[] hitungInvertHistogram(int imageChannel)
        {
            //init of bins
            BIN = 256;
            //initializaation of histogram el
            float[] h = new float[BIN];
            //histogram init
            for (int i = 0; i < BIN; i++)
            {
                h[i] = 0;
            }
            //histogram calculation
            for (int x = 0; x < invertImage.Width; x++)
                for (int y = 0; y < invertImage.Height; y++)
                {
                    Color w = invertImage.GetPixel(x, y);
                    int r = (int)(w.R * BIN / 256);
                    int g = (int)(w.G * BIN / 256);
                    int b = (int)(w.B * BIN / 256);

                    //calculate histogram
                    if (imageChannel == 1)
                        h[r] = h[r] + 1;
                    else if (imageChannel == 2)
                        h[g] = h[g] + 1;
                    else if (imageChannel == 3)
                        h[b] = h[b] + 1;
                }
            return h;
        }

        // invert
        // red invert
        private void invertRedChannel()
        {
            if (invertImage == null) return;
            int pilChannel = 1;
            //displaying Red Channel
            Bitmap redImage = inverseImageConvert(pilChannel);
            pictureBox7.Image = redImage;
            label12.ForeColor = Color.Red;
        }

        // green invert
        private void invertGreenChannel()
        {
            if (invertImage == null) return;
            int pilChannel = 2;
            //displaying Red Channel
            Bitmap greenImage = inverseImageConvert(pilChannel);
            pictureBox6.Image = greenImage;
            label11.ForeColor = Color.Green;
        }

        // blue invert
        private void invertBlueChannel()
        {
            if (invertImage == null) return;
            int pilChannel = 3;
            //displaying Red Channel
            Bitmap redImage = inverseImageConvert(pilChannel);
            pictureBox5.Image = redImage;
            label10.ForeColor = Color.Blue;
        }

        // invert image
        private Bitmap imageConvert(int imageChannel)
        {
            if (sourceImage == null) return null;
            Bitmap convImage = new Bitmap(sourceImage);
            for (int x = 0; x < sourceImage.Width; x++)
                for (int y = 0; y < sourceImage.Height; y++)
                {
                    //get the RGB value of the pixel at (x,y)
                    Color w = sourceImage.GetPixel(x, y);
                    byte r = w.R; //red value
                    byte g = w.G; // green value
                    byte b = w.B; // blue value                

                    Color redColor = Color.FromArgb(r, 0, 0);
                    Color greenColor = Color.FromArgb(0, g, 0);
                    Color blueColor = Color.FromArgb(0, 0, b);

                    //set the image pixel
                    if (imageChannel == 1) //red
                    {
                        convImage.SetPixel(x, y, redColor);
                    }
                    else if (imageChannel == 2) //green
                    {
                        convImage.SetPixel(x, y, greenColor);
                    }
                    else if (imageChannel == 3) //blue
                    {             
                        convImage.SetPixel(x, y, blueColor);
                    }
                }
            return convImage;
        }
        // invert
        private Bitmap inverseImageConvert(int imageChannel)
        {
            if (sourceImage == null) return null;
            Bitmap convImage = new Bitmap(invertImage);
            for (int x = 0; x < invertImage.Width; x++)
                for (int y = 0; y < invertImage.Height; y++)
                {
                    //get the RGB value of the pixel at (x,y)
                    Color w = invertImage.GetPixel(x, y);
                    byte r = w.R; //red value
                    byte g = w.G; // green value
                    byte b = w.B; // blue value                

                    Color redColor = Color.FromArgb(r, 0, 0);
                    Color greenColor = Color.FromArgb(0, g, 0);
                    Color blueColor = Color.FromArgb(0, 0, b);

                    //set the image pixel
                    if (imageChannel == 1) //red
                    {
                        convImage.SetPixel(x, y, redColor);
                    }
                    else if (imageChannel == 2) //green
                    {
                        convImage.SetPixel(x, y, greenColor);
                    }
                    else if (imageChannel == 3) //blue
                    {
                        convImage.SetPixel(x, y, blueColor);
                    }
                }
            return convImage;
        }
        // invert histogram
        private void invertRedHistogram()
        {
            if (sourceImage == null || invertImage == null) return;
            int pilChannel = 1;
            //delete the histogram
            if (chart4.Series.Count > 0)
            {
                chart4.Series.RemoveAt(0);
            }
            //chart init
            chart4.Series.Add("Red Channel Image");
            chart4.Series["Red Channel Image"].Color = Color.Red;
            foreach (var series in chart4.Series)
            {
                series.Points.Clear();
            }
            float[] his = new float[BIN];
            his = hitungInvertHistogram(pilChannel);
            for (int i = 0; i < BIN; i++)
            {
                chart4.Series["Red Channel Image"].Points.AddXY(i, his[i]);
            }
            label14.ForeColor = Color.Red;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void invertGreenHistogram()
        {
            if (sourceImage == null || invertImage == null) return;
            int pilChannel = 2;
            //delete the histogram
            if (chart5.Series.Count > 0)
            {
                chart5.Series.RemoveAt(0);
            }
            //chart init
            chart5.Series.Add("Green Channel Image");
            chart5.Series["Green Channel Image"].Color = Color.Green;
            foreach (var series in chart5.Series)
            {
                series.Points.Clear();
            }
            float[] his = new float[BIN];
            his = hitungInvertHistogram(pilChannel);
            for (int i = 0; i < BIN; i++)
            {
                chart5.Series["Green Channel Image"].Points.AddXY(i, his[i]);
            }
            label15.ForeColor = Color.Green;
        }
        private void invertBlueHistogram()
        {
            if (sourceImage == null || invertImage == null) return;
            int pilChannel = 3;
            //delete the histogram
            if (chart6.Series.Count > 0)
            {
                chart6.Series.RemoveAt(0);
            }
            //chart init
            chart6.Series.Add("Blue Channel Image");
            chart6.Series["Blue Channel Image"].Color = Color.Blue;
            foreach (var series in chart6.Series)
            {
                series.Points.Clear();
            }
            float[] his = new float[BIN];
            his = hitungInvertHistogram(pilChannel);
            for (int i = 0; i < BIN; i++)
            {
                chart6.Series["Blue Channel Image"].Points.AddXY(i, his[i]);
            }
            label16.ForeColor = Color.Blue;
        }
  
    }
}
