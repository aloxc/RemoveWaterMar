namespace RemoveWaterMar
{
    partial class ScaleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScaleForm));
            btnOpen = new Button();
            grpBoxPercent = new GroupBox();
            rbt14 = new RadioButton();
            rbt13 = new RadioButton();
            rbt34 = new RadioButton();
            rbt12 = new RadioButton();
            rbt23 = new RadioButton();
            btnDoit = new Button();
            lbxFile = new ListViewEx();
            gBoxMethod = new GroupBox();
            rbtnCpu = new RadioButton();
            rbtnGpu = new RadioButton();
            btnStop = new Button();
            btnClear = new Button();
            btnDeleteSelect = new Button();
            btnDeleteDone = new Button();
            lblTimeTotal = new Label();
            lblTaskCount = new Label();
            lblDoneCount = new Label();
            lblLog = new Label();
            grpBoxPercent.SuspendLayout();
            gBoxMethod.SuspendLayout();
            SuspendLayout();
            // 
            // btnOpen
            // 
            btnOpen.Location = new Point(12, 19);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(211, 34);
            btnOpen.TabIndex = 0;
            btnOpen.Text = "打开";
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += btnOpen_Click;
            // 
            // grpBoxPercent
            // 
            grpBoxPercent.Controls.Add(rbt14);
            grpBoxPercent.Controls.Add(rbt13);
            grpBoxPercent.Controls.Add(rbt34);
            grpBoxPercent.Controls.Add(rbt12);
            grpBoxPercent.Controls.Add(rbt23);
            grpBoxPercent.Location = new Point(12, 76);
            grpBoxPercent.Name = "grpBoxPercent";
            grpBoxPercent.Size = new Size(211, 218);
            grpBoxPercent.TabIndex = 26;
            grpBoxPercent.TabStop = false;
            grpBoxPercent.Text = "缩放比例";
            // 
            // rbt14
            // 
            rbt14.AutoSize = true;
            rbt14.Location = new Point(6, 109);
            rbt14.Name = "rbt14";
            rbt14.Size = new Size(65, 28);
            rbt14.TabIndex = 30;
            rbt14.TabStop = true;
            rbt14.Text = "1/4";
            rbt14.UseVisualStyleBackColor = true;
            // 
            // rbt13
            // 
            rbt13.AutoSize = true;
            rbt13.Location = new Point(6, 73);
            rbt13.Name = "rbt13";
            rbt13.Size = new Size(65, 28);
            rbt13.TabIndex = 29;
            rbt13.TabStop = true;
            rbt13.Text = "1/3";
            rbt13.UseVisualStyleBackColor = true;
            // 
            // rbt34
            // 
            rbt34.AutoSize = true;
            rbt34.Location = new Point(6, 177);
            rbt34.Name = "rbt34";
            rbt34.Size = new Size(65, 28);
            rbt34.TabIndex = 28;
            rbt34.TabStop = true;
            rbt34.Text = "3/4";
            rbt34.UseVisualStyleBackColor = true;
            // 
            // rbt12
            // 
            rbt12.AutoSize = true;
            rbt12.Location = new Point(6, 39);
            rbt12.Name = "rbt12";
            rbt12.Size = new Size(65, 28);
            rbt12.TabIndex = 26;
            rbt12.TabStop = true;
            rbt12.Text = "1/2";
            rbt12.UseVisualStyleBackColor = true;
            // 
            // rbt23
            // 
            rbt23.AutoSize = true;
            rbt23.Location = new Point(6, 143);
            rbt23.Name = "rbt23";
            rbt23.Size = new Size(65, 28);
            rbt23.TabIndex = 27;
            rbt23.TabStop = true;
            rbt23.Text = "2/3";
            rbt23.UseVisualStyleBackColor = true;
            // 
            // btnDoit
            // 
            btnDoit.Location = new Point(12, 450);
            btnDoit.Name = "btnDoit";
            btnDoit.Size = new Size(211, 34);
            btnDoit.TabIndex = 28;
            btnDoit.Text = "处理";
            btnDoit.UseVisualStyleBackColor = true;
            btnDoit.Click += btnDoit_Click;
            // 
            // lbxFile
            // 
            lbxFile.AllowDrop = true;
            lbxFile.FullRowSelect = true;
            lbxFile.Location = new Point(244, 19);
            lbxFile.Name = "lbxFile";
            lbxFile.OwnerDraw = true;
            lbxFile.ProgressColor = Color.Red;
            lbxFile.ProgressColumIndex = -1;
            lbxFile.ProgressTextColor = Color.Black;
            lbxFile.Size = new Size(1300, 847);
            lbxFile.TabIndex = 29;
            lbxFile.UseCompatibleStateImageBehavior = false;
            lbxFile.View = View.Details;
            lbxFile.DrawSubItem += lbxFile_DrawSubItem;
            lbxFile.DragDrop += Scale_DragDrop;
            lbxFile.DragEnter += Scale_DragEnter;
            // 
            // gBoxMethod
            // 
            gBoxMethod.Controls.Add(rbtnCpu);
            gBoxMethod.Controls.Add(rbtnGpu);
            gBoxMethod.Location = new Point(12, 300);
            gBoxMethod.Name = "gBoxMethod";
            gBoxMethod.Size = new Size(211, 129);
            gBoxMethod.TabIndex = 30;
            gBoxMethod.TabStop = false;
            gBoxMethod.Text = "处理方式";
            // 
            // rbtnCpu
            // 
            rbtnCpu.AutoSize = true;
            rbtnCpu.Location = new Point(6, 89);
            rbtnCpu.Name = "rbtnCpu";
            rbtnCpu.Size = new Size(143, 28);
            rbtnCpu.TabIndex = 6;
            rbtnCpu.Text = "使用CPU处理";
            rbtnCpu.UseVisualStyleBackColor = true;
            // 
            // rbtnGpu
            // 
            rbtnGpu.AutoSize = true;
            rbtnGpu.Checked = true;
            rbtnGpu.Location = new Point(6, 44);
            rbtnGpu.Name = "rbtnGpu";
            rbtnGpu.Size = new Size(144, 28);
            rbtnGpu.TabIndex = 7;
            rbtnGpu.TabStop = true;
            rbtnGpu.Text = "使用GPU处理";
            rbtnGpu.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(9, 515);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(214, 34);
            btnStop.TabIndex = 31;
            btnStop.Text = "停止";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(6, 580);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(217, 34);
            btnClear.TabIndex = 32;
            btnClear.Text = "清空列表";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnDeleteSelect
            // 
            btnDeleteSelect.Location = new Point(6, 645);
            btnDeleteSelect.Name = "btnDeleteSelect";
            btnDeleteSelect.Size = new Size(217, 34);
            btnDeleteSelect.TabIndex = 33;
            btnDeleteSelect.Text = "删除选中";
            btnDeleteSelect.UseVisualStyleBackColor = true;
            btnDeleteSelect.Click += btnDeleteSelect_Click;
            // 
            // btnDeleteDone
            // 
            btnDeleteDone.Location = new Point(9, 710);
            btnDeleteDone.Name = "btnDeleteDone";
            btnDeleteDone.Size = new Size(217, 34);
            btnDeleteDone.TabIndex = 34;
            btnDeleteDone.Text = "删除已完成";
            btnDeleteDone.UseVisualStyleBackColor = true;
            btnDeleteDone.Click += btnDeleteDone_Click;
            // 
            // lblTimeTotal
            // 
            lblTimeTotal.AutoSize = true;
            lblTimeTotal.ForeColor = Color.Red;
            lblTimeTotal.Location = new Point(3, 834);
            lblTimeTotal.Name = "lblTimeTotal";
            lblTimeTotal.Size = new Size(103, 24);
            lblTimeTotal.TabIndex = 36;
            lblTimeTotal.Text = "总用时 3 秒";
            // 
            // lblTaskCount
            // 
            lblTaskCount.AutoSize = true;
            lblTaskCount.ForeColor = Color.Red;
            lblTaskCount.Location = new Point(6, 759);
            lblTaskCount.Name = "lblTaskCount";
            lblTaskCount.Size = new Size(91, 24);
            lblTaskCount.TabIndex = 37;
            lblTaskCount.Text = "总任务 10";
            // 
            // lblDoneCount
            // 
            lblDoneCount.AutoSize = true;
            lblDoneCount.ForeColor = Color.Red;
            lblDoneCount.Location = new Point(6, 797);
            lblDoneCount.Name = "lblDoneCount";
            lblDoneCount.Size = new Size(91, 24);
            lblDoneCount.TabIndex = 38;
            lblDoneCount.Text = "已完成 10";
            // 
            // lblLog
            // 
            lblLog.BackColor = SystemColors.ActiveCaptionText;
            lblLog.ForeColor = SystemColors.ButtonHighlight;
            lblLog.Location = new Point(7, 873);
            lblLog.Name = "lblLog";
            lblLog.Size = new Size(1537, 24);
            lblLog.TabIndex = 39;
            // 
            // ScaleForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1586, 917);
            Controls.Add(lblLog);
            Controls.Add(lblDoneCount);
            Controls.Add(lblTaskCount);
            Controls.Add(lblTimeTotal);
            Controls.Add(btnDeleteDone);
            Controls.Add(btnDeleteSelect);
            Controls.Add(btnClear);
            Controls.Add(btnStop);
            Controls.Add(gBoxMethod);
            Controls.Add(lbxFile);
            Controls.Add(btnDoit);
            Controls.Add(grpBoxPercent);
            Controls.Add(btnOpen);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ScaleForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "调整分辨率";
            Load += ScaleForm_Load;
            DragDrop += Scale_DragDrop;
            DragEnter += Scale_DragEnter;
            grpBoxPercent.ResumeLayout(false);
            grpBoxPercent.PerformLayout();
            gBoxMethod.ResumeLayout(false);
            gBoxMethod.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnOpen;
        private GroupBox grpBoxPercent;
        private RadioButton rbt14;
        private RadioButton rbt13;
        private RadioButton rbt34;
        private RadioButton rbt12;
        private RadioButton rbt23;
        private Button btnDoit;
        private ListViewEx lbxFile;
        private GroupBox gBoxMethod;
        private RadioButton rbtnCpu;
        private RadioButton rbtnGpu;
        private Button btnStop;
        private Button btnClear;
        private Button btnDeleteSelect;
        private Button btnDeleteDone;
        private Label lblTimeTotal;
        private Label lblTaskCount;
        private Label lblDoneCount;
        private Label lblLog;
    }
}