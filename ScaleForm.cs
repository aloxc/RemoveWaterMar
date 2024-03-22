using FFmpeg.NET;
using Serilog;
using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using static System.Windows.Forms.ListView;

namespace RemoveWaterMar
{
    public partial class ScaleForm : Form
    {
        Engine ffmpeg = new Engine(@"ffmpeg.exe");
        CancellationToken token = new CancellationToken();
        private Process process;
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
        private List<VideoInfo> videoList = new List<VideoInfo>();
        private Barrier barrier;
        private Random random = new Random();
        private readonly static string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly static string imageFileStart = "Snipaste";

        public ScaleForm()
        {
            InitializeComponent();
            tip10bit.SetToolTip(cbx10bit, "当压缩去水印退出，可选中试试");
            Application.EnableVisualStyles();
            this.DoubleBuffered = true;
            string rootPath = Directory.GetCurrentDirectory();
            DirectoryInfo root = new DirectoryInfo(rootPath);
            Control.CheckForIllegalCrossThreadCalls = false;
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("./log.txt", rollingInterval: RollingInterval.Month).CreateLogger();
        }

        private (int min, int max) getScaleSize()
        {
            if (rbt11.Checked)
            {
                return (1, 1);
            }
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
            videoList.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string filter = "视频文件(";
            foreach(string allow in Util.allowFiles)
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
            openFileDialog.Multiselect = true;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                string[] files = openFileDialog.FileNames;
                setListViewData(files);
            }
            else
            {
                return;
            }
        }
        private void setListViewData(string[] files)
        {
            this.lblTaskCount.Text = "正在解析 " + files.Length + "个视频";
            barrier = new Barrier(files.Length, a =>
            {
                Log.Information("回调一次,新添加任务数量" + videoList.Count);
                int no = 0;
                List<ListViewItem> listItems = new List<ListViewItem>();
                
                foreach (VideoInfo v in videoList)
                {
                    //Log.Information("视频：" + v.path + "  " + v.isErrorVideo());
                    if (v.isErrorVideo())
                    {
                        MessageBox.Show(v.path, "无法解析的视频", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }
                    ListViewItem item = new ListViewItem(v.toListItem());
                    listItems.Add(item);
                    item.UseItemStyleForSubItems = false;
                    if (no % 2 == 0)
                    {
                        item.SubItems[0].BackColor = Color.YellowGreen;
                        item.SubItems[1].BackColor = Color.YellowGreen;
                        item.SubItems[2].BackColor = Color.YellowGreen;
                        item.SubItems[3].BackColor = Color.YellowGreen;
                        item.SubItems[4].BackColor = Color.YellowGreen;
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
                    no++;
                }
                this.lbxFile.Items.AddRange(listItems.ToArray());
            });
            for (int i = 0; i < files.Length; i++)
            {
                string f = files[i];
                int taskNo = i;
                ThreadPool.QueueUserWorkItem(new WaitCallback(state =>
                {
                    string f = files[taskNo];
                    var inputFile = new InputFile(f);
                    GetVideoInfo(inputFile);
                }));
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
            videoList.Clear();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 0)
            {
                MessageBox.Show("至少拖放一个视频文件");
                return;
            }
            string filter = "";
            string tips = "只能视频文件( ";
            foreach (string allow in Util.allowFiles)
            {
                filter += "*." + allow + ",";
                tips += "." + allow + " ";
            }
            tips += ")";
          
            foreach (string f in files)
            {
                string[] infos = f.Split(".");
                string extension = infos[infos.Length - 1];
                if (!filter.Contains(extension + ","))
                {
                    MessageBox.Show(tips);
                    return;
                }
            }
            setListViewData(files);
        }

        public async void GetVideoInfo(InputFile inputFile)
        {
            MetaData data = await ffmpeg.GetMetaDataAsync(inputFile, token);
            if(data == null)
            {
                Log.Information("视频有问题" + inputFile.Name);
                videoList.Add(new VideoInfo(inputFile.Name,null,0,null,-1));
                barrier.SignalAndWait();
                return;
            }
            int seconds = (int)data.Duration.TotalSeconds;
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            string timeString = time.ToString(@"hh\:mm\:ss");

            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            StringBuilder sb = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                int index = random.Next(chars.Length);
                sb.Append(chars[index]);
            }

            string currentImageFile = "./" + imageFileStart + Convert.ToInt64(ts.TotalMilliseconds).ToString() + "_" + sb.ToString() + ".jpg";

            var outputFile = new OutputFile(currentImageFile);
            int durationMilliseconds = (int)data.Duration.TotalMilliseconds;
            ConversionOptions conversionOptions = new ConversionOptions()
            {
                Seek = TimeSpan.FromMilliseconds(random.Next(1, durationMilliseconds - 1))
            };

            await ffmpeg.GetThumbnailAsync(inputFile, outputFile, conversionOptions, token);
            Image image = Image.FromFile(currentImageFile);
            string frameSize = image.Width + "x" + image.Height;

            videoList.Add(new VideoInfo(inputFile.Name, frameSize, 0, timeString, 0));
            Log.Debug("已解析：" + inputFile.Name + ";" + frameSize);
            barrier.SignalAndWait();
        }

        private void ScaleForm_Load(object sender, EventArgs e)
        {
            lbxFile.HeaderStyle = ColumnHeaderStyle.Clickable;
            lbxFile.View = View.Details;

            this.lbxFile.Columns.Add("文件", 380, HorizontalAlignment.Left);
            this.lbxFile.Columns.Add("时长", 100, HorizontalAlignment.Left);
            this.lbxFile.Columns.Add("分辨率", 150, HorizontalAlignment.Left);
            this.lbxFile.Columns.Add("进度", 100, HorizontalAlignment.Left);
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
            return (rbt11.Checked || rbt12.Checked || rbt13.Checked 
                || rbt14.Checked || rbt23.Checked || rbt34.Checked);
        }
        private void btnDoit_Click(object sender, EventArgs e)
        {
            spendTimeTotal = new TimeSpan(DateTime.Now.Ticks);

            if (!checkScaleButton())
            {
                MessageBox.Show("请选择缩放比例");
                return;
            }
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
                if (!percentColum.Contains("100"))
                {
                    doneAll = false;
                }
            }
            if (doneAll)
            {
                return;
            }
            spendTime = new TimeSpan(DateTime.Now.Ticks);
            taskTimer.Change(0,1000);
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
                        if (cbx10bit.Checked)
                        {
                            param = "-i \"" + filePath + "\" -vf  scale=" + size.width + ":" + size.height + " -c:v h264_nvenc -gpu 0 -pix_fmt yuv420p \"" + outFile + "\" -y";
                        }
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
                    double rx = (Convert.ToDouble(cur) / Convert.ToDouble(duration)) * 100;

                    string percent = string.Format("{0:F1}", rx) ;
                    Log.Debug(percent);
                    lbxFile.Items[index].SubItems[percentColumIndex].Text = percent;

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
            TimeSpan end = new TimeSpan(DateTime.Now.Ticks);    //获取当前时间的刻度数
            TimeSpan abs = end.Subtract(spendTime).Duration();      //时间差的绝对值

            this.btnDoit.Enabled = true;
            this.btnStop.Enabled = false;
            this.btnOpen.Enabled = true;

            doneCount++;
            this.lblDoneCount.Text = "已完成 " + doneCount;
            lblLog.Text = "";
            this.lblLog.Visible = false;
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
