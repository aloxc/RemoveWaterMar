namespace RemoveWaterMar
{
    partial class TestForm
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
            curveButton = new Button();
            piechartButton = new Button();
            lineButton = new Button();
            rectangleButton = new Button();
            cyliderButton = new Button();
            ellispeButton = new Button();
            moveEllispeButton = new Button();
            fontButton = new Button();
            drawEllispeButton = new Button();
            fillRectangleButton = new Button();
            scaleEllispeButton = new Button();
            btnOpen = new Button();
            btnShuip = new Button();
            btnShuiping = new Button();
            btnChuizhi = new Button();
            btnAlpha = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // curveButton
            // 
            curveButton.Location = new Point(26, 1227);
            curveButton.Name = "curveButton";
            curveButton.Size = new Size(112, 34);
            curveButton.TabIndex = 0;
            curveButton.Text = "绘制曲线";
            curveButton.UseVisualStyleBackColor = true;
            curveButton.Click += curveButton_Click;
            // 
            // piechartButton
            // 
            piechartButton.Location = new Point(26, 1148);
            piechartButton.Name = "piechartButton";
            piechartButton.Size = new Size(112, 34);
            piechartButton.TabIndex = 1;
            piechartButton.Text = "饼图";
            piechartButton.UseVisualStyleBackColor = true;
            piechartButton.Click += piechartButton_Click;
            // 
            // lineButton
            // 
            lineButton.Location = new Point(24, 42);
            lineButton.Name = "lineButton";
            lineButton.Size = new Size(141, 34);
            lineButton.TabIndex = 2;
            lineButton.Text = "直线";
            lineButton.UseVisualStyleBackColor = true;
            lineButton.Click += lineButton_Click;
            // 
            // rectangleButton
            // 
            rectangleButton.Location = new Point(24, 121);
            rectangleButton.Name = "rectangleButton";
            rectangleButton.Size = new Size(141, 34);
            rectangleButton.TabIndex = 3;
            rectangleButton.Text = "矩形";
            rectangleButton.UseVisualStyleBackColor = true;
            rectangleButton.Click += rectangleButton_Click;
            // 
            // cyliderButton
            // 
            cyliderButton.Location = new Point(24, 279);
            cyliderButton.Name = "cyliderButton";
            cyliderButton.Size = new Size(141, 34);
            cyliderButton.TabIndex = 5;
            cyliderButton.Text = "圆柱体";
            cyliderButton.UseVisualStyleBackColor = true;
            cyliderButton.Click += cyliderButton_Click;
            // 
            // ellispeButton
            // 
            ellispeButton.Location = new Point(24, 200);
            ellispeButton.Name = "ellispeButton";
            ellispeButton.Size = new Size(141, 34);
            ellispeButton.TabIndex = 4;
            ellispeButton.Text = "圆形";
            ellispeButton.UseVisualStyleBackColor = true;
            ellispeButton.Click += ellispeButton_Click;
            // 
            // moveEllispeButton
            // 
            moveEllispeButton.Location = new Point(24, 595);
            moveEllispeButton.Name = "moveEllispeButton";
            moveEllispeButton.Size = new Size(141, 34);
            moveEllispeButton.TabIndex = 9;
            moveEllispeButton.Text = "圆形平移";
            moveEllispeButton.UseVisualStyleBackColor = true;
            moveEllispeButton.Click += moveEllispeButton_Click;
            // 
            // fontButton
            // 
            fontButton.Location = new Point(24, 516);
            fontButton.Name = "fontButton";
            fontButton.Size = new Size(141, 34);
            fontButton.TabIndex = 8;
            fontButton.Text = "写字";
            fontButton.UseVisualStyleBackColor = true;
            fontButton.Click += fontButton_Click;
            // 
            // drawEllispeButton
            // 
            drawEllispeButton.Location = new Point(24, 437);
            drawEllispeButton.Name = "drawEllispeButton";
            drawEllispeButton.Size = new Size(141, 34);
            drawEllispeButton.TabIndex = 7;
            drawEllispeButton.Text = "线性渐变画圆";
            drawEllispeButton.UseVisualStyleBackColor = true;
            drawEllispeButton.Click += drawEllispeButton_Click;
            // 
            // fillRectangleButton
            // 
            fillRectangleButton.Location = new Point(24, 358);
            fillRectangleButton.Name = "fillRectangleButton";
            fillRectangleButton.Size = new Size(141, 34);
            fillRectangleButton.TabIndex = 6;
            fillRectangleButton.Text = "填充矩形";
            fillRectangleButton.UseVisualStyleBackColor = true;
            fillRectangleButton.Click += fillRectangleButton_Click;
            // 
            // scaleEllispeButton
            // 
            scaleEllispeButton.Location = new Point(24, 674);
            scaleEllispeButton.Name = "scaleEllispeButton";
            scaleEllispeButton.Size = new Size(141, 34);
            scaleEllispeButton.TabIndex = 10;
            scaleEllispeButton.Text = "圆形缩放";
            scaleEllispeButton.UseVisualStyleBackColor = true;
            scaleEllispeButton.Click += scaleEllispeButton_Click;
            // 
            // btnOpen
            // 
            btnOpen.BackColor = Color.IndianRed;
            btnOpen.Location = new Point(26, 753);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(112, 34);
            btnOpen.TabIndex = 12;
            btnOpen.Text = "打开图像";
            btnOpen.UseVisualStyleBackColor = false;
            btnOpen.Click += btnOpen_Click;
            // 
            // btnShuip
            // 
            btnShuip.Location = new Point(24, 832);
            btnShuip.Name = "btnShuip";
            btnShuip.Size = new Size(112, 34);
            btnShuip.TabIndex = 13;
            btnShuip.Text = "水平对展";
            btnShuip.UseVisualStyleBackColor = true;
            btnShuip.Click += btnShuip_Click;
            // 
            // btnShuiping
            // 
            btnShuiping.Location = new Point(24, 990);
            btnShuiping.Name = "btnShuiping";
            btnShuiping.Size = new Size(112, 34);
            btnShuiping.TabIndex = 14;
            btnShuiping.Text = "水平展开";
            btnShuiping.UseVisualStyleBackColor = true;
            btnShuiping.Click += btnShuiping_Click;
            // 
            // btnChuizhi
            // 
            btnChuizhi.Location = new Point(26, 911);
            btnChuizhi.Name = "btnChuizhi";
            btnChuizhi.Size = new Size(112, 34);
            btnChuizhi.TabIndex = 14;
            btnChuizhi.Text = "垂直遮罩";
            btnChuizhi.UseVisualStyleBackColor = true;
            btnChuizhi.Click += btnChuizhi_Click;
            // 
            // btnAlpha
            // 
            btnAlpha.Location = new Point(26, 1069);
            btnAlpha.Name = "btnAlpha";
            btnAlpha.Size = new Size(112, 34);
            btnAlpha.TabIndex = 15;
            btnAlpha.Text = "修改透明度";
            btnAlpha.UseVisualStyleBackColor = true;
            btnAlpha.Click += btnAlpha_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(362, 104);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1577, 1242);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 16;
            pictureBox1.TabStop = false;
            // 
            // TestForm
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2692, 1632);
            Controls.Add(pictureBox1);
            Controls.Add(btnAlpha);
            Controls.Add(btnChuizhi);
            Controls.Add(btnShuip);
            Controls.Add(btnShuiping);
            Controls.Add(btnOpen);
            Controls.Add(scaleEllispeButton);
            Controls.Add(moveEllispeButton);
            Controls.Add(fontButton);
            Controls.Add(drawEllispeButton);
            Controls.Add(fillRectangleButton);
            Controls.Add(cyliderButton);
            Controls.Add(ellispeButton);
            Controls.Add(rectangleButton);
            Controls.Add(lineButton);
            Controls.Add(piechartButton);
            Controls.Add(curveButton);
            Name = "TestForm";
            Text = "TestForm";
            WindowState = FormWindowState.Maximized;
            Load += TestForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button curveButton;
        private Button piechartButton;
        private Button lineButton;
        private Button rectangleButton;
        private Button cyliderButton;
        private Button ellispeButton;
        private Button moveEllispeButton;
        private Button fontButton;
        private Button drawEllispeButton;
        private Button fillRectangleButton;
        private Button scaleEllispeButton;
        private Button btnOpen;
        private Button btnShuip;
        private Button btnShuiping;
        private Button btnChuizhi;
        private Button btnAlpha;
        private PictureBox pictureBox1;
    }
}