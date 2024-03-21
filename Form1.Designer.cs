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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaterMark));
            btnOpen = new Button();
            picBox = new PictureBox();
            btnDoit = new Button();
            rbtnCpu = new RadioButton();
            rbtnGpu = new RadioButton();
            gBoxMethod = new GroupBox();
            btnStop = new Button();
            btnPrevPic = new Button();
            picBoxPrev = new PictureBox();
            btnOpenOld = new Button();
            btnOpenNew = new Button();
            btnHelp = new Button();
            btnPrevVideo = new Button();
            removeWaterTimer = new System.Windows.Forms.Timer(components);
            btnTest = new Button();
            notifyIcon1 = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            btnResetThumbnail = new Button();
            btnSetScale = new Button();
            lblWidth = new Label();
            lblHeight = new Label();
            tbxWidth = new TextBox();
            tbxHeight = new TextBox();
            grpBoxPercent = new GroupBox();
            rbt11 = new RadioButton();
            rbt14 = new RadioButton();
            rbt13 = new RadioButton();
            rbt34 = new RadioButton();
            rbt12 = new RadioButton();
            rbt23 = new RadioButton();
            btnMergeM3u8 = new Button();
            ((System.ComponentModel.ISupportInitialize)picBox).BeginInit();
            gBoxMethod.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picBoxPrev).BeginInit();
            grpBoxPercent.SuspendLayout();
            SuspendLayout();
            // 
            // btnOpen
            // 
            btnOpen.Location = new Point(8, 8);
            btnOpen.Margin = new Padding(2);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(71, 24);
            btnOpen.TabIndex = 0;
            btnOpen.Text = "打开视频";
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += btnOpen_Click;
            // 
            // picBox
            // 
            picBox.BorderStyle = BorderStyle.Fixed3D;
            picBox.Location = new Point(8, 134);
            picBox.Margin = new Padding(2);
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
            btnDoit.BackColor = Color.MistyRose;
            btnDoit.Location = new Point(8, 50);
            btnDoit.Margin = new Padding(2);
            btnDoit.Name = "btnDoit";
            btnDoit.Size = new Size(92, 24);
            btnDoit.TabIndex = 5;
            btnDoit.Text = "处理视频";
            btnDoit.UseVisualStyleBackColor = false;
            btnDoit.Click += btnDoit_Click;
            // 
            // rbtnCpu
            // 
            rbtnCpu.AutoSize = true;
            rbtnCpu.Location = new Point(15, 0);
            rbtnCpu.Margin = new Padding(2);
            rbtnCpu.Name = "rbtnCpu";
            rbtnCpu.Size = new Size(98, 21);
            rbtnCpu.TabIndex = 6;
            rbtnCpu.Text = "使用CPU处理";
            rbtnCpu.UseVisualStyleBackColor = true;
            rbtnCpu.Click += method_Click;
            // 
            // rbtnGpu
            // 
            rbtnGpu.AutoSize = true;
            rbtnGpu.Checked = true;
            rbtnGpu.Location = new Point(145, 0);
            rbtnGpu.Margin = new Padding(2);
            rbtnGpu.Name = "rbtnGpu";
            rbtnGpu.Size = new Size(99, 21);
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
            gBoxMethod.Location = new Point(100, 8);
            gBoxMethod.Margin = new Padding(2);
            gBoxMethod.Name = "gBoxMethod";
            gBoxMethod.Padding = new Padding(2);
            gBoxMethod.Size = new Size(266, 27);
            gBoxMethod.TabIndex = 9;
            gBoxMethod.TabStop = false;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(129, 50);
            btnStop.Margin = new Padding(2);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(92, 24);
            btnStop.TabIndex = 10;
            btnStop.Text = "停止处理";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // btnPrevPic
            // 
            btnPrevPic.Location = new Point(250, 50);
            btnPrevPic.Margin = new Padding(2);
            btnPrevPic.Name = "btnPrevPic";
            btnPrevPic.Size = new Size(92, 24);
            btnPrevPic.TabIndex = 12;
            btnPrevPic.Text = "预览图片";
            btnPrevPic.UseVisualStyleBackColor = true;
            btnPrevPic.Click += btnPrevPic_Click;
            btnPrevPic.Leave += btnPrev_Leave;
            btnPrevPic.MouseLeave += btnPrev_Leave;
            // 
            // picBoxPrev
            // 
            picBoxPrev.Location = new Point(388, 13);
            picBoxPrev.Margin = new Padding(2);
            picBoxPrev.Name = "picBoxPrev";
            picBoxPrev.Size = new Size(14, 14);
            picBoxPrev.TabIndex = 13;
            picBoxPrev.TabStop = false;
            // 
            // btnOpenOld
            // 
            btnOpenOld.Location = new Point(8, 88);
            btnOpenOld.Margin = new Padding(2);
            btnOpenOld.Name = "btnOpenOld";
            btnOpenOld.RightToLeft = RightToLeft.No;
            btnOpenOld.Size = new Size(92, 24);
            btnOpenOld.TabIndex = 14;
            btnOpenOld.Text = "打开旧视频";
            btnOpenOld.UseVisualStyleBackColor = true;
            btnOpenOld.Click += btnOpenOldVideo_Click;
            // 
            // btnOpenNew
            // 
            btnOpenNew.Location = new Point(129, 88);
            btnOpenNew.Margin = new Padding(2);
            btnOpenNew.Name = "btnOpenNew";
            btnOpenNew.Size = new Size(92, 24);
            btnOpenNew.TabIndex = 15;
            btnOpenNew.Text = "打开新视频";
            btnOpenNew.UseVisualStyleBackColor = true;
            btnOpenNew.Click += btnOpenNewVideo_Click;
            // 
            // btnHelp
            // 
            btnHelp.Location = new Point(492, 50);
            btnHelp.Margin = new Padding(2);
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new Size(92, 24);
            btnHelp.TabIndex = 16;
            btnHelp.Text = "帮助";
            btnHelp.UseVisualStyleBackColor = true;
            btnHelp.Click += btnHelp_Click;
            // 
            // btnPrevVideo
            // 
            btnPrevVideo.Location = new Point(370, 50);
            btnPrevVideo.Margin = new Padding(2);
            btnPrevVideo.Name = "btnPrevVideo";
            btnPrevVideo.Size = new Size(92, 24);
            btnPrevVideo.TabIndex = 17;
            btnPrevVideo.Text = "预览视频";
            btnPrevVideo.UseVisualStyleBackColor = true;
            btnPrevVideo.Click += btnPrevVideo_Click;
            // 
            // removeWaterTimer
            // 
            removeWaterTimer.Interval = 300;
            removeWaterTimer.Tick += removeWaterTimerCall;
            // 
            // btnTest
            // 
            btnTest.Location = new Point(370, 88);
            btnTest.Margin = new Padding(2);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(92, 24);
            btnTest.TabIndex = 18;
            btnTest.Text = "测试";
            btnTest.UseVisualStyleBackColor = true;
            btnTest.Click += test_Click;
            // 
            // notifyIcon1
            // 
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "淡化视频水印";
            notifyIcon1.Visible = true;
            notifyIcon1.BalloonTipClicked += notifyIcon1_BalloonTipClosed;
            notifyIcon1.BalloonTipClosed += notifyIcon1_BalloonTipClosed;
            notifyIcon1.Click += notifyIcon1_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(24, 24);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            contextMenuStrip1.Text = "处理成功";
            // 
            // btnResetThumbnail
            // 
            btnResetThumbnail.Location = new Point(250, 88);
            btnResetThumbnail.Margin = new Padding(2);
            btnResetThumbnail.Name = "btnResetThumbnail";
            btnResetThumbnail.Size = new Size(92, 24);
            btnResetThumbnail.TabIndex = 19;
            btnResetThumbnail.Text = "重获缩略图";
            btnResetThumbnail.UseVisualStyleBackColor = true;
            btnResetThumbnail.Click += btnResetThumbnail_Click;
            // 
            // btnSetScale
            // 
            btnSetScale.Location = new Point(492, 88);
            btnSetScale.Margin = new Padding(2);
            btnSetScale.Name = "btnSetScale";
            btnSetScale.Size = new Size(92, 24);
            btnSetScale.TabIndex = 20;
            btnSetScale.Text = "调整分辨率";
            btnSetScale.UseVisualStyleBackColor = true;
            btnSetScale.Click += btnSetScale_Click;
            // 
            // lblWidth
            // 
            lblWidth.AutoSize = true;
            lblWidth.Location = new Point(602, 53);
            lblWidth.Margin = new Padding(2, 0, 2, 0);
            lblWidth.Name = "lblWidth";
            lblWidth.Size = new Size(32, 17);
            lblWidth.TabIndex = 21;
            lblWidth.Text = "宽：";
            // 
            // lblHeight
            // 
            lblHeight.AutoSize = true;
            lblHeight.Location = new Point(602, 91);
            lblHeight.Margin = new Padding(2, 0, 2, 0);
            lblHeight.Name = "lblHeight";
            lblHeight.Size = new Size(32, 17);
            lblHeight.TabIndex = 22;
            lblHeight.Text = "高：";
            // 
            // tbxWidth
            // 
            tbxWidth.Location = new Point(628, 52);
            tbxWidth.Margin = new Padding(2);
            tbxWidth.Name = "tbxWidth";
            tbxWidth.Size = new Size(61, 23);
            tbxWidth.TabIndex = 23;
            // 
            // tbxHeight
            // 
            tbxHeight.Location = new Point(628, 89);
            tbxHeight.Margin = new Padding(2);
            tbxHeight.Name = "tbxHeight";
            tbxHeight.Size = new Size(61, 23);
            tbxHeight.TabIndex = 24;
            // 
            // grpBoxPercent
            // 
            grpBoxPercent.Controls.Add(rbt11);
            grpBoxPercent.Controls.Add(rbt14);
            grpBoxPercent.Controls.Add(rbt13);
            grpBoxPercent.Controls.Add(rbt34);
            grpBoxPercent.Controls.Add(rbt12);
            grpBoxPercent.Controls.Add(rbt23);
            grpBoxPercent.Location = new Point(697, 36);
            grpBoxPercent.Margin = new Padding(2);
            grpBoxPercent.Name = "grpBoxPercent";
            grpBoxPercent.Padding = new Padding(2);
            grpBoxPercent.Size = new Size(106, 85);
            grpBoxPercent.TabIndex = 25;
            grpBoxPercent.TabStop = false;
            // 
            // rbt11
            // 
            rbt11.AutoSize = true;
            rbt11.ForeColor = Color.Red;
            rbt11.Location = new Point(4, 12);
            rbt11.Margin = new Padding(2);
            rbt11.Name = "rbt11";
            rbt11.Size = new Size(50, 21);
            rbt11.TabIndex = 26;
            rbt11.TabStop = true;
            rbt11.Text = "压缩";
            rbt11.UseVisualStyleBackColor = true;
            rbt11.Click += rbt11_Click;
            // 
            // rbt14
            // 
            rbt14.AutoSize = true;
            rbt14.Location = new Point(55, 11);
            rbt14.Margin = new Padding(2);
            rbt14.Name = "rbt14";
            rbt14.Size = new Size(45, 21);
            rbt14.TabIndex = 30;
            rbt14.TabStop = true;
            rbt14.Text = "1/4";
            rbt14.UseVisualStyleBackColor = true;
            rbt14.Click += rbt14_Click;
            // 
            // rbt13
            // 
            rbt13.AutoSize = true;
            rbt13.Location = new Point(4, 60);
            rbt13.Margin = new Padding(2);
            rbt13.Name = "rbt13";
            rbt13.Size = new Size(45, 21);
            rbt13.TabIndex = 29;
            rbt13.TabStop = true;
            rbt13.Text = "1/3";
            rbt13.UseVisualStyleBackColor = true;
            rbt13.Click += rbt13_Click;
            // 
            // rbt34
            // 
            rbt34.AutoSize = true;
            rbt34.Location = new Point(55, 60);
            rbt34.Margin = new Padding(2);
            rbt34.Name = "rbt34";
            rbt34.Size = new Size(45, 21);
            rbt34.TabIndex = 28;
            rbt34.TabStop = true;
            rbt34.Text = "3/4";
            rbt34.UseVisualStyleBackColor = true;
            rbt34.Click += rbt34_Click;
            // 
            // rbt12
            // 
            rbt12.AutoSize = true;
            rbt12.Location = new Point(4, 36);
            rbt12.Margin = new Padding(2);
            rbt12.Name = "rbt12";
            rbt12.Size = new Size(45, 21);
            rbt12.TabIndex = 26;
            rbt12.TabStop = true;
            rbt12.Text = "1/2";
            rbt12.UseVisualStyleBackColor = true;
            rbt12.Click += rbt12_Click;
            // 
            // rbt23
            // 
            rbt23.AutoSize = true;
            rbt23.Location = new Point(55, 36);
            rbt23.Margin = new Padding(2);
            rbt23.Name = "rbt23";
            rbt23.Size = new Size(45, 21);
            rbt23.TabIndex = 27;
            rbt23.TabStop = true;
            rbt23.Text = "2/3";
            rbt23.UseVisualStyleBackColor = true;
            rbt23.Click += rbt23_Click;
            // 
            // btnMergeM3u8
            // 
            btnMergeM3u8.Location = new Point(492, 14);
            btnMergeM3u8.Name = "btnMergeM3u8";
            btnMergeM3u8.Size = new Size(92, 23);
            btnMergeM3u8.TabIndex = 26;
            btnMergeM3u8.Text = "合并m3u8";
            btnMergeM3u8.UseVisualStyleBackColor = true;
            btnMergeM3u8.Click += btnMergeM3u8_Click;
            // 
            // WaterMark
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1253, 928);
            Controls.Add(btnMergeM3u8);
            Controls.Add(grpBoxPercent);
            Controls.Add(tbxHeight);
            Controls.Add(tbxWidth);
            Controls.Add(lblHeight);
            Controls.Add(lblWidth);
            Controls.Add(btnSetScale);
            Controls.Add(btnResetThumbnail);
            Controls.Add(btnTest);
            Controls.Add(btnPrevVideo);
            Controls.Add(btnHelp);
            Controls.Add(btnOpenNew);
            Controls.Add(btnOpenOld);
            Controls.Add(picBoxPrev);
            Controls.Add(btnPrevPic);
            Controls.Add(btnStop);
            Controls.Add(gBoxMethod);
            Controls.Add(btnDoit);
            Controls.Add(picBox);
            Controls.Add(btnOpen);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            Name = "WaterMark";
            Text = "淡化视频水印";
            Load += WaterMark_Load;
            DragDrop += WaterMark_DragDrop;
            DragEnter += WaterMark_DragEnter;
            Paint += WaterMark_Paint;
            ((System.ComponentModel.ISupportInitialize)picBox).EndInit();
            gBoxMethod.ResumeLayout(false);
            gBoxMethod.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picBoxPrev).EndInit();
            grpBoxPercent.ResumeLayout(false);
            grpBoxPercent.PerformLayout();
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
        private Button btnPrevPic;
        private PictureBox picBoxPrev;
        private Button btnOpenOld;
        private Button btnOpenNew;
        private Button btnHelp;
        private Button btnPrevVideo;
        private System.Windows.Forms.Timer removeWaterTimer;
        private Button btnTest;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private Button btnResetThumbnail;
        private Button btnSetScale;
        private Label lblWidth;
        private Label lblHeight;
        private TextBox tbxWidth;
        private TextBox tbxHeight;
        private GroupBox grpBoxPercent;
        private RadioButton rbt12;
        private RadioButton rbt34;
        private RadioButton rbt23;
        private RadioButton rbt14;
        private RadioButton rbt13;
        private RadioButton rbt11;
        private Button btnMergeM3u8;
    }
}