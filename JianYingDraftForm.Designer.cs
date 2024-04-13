namespace RemoveWaterMar
{
    partial class JianYingDraftForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JianYingDraftForm));
            btnDoit = new Button();
            btnDraftDir = new Button();
            lvDraftList = new ListViewEx();
            btnStopBatch = new Button();
            lblTaskCount = new Label();
            lblDoneCount = new Label();
            SuspendLayout();
            // 
            // btnDoit
            // 
            btnDoit.Location = new Point(136, 24);
            btnDoit.Name = "btnDoit";
            btnDoit.Size = new Size(75, 23);
            btnDoit.TabIndex = 999;
            btnDoit.Text = "导出";
            btnDoit.UseVisualStyleBackColor = true;
            btnDoit.Click += btnDoit_Click;
            // 
            // btnDraftDir
            // 
            btnDraftDir.Location = new Point(20, 24);
            btnDraftDir.Name = "btnDraftDir";
            btnDraftDir.Size = new Size(75, 23);
            btnDraftDir.TabIndex = 2;
            btnDraftDir.Text = "草稿路径";
            btnDraftDir.UseVisualStyleBackColor = true;
            btnDraftDir.Click += btnDraftDir_Click;
            // 
            // lvDraftList
            // 
            lvDraftList.Location = new Point(20, 67);
            lvDraftList.Name = "lvDraftList";
            lvDraftList.OwnerDraw = true;
            lvDraftList.ProgressColor = Color.Green;
            lvDraftList.ProgressColumIndex = -1;
            lvDraftList.ProgressTextColor = Color.Black;
            lvDraftList.Size = new Size(768, 336);
            lvDraftList.TabIndex = 1;
            lvDraftList.UseCompatibleStateImageBehavior = false;
            lvDraftList.View = View.Details;
            // 
            // btnStopBatch
            // 
            btnStopBatch.Location = new Point(252, 25);
            btnStopBatch.Name = "btnStopBatch";
            btnStopBatch.Size = new Size(75, 23);
            btnStopBatch.TabIndex = 4;
            btnStopBatch.Text = "停止";
            btnStopBatch.UseVisualStyleBackColor = true;
            // 
            // lblTaskCount
            // 
            lblTaskCount.AutoSize = true;
            lblTaskCount.ForeColor = Color.Red;
            lblTaskCount.Location = new Point(356, 29);
            lblTaskCount.Name = "lblTaskCount";
            lblTaskCount.Size = new Size(55, 17);
            lblTaskCount.TabIndex = 7;
            lblTaskCount.Text = "总任务 0";
            // 
            // lblDoneCount
            // 
            lblDoneCount.AutoSize = true;
            lblDoneCount.ForeColor = Color.Red;
            lblDoneCount.Location = new Point(449, 30);
            lblDoneCount.Name = "lblDoneCount";
            lblDoneCount.Size = new Size(55, 17);
            lblDoneCount.TabIndex = 8;
            lblDoneCount.Text = "已完成 0";
            // 
            // JianYingDraftForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 417);
            Controls.Add(lblDoneCount);
            Controls.Add(lblTaskCount);
            Controls.Add(btnStopBatch);
            Controls.Add(btnDraftDir);
            Controls.Add(lvDraftList);
            Controls.Add(btnDoit);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "JianYingDraftForm";
            ShowInTaskbar = false;
            Text = "剪映草稿工具";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnDoit;
        private Button btnDraftDir;
        private ListViewEx lvDraftList;
        private Button btnStopBatch;
        private Label lblTaskCount;
        private Label lblDoneCount;
    }
}