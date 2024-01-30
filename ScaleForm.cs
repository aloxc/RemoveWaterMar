using FFmpeg.NET;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RemoveWaterMar
{
    public partial class ScaleForm : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        private readonly static string logFileStart = "log";
        Engine ffmpeg = new Engine(@"ffmpeg.exe");
        CancellationToken token = new CancellationToken();
        private Process process;
        private bool processing = false;
        //调整分辨率后视频保存文件的后缀
        private readonly static string scaleName = "__Scale";
        private int index = 0;
        private TimeSpan spendTime;
        private TimeSpan spendTimeTotal;
        private int taskCount = 0;
        private int doneCount = 0;
        private string outFile = null;
        private System.Threading.Timer taskTimer = null;
        private readonly int percentColumIndex = 3;

        public ScaleForm()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            this.DoubleBuffered = true;
            string rootPath = Directory.GetCurrentDirectory();
            DirectoryInfo root = new DirectoryInfo(rootPath);
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("./log.txt", rollingInterval: RollingInterval.Month).CreateLogger();
        }

        private (int min, int max) getScaleSize()
        {
            if (rbt12.Checked)
            {
                return (1, 2);
            }
            if (rbt13.Checked)
            {
                return (1, 3);
            }
            if (rbt14.Checked)
            {
                return (1, 4);
            }
            if (rbt23.Checked)
            {
                return (2, 3);
            }
            if (rbt34.Checked)
            {
                return (3, 4);
            }
            return (0, 0);
        }
        private (string width, string height) getNewSize(string frameSize)
        {
            var scale = getScaleSize();
            if (scale.min == 0 || scale.max == 0)
            {
                return (null, null);
            }
            string[] frameInfo = frameSize.Split("x");
            int w = Int32.Parse(frameInfo[0]);
            int h = Int32.Parse(frameInfo[1]);
            return (
                Convert.ToString(w * scale.min / scale.max),
                Convert.ToString(h * scale.min / scale.max)
                );
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "视频文件(*.mp4,*.mkv)|*.mp4;*.mkv";
            openFileDialog.Multiselect = true;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                string[] files = openFileDialog.FileNames;
                for (int i = 0; i < files.Length; i++)
                {
                    string f = files[i];
                    var inputFile = new InputFile(f);
                    GetVideoInfo(i, inputFile);
                }
            }
            else
            {
                return;
            }

        }

        private void Scale_DragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            //MessageBox.Show("drag");
        }

        private void Scale_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 0)
            {
                MessageBox.Show("至少拖放一个视频文件");
                return;
            }
            foreach (string f in files)
            {
                if (!f.ToLower().EndsWith(".mp4") && !f.ToLower().EndsWith(".mkv"))
                {
                    MessageBox.Show("只能视频文件(.mp4和.mkv)");
                    return;
                }
            }
            for (int i = 0; i < files.Length; i++)
            {
                string f = files[i];
                var inputFile = new InputFile(f);
                GetVideoInfo(i,inputFile);
            }
        }
        public async void GetVideoInfo(int i,InputFile inputFile)
        {
            MetaData data = await ffmpeg.GetMetaDataAsync(inputFile, token);
            int seconds = (int)data.Duration.TotalSeconds;
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            string timeString = time.ToString(@"hh\:mm\:ss");

            string frameSize = data.VideoData.FrameSize;
            Log.Debug(inputFile.Name + ";" + frameSize);
            ListViewItem item = lbxFile.Items.Add(new ListViewItem(new string[] { inputFile.Name, timeString, frameSize, "0","" }));
            item.UseItemStyleForSubItems = false;
            
            if (i % 2 == 0)
            {
                item.SubItems[0].BackColor = Color.Gainsboro;
                item.SubItems[1].BackColor = Color.Gainsboro;
                item.SubItems[2].BackColor = Color.Gainsboro;
                item.SubItems[3].BackColor = Color.Gainsboro;
                item.SubItems[4].BackColor = Color.Gainsboro;
            }
            else
            {
                item.SubItems[0].BackColor = Color.MistyRose;
                item.SubItems[1].BackColor = Color.MistyRose;
                item.SubItems[2].BackColor = Color.MistyRose;
                item.SubItems[3].BackColor = Color.MistyRose;
                item.SubItems[4].BackColor = Color.MistyRose;
            }
            taskCount++;
            this.lblTaskCount.Text = "总任务 " + taskCount;
            this.lblDoneCount.Text = "已完成 " + doneCount;
            
        }

        private void ScaleForm_Load(object sender, EventArgs e)
        {
            lbxFile.HeaderStyle = ColumnHeaderStyle.Clickable;
            lbxFile.View = View.Details;

            this.lbxFile.Columns.Add("文件", 780, HorizontalAlignment.Left);
            this.lbxFile.Columns.Add("时长", 100, HorizontalAlignment.Left);
            this.lbxFile.Columns.Add("分辨率", 150, HorizontalAlignment.Left);
            this.lbxFile.Columns.Add("进度", 200, HorizontalAlignment.Left);
            this.lbxFile.Columns.Add("用时", 50, HorizontalAlignment.Left);
            this.btnStop.Enabled = false;

            this.lbxFile.ProgressTextColor = Color.Red;
            this.lbxFile.ProgressColor = Color.YellowGreen;

            this.lblDoneCount.Text = "";
            this.lblTaskCount.Text = "";
            this.lblTimeTotal.Text = "";
            this.lblLog.Visible = false;
        }

        private void lbxFile_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            this.lbxFile.ProgressColumIndex = percentColumIndex;
        }

        private bool checkScaleButton()
        {
            return (rbt12.Checked || rbt13.Checked || rbt14.Checked || rbt23.Checked || rbt34.Checked);
        }
        private void btnDoit_Click(object sender, EventArgs e)
        {
            spendTimeTotal = new TimeSpan(DateTime.Now.Ticks);

            if (!checkScaleButton())
            {
                MessageBox.Show("请选择缩放比例");
                return;
            }
            taskTimer = new System.Threading.Timer(new TimerCallback(runTask));
            taskTimer.Change(0,1000);
        }
        private void runTask(object v)
        {
            Log.Debug("runTask,开始");
            int count = lbxFile.Items.Count;
            if (count == 0)
            {
                Log.Debug("runTask,列表中没有任务");
                return;
            }
            Process[] ps = Process.GetProcesses();
            bool hasFfmpeg = false;
            foreach (Process p in ps)
            {
                if (p.ProcessName.ToLower().Equals("ffmpeg"))
                {
                    hasFfmpeg = true;
                }
            }
            if (hasFfmpeg)
            {
                Log.Debug("runTask,有ffmpeg");
                return;
            }
            bool doneAll = true;
            for (int i = 0; i < count; i++)
            {
                index = i;
                spendTime = new TimeSpan(DateTime.Now.Ticks);
                string filePath = lbxFile.Items[i].SubItems[0].Text;
                string percentColum = lbxFile.Items[i].SubItems[percentColumIndex].Text;
                if (!percentColum.Contains("100"))
                {
                    doneAll = false;
                    Log.Debug("runTask,添加任务" + (i + 1));
                    FileInfo f = new FileInfo(filePath);
                    string path = Path.GetDirectoryName(filePath);
                    string tempName = f.Name.Remove(f.Name.LastIndexOf("."));
                    process = new Process();
                    process.StartInfo.FileName = "ffmpeg";
                    process.StartInfo.WorkingDirectory = path;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    string frameSize = lbxFile.Items[i].SubItems[2].Text;
                    outFile = path + "\\" + tempName + scaleName + ".mp4";
                    var size = getNewSize(frameSize);
                    this.lblLog.Visible = true;
                    string param = "-i \"" + filePath + "\" -vf  scale=" + size.width + ":" + size.height + " \"" + outFile + "\" -y";
                    if (rbtnGpu.Checked)
                    {
                        param = "-i \"" + filePath + "\" -vf  scale=" + size.width + ":" + size.height + " -c:v h264_nvenc -gpu 0 \"" + outFile + "\" -y";
                    }
                    Log.Debug(param);
                    process.StartInfo.Arguments = param;
                    process.StartInfo.CreateNoWindow = true;//显示命令行窗口
                                                            //不使用操作系统使用的shell启动进程
                    process.StartInfo.UseShellExecute = false;
                    //将输出信息重定向
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.EnableRaisingEvents = true;

                    process.Exited += new EventHandler(scale_Exited);
                    process.OutputDataReceived += new DataReceivedEventHandler(scale_OutputDataReceived);
                    process.ErrorDataReceived += new DataReceivedEventHandler(scale_ErrorDataReceived);
                    process.Start();
                    //开始异步读取输出
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    processing = true;
                    this.btnDoit.Enabled = false;
                    this.btnStop.Enabled = true;
                    this.btnOpen.Enabled = false;
                    break;
                }
            }
            if (doneAll)
            {
                taskTimer.Change(Timeout.Infinite, 1000);
            }
        }
        private void btnDoit_Click1(object sender, EventArgs e)
        {
            //Task.Run(()=>doTask());
            Task.Run(() =>
            {
                spendTimeTotal = new TimeSpan(DateTime.Now.Ticks);

                if (!checkScaleButton())
                {
                    MessageBox.Show("请选择缩放比例");
                    return;
                }
                int count = lbxFile.Items.Count;
                if (count == 0)
                {
                    MessageBox.Show("请添加需要处理的文件");
                    return;
                }
                for (int i = 0; i < count; i++)
                {
                    index = i;
                    spendTime = new TimeSpan(DateTime.Now.Ticks);
                    string filePath = lbxFile.Items[i].SubItems[0].Text;
                    string percentColum = lbxFile.Items[i].SubItems[percentColumIndex].Text;
                    FileInfo f = new FileInfo(filePath);
                    string path = Path.GetDirectoryName(filePath);
                    string tempName = f.Name.Remove(f.Name.LastIndexOf("."));
                    process = new Process();
                    process.StartInfo.FileName = "ffmpeg";
                    process.StartInfo.WorkingDirectory = path;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    string frameSize = lbxFile.Items[i].SubItems[2].Text;
                    outFile = path + "\\" + tempName + scaleName + ".mp4";
                    var size = getNewSize(frameSize);
                    this.lblLog.Visible = true;
                    string param = "-i \"" + filePath + "\" -vf  scale=" + size.width + ":" + size.height + " \"" + outFile + "\" -y";
                    if (rbtnGpu.Checked)
                    {
                        param = "-i \"" + filePath + "\" -vf  scale=" + size.width + ":" + size.height + " -c:v h264_nvenc -gpu 0 \"" + outFile +"\" -y";
                    }
                    Log.Debug(param);
                    process.StartInfo.Arguments = param;
                    process.StartInfo.CreateNoWindow = true;//显示命令行窗口
                                                            //不使用操作系统使用的shell启动进程
                    process.StartInfo.UseShellExecute = false;
                    //将输出信息重定向
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.EnableRaisingEvents = true;

                    process.Exited += new EventHandler(scale_Exited);
                    process.OutputDataReceived += new DataReceivedEventHandler(scale_OutputDataReceived);
                    process.ErrorDataReceived += new DataReceivedEventHandler(scale_ErrorDataReceived);

                    process.Start();
                    //开始异步读取输出
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    processing = true;
                    this.btnDoit.Enabled = false;
                    this.btnStop.Enabled = true;
                    this.btnOpen.Enabled = false;
                }
            });
        }

        void scale_OutputDataReceived(Object sender, DataReceivedEventArgs e)
        {
            //这里是正常的输出
            //执行过程中停止了
            //Log.Information("out " + e.Data);
            int count = lbxFile.Items.Count;
            for (int i = 0; i < count; i++)
            {
                spendTime = new TimeSpan(DateTime.Now.Ticks);
                string filePath = lbxFile.Items[i].SubItems[0].Text;
                if (filePath.Equals(outFile.Replace(scaleName, "")) && !lbxFile.Items[i].SubItems[percentColumIndex].Text.Contains("100")){
                    FileInfo f = new FileInfo(outFile);
                    f.Delete();
                }
            }
        }


        void scale_ErrorDataReceived(Object sender, DataReceivedEventArgs e)
        {
            //这里得到的是错误信息
            string info = e.Data;
            if (info != null && info.Contains("frame") && info.Contains("fps") && info.Contains("size") && info.Contains("time") && info.Contains("bitrate") && info.Contains("speed"))
            {
                lblLog.Text = info;
                //string s = "frame= 2700 fps=178 q=26.0 size=   15616kB time=00:01:31.90 bitrate=1391.9kbits/s speed=6.07x";
                string r = "^frame.+time=(.+)bitrate=.+";
                Match match = Regex.Match(info, r);
                if (match.Success)
                {
                    string time = match.Groups[1].Value;
                    if (time.Contains("."))
                    {
                        time = time.Split(".")[0];
                    }
                    string[] times = time.Split(":");
                    int cur = int.Parse(times[0]) * 60 * 60 + int.Parse(times[1]) * 60 + int.Parse(times[2]);
                    TimeSpan timeSpan = TimeSpan.Parse(lbxFile.Items[index].SubItems[1].Text);
                    int duration = (int)timeSpan.TotalSeconds;
                    double rx = Math.Round((Convert.ToDouble(cur) / Convert.ToDouble(duration)), 2) * 100;
                    int percent = Convert.ToInt32(rx);

                    lbxFile.Items[index].SubItems[percentColumIndex].Text = Convert.ToString(percent);

                    TimeSpan end = new TimeSpan(DateTime.Now.Ticks);    //获取当前时间的刻度数
                    TimeSpan abs = end.Subtract(spendTime).Duration();
                    TimeSpan absTotal = end.Subtract(spendTimeTotal).Duration();
                    lbxFile.Items[index].SubItems[4].Text = Convert.ToString((int)abs.TotalSeconds);
                    this.lblTimeTotal.Text  = "总用时 " + ((int)absTotal.TotalSeconds) + " 秒";
                }
            }
        }

        void scale_Exited(Object sender, EventArgs e)
        {
            if (processing)
            {
                TimeSpan end = new TimeSpan(DateTime.Now.Ticks);    //获取当前时间的刻度数
                TimeSpan abs = end.Subtract(spendTime).Duration();      //时间差的绝对值

                processing = false;
                this.btnDoit.Enabled = true;
                this.btnStop.Enabled = false;
                this.btnOpen.Enabled = true;

                doneCount++;
                this.lblDoneCount.Text = "已完成 " + doneCount;
                lblLog.Text = "";
                this.lblLog.Visible = false;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            taskTimer.Change(Timeout.Infinite, 1000);
            process.Kill();
            this.btnDoit.Enabled = true;
            this.btnStop.Enabled = false;
            this.btnOpen.Enabled = true;
            lblLog.Text = "";
            this.lblLog.Visible = false;
            /* if (processing)
             {
                 process.Kill();
                 //processing = false;
                 this.btnDoit.Enabled = true;
                 this.btnStop.Enabled = false;
                 this.btnOpen.Enabled = true;
                 lblLog.Text = "";

                 this.lblLog.Visible = false;
             }*/
        }

        private void btnDeleteDone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lbxFile.Items)
            {
                if (item.SubItems[percentColumIndex].Text.Contains("100"))
                {
                    if (doneCount > 0)
                    {
                        doneCount--;
                    }
                    lbxFile.Items.Remove(item);
                }
                this.lblDoneCount.Text = "已完成 " + doneCount;
            }
        }

        private void btnDeleteSelect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lbxFile.SelectedItems)
            {
                if (item.SubItems[percentColumIndex].Text.Contains("100"))
                {
                    if (doneCount > 0)
                    {
                        doneCount--;
                    }
                    this.lblDoneCount.Text = "已完成 " + doneCount;
                    lbxFile.Items.Remove(item);
                }
                else
                {
                    if (taskCount > 0)
                    {
                        taskCount--;
                    }
                    this.lblTaskCount.Text = "总任务 " + taskCount;
                }
                lbxFile.Items.Remove(item);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lbxFile.Items)
            {
                lbxFile.Items.Remove(item);
            }
            taskCount = 0;
            doneCount = 0;
            this.lblTaskCount.Text = "";
            this.lblDoneCount.Text = "";
        }
    }
}
