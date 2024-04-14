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
            lvProjectList = new ListViewEx();
            btnStopBatch = new Button();
            lblTaskCount = new Label();
            btnSetOutPath = new Button();
            SuspendLayout();
            // 
            // btnDoit
            // 
            btnDoit.Location = new Point(246, 24);
            btnDoit.Name = "btnDoit";
            btnDoit.Size = new Size(75, 23);
            btnDoit.TabIndex = 999;
            btnDoit.Text = "导出选中";
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
            lvProjectList.FullRowSelect = true;
            lvProjectList.Location = new Point(12, 67);
            lvProjectList.Name = "lvDraftList";
            lvProjectList.OwnerDraw = true;
            lvProjectList.ProgressColor = Color.Green;
            lvProjectList.ProgressColumIndex = -1;
            lvProjectList.ProgressTextColor = Color.Black;
            lvProjectList.Size = new Size(629, 314);
            lvProjectList.TabIndex = 1;
            lvProjectList.UseCompatibleStateImageBehavior = false;
            lvProjectList.View = View.Details;
            // 
            // btnStopBatch
            // 
            btnStopBatch.Location = new Point(359, 25);
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
            lblTaskCount.Location = new Point(469, 29);
            lblTaskCount.Name = "lblTaskCount";
            lblTaskCount.Size = new Size(55, 17);
            lblTaskCount.TabIndex = 7;
            lblTaskCount.Text = "总任务 0";

            // 
            // btnSetOutPath
            // 
            btnSetOutPath.Location = new Point(133, 24);
            btnSetOutPath.Name = "btnSetOutPath";
            btnSetOutPath.Size = new Size(75, 23);
            btnSetOutPath.TabIndex = 1000;
            btnSetOutPath.Text = "输出目录";
            btnSetOutPath.UseVisualStyleBackColor = true;
            btnSetOutPath.Click += btnSetOutPath_Click;
            // 
            // JianYingDraftForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(653, 400);
            Controls.Add(btnSetOutPath);
            Controls.Add(lblTaskCount);
            Controls.Add(btnStopBatch);
            Controls.Add(btnDraftDir);
            Controls.Add(lvProjectList);
            Controls.Add(btnDoit);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "JianYingDraftForm";
            ShowInTaskbar = false;
            Text = "剪映草稿工具";
            Load += JianYingDraftForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnDoit;
        private Button btnDraftDir;
        private ListViewEx lvProjectList;
        private Button btnStopBatch;
        private Label lblTaskCount;
        private Button btnSetOutPath;
    }
}