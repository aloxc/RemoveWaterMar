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
            richTextBox1 = new RichTextBox();
            SuspendLayout();
            // 
            // curveButton
            // 
            curveButton.Location = new Point(24, 1024);
            curveButton.Name = "curveButton";
            curveButton.Size = new Size(112, 34);
            curveButton.TabIndex = 0;
            curveButton.Text = "绘制曲线";
            curveButton.UseVisualStyleBackColor = true;
            curveButton.Click += curveButton_Click;
            // 
            // piechartButton
            // 
            piechartButton.Location = new Point(179, 1024);
            piechartButton.Name = "piechartButton";
            piechartButton.Size = new Size(112, 34);
            piechartButton.TabIndex = 1;
            piechartButton.Text = "饼图";
            piechartButton.UseVisualStyleBackColor = true;
            piechartButton.Click += piechartButton_Click;
            // 
            // lineButton
            // 
            lineButton.Location = new Point(964, 37);
            lineButton.Name = "lineButton";
            lineButton.Size = new Size(142, 34);
            lineButton.TabIndex = 2;
            lineButton.Text = "直线";
            lineButton.UseVisualStyleBackColor = true;
            lineButton.Click += lineButton_Click;
            // 
            // rectangleButton
            // 
            rectangleButton.Location = new Point(964, 123);
            rectangleButton.Name = "rectangleButton";
            rectangleButton.Size = new Size(142, 34);
            rectangleButton.TabIndex = 3;
            rectangleButton.Text = "矩形";
            rectangleButton.UseVisualStyleBackColor = true;
            rectangleButton.Click += rectangleButton_Click;
            // 
            // cyliderButton
            // 
            cyliderButton.Location = new Point(964, 295);
            cyliderButton.Name = "cyliderButton";
            cyliderButton.Size = new Size(142, 34);
            cyliderButton.TabIndex = 5;
            cyliderButton.Text = "圆柱体";
            cyliderButton.UseVisualStyleBackColor = true;
            cyliderButton.Click += cyliderButton_Click;
            // 
            // ellispeButton
            // 
            ellispeButton.Location = new Point(964, 209);
            ellispeButton.Name = "ellispeButton";
            ellispeButton.Size = new Size(142, 34);
            ellispeButton.TabIndex = 4;
            ellispeButton.Text = "圆形";
            ellispeButton.UseVisualStyleBackColor = true;
            ellispeButton.Click += ellispeButton_Click;
            // 
            // moveEllispeButton
            // 
            moveEllispeButton.Location = new Point(964, 639);
            moveEllispeButton.Name = "moveEllispeButton";
            moveEllispeButton.Size = new Size(142, 34);
            moveEllispeButton.TabIndex = 9;
            moveEllispeButton.Text = "圆形平移";
            moveEllispeButton.UseVisualStyleBackColor = true;
            moveEllispeButton.Click += moveEllispeButton_Click;
            // 
            // fontButton
            // 
            fontButton.Location = new Point(964, 553);
            fontButton.Name = "fontButton";
            fontButton.Size = new Size(142, 34);
            fontButton.TabIndex = 8;
            fontButton.Text = "写字";
            fontButton.UseVisualStyleBackColor = true;
            fontButton.Click += fontButton_Click;
            // 
            // drawEllispeButton
            // 
            drawEllispeButton.Location = new Point(964, 467);
            drawEllispeButton.Name = "drawEllispeButton";
            drawEllispeButton.Size = new Size(142, 34);
            drawEllispeButton.TabIndex = 7;
            drawEllispeButton.Text = "线性渐变画圆";
            drawEllispeButton.UseVisualStyleBackColor = true;
            drawEllispeButton.Click += drawEllispeButton_Click;
            // 
            // fillRectangleButton
            // 
            fillRectangleButton.Location = new Point(964, 381);
            fillRectangleButton.Name = "fillRectangleButton";
            fillRectangleButton.Size = new Size(142, 34);
            fillRectangleButton.TabIndex = 6;
            fillRectangleButton.Text = "填充矩形";
            fillRectangleButton.UseVisualStyleBackColor = true;
            fillRectangleButton.Click += fillRectangleButton_Click;
            // 
            // scaleEllispeButton
            // 
            scaleEllispeButton.Location = new Point(964, 725);
            scaleEllispeButton.Name = "scaleEllispeButton";
            scaleEllispeButton.Size = new Size(142, 34);
            scaleEllispeButton.TabIndex = 10;
            scaleEllispeButton.Text = "圆形缩放";
            scaleEllispeButton.UseVisualStyleBackColor = true;
            scaleEllispeButton.Click += scaleEllispeButton_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(525, 172);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(407, 464);
            richTextBox1.TabIndex = 11;
            richTextBox1.Text = "";
            // 
            // TestForm
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1234, 1102);
            Controls.Add(richTextBox1);
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
            Load += TestForm_Load;
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
        private RichTextBox richTextBox1;
    }
}