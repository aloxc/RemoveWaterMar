using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RemoveWaterMar
{
    public partial class TestForm : Form
    {
        Bitmap Night_bitmap;//水平遮罩，垂直遮罩公用变量
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        public TestForm()
        {
            InitializeComponent();
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("./log.txt", rollingInterval: RollingInterval.Month).CreateLogger();

        }

        private void TestForm_Load(object sender, EventArgs e)
        {
        }

        void btnOpen_Click(object sender, EventArgs e)
        {

            openFileDialog1.Filter = "*.jpg,*.jpeg,*.bmp,*.gif,*.ico,*.png,*.tif,*.wmf | *.jpg; *.jpeg; *.bmp; *.gif; *.ico; *.png; *.tif; *.wmf";
            //设置打开图像的类型
            openFileDialog1.ShowDialog();//打开对话框
            if (openFileDialog1.FileName.Trim() == "")
                return;
            try
            {
                Bitmap Lazy_bitmap = new Bitmap(this.openFileDialog1.FileName);
                //得到原始大小的图像
                Night_bitmap = new Bitmap(Lazy_bitmap, this.pictureBox1.Width, this.pictureBox1.Height);
                this.pictureBox1.Image = Night_bitmap;
            }
            catch (Exception Err)
            {
                MessageBox.Show(this, "打开图像文件错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void btnShuiping_Click(object sender, EventArgs e)
        {
            int Lazy_width = this.pictureBox1.Width;//图像宽度 
            int Lazy_height = this.pictureBox1.Height;//图像高度
            Graphics Lazy_pic = this.pictureBox1.CreateGraphics();
            Night_bitmap = new Bitmap(this.pictureBox1.Image, this.pictureBox1.Width, this.pictureBox1.Height);

            //获取Graphics对象
            Lazy_pic.Clear(Color.Red);//初始为红色
            //Lazy_pic.DrawImage(Night_bitmap, this.pictureBox1.Location.X,pictureBox1.Location.Y, Lazy_width, Lazy_height);
            //Bitmap Lazy_bitmap = new Bitmap(Lazy_width, Lazy_height);
            Bitmap Lazy_bitmap = new Bitmap(this.pictureBox1.Image);
            int x = 0;
            while (x < Lazy_width)
            {
                for (int i = 0; i <= Lazy_height - 1; i++)
                {
                    //Lazy_bitmap.SetPixel(x, i, Color.FromArgb(10,00,255,255));

                    Lazy_bitmap.SetPixel(x, i, Color.FromArgb(100, Night_bitmap.GetPixel(x, i)));
                    //Lazy_bitmap.SetPixel(x, i, Night_bitmap.GetPixel(x, i));
                    //上半部分自上而下遮盖，执行到图像一半
                }
                x++;
                this.pictureBox1.Refresh();//图像刷新
                this.pictureBox1.Image = Lazy_bitmap;
                //System.Threading.Thread.Sleep(30);//通过挂起线程时间来控制遮盖的速度,此处为睡眠30/1000毫秒
            }

        }

        void btnShuip_Click(object sender, EventArgs e)
        {
            int Lazy_width = this.pictureBox1.Width;//图像宽度 
            int Lazy_height = this.pictureBox1.Height;//图像高度
            Graphics Lazy_pic = this.pictureBox1.CreateGraphics();
            //获取Graphics对象
            Lazy_pic.Clear(Color.Red);//初始为红色
            Bitmap Lazy_bitmap = new Bitmap(Lazy_width, Lazy_height);
            int x = 0;
            while (x <= Lazy_width / 2)
            {
                for (int i = 0; i <= Lazy_height - 1; i++)
                {
                    Lazy_bitmap.SetPixel(x, i, Night_bitmap.GetPixel(x, i));
                    //上半部分自上而下遮盖，执行到图像一半
                }
                for (int i = 0; i <= Lazy_height - 1; i++)
                {
                    Lazy_bitmap.SetPixel(Lazy_width - x - 1, i, Night_bitmap.GetPixel(Lazy_width - x - 1, i));
                    //下半部分自下而上遮盖，执行到图像一半
                }
                x++;
                this.pictureBox1.Refresh();//图像刷新
                this.pictureBox1.Image = Lazy_bitmap;
                System.Threading.Thread.Sleep(30);//通过挂起线程时间来控制遮盖的速度,此处为睡眠30/1000毫秒
            }
        }

        void btnChuizhi_Click(object sender, EventArgs e)
        {
            int Lazy_width = this.pictureBox1.Width;//图像宽度 
            int Lazy_height = this.pictureBox1.Height;//图像高度
            Graphics Lazy_pic = this.pictureBox1.CreateGraphics();
            //获取Graphics对象
            Lazy_pic.Clear(Color.Red);//初始为红色
            Bitmap Lazy_bitmap = new Bitmap(Lazy_width, Lazy_height);
            int x = 0;
            while (x <= Lazy_height / 2)
            {
                for (int i = 0; i <= Lazy_width - 1; i++)
                {
                    Lazy_bitmap.SetPixel(i, x, Night_bitmap.GetPixel(i, x));
                    //上半部分自上而下遮盖，执行到图像一半
                }
                for (int i = 0; i <= Lazy_width - 1; i++)
                {
                    Lazy_bitmap.SetPixel(i, Lazy_height - x - 1, Night_bitmap.GetPixel(i, Lazy_height - x - 1));
                    //下半部分自下而上遮盖，执行到图像一半
                }
                x++;
                this.pictureBox1.Refresh();//图像刷新
                this.pictureBox1.Image = Lazy_bitmap;
                System.Threading.Thread.Sleep(30);//通过挂起线程时间来控制遮盖的速度,此处为睡眠30/1000毫秒

            }
        }
        private void lineButton_Click(object sender, EventArgs e)
        {
            // 画直线  
            Graphics gra = this.CreateGraphics();
            Pen pen = new Pen(Color.Red);
            pen.Width = 2;
            Point startPoint = new Point(20, 20);
            Point endPoint = new Point(70, 20);
            gra.DrawLine(pen, startPoint, endPoint);

            pen.Dispose();
            gra.Dispose();
        }

        private void rectangleButton_Click(object sender, EventArgs e)
        {
            //画矩形  
            Graphics gra = this.CreateGraphics();
            Pen pen = new Pen(Color.Red);
            gra.DrawRectangle(pen, 20, 50, 100, 100);
            pen.Dispose();
            gra.Dispose();
        }
        private void cyliderButton_Click(object sender, EventArgs e)
        {
            //圆柱体,有许多个椭圆有底部逐渐叠起来的，最后填充颜色  

            int height = this.ClientSize.Height - 150;
            int width = this.ClientSize.Width - 50;
            int vHeight = 200;
            int vWidth = 100;
            Graphics gra = this.CreateGraphics();
            gra.Clear(Color.White);
            Pen pen = new Pen(Color.Gray, 2);
            SolidBrush brush = new SolidBrush(Color.Gainsboro);

            for (int i = height / 2; i > 0; i--)
            {
                gra.DrawEllipse(pen, width / 2, i, vHeight, vWidth);
            }

            gra.FillEllipse(brush, width / 2, 0, vHeight, vWidth);
        }

        private void fillRectangleButton_Click(object sender, EventArgs e)
        {
            //画矩形  
            Graphics gra = this.CreateGraphics();
            Pen pen = new Pen(Color.Red, 3);
            Brush brush = pen.Brush;
            Rectangle rect = new Rectangle(20, 50, 100, 100);
            gra.FillRectangle(brush, rect);
            gra.Dispose();
        }

        private void drawEllispeButton_Click(object sender, EventArgs e)
        {
            Graphics gra = this.CreateGraphics();
            Rectangle rect = new Rectangle(0, 0, 200, 100);
            LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Orange, Color.Purple, 90);
            gra.FillEllipse(brush, rect);
            gra.Dispose();
        }

        private void fontButton_Click(object sender, EventArgs e)
        {
            Graphics gra = this.CreateGraphics();
            Font font = new Font("隶书", 24, FontStyle.Italic);
            Pen pen = new Pen(Color.Blue, 3);
            gra.DrawString("Windows应用程序设计", font, pen.Brush, 10, 100);
        }

        private void ellispeButton_Click(object sender, EventArgs e)
        {
            // 画圆形  
            Graphics gra = this.CreateGraphics();
            Pen pen = new Pen(Color.Red);
            gra.DrawEllipse(pen, 0, 0, 200, 100);
            pen.Dispose();
            gra.Dispose();
        }

        private void moveEllispeButton_Click(object sender, EventArgs e)
        {
            // 移动圆形  
            Graphics gra = this.CreateGraphics();
            Pen pen = new Pen(Color.Red);
            gra.TranslateTransform(10, 10);// 改变起坐标(10,10)  
            gra.DrawEllipse(pen, 0, 0, 200, 100);

            gra.Dispose();
        }

        private void scaleEllispeButton_Click(object sender, EventArgs e)
        {
            // 缩放圆形  
            float xScale = 1.5F;
            float yScale = 2F;
            Graphics gra = this.CreateGraphics();
            Pen pen = new Pen(Color.Red);
            gra.ScaleTransform(xScale, yScale);// X轴放大1.5倍， Y轴放大2倍  
            gra.DrawEllipse(pen, 0, 0, 200, 100);
            gra.Dispose();
        }

        private void curveButton_Click(object sender, EventArgs e)
        {
            //绘制曲线  
            Graphics gra = this.CreateGraphics();
            Pen pen = new Pen(Color.Blue, 3);
            Point oo1 = new Point(30, this.ClientSize.Height - 100);
            Point oo2 = new Point(this.ClientSize.Width - 50, this.ClientSize.Height - 100);
            gra.DrawLine(pen, oo1, oo2);
            Point oo3 = new Point(30, 30);
            gra.DrawLine(pen, oo1, oo3);
            Font font = new System.Drawing.Font("宋体", 12, FontStyle.Bold);
            gra.DrawString("X", font, pen.Brush, oo2);
            gra.DrawString("Y", font, pen.Brush, 10, 10);

            int x1 = 0, x2 = 0;
            double a = 0;
            double y1 = 0, y2 = this.ClientSize.Height - 100;
            for (x2 = 0; x2 < this.ClientSize.Width; x2++)
            {
                a = 2 * Math.PI * x2 / (this.ClientSize.Width);
                y2 = Math.Sin(a);
                y2 = (1 - y2) * (this.ClientSize.Height - 100) / 2;
                gra.DrawLine(pen, x1 + 30, (float)y1, x2 + 30, (float)y2);
                x1 = x2;
                y1 = y2;
            }
            gra.Dispose();
        }

        private void piechartButton_Click(object sender, EventArgs e)
        {
            //饼图  
            Graphics gra = this.CreateGraphics();
            Pen pen = new Pen(Color.Blue, 3);
            Rectangle rect = new Rectangle(50, 50, 200, 100);
            Brush brush = new SolidBrush(Color.Blue);
            gra.FillPie(pen.Brush, rect, 0, 60);
            gra.FillPie(brush, rect, 60, 150);
            brush = new SolidBrush(Color.Yellow);
            gra.FillPie(brush, rect, 210, 150);

        }

        private void btnAlpha_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(this.pictureBox1.Image);
            Graphics grapic = Graphics.FromImage(this.pictureBox1.Image);
          
            Log.Information("height:" + bitmap.Height + ",width:" + bitmap.Width);
            int iWidth = bitmap.Width;
            int iHeight = bitmap.Height;
            int lineWidth = iWidth / 256;
            int lastWidth = bitmap.Width - lineWidth * 256;

            Random rd = new Random();
            for (int i = 0; i <= 100; i++)
            {
                //grapic.DrawLine(new Pen(Color.FromArgb(rd.Next(256), Color.FromArgb(rd.Next(256), rd.Next(256), rd.Next(256))),
                    //lineWidth * 2), new Point(i* lineWidth, 0), new Point(i * lineWidth, bitmap.Height));
                if(i == 255 && lastWidth != 0)
                {
                    lastWidth = lineWidth + lastWidth + 1;
                    grapic.DrawLine(new Pen(Color.FromArgb(255, Color.Red ),
                    lastWidth), new Point(lineWidth * 255 +lastWidth / 2, 0), new Point(lineWidth * 255 + lastWidth / 2, bitmap.Height));
                }
                else
                {
                    grapic.DrawLine(new Pen(Color.FromArgb(255, i%2==0?Color.Black:Color.White),
                    lineWidth), new Point(i * lineWidth + lineWidth/2, 0), new Point(i * lineWidth + lineWidth / 2, bitmap.Height));
                }
                Log.Information("cur:" + i * lineWidth + ",lineWidth * 2:" + lineWidth * 2 );
                this.pictureBox1.Refresh();
                Thread.Sleep(3);
            }


        }
        private void btnAlpha_Click_bak(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            Bitmap bitmap = new Bitmap(this.pictureBox1.Image);
            Graphics grapic = Graphics.FromImage(this.pictureBox1.Image);
            /*float[][] matrixItems ={
               new float[] {1, 0, 0, 0, 0},
               new float[] {0, 1, 0, 0, 0},
               new float[] {0, 0, 1, 0, 0},
               new float[] {0, 0, 0, 0.8f, 0},
               new float[] {0, 0, 0, 0, 1}};
            ColorMatrix colorMatrix = new ColorMatrix(matrixItems);
            ImageAttributes imageAtt = new ImageAttributes();
            imageAtt.SetColorMatrix(
               colorMatrix,
               ColorMatrixFlag.Default,
               ColorAdjustType.Bitmap);*/

            // First draw a wide black line.
            /*
            grapic.DrawLine(
               new Pen(Color.FromArgb(250, Color.Red), bitmap.Width),
               new Point(0, 0),
               new Point(bitmap.Width, bitmap.Height));
            */
            Log.Information("height:" + bitmap.Height + ",width:" + bitmap.Width);
            int iWidth = bitmap.Width;
            int iHeight = bitmap.Height;
            int lineWidth = iWidth / 256;
            int lastWidth = bitmap.Width - lineWidth * 256;

            Random rd = new Random();
            for (int i = 0; i <= 255; i++)
            {
                //grapic.DrawLine(new Pen(Color.FromArgb(rd.Next(256), Color.FromArgb(rd.Next(256), rd.Next(256), rd.Next(256))),
                //lineWidth * 2), new Point(i* lineWidth, 0), new Point(i * lineWidth, bitmap.Height));
                if (i == 255 && lastWidth != 0)
                {
                    lastWidth = lineWidth + lastWidth + 1;
                    grapic.DrawLine(new Pen(Color.FromArgb(255, Color.Red),
                    lastWidth), new Point(lineWidth * 255 + lastWidth / 2, 0), new Point(lineWidth * 255 + lastWidth / 2, bitmap.Height));
                }
                else
                {
                    grapic.DrawLine(new Pen(Color.FromArgb(255, i % 2 == 0 ? Color.Black : Color.White),
                    lineWidth), new Point(i * lineWidth + lineWidth / 2, 0), new Point(i * lineWidth + lineWidth / 2, bitmap.Height));
                }
                Log.Information("cur:" + i * lineWidth + ",lineWidth * 2:" + lineWidth * 2);
                this.pictureBox1.Refresh();
                Thread.Sleep(3);
            }
            //grapic.DrawLine(new Pen(Color.FromArgb(250, Color.Red), bitmap.Height),new Point(0, 0),new Point(bitmap.Width, 0));
            //grapic.DrawLine(new Pen(Color.FromArgb(250, Color.Green), bitmap.Width),new Point(0, 0),new Point(0, bitmap.Height));
            //grapic.DrawLine(new Pen(Color.FromArgb(100, Color.Yellow), bitmap.Width),new Point(bitmap.Width/2, 0),new Point(bitmap.Width / 2, bitmap.Height));
            //this.pictureBox1.Refresh();
            // Now draw the semitransparent bitmap image.

            /*grapic.DrawImage(
               bitmap,
               new Rectangle(30, 0, iWidth, iHeight),  // destination rectangle
               0.0f,                          // source rectangle x
               0.0f,                          // source rectangle y
               iWidth,                        // source rectangle width
               iHeight,                       // source rectangle height
               GraphicsUnit.Pixel,
               imageAtt);*/


            //pictureBox1.Image = bitmap;


        }
    }
}
