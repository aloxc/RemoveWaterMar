using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RemoveWaterMar
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
            timer1.Enabled = true;
        }

        private void time1_Tick(object sender, EventArgs e)
        {
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));

            int R = ran.Next(255);
            int G = ran.Next(255);
            int B = ran.Next(255);
            B = (R + G > 400) ? R + G - 400 : B;//0 : 380 - R - G;
            B = (B > 255) ? 255 : B;
            Color color = Color.FromArgb(R, G, B);
            Color backColor = Color.FromArgb(255 - R, 255 - G, 255 - B);
            this.lblCPU_GPU.ForeColor = color;
            this.lblCPU_GPU.BackColor = backColor;
            this.palBack.BackColor = backColor;

        }
    }
}
