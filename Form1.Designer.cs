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
            ((System.ComponentModel.ISupportInitialize)picBox).BeginInit();
            gBoxMethod.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picBoxPrev).BeginInit();
            grpBoxPercent.SuspendLayout();
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
            picBox.Location = new Point(12, 189);
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
            btnDoit.Location = new Point(13, 70);
            btnDoit.Name = "btnDoit";
            btnDoit.Size = new Size(145, 34);
            btnDoit.TabIndex = 5;
            btnDoit.Text = "处理视频";
            btnDoit.UseVisualStyleBackColor = false;
            btnDoit.Click += btnDoit_Click;
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
            btnStop.Location = new Point(203, 70);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(145, 34);
            btnStop.TabIndex = 10;
            btnStop.Text = "停止处理";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // btnPrevPic
            // 
            btnPrevPic.Location = new Point(393, 70);
            btnPrevPic.Name = "btnPrevPic";
            btnPrevPic.Size = new Size(145, 34);
            btnPrevPic.TabIndex = 12;
            btnPrevPic.Text = "预览图片";
            btnPrevPic.UseVisualStyleBackColor = true;
            btnPrevPic.Click += btnPrevPic_Click;
            btnPrevPic.Leave += btnPrev_Leave;
            btnPrevPic.MouseLeave += btnPrev_Leave;
            // 
            // picBoxPrev
            // 
            picBoxPrev.Location = new Point(609, 19);
            picBoxPrev.Name = "picBoxPrev";
            picBoxPrev.Size = new Size(22, 20);
            picBoxPrev.TabIndex = 13;
            picBoxPrev.TabStop = false;
            // 
            // btnOpenOld
            // 
            btnOpenOld.Location = new Point(13, 124);
            btnOpenOld.Name = "btnOpenOld";
            btnOpenOld.RightToLeft = RightToLeft.No;
            btnOpenOld.Size = new Size(145, 34);
            btnOpenOld.TabIndex = 14;
            btnOpenOld.Text = "打开旧视频";
            btnOpenOld.UseVisualStyleBackColor = true;
            btnOpenOld.Click += btnOpenOldVideo_Click;
            // 
            // btnOpenNew
            // 
            btnOpenNew.Location = new Point(203, 124);
            btnOpenNew.Name = "btnOpenNew";
            btnOpenNew.Size = new Size(145, 34);
            btnOpenNew.TabIndex = 15;
            btnOpenNew.Text = "打开新视频";
            btnOpenNew.UseVisualStyleBackColor = true;
            btnOpenNew.Click += btnOpenNewVideo_Click;
            // 
            // btnHelp
            // 
            btnHelp.Location = new Point(773, 70);
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new Size(145, 34);
            btnHelp.TabIndex = 16;
            btnHelp.Text = "帮助";
            btnHelp.UseVisualStyleBackColor = true;
            btnHelp.Click += btnHelp_Click;
            // 
            // btnPrevVideo
            // 
            btnPrevVideo.Location = new Point(582, 70);
            btnPrevVideo.Name = "btnPrevVideo";
            btnPrevVideo.Size = new Size(145, 34);
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
            btnTest.Location = new Point(582, 124);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(145, 34);
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
            btnResetThumbnail.Location = new Point(393, 124);
            btnResetThumbnail.Name = "btnResetThumbnail";
            btnResetThumbnail.Size = new Size(145, 34);
            btnResetThumbnail.TabIndex = 19;
            btnResetThumbnail.Text = "重获缩略图";
            btnResetThumbnail.UseVisualStyleBackColor = true;
            btnResetThumbnail.Click += btnResetThumbnail_Click;
            // 
            // btnSetScale
            // 
            btnSetScale.Location = new Point(773, 124);
            btnSetScale.Name = "btnSetScale";
            btnSetScale.Size = new Size(145, 34);
            btnSetScale.TabIndex = 20;
            btnSetScale.Text = "调整分辨率";
            btnSetScale.UseVisualStyleBackColor = true;
            btnSetScale.Click += btnSetScale_Click;
            // 
            // lblWidth
            // 
            lblWidth.AutoSize = true;
            lblWidth.Location = new Point(946, 75);
            lblWidth.Name = "lblWidth";
            lblWidth.Size = new Size(46, 24);
            lblWidth.TabIndex = 21;
            lblWidth.Text = "宽：";
            // 
            // lblHeight
            // 
            lblHeight.AutoSize = true;
            lblHeight.Location = new Point(946, 129);
            lblHeight.Name = "lblHeight";
            lblHeight.Size = new Size(46, 24);
            lblHeight.TabIndex = 22;
            lblHeight.Text = "高：";
            // 
            // tbxWidth
            // 
            tbxWidth.Location = new Point(987, 73);
            tbxWidth.Name = "tbxWidth";
            tbxWidth.Size = new Size(93, 30);
            tbxWidth.TabIndex = 23;
            // 
            // tbxHeight
            // 
            tbxHeight.Location = new Point(987, 126);
            tbxHeight.Name = "tbxHeight";
            tbxHeight.Size = new Size(93, 30);
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
            grpBoxPercent.Location = new Point(1096, 51);
            grpBoxPercent.Name = "grpBoxPercent";
            grpBoxPercent.Size = new Size(167, 120);
            grpBoxPercent.TabIndex = 25;
            grpBoxPercent.TabStop = false;
            // 
            // rbt11
            // 
            rbt11.AutoSize = true;
            rbt11.Location = new Point(6, 17);
            rbt11.Name = "rbt11";
            rbt11.Size = new Size(65, 28);
            rbt11.TabIndex = 26;
            rbt11.TabStop = true;
            rbt11.Text = "1/1";
            rbt11.UseVisualStyleBackColor = true;
            rbt11.Click += rbt11_Click;
            // 
            // rbt14
            // 
            rbt14.AutoSize = true;
            rbt14.Location = new Point(87, 15);
            rbt14.Name = "rbt14";
            rbt14.Size = new Size(65, 28);
            rbt14.TabIndex = 30;
            rbt14.TabStop = true;
            rbt14.Text = "1/4";
            rbt14.UseVisualStyleBackColor = true;
            rbt14.Click += rbt14_Click;
            // 
            // rbt13
            // 
            rbt13.AutoSize = true;
            rbt13.Location = new Point(6, 85);
            rbt13.Name = "rbt13";
            rbt13.Size = new Size(65, 28);
            rbt13.TabIndex = 29;
            rbt13.TabStop = true;
            rbt13.Text = "1/3";
            rbt13.UseVisualStyleBackColor = true;
            rbt13.Click += rbt13_Click;
            // 
            // rbt34
            // 
            rbt34.AutoSize = true;
            rbt34.Location = new Point(87, 85);
            rbt34.Name = "rbt34";
            rbt34.Size = new Size(65, 28);
            rbt34.TabIndex = 28;
            rbt34.TabStop = true;
            rbt34.Text = "3/4";
            rbt34.UseVisualStyleBackColor = true;
            rbt34.Click += rbt34_Click;
            // 
            // rbt12
            // 
            rbt12.AutoSize = true;
            rbt12.Location = new Point(6, 51);
            rbt12.Name = "rbt12";
            rbt12.Size = new Size(65, 28);
            rbt12.TabIndex = 26;
            rbt12.TabStop = true;
            rbt12.Text = "1/2";
            rbt12.UseVisualStyleBackColor = true;
            rbt12.Click += rbt12_Click;
            // 
            // rbt23
            // 
            rbt23.AutoSize = true;
            rbt23.Location = new Point(87, 51);
            rbt23.Name = "rbt23";
            rbt23.Size = new Size(65, 28);
            rbt23.TabIndex = 27;
            rbt23.TabStop = true;
            rbt23.Text = "2/3";
            rbt23.UseVisualStyleBackColor = true;
            rbt23.Click += rbt23_Click;
            // 
            // WaterMark
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1969, 1310);
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
    }
}