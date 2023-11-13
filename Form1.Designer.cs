namespace RemoveWaterMar
{
    partial class WaterMark
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaterMark));
            btnOpen = new Button();
            picBox = new PictureBox();
            btnDoit = new Button();
            rbtnCpu = new RadioButton();
            rbtnGpu = new RadioButton();
            gBoxMethod = new GroupBox();
            btnStop = new Button();
            btnPrev = new Button();
            picBoxPrev = new PictureBox();
            btnOpenOld = new Button();
            btnOpenNew = new Button();
            ((System.ComponentModel.ISupportInitialize)picBox).BeginInit();
            gBoxMethod.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picBoxPrev).BeginInit();
            SuspendLayout();
            // 
            // btnOpen
            // 
            btnOpen.Location = new Point(13, 11);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(112, 34);
            btnOpen.TabIndex = 0;
            btnOpen.Text = "打开视频";
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += btnOpen_Click;
            // 
            // picBox
            // 
            picBox.BorderStyle = BorderStyle.Fixed3D;
            picBox.Location = new Point(13, 65);
            picBox.Name = "picBox";
            picBox.Size = new Size(1920, 1080);
            picBox.SizeMode = PictureBoxSizeMode.AutoSize;
            picBox.TabIndex = 4;
            picBox.TabStop = false;
            picBox.Paint += picBox_Paint;
            picBox.MouseDown += picBox_MouseDown;
            picBox.MouseMove += picBox_MouseMove;
            picBox.MouseUp += picBox_MouseUp;
            // 
            // btnDoit
            // 
            btnDoit.Location = new Point(608, 11);
            btnDoit.Name = "btnDoit";
            btnDoit.Size = new Size(112, 34);
            btnDoit.TabIndex = 5;
            btnDoit.Text = "处理视频";
            btnDoit.UseVisualStyleBackColor = true;
            btnDoit.MouseClick += btnDoit_MouseClick;
            // 
            // rbtnCpu
            // 
            rbtnCpu.AutoSize = true;
            rbtnCpu.Location = new Point(23, 0);
            rbtnCpu.Name = "rbtnCpu";
            rbtnCpu.Size = new Size(143, 28);
            rbtnCpu.TabIndex = 6;
            rbtnCpu.Text = "使用CPU处理";
            rbtnCpu.UseVisualStyleBackColor = true;
            rbtnCpu.Click += method_Click;
            // 
            // rbtnGpu
            // 
            rbtnGpu.AutoSize = true;
            rbtnGpu.Checked = true;
            rbtnGpu.Location = new Point(228, 0);
            rbtnGpu.Name = "rbtnGpu";
            rbtnGpu.Size = new Size(144, 28);
            rbtnGpu.TabIndex = 7;
            rbtnGpu.TabStop = true;
            rbtnGpu.Text = "使用GPU处理";
            rbtnGpu.UseVisualStyleBackColor = true;
            rbtnGpu.Click += method_Click;
            // 
            // gBoxMethod
            // 
            gBoxMethod.Controls.Add(rbtnCpu);
            gBoxMethod.Controls.Add(rbtnGpu);
            gBoxMethod.Location = new Point(157, 11);
            gBoxMethod.Name = "gBoxMethod";
            gBoxMethod.Size = new Size(418, 38);
            gBoxMethod.TabIndex = 9;
            gBoxMethod.TabStop = false;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(753, 11);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(112, 34);
            btnStop.TabIndex = 10;
            btnStop.Text = "停止处理";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // btnPrev
            // 
            btnPrev.Location = new Point(897, 11);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(112, 34);
            btnPrev.TabIndex = 12;
            btnPrev.Text = "预览";
            btnPrev.UseVisualStyleBackColor = true;
            btnPrev.Click += btnPrev_Click;
            btnPrev.Leave += btnPrev_Leave;
            btnPrev.MouseLeave += btnPrev_Leave;
            // 
            // picBoxPrev
            // 
            picBoxPrev.Location = new Point(1048, 21);
            picBoxPrev.Name = "picBoxPrev";
            picBoxPrev.Size = new Size(22, 20);
            picBoxPrev.TabIndex = 13;
            picBoxPrev.TabStop = false;
            // 
            // btnOpenOld
            // 
            btnOpenOld.Location = new Point(1042, 11);
            btnOpenOld.Name = "btnOpenOld";
            btnOpenOld.RightToLeft = RightToLeft.No;
            btnOpenOld.Size = new Size(145, 34);
            btnOpenOld.TabIndex = 14;
            btnOpenOld.Text = "打开旧视频";
            btnOpenOld.UseVisualStyleBackColor = true;
            btnOpenOld.Click += btnOpenOld_Click;
            // 
            // btnOpenNew
            // 
            btnOpenNew.Location = new Point(1219, 11);
            btnOpenNew.Name = "btnOpenNew";
            btnOpenNew.Size = new Size(145, 34);
            btnOpenNew.TabIndex = 15;
            btnOpenNew.Text = "打开新视频";
            btnOpenNew.UseVisualStyleBackColor = true;
            btnOpenNew.Click += btnOpenNew_Click;
            // 
            // WaterMark
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(3063, 1184);
            Controls.Add(btnOpenNew);
            Controls.Add(btnOpenOld);
            Controls.Add(picBoxPrev);
            Controls.Add(btnPrev);
            Controls.Add(btnStop);
            Controls.Add(gBoxMethod);
            Controls.Add(btnDoit);
            Controls.Add(picBox);
            Controls.Add(btnOpen);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "WaterMark";
            Text = "淡化视频水印";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)picBox).EndInit();
            gBoxMethod.ResumeLayout(false);
            gBoxMethod.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picBoxPrev).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnOpen;
        private PictureBox picBox;
        private Button btnDoit;
        private RadioButton rbtnCpu;
        private RadioButton rbtnGpu;
        private GroupBox gBoxMethod;
        private Button btnStop;
        private Button btnPrev;
        private PictureBox picBoxPrev;
        private Button btnOpenOld;
        private Button btnOpenNew;
    }
}