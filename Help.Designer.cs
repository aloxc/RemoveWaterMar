namespace RemoveWaterMar
{
    partial class Help
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            lblCPU_GPU = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            palBack = new Panel();
            palBack.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(54, 46);
            label1.Name = "label1";
            label1.Size = new Size(568, 24);
            label1.TabIndex = 0;
            label1.Text = "主界面先点击打开视频，然后在显示的图片上使用鼠标左键框选出内容";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(54, 117);
            label2.Name = "label2";
            label2.Size = new Size(473, 24);
            label2.TabIndex = 1;
            label2.Text = "然后可以使用gpu或者cpu处理视频，也可以点击预览效果";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(54, 188);
            label3.Name = "label3";
            label3.Size = new Size(532, 24);
            label3.TabIndex = 2;
            label3.Text = "在未处理视频前可以看原始视频，处理完成后可以看处理后的视频";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(54, 259);
            label4.Name = "label4";
            label4.Size = new Size(388, 24);
            label4.TabIndex = 3;
            label4.Text = "观看处理前后的时候的时候，键盘按键作用如下";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(54, 322);
            label5.Name = "label5";
            label5.Size = new Size(201, 24);
            label5.TabIndex = 4;
            label5.Text = "键盘方向上键：音量减5";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(54, 391);
            label6.Name = "label6";
            label6.Size = new Size(201, 24);
            label6.TabIndex = 5;
            label6.Text = "键盘方向下键：音量加5";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(54, 460);
            label7.Name = "label7";
            label7.Size = new Size(201, 24);
            label7.TabIndex = 6;
            label7.Text = "键盘方向左键：快退5秒";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(54, 529);
            label8.Name = "label8";
            label8.Size = new Size(201, 24);
            label8.TabIndex = 7;
            label8.Text = "键盘方向右键：快进5秒";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(54, 598);
            label9.Name = "label9";
            label9.Size = new Size(212, 24);
            label9.TabIndex = 8;
            label9.Text = "键盘上翻页键：快退10秒";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(54, 667);
            label10.Name = "label10";
            label10.Size = new Size(212, 24);
            label10.TabIndex = 9;
            label10.Text = "键盘下翻页键：快进10秒";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(54, 736);
            label11.Name = "label11";
            label11.Size = new Size(207, 24);
            label11.TabIndex = 10;
            label11.Text = "键盘home键：快退15秒";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(54, 805);
            label12.Name = "label12";
            label12.Size = new Size(191, 24);
            label12.TabIndex = 11;
            label12.Text = "键盘end键：快进15秒";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(54, 874);
            label13.Name = "label13";
            label13.Size = new Size(208, 24);
            label13.TabIndex = 12;
            label13.Text = "键盘空格键：暂停或播放";
            // 
            // lblCPU_GPU
            // 
            lblCPU_GPU.AutoSize = true;
            lblCPU_GPU.BackColor = SystemColors.ActiveCaption;
            lblCPU_GPU.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            lblCPU_GPU.Location = new Point(52, 28);
            lblCPU_GPU.Name = "lblCPU_GPU";
            lblCPU_GPU.Size = new Size(457, 92);
            lblCPU_GPU.TabIndex = 13;
            lblCPU_GPU.Text = "GPU处理速度比CPU快的多\n尽量使用GPU处理视频";
            // 
            // timer1
            // 
            timer1.Tick += time1_Tick;
            // 
            // palBack
            // 
            palBack.BackColor = SystemColors.ActiveCaption;
            palBack.Controls.Add(lblCPU_GPU);
            palBack.Location = new Point(2, 901);
            palBack.Margin = new Padding(0);
            palBack.Name = "palBack";
            palBack.Size = new Size(665, 150);
            palBack.TabIndex = 1;
            // 
            // Help
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(667, 1053);
            Controls.Add(palBack);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Help";
            ShowInTaskbar = false;
            Text = "帮助";
            palBack.ResumeLayout(false);
            palBack.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label lblCPU_GPU;
        private System.Windows.Forms.Timer timer1;
        private Panel palBack;
    }
}