using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoveWaterMar
{
    [ToolboxItem(false), Browsable(false), Description("用于屏蔽用户界面的控件;")]

    public class MaskLayer : Control
    {
        private int alpha;
        private Boolean bShow = true;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        public MaskLayer()
        {
            timer.Interval = 100;
            timer.Tick += Timer1_Tick;
            timer.Start();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            alpha = 75;
            //SetStyle(System.Windows.Forms.ControlStyles.Opaque, true);

        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (bShow)
                ShowMask(bShow);
            else
            {
                ShowMask(bShow);
                BackgroundImage = null;
            }

        }
        private delegate void ShowMaskCallback(Boolean bShow);
        private void ShowMask(Boolean bShow)
        {
            // InvokeRequired需要比较调用线程ID和创建线程ID
            // 如果它们不相同则返回true
            if (this.InvokeRequired)
            {
                ShowMaskCallback d = new ShowMaskCallback(ShowMask);
                this.Invoke(d, new object[] { bShow });
            }
            else
            {
                if (bShow)
                    this.Show();
                else
                    this.Hide();
            }
        }
        //显示线程

        public void DelayShowMaskByScreenCopy(Control parentControl)
        {
            this.BackColor = Color.Black;
            this.Left = 0;
            this.Top = 0;
            this.Width = parentControl.Width;
            this.Height = parentControl.Height;
            this.Parent = parentControl;
            if (this.BackgroundImage == null)
            {
                Rectangle rect = parentControl.ClientRectangle;
                Rectangle sRect = parentControl.RectangleToScreen(rect);
                Bitmap bit = new Bitmap(rect.Width, rect.Height);//实例化一个和窗体一样大的bitmap
                Graphics g = Graphics.FromImage(bit);
                g.CompositingQuality = CompositingQuality.HighQuality;//质量设为最高
                g.CopyFromScreen(sRect.Left, sRect.Top, 0, 0, new Size(this.Width, this.Height));
                this.BackgroundImage = bit;
            }
            bShow = true;

        }
        public void DelayShowMaskByColorFill(Color color, Control parentControl)
        {
            this.BackColor = Color.Black;
            this.Left = 0;
            this.Top = 0;
            this.Width = parentControl.Width;
            this.Height = parentControl.Height;
            this.Parent = parentControl;
            SetStyle(ControlStyles.AllPaintingInWmPaint, false); // 擦除背景.有轻微闪烁现象
            if (this.BackgroundImage == null)
            {
                Rectangle rect = parentControl.ClientRectangle;
                Rectangle sRect = parentControl.RectangleToScreen(rect);
                Bitmap bit = new Bitmap(rect.Width, rect.Height);//实例化一个和窗体一样大的bitmap
                Graphics g = Graphics.FromImage(bit);
                g.CompositingQuality = CompositingQuality.HighQuality;//质量设为最高
                g.FillRectangle(new SolidBrush(Color.FromArgb(alpha, color)), this.ClientRectangle);
                this.BackgroundImage = bit;
            }
            bShow = true;

        }
        public void DelayHide()
        {
            bShow = false;

        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            return;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(BackgroundImage, 0, 0, this.Width, this.Height);
            Color color = Color.FromArgb(alpha, this.BackColor);
            using (SolidBrush brush = new SolidBrush(color))
            {
                g.FillRectangle(brush, 0, 0, this.Size.Width, this.Size.Height);
            }
            if (!this.DesignMode)
            {
                using (Pen pen = new Pen(color))
                {
                    g.DrawRectangle(pen, 0, 0, this.Width, this.Height);
                }
            }
            else
                g.DrawRectangle(Pens.Black, 1, 1, this.Width - 2, this.Height - 2);


            e.Graphics.DrawImage(bmp, 0, 0);
            g.Dispose();
            bmp.Dispose();

        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_NOCLOSE = 0x200;
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020;

                cp.ClassStyle = cp.ClassStyle | CS_NOCLOSE;
                return cp;

            }
        }

        public int Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                if (value < 0) alpha = 0;
                else if (value > 255) alpha = 255;
                else alpha = value;
                this.Invalidate();
            }
        }

    }
}
