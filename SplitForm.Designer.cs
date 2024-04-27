namespace RemoveWaterMar
{
    partial class SplitForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplitForm));
            tabControl1 = new TabControl();
            tabOne = new TabPage();
            cbxOneGpu = new CheckBox();
            lblDurationTime = new Label();
            lblFile = new Label();
            btnStopOne = new Button();
            lblTimeTotalOne = new Label();
            tbxEnd = new TextBox();
            tbxStart = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            btnDoOne = new Button();
            btnOpenVideo = new Button();
            tabBatch = new TabPage();
            cbxShutdown = new CheckBox();
            btnDeleteSelect = new Button();
            btnMakeList = new Button();
            btnClearAll = new Button();
            btnCleanDone = new Button();
            cbxBatchGpu = new CheckBox();
            lblTimeTotal = new Label();
            lblDoneCount = new Label();
            lblTaskCount = new Label();
            btnStopBatch = new Button();
            lbxFile = new ListViewEx();
            btnBatchDoIt = new Button();
            btnBatchOpenConfig = new Button();
            lblLog = new Label();
            tipsMakeList = new ToolTip(components);
            shutdownTimer = new System.Windows.Forms.Timer(components);
            tabControl1.SuspendLayout();
            tabOne.SuspendLayout();
            tabBatch.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabOne);
            tabControl1.Controls.Add(tabBatch);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(795, 358);
            tabControl1.TabIndex = 0;
            // 
            // tabOne
            // 
            tabOne.Controls.Add(cbxOneGpu);
            tabOne.Controls.Add(lblDurationTime);
            tabOne.Controls.Add(lblFile);
            tabOne.Controls.Add(btnStopOne);
            tabOne.Controls.Add(lblTimeTotalOne);
            tabOne.Controls.Add(tbxEnd);
            tabOne.Controls.Add(tbxStart);
            tabOne.Controls.Add(label4);
            tabOne.Controls.Add(label3);
            tabOne.Controls.Add(label2);
            tabOne.Controls.Add(label1);
            tabOne.Controls.Add(btnDoOne);
            tabOne.Controls.Add(btnOpenVideo);
            tabOne.Location = new Point(4, 26);
            tabOne.Name = "tabOne";
            tabOne.Padding = new Padding(3);
            tabOne.Size = new Size(787, 328);
            tabOne.TabIndex = 0;
            tabOne.Text = "单文件";
            tabOne.UseVisualStyleBackColor = true;
            // 
            // cbxOneGpu
            // 
            cbxOneGpu.AutoSize = true;
            cbxOneGpu.Checked = true;
            cbxOneGpu.CheckState = CheckState.Checked;
            cbxOneGpu.Location = new Point(117, 12);
            cbxOneGpu.Name = "cbxOneGpu";
            cbxOneGpu.Size = new Size(75, 21);
            cbxOneGpu.TabIndex = 12;
            cbxOneGpu.Text = "Gpu处理";
            cbxOneGpu.UseVisualStyleBackColor = true;
            // 
            // lblDurationTime
            // 
            lblDurationTime.AutoSize = true;
            lblDurationTime.ForeColor = Color.Red;
            lblDurationTime.Location = new Point(14, 95);
            lblDurationTime.Name = "lblDurationTime";
            lblDurationTime.Size = new Size(0, 17);
            lblDurationTime.TabIndex = 11;
            // 
            // lblFile
            // 
            lblFile.AutoSize = true;
            lblFile.Location = new Point(14, 57);
            lblFile.Name = "lblFile";
            lblFile.Size = new Size(0, 17);
            lblFile.TabIndex = 10;
            // 
            // btnStopOne
            // 
            btnStopOne.Location = new Point(137, 261);
            btnStopOne.Name = "btnStopOne";
            btnStopOne.Size = new Size(75, 23);
            btnStopOne.TabIndex = 9;
            btnStopOne.Text = "停止处理";
            btnStopOne.UseVisualStyleBackColor = true;
            btnStopOne.Click += btnStopOne_Click;
            // 
            // lblTimeTotalOne
            // 
            lblTimeTotalOne.AutoSize = true;
            lblTimeTotalOne.ForeColor = Color.Red;
            lblTimeTotalOne.Location = new Point(242, 267);
            lblTimeTotalOne.Name = "lblTimeTotalOne";
            lblTimeTotalOne.Size = new Size(67, 17);
            lblTimeTotalOne.TabIndex = 8;
            lblTimeTotalOne.Text = "用时：0 秒";
            // 
            // tbxEnd
            // 
            tbxEnd.Location = new Point(72, 212);
            tbxEnd.Name = "tbxEnd";
            tbxEnd.Size = new Size(159, 23);
            tbxEnd.TabIndex = 7;
            tbxEnd.Text = "00:07:23";
            // 
            // tbxStart
            // 
            tbxStart.Location = new Point(72, 163);
            tbxStart.Name = "tbxStart";
            tbxStart.Size = new Size(159, 23);
            tbxStart.TabIndex = 6;
            tbxStart.Text = "00:03:23";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = SystemColors.ButtonShadow;
            label4.Location = new Point(237, 215);
            label4.Name = "label4";
            label4.Size = new Size(132, 17);
            label4.TabIndex = 5;
            label4.Text = "格式 00:00:00，可不填";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.ButtonShadow;
            label3.Location = new Point(237, 166);
            label3.Name = "label3";
            label3.Size = new Size(88, 17);
            label3.TabIndex = 4;
            label3.Text = "格式  00:00:00";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 215);
            label2.Name = "label2";
            label2.Size = new Size(56, 17);
            label2.TabIndex = 3;
            label2.Text = "结束时间";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 166);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 2;
            label1.Text = "开始时间";
            // 
            // btnDoOne
            // 
            btnDoOne.Location = new Point(14, 261);
            btnDoOne.Name = "btnDoOne";
            btnDoOne.Size = new Size(75, 23);
            btnDoOne.TabIndex = 1;
            btnDoOne.Text = "处理视频";
            btnDoOne.UseVisualStyleBackColor = true;
            btnDoOne.Click += btnDoOne_Click;
            // 
            // btnOpenVideo
            // 
            btnOpenVideo.Location = new Point(14, 10);
            btnOpenVideo.Name = "btnOpenVideo";
            btnOpenVideo.Size = new Size(75, 23);
            btnOpenVideo.TabIndex = 0;
            btnOpenVideo.Text = "打开视频";
            btnOpenVideo.UseVisualStyleBackColor = true;
            btnOpenVideo.Click += btnOpenVideo_Click;
            // 
            // tabBatch
            // 
            tabBatch.Controls.Add(cbxShutdown);
            tabBatch.Controls.Add(btnDeleteSelect);
            tabBatch.Controls.Add(btnMakeList);
            tabBatch.Controls.Add(btnClearAll);
            tabBatch.Controls.Add(btnCleanDone);
            tabBatch.Controls.Add(cbxBatchGpu);
            tabBatch.Controls.Add(lblTimeTotal);
            tabBatch.Controls.Add(lblDoneCount);
            tabBatch.Controls.Add(lblTaskCount);
            tabBatch.Controls.Add(btnStopBatch);
            tabBatch.Controls.Add(lbxFile);
            tabBatch.Controls.Add(btnBatchDoIt);
            tabBatch.Controls.Add(btnBatchOpenConfig);
            tabBatch.Location = new Point(4, 26);
            tabBatch.Name = "tabBatch";
            tabBatch.Padding = new Padding(3);
            tabBatch.Size = new Size(787, 328);
            tabBatch.TabIndex = 1;
            tabBatch.Text = "批处理";
            tabBatch.UseVisualStyleBackColor = true;
            // 
            // cbxShutdown
            // 
            cbxShutdown.AutoSize = true;
            cbxShutdown.Location = new Point(519, 13);
            cbxShutdown.Name = "cbxShutdown";
            cbxShutdown.Size = new Size(51, 21);
            cbxShutdown.TabIndex = 18;
            cbxShutdown.Text = "关机";
            cbxShutdown.UseVisualStyleBackColor = true;
            cbxShutdown.CheckedChanged += cbxShutdown_CheckedChanged;
            // 
            // btnDeleteSelect
            // 
            btnDeleteSelect.Location = new Point(204, 11);
            btnDeleteSelect.Name = "btnDeleteSelect";
            btnDeleteSelect.Size = new Size(75, 23);
            btnDeleteSelect.TabIndex = 17;
            btnDeleteSelect.Text = "清理选中";
            btnDeleteSelect.UseVisualStyleBackColor = true;
            btnDeleteSelect.Click += btnDeleteSelect_Click;
            // 
            // btnMakeList
            // 
            btnMakeList.BackColor = Color.Red;
            btnMakeList.Location = new Point(6, 10);
            btnMakeList.Name = "btnMakeList";
            btnMakeList.Size = new Size(20, 23);
            btnMakeList.TabIndex = 16;
            btnMakeList.UseVisualStyleBackColor = false;
            btnMakeList.Click += btnMakeList_Click;
            // 
            // btnClearAll
            // 
            btnClearAll.Location = new Point(366, 11);
            btnClearAll.Name = "btnClearAll";
            btnClearAll.Size = new Size(75, 23);
            btnClearAll.TabIndex = 15;
            btnClearAll.Text = "清理所有";
            btnClearAll.UseVisualStyleBackColor = true;
            btnClearAll.Click += btnClearAll_Click;
            // 
            // btnCleanDone
            // 
            btnCleanDone.Location = new Point(285, 11);
            btnCleanDone.Name = "btnCleanDone";
            btnCleanDone.Size = new Size(75, 23);
            btnCleanDone.TabIndex = 14;
            btnCleanDone.Text = "清理完成";
            btnCleanDone.UseVisualStyleBackColor = true;
            btnCleanDone.Click += btnCleanDone_Click;
            // 
            // cbxBatchGpu
            // 
            cbxBatchGpu.AutoSize = true;
            cbxBatchGpu.Checked = true;
            cbxBatchGpu.CheckState = CheckState.Checked;
            cbxBatchGpu.Location = new Point(445, 13);
            cbxBatchGpu.Name = "cbxBatchGpu";
            cbxBatchGpu.Size = new Size(75, 21);
            cbxBatchGpu.TabIndex = 13;
            cbxBatchGpu.Text = "Gpu处理";
            cbxBatchGpu.UseVisualStyleBackColor = true;
            // 
            // lblTimeTotal
            // 
            lblTimeTotal.AutoSize = true;
            lblTimeTotal.ForeColor = Color.Red;
            lblTimeTotal.Location = new Point(693, 13);
            lblTimeTotal.Name = "lblTimeTotal";
            lblTimeTotal.Size = new Size(79, 17);
            lblTimeTotal.TabIndex = 6;
            lblTimeTotal.Text = "总用时：0 秒";
            // 
            // lblDoneCount
            // 
            lblDoneCount.AutoSize = true;
            lblDoneCount.ForeColor = Color.Red;
            lblDoneCount.Location = new Point(633, 13);
            lblDoneCount.Name = "lblDoneCount";
            lblDoneCount.Size = new Size(63, 17);
            lblDoneCount.TabIndex = 5;
            lblDoneCount.Text = "已完成：0";
            // 
            // lblTaskCount
            // 
            lblTaskCount.AutoSize = true;
            lblTaskCount.ForeColor = Color.Red;
            lblTaskCount.Location = new Point(572, 13);
            lblTaskCount.Name = "lblTaskCount";
            lblTaskCount.Size = new Size(63, 17);
            lblTaskCount.TabIndex = 4;
            lblTaskCount.Text = "总数量：0";
            // 
            // btnStopBatch
            // 
            btnStopBatch.Location = new Point(158, 11);
            btnStopBatch.Name = "btnStopBatch";
            btnStopBatch.Size = new Size(40, 23);
            btnStopBatch.TabIndex = 3;
            btnStopBatch.Text = "停止";
            btnStopBatch.UseVisualStyleBackColor = true;
            btnStopBatch.Click += btnStopBatch_Click;
            // 
            // lbxFile
            // 
            lbxFile.Location = new Point(14, 39);
            lbxFile.Name = "lbxFile";
            lbxFile.OwnerDraw = true;
            lbxFile.ProgressColor = Color.Green;
            lbxFile.ProgressColumIndex = -1;
            lbxFile.ProgressTextColor = Color.Black;
            lbxFile.Size = new Size(767, 283);
            lbxFile.TabIndex = 2;
            lbxFile.UseCompatibleStateImageBehavior = false;
            lbxFile.View = View.Details;
            // 
            // btnBatchDoIt
            // 
            btnBatchDoIt.Location = new Point(110, 11);
            btnBatchDoIt.Name = "btnBatchDoIt";
            btnBatchDoIt.Size = new Size(42, 23);
            btnBatchDoIt.TabIndex = 1;
            btnBatchDoIt.Text = "处理";
            btnBatchDoIt.UseVisualStyleBackColor = true;
            btnBatchDoIt.Click += btnBatchDoIt_Click;
            // 
            // btnBatchOpenConfig
            // 
            btnBatchOpenConfig.Location = new Point(29, 11);
            btnBatchOpenConfig.Name = "btnBatchOpenConfig";
            btnBatchOpenConfig.Size = new Size(75, 23);
            btnBatchOpenConfig.TabIndex = 0;
            btnBatchOpenConfig.Text = "打开配置";
            btnBatchOpenConfig.UseVisualStyleBackColor = true;
            btnBatchOpenConfig.Click += btnBatchOpenConfig_Click;
            // 
            // lblLog
            // 
            lblLog.BackColor = SystemColors.ActiveCaptionText;
            lblLog.ForeColor = Color.Red;
            lblLog.Location = new Point(12, 373);
            lblLog.Name = "lblLog";
            lblLog.Size = new Size(795, 17);
            lblLog.TabIndex = 8;
            // 
            // tipsMakeList
            // 
            tipsMakeList.ShowAlways = true;
            // 
            // shutdownTimer
            // 
            
            // 
            // SplitForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(819, 399);
            Controls.Add(lblLog);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SplitForm";
            ShowInTaskbar = false;
            Text = "视频截取";
            Load += SplitForm_Load;
            tabControl1.ResumeLayout(false);
            tabOne.ResumeLayout(false);
            tabOne.PerformLayout();
            tabBatch.ResumeLayout(false);
            tabBatch.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabOne;
        private TabPage tabBatch;
        private Button btnOpenVideo;
        private Button btnDoOne;
        private Label label1;
        private Label label2;
        private Label lblLog;
        private TextBox tbxEnd;
        private TextBox tbxStart;
        private Label label4;
        private Label label3;
        private Button btnBatchOpenConfig;
        private Button btnBatchDoIt;
        private ListViewEx lbxFile;
        private Button btnStopBatch;
        private Label lblDoneCount;
        private Label lblTaskCount;
        private Label lblTimeTotal;
        private Label lblTimeTotalOne;
        private Button btnStopOne;
        private Label lblFile;
        private Label lblDurationTime;
        private CheckBox cbxOneGpu;
        private CheckBox cbxBatchGpu;
        private Button btnClearAll;
        private Button btnCleanDone;
        private Button btnMakeList;
        private Button btnDeleteSelect;
        private ToolTip tipsMakeList;
        private CheckBox cbxShutdown;
        private System.Windows.Forms.Timer shutdownTimer;
    }
}