using FFmpeg.NET;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoveWaterMar
{
    public partial class SplitForm : Form
    {
        private bool processing = false;
        private Process process;
        private TimeSpan spendTime;
        private TimeSpan spendTimeTotal;
        //调整分辨率后视频保存文件的后缀
        private readonly static string SPLIT = "__Split";
        private System.Threading.Timer taskTimer = null;
        CancellationToken token = new CancellationToken();
        private string fff = null;
        private readonly int percentColumIndex = 3;
        private int index = 0;
        private string outFile = null;
        private int doneCount = 0;
        public SplitForm()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            this.DoubleBuffered = true;
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("./log.txt", rollingInterval: RollingInterval.Month).CreateLogger();
        }

        private void SplitForm_Load(object sender, EventArgs e)
        {
            lbxFile.HeaderStyle = ColumnHeaderStyle.Clickable;
            lbxFile.View = View.Details;

            this.lbxFile.Columns.Add("文件", 360, HorizontalAlignment.Left);
            this.lbxFile.Columns.Add("开始时间", 150, HorizontalAlignment.Left);
            this.lbxFile.Columns.Add("结束时间", 150, HorizontalAlignment.Left);
            this.lbxFile.Columns.Add("用时", 100, HorizontalAlignment.Left);
            this.btnStopBatch.Enabled = false;

            this.lbxFile.ProgressTextColor = Color.Red;
            this.lbxFile.ProgressColor = Color.YellowGreen;

            this.lblDoneCount.Text = "";
            this.lblTaskCount.Text = "";
            this.lblTimeTotal.Text = "";
            this.btnStopOne.Enabled = false;
            this.btnDoOne.Enabled = false;
            //this.lblLog.Visible = false;

        }

        private void btnOpenVideo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string filter = "视频文件(";
            foreach (string allow in Util.allowFiles)
            {
                filter += "*." + allow + ",";
            }
            filter = filter.Remove(filter.LastIndexOf(","));
            filter += ")|";
            foreach (string allow in Util.allowFiles)
            {
                filter += "*." + allow + ";";
            }
            filter = filter.Remove(filter.LastIndexOf(";"));
            openFileDialog.Filter = filter;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.lblFile.Text = openFileDialog.FileName;
                this.fff = openFileDialog.FileName;

            }
            else
            {
                return;
            }
            openOneVideoHander();
        }
        private void openOneVideoHander()
        {
            var inputFile = new InputFile(this.lblFile.Text);
            Engine ffmpeg = new Engine(@"ffmpeg.exe");

            // 保存位于视频第 15 秒的帧。
            CancellationToken token = new CancellationToken();
            GetOneVideoInfo(ffmpeg, inputFile, token);
        }
        public async void GetOneVideoInfo(Engine ffmpeg, InputFile inputFile, CancellationToken token)
        {
            //MetaData data = await ffmpeg.GetMetaDataAsync(inputFile, token).GetAwaiter().GetResult();
            MetaData data = await ffmpeg.GetMetaDataAsync(inputFile, token);
            if (data == null)
            {
                MessageBox.Show(inputFile.FileInfo.Name, "无法解析的视频", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int duration = (int)data.Duration.TotalSeconds;
            this.lblDurationTime.Text = "时长" + data.Duration.ToString(@"hh\:mm\:ss");
            this.btnDoOne.Enabled = true;
        }

        private void btnDoOne_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(this.lblFile.Text);
            FileInfo f = new FileInfo(fff);
            //MessageBox.Show(path);
            //MessageBox.Show(filePath);
            //MessageBox.Show(f.Name);
            spendTime = new TimeSpan(DateTime.Now.Ticks);

            string tempName = f.Name.Remove(f.Name.LastIndexOf("."));

            process = new Process();
            process.StartInfo.FileName = "ffmpeg";
            process.StartInfo.WorkingDirectory = path;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            //ffmpeg -ss 00:03:23 -accurate_seek -i 16__趣经女儿国.mp4 -c:v libx264  -avoid_negative_ts 1 -y output.mp4
            string param = "-ss " + tbxStart.Text;
            if (tbxEnd.Text.Trim().Length != 0)
            {
                param += " -to " + this.tbxEnd.Text;

            }
            param += " -accurate_seek -i \"" + this.lblFile.Text + "\"";
            if (cbxOneGpu.Checked)
            {
                param += " -c:v h264_nvenc -gpu 0 ";
            }
            else
            {
                param += " -c:v libx264 ";
            }
            param += " -avoid_negative_ts 1 -y " + path + "\\" + tempName + SPLIT + ".mp4";

            Log.Debug("ffmpeg " + param);
            process.StartInfo.Arguments = param;
            process.StartInfo.CreateNoWindow = true;//显示命令行窗口
            //不使用操作系统使用的shell启动进程
            process.StartInfo.UseShellExecute = false;
            //将输出信息重定向
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.EnableRaisingEvents = true;

            process.Exited += new EventHandler(p_one_Exited);
            process.OutputDataReceived += new DataReceivedEventHandler(p_one_OutputDataReceived);
            process.ErrorDataReceived += new DataReceivedEventHandler(p_one_ErrorDataReceived);

            process.Start();
            //开始异步读取输出
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            processing = true;
            this.btnDoOne.Enabled = false;
            this.btnStopOne.Enabled = true;
        }

        private void btnStopOne_Click(object sender, EventArgs e)
        {
            if (processing)
            {
                process.Kill();
                processing = false;
                this.lblLog.Text = "已停止";
                this.btnDoOne.Enabled = true;
                this.btnStopOne.Enabled = false;
            }
        }

        void p_one_OutputDataReceived(Object sender, DataReceivedEventArgs e)
        {
            //这里是正常的输出
            Log.Information("out " + e.Data);
        }


        void p_one_ErrorDataReceived(Object sender, DataReceivedEventArgs e)
        {
            //这里得到的是错误信息
            string info = e.Data;
            Log.Information("Error " + info);
            if (info != null && info.Contains("frame") && info.Contains("fps") && info.Contains("size") && info.Contains("time") && info.Contains("bitrate") && info.Contains("speed"))
            {
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

                    TimeSpan end = new TimeSpan(DateTime.Now.Ticks);    //获取当前时间的刻度数
                    TimeSpan abs = end.Subtract(spendTime).Duration();      //时间差的绝对值
                    this.lblTimeTotalOne.Text = "用时：" + ((int)abs.TotalSeconds) + "秒";
                    this.lblLog.Text = info;
                }
            }
        }

        void p_one_Exited(Object sender, EventArgs e)
        {
            if (processing)
            {
                processing = false;
                this.btnDoOne.Enabled = true;
                this.btnStopOne.Enabled = false;
                this.lblLog.Text = "已完成";
            }
        }

        private void btnBatchOpenConfig_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string filter = "文本文件(";
            filter += "*.txt";
            filter += ")|*.txt";
            openFileDialog.Filter = filter;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            string pattern = @"^(\d{2}):(\d{2}):(\d{2})$"; // 正则表达式，匹配HH:mm:ss格式
            List<SplitFile> splitFiles = new List<SplitFile>();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string content = null;
                    //Pass the file path and file name to the StreamReader constructor
                    StreamReader sr = new StreamReader(openFileDialog.FileName);
                    //Read the first line of text
                    string line = sr.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        content += line;
                        string[] infos = line.Split('\t');
                        if (infos.Length != 2 && infos.Length != 3)
                        {
                            MessageBox.Show(infos[0], "配置文件格式错误,每行一个任务，使用制表符分割\n第一列是文件路径\n第二列是开始时间00:00:00\n第三列是结束时间(可选)00:00:00\n\n");
                            splitFiles.Clear();
                            return;
                        }
                        //Environment.CurrentDirectory = Path.GetDirectoryName(infos[0]) + "\\";

                        FileInfo fileInfo = new FileInfo(@infos[0]);
                        bool isVideo = false;
                        foreach (string ext in Util.allowFiles)
                        {
                            if (infos[0].EndsWith(ext))
                            {
                                isVideo = true;
                            }
                        }
                        if (!isVideo)
                        {
                            MessageBox.Show(infos[0], "不是视频文件(*.mkv,*.mp4,*.mov)");
                            splitFiles.Clear();
                            return;
                        }

                        if (!fileInfo.Exists)
                        {
                            MessageBox.Show(infos[0], "视频文件不存在");
                            splitFiles.Clear();
                            return;
                        }
                        string startT = infos[1];

                        bool isMatch = Regex.IsMatch(startT, pattern);
                        if (!isMatch)
                        {
                            MessageBox.Show(infos[0] + "\n" + startT + "\n正确格式00:01:23", "开始时间格式不正确");
                            splitFiles.Clear();
                            return;
                        }
                        SplitFile file = new SplitFile(infos[0], infos[1]);
                        if (infos.Length == 3)
                        {
                            startT = infos[2];

                            isMatch = Regex.IsMatch(startT, pattern);
                            if (!isMatch)
                            {
                                MessageBox.Show(infos[0] + "\n" + startT + "\n正确格式00:01:23", "结束时间格式不正确");
                                splitFiles.Clear();
                                return;
                            }
                            file.end = infos[2];
                        }
                        splitFiles.Add(file);
                        line = sr.ReadLine();
                    }
                    //close the file
                    sr.Close();
                    Console.ReadLine();
                }
                catch (Exception eere)
                {
                    MessageBox.Show(eere.Message, "读取配置出错（可能是文件编码不是utf8）");
                }
                finally
                {
                }

            }
            else
            {
                return;
            }
            if (splitFiles.Count > 0)
            {
                for (int i = 0; i < splitFiles.Count; i++)
                {
                    SplitFile sf = splitFiles[i];
                    List<ListViewItem> listItems = new List<ListViewItem>();

                    ListViewItem item = new ListViewItem(sf.toListItem());
                    listItems.Add(item);
                    item.UseItemStyleForSubItems = false;
                    if (i % 2 == 0)
                    {
                        item.SubItems[0].BackColor = Color.YellowGreen;
                        item.SubItems[1].BackColor = Color.YellowGreen;
                        item.SubItems[2].BackColor = Color.YellowGreen;
                        item.SubItems[3].BackColor = Color.YellowGreen;
                    }
                    else
                    {
                        item.SubItems[0].BackColor = Color.MistyRose;
                        item.SubItems[1].BackColor = Color.MistyRose;
                        item.SubItems[2].BackColor = Color.MistyRose;
                        item.SubItems[3].BackColor = Color.MistyRose;
                    }

                    this.lbxFile.Items.AddRange(listItems.ToArray());

                }

                this.lblTaskCount.Text = "总任务 " + splitFiles.Count;
                this.lblDoneCount.Text = "已完成 " + 0;
            }
        }

        private void btnBatchDoIt_Click(object sender, EventArgs e)
        {
            spendTimeTotal = new TimeSpan(DateTime.Now.Ticks);

            taskTimer = new System.Threading.Timer(new TimerCallback(checkAndRunTask));




            int count = lbxFile.Items.Count;
            if (count == 0)
            {
                Log.Debug("click,列表中没有任务");
                return;
            }
            bool doneAll = true;

            for (int i = 0; i < count; i++)
            {
                string percentColum = lbxFile.Items[i].SubItems[percentColumIndex].Text;
                if (!percentColum.Contains("完成"))
                {
                    doneAll = false;
                }
            }
            if (doneAll)
            {
                return;
            }
            spendTime = new TimeSpan(DateTime.Now.Ticks);
            taskTimer.Change(0, 1000);
        }
        private void checkAndRunTask(object v)
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
                string filePath = lbxFile.Items[i].SubItems[0].Text;
                string sStart = lbxFile.Items[i].SubItems[1].Text;
                string sEnd = lbxFile.Items[i].SubItems[2].Text;
                string percentColum = lbxFile.Items[i].SubItems[percentColumIndex].Text;
                if (!percentColum.Contains("完成"))
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
                    outFile = path + "\\" + tempName + SPLIT + ".mp4";
                    string param = "-ss " + sStart;
                    if (sEnd.Trim().Length != 0)
                    {
                        param += " -to " + sEnd.Trim();

                    }
                    param += " -accurate_seek -i \"" + filePath + "\"";
                    if (cbxBatchGpu.Checked)
                    {
                        param += " -c:v h264_nvenc -gpu 0 ";
                    }
                    else
                    {
                        param += " -c:v libx264 ";
                    }
                    param += " -avoid_negative_ts 1 -y " + outFile;

                    Log.Debug("ffmpeg " + param);
                    process.StartInfo.Arguments = param;
                    process.StartInfo.CreateNoWindow = true;//显示命令行窗口
                                                            //不使用操作系统使用的shell启动进程
                    process.StartInfo.UseShellExecute = false;
                    //将输出信息重定向
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.EnableRaisingEvents = true;

                    process.Exited += new EventHandler(batch_Exited);
                    process.OutputDataReceived += new DataReceivedEventHandler(batch_OutputDataReceived);
                    process.ErrorDataReceived += new DataReceivedEventHandler(batch_ErrorDataReceived);
                    process.Start();
                    //开始异步读取输出
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    this.btnBatchDoIt.Enabled = false;
                    this.btnStopBatch.Enabled = true;
                    this.btnBatchOpenConfig.Enabled = false;
                    break;
                }
            }
            if (doneAll)
            {
                taskTimer.Change(Timeout.Infinite, 1000);
            }
        }

        private void btnStopBatch_Click(object sender, EventArgs e)
        {
            taskTimer.Change(Timeout.Infinite, 1000);
            process.Kill();
            this.btnBatchDoIt.Enabled = true;
            this.btnStopBatch.Enabled = false;
            this.btnBatchOpenConfig.Enabled = true;
            lblLog.Text = "";
        }

        void batch_OutputDataReceived(Object sender, DataReceivedEventArgs e)
        {
            //这里是正常的输出
            //执行过程中停止了
            Log.Information("out " + e.Data);
            int count = lbxFile.Items.Count;
            spendTime = new TimeSpan(DateTime.Now.Ticks);

        }


        void batch_ErrorDataReceived(Object sender, DataReceivedEventArgs e)
        {
            //这里得到的是错误信息
            string info = e.Data;
            Log.Information("error " + e.Data);
            if (info != null && info.Contains("frame") && info.Contains("fps") && info.Contains("size") && info.Contains("time") && info.Contains("bitrate") && info.Contains("speed"))
            {
                lblLog.Text = info;
                //string s = "frame= 2700 fps=178 q=26.0 size=   15616kB time=00:01:31.90 bitrate=1391.9kbits/s speed=6.07x";
                string r = "^frame.+time=(.+)bitrate=.+";
                Match match = Regex.Match(info, r);
                if (match.Success)
                {
                    TimeSpan end = new TimeSpan(DateTime.Now.Ticks);    //获取当前时间的刻度数
                    TimeSpan abs = end.Subtract(spendTime).Duration();
                    TimeSpan absTotal = end.Subtract(spendTimeTotal).Duration();
                    lbxFile.Items[index].SubItems[3].Text = Convert.ToString((int)abs.TotalSeconds);
                    this.lblTimeTotal.Text = "总用时 " + ((int)absTotal.TotalSeconds) + " 秒";
                }
            }
        }

        void batch_Exited(Object sender, EventArgs e)
        {
            Log.Information("exit ");
            TimeSpan end = new TimeSpan(DateTime.Now.Ticks);    //获取当前时间的刻度数
            TimeSpan abs = end.Subtract(spendTime).Duration();      //时间差的绝对值
            lbxFile.Items[index].SubItems[3].Text = "(完成)";

            this.btnBatchDoIt.Enabled = true;
            this.btnStopBatch.Enabled = false;
            this.btnBatchOpenConfig.Enabled = true;

            doneCount++;
            this.lblDoneCount.Text = "已完成 " + doneCount;
            lblLog.Text = "";
        }

        private void btnCleanDone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lbxFile.Items)
            {
                if (item.SubItems[percentColumIndex].Text.Contains("完成"))
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

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lbxFile.Items)
            {
                lbxFile.Items.Remove(item);
            }
            doneCount = 0;
            this.lblTaskCount.Text = "";
            this.lblDoneCount.Text = "";
        }

        private void btnMakeList_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            // 设置对话框的属性
            saveFileDialog1.InitialDirectory = "D:\\";  // 设置默认文件夹
            saveFileDialog1.Filter = "文本文件 (*.txt)|*.txt|所有文件 (*.*)|*.*";  // 设置文件类型过滤器

            // 显示保存文件对话框
            DialogResult result = saveFileDialog1.ShowDialog();

            // 处理用户的文件保存
            if (result == DialogResult.OK)
            {
                // 获取用户所选文件的路径
                string selectedFilePath = saveFileDialog1.FileName;
                string p = Path.GetDirectoryName(selectedFilePath);    
                DirectoryInfo inx = new DirectoryInfo(p);
                string exts = "";
                foreach (string ext in Util.allowFiles)
                {
                    exts += "." + ext + ";";

                }
                foreach (FileInfo file in inx.GetFiles())
                {
                    if(exts.IndexOf(file.Extension + ";") > -1)
                    {
                        System.IO.File.AppendAllText(selectedFilePath, file.FullName + "\n");
                    }

                }
            }
        }
    }
    class SplitFile
    {
        public string path { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public SplitFile(string path, string start, string end)
        {
            this.path = path;
            this.start = start;
            this.end = end;
        }
        public SplitFile(string path, string start)
        {
            this.path = path;
            this.start = start;
        }
        public string[] toListItem()
        {
            string[] arr = new string[] { path, start,end == null ? "" : end,null };
            return arr;
        }
    }
}
