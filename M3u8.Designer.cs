namespace RemoveWaterMar
{
    partial class M3u8
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(M3u8));
            label1 = new Label();
            label2 = new Label();
            tboxName = new TextBox();
            tboxPath = new TextBox();
            btnOpenM3u8 = new Button();
            btnMerge = new Button();
            lblStatus = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 19);
            label1.Name = "label1";
            label1.Size = new Size(68, 17);
            label1.TabIndex = 0;
            label1.Text = "视频片段：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 52);
            label2.Name = "label2";
            label2.Size = new Size(68, 17);
            label2.TabIndex = 1;
            label2.Text = "合并名称：";
            // 
            // tboxName
            // 
            tboxName.Location = new Point(74, 49);
            tboxName.Name = "tboxName";
            tboxName.Size = new Size(468, 23);
            tboxName.TabIndex = 2;
            // 
            // tboxPath
            // 
            tboxPath.Location = new Point(74, 16);
            tboxPath.Name = "tboxPath";
            tboxPath.Size = new Size(468, 23);
            tboxPath.TabIndex = 3;
            // 
            // btnOpenM3u8
            // 
            btnOpenM3u8.Location = new Point(573, 16);
            btnOpenM3u8.Name = "btnOpenM3u8";
            btnOpenM3u8.Size = new Size(75, 23);
            btnOpenM3u8.TabIndex = 4;
            btnOpenM3u8.Text = "浏览";
            btnOpenM3u8.UseVisualStyleBackColor = true;
            btnOpenM3u8.Click += btnOpenM3u8_Click;
            // 
            // btnMerge
            // 
            btnMerge.Location = new Point(573, 52);
            btnMerge.Name = "btnMerge";
            btnMerge.Size = new Size(75, 23);
            btnMerge.TabIndex = 5;
            btnMerge.Text = "开始合并";
            btnMerge.UseVisualStyleBackColor = true;
            btnMerge.Click += btnMerge_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = Color.Red;
            lblStatus.Location = new Point(12, 83);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 17);
            lblStatus.TabIndex = 6;
            // 
            // M3u8
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(663, 109);
            Controls.Add(lblStatus);
            Controls.Add(btnMerge);
            Controls.Add(btnOpenM3u8);
            Controls.Add(tboxPath);
            Controls.Add(tboxName);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "M3u8";
            ShowInTaskbar = false;
            Text = "M3u8合并器";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox tboxName;
        private TextBox tboxPath;
        private Button btnOpenM3u8;
        private Button btnMerge;
        private Label lblStatus;
    }
}