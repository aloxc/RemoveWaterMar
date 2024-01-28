using FFmpeg.NET;
using RemoveWaterMar.Properties;
using Serilog;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RemoveWaterMar
{
    public partial class WaterMark : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        //视频分辨率的宽度
        private int picWidth = 0;
        //视频分辨率的高度
        private int picHeight = 0;
        private readonly static string imageFileStart = "Snipaste";
        private readonly static string logFileStart = "log";
        private string currentImageFile = "snip.jpg";
        //去水印后视频保存文件的后缀
        private readonly static string guding = "__WaterMarout";
        //调整分辨率后视频保存文件的后缀
        private readonly static string scale = "__Scale";

        //需要做水印处理的x
        private int x;
        //需要做水印处理的y
        private int y;
        //需要做水印处理的width
        private int width;
        //需要做水印处理的height
        private int height;
        private static int lastPercent = 0;
        private static int lastX = -1;
        private Color maskColor = Color.FromArgb(100, Color.Green);
        private int perMaskWidth = 0;

        Point startPoint;  //起始点
        Point endPoint;   //结束点
        bool blnDraw;
        bool read = false;
        bool draw = false;
        private string filePath = null;
        private string fileName = null;
        private int duration = 0;
        private int durationMilliseconds = 0;
        private TimeSpan spendTime;
        private Process process;
        private bool processing = false;
        private bool notifyStatus = false;
        private string prevfile;
        private Graphics graphics;
        private Icon blueIcon = Resources.icon;
        private Icon redIcon = Resources.icon2;
        public WaterMark()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            this.DoubleBuffered = true;
            string rootPath = Directory.GetCurrentDirectory();
            DirectoryInfo root = new DirectoryInfo(rootPath);
            FileInfo[] files = root.GetFiles();
            foreach (FileInfo f in files)
            {
                string fName = f.Name;
                if (fName.StartsWith(imageFileStart) || fName.StartsWith(logFileStart))
                {
                    File.Delete("./" + fName);
                }
            }
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("./log.txt", rollingInterval: RollingInterval.Month).CreateLogger();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "视频文件(*.mp4,*.mkv)|*.mp4;*.mkv";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                FileInfo fileInfo = new FileInfo(filePath);
                fileName = fileInfo.Name;
                this.Text = "淡化视频水印       " + this.fileName;
            }
            else
            {
                return;
            }
            openVideoHander();
        }
        private void openVideoHander()
        {
            resetValue();
            var inputFile = new InputFile(filePath);
            Engine ffmpeg = new Engine(@"ffmpeg.exe");

            // 保存位于视频第 15 秒的帧。
            CancellationToken token = new CancellationToken();
            GetVideoInfo(ffmpeg, inputFile, token);
            read = true;
        }
        private void resetValue()
        {
            lastPercent = 0;
            lastX = -1;
        }
        public async void GetVideoInfo(Engine ffmpeg, InputFile inputFile, CancellationToken token)
        {
            //MetaData data = await ffmpeg.GetMetaDataAsync(inputFile, token).GetAwaiter().GetResult();
            MetaData data = await ffmpeg.GetMetaDataAsync(inputFile, token);
            duration = (int)data.Duration.TotalSeconds;
            durationMilliseconds = (int)data.Duration.TotalMilliseconds;
            string frameSize = data.VideoData.FrameSize;
            string[] frameInfo = frameSize.Split("x");
            picWidth = Int32.Parse(frameInfo[0]);
            picHeight = Int32.Parse(frameInfo[1]);
            this.tbxHeight.Text = Convert.ToString(picHeight);
            this.tbxWidth.Text = Convert.ToString(picWidth);
            this.Text = "淡化视频水印       " + this.fileName + " 总时长：" + duration + "秒,分辨率" + this.picWidth + " * " + this.picHeight;
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            currentImageFile = "./" + imageFileStart + Convert.ToInt64(ts.TotalMilliseconds).ToString() + ".jpg";
            var outputFile = new OutputFile(currentImageFile);
            Random random = new Random();
            ConversionOptions conversionOptions = new ConversionOptions()
            {
                Seek = TimeSpan.FromMilliseconds(random.Next(1, durationMilliseconds - 1))
            };
            await ffmpeg.GetThumbnailAsync(inputFile, outputFile, conversionOptions, token);
            this.picBox.Load(currentImageFile);
            pic = new Bitmap(this.picBox.Image, this.picBox.Width, this.picBox.Height);
            picGraphics = Graphics.FromImage(this.picBox.Image);
            perMaskWidth = pic.Width / 100;

        }
        private Graphics picGraphics;
        private Bitmap pic = null;
        private void btnResetThumbnail_Click(object sender, EventArgs e)
        {
            ResetThumbnail_Click();
        }
        private async void ResetThumbnail_Click()
        {
            CancellationToken token = new CancellationToken();
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            currentImageFile = "./" + imageFileStart + Convert.ToInt64(ts.TotalMilliseconds).ToString() + ".jpg";
            var outputFile = new OutputFile(currentImageFile);
            Random random = new Random();
            ConversionOptions conversionOptions = new ConversionOptions()
            {
                Seek = TimeSpan.FromMilliseconds(random.Next(1, durationMilliseconds - 1))
            };
            Engine ffmpeg = new Engine(@"ffmpeg.exe");
            var inputFile = new InputFile(filePath);
            await ffmpeg.GetThumbnailAsync(inputFile, outputFile, conversionOptions, token);
            this.picBox.Load(currentImageFile);
            pic = new Bitmap(this.picBox.Image, this.picBox.Width, this.picBox.Height);
            picGraphics = Graphics.FromImage(this.picBox.Image);
            perMaskWidth = pic.Width / 100;
        }

        private void picBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (blnDraw)
            {
                if (e.Button != MouseButtons.Left) return;
                endPoint = e.Location;
                picBox.Invalidate();//此代码不可省略
            }
        }

        private void picBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
                blnDraw = true;
            }
        }

        private void picBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                endPoint = e.Location;
                blnDraw = false;
            }
        }


        private void btnPrevPic_Click(object sender, EventArgs e)
        {
            if (read == false) return;
            if (draw == false)
            {
                return;
            }

            Process preProcess = new Process();
            preProcess.StartInfo.FileName = "ffmpeg";
            preProcess.StartInfo.WorkingDirectory = "./";
            preProcess.StartInfo.CreateNoWindow = false;//显示命令行窗口
            preProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            prevfile = currentImageFile.Replace(".jpg", "__out__" + Convert.ToInt64(ts.TotalMilliseconds).ToString() + ".jpg");
            string param = "-i \"" + currentImageFile + "\" -filter_complex  \"delogo=x=" + x + ":y=" + y + ":w=" + width + ":h=" + height + ":show=0\" \"" + prevfile + "\" -y";

            preProcess.StartInfo.Arguments = param;
            preProcess.StartInfo.CreateNoWindow = true;
            //不使用操作系统使用的shell启动进程
            preProcess.StartInfo.UseShellExecute = false;
            //将输出信息重定向
            preProcess.StartInfo.RedirectStandardInput = true;
            preProcess.StartInfo.RedirectStandardOutput = true;
            preProcess.StartInfo.RedirectStandardError = true;
            preProcess.EnableRaisingEvents = true;

            preProcess.Exited += new EventHandler(prevPicExited);
            preProcess.Start();

            //开始异步读取输出
            preProcess.BeginOutputReadLine();
            preProcess.BeginErrorReadLine();

            Point mouse = MousePosition;
            picBoxPrev.Location = mouse;
            picBoxPrev.Size = new Size(picWidth, picHeight);
            picBoxPrev.SizeMode = PictureBoxSizeMode.AutoSize;

            picBoxPrev.TabIndex = 11;
            picBoxPrev.TabStop = false;
        }

        void prevPicExited(Object sender, EventArgs e)
        {
            picBoxPrev.Load(prevfile);
            picBoxPrev.Show();

        }

        private void picBox_Paint(object sender, PaintEventArgs e)
        {
            if (read == false) return;
            PictureBox pic = sender as PictureBox;
            Pen pen = new Pen(Color.Red, 3);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;     //绘制线的格式
            if (blnDraw)
            {
                x = Math.Min(startPoint.X, endPoint.X);
                y = Math.Min(startPoint.Y, endPoint.Y);
                width = Math.Abs(startPoint.X - endPoint.X);
                height = Math.Abs(startPoint.Y - endPoint.Y);
                e.Graphics.DrawRectangle(pen, x, y, width, height);
                draw = true;
                blnDraw = true;
            }
            pen.Dispose();
        }
        private void btnDoit_Click(object sender, EventArgs e)
        {
            if (read == false) return;
            if (draw == false)
            {
                return;
            }
            resetValue();
            string path = Path.GetDirectoryName(filePath);
            FileInfo f = new FileInfo(filePath);
            //MessageBox.Show(path);
            //MessageBox.Show(filePath);
            //MessageBox.Show(f.Name);
            spendTime = new TimeSpan(DateTime.Now.Ticks);

            string tempName = f.Name.Remove(f.Name.LastIndexOf("."));

            process = new Process();
            process.StartInfo.FileName = "ffmpeg";
            process.StartInfo.WorkingDirectory = path;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            string param = "-i \"" + filePath + "\" -vf  \"delogo=x=" + x + ":y=" + y + ":w=" + width + ":h=" + height + ":show=0\" \"" + path + "\\" + tempName + guding + ".mp4\" -y";
            if (rbtnGpu.Checked)
            {
                param = "-i \"" + filePath + "\" -vf  \"delogo=x=" + x + ":y=" + y + ":w=" + width + ":h=" + height + ":show=0\" -c:v h264_nvenc -gpu 0 \"" + path + "\\" + tempName + guding + ".mp4\" -y";
            }
            process.StartInfo.Arguments = param;
            process.StartInfo.CreateNoWindow = true;//显示命令行窗口
            //不使用操作系统使用的shell启动进程
            process.StartInfo.UseShellExecute = false;
            //将输出信息重定向
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.EnableRaisingEvents = true;

            process.Exited += new EventHandler(p_Exited);
            process.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            process.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);

            process.Start();
            //开始异步读取输出
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            processing = true;
            this.btnDoit.Enabled = false;
            this.btnSetScale.Enabled = false;
            this.btnResetThumbnail.Enabled = false;
            this.btnStop.Enabled = true;
        }

        private void removeWaterTimerCall(object sender, EventArgs e)
        {
            Log.Information("timer :" + notifyStatus);
            if (notifyStatus)
            {
                notifyIcon1.Icon = redIcon;
            }
            else
            {
                notifyIcon1.Icon = blueIcon;
            }
            notifyStatus = !notifyStatus;
        }

        void p_OutputDataReceived(Object sender, DataReceivedEventArgs e)
        {
            //这里是正常的输出
            //Log.Information("out " + e.Data);
        }


        void p_ErrorDataReceived(Object sender, DataReceivedEventArgs e)
        {
            //这里得到的是错误信息
            string info = e.Data;
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
                    double rx = Math.Round((Convert.ToDouble(cur) / Convert.ToDouble(duration)), 2) * 100;
                    int percent = Convert.ToInt32(rx);

                    if (lastPercent != percent)
                    {
                        int curLineWidth = (lastPercent + 1) * perMaskWidth;
                        int curLineCenter = (lastPercent + 1) * perMaskWidth;// + perAlphaWidth / 2;
                        Pen pen = new Pen(maskColor, curLineWidth);
                        //Log.Information("lastAlpha:" + lastPercent + ",percent:" + percent + ",curLineCenter:" + curLineCenter + ",picHeight:" + picHeight + ",lastPercent:" + lastPercent);
                        this.picBox.Load(currentImageFile);
                        Graphics.FromImage(this.picBox.Image).DrawLine(pen, new Point(curLineWidth / 2, 0), new Point(curLineWidth / 2, pic.Height));
                        lastPercent = percent;
                        this.picBox.Refresh();
                    }
                    TimeSpan end = new TimeSpan(DateTime.Now.Ticks);    //获取当前时间的刻度数
                    TimeSpan abs = end.Subtract(spendTime).Duration();      //时间差的绝对值
                    this.Text = "淡化视频水印        总时长：" + duration + "秒   当前时长：" + cur + "秒   进度：" + percent + "%   用时：" + ((int)abs.TotalSeconds) + "秒   " + this.fileName;
                }
            }
        }

        void p_Exited(Object sender, EventArgs e)
        {
            if (processing)
            {
                graphics.Dispose();
                FileInfo f = new FileInfo(filePath);
                TimeSpan end = new TimeSpan(DateTime.Now.Ticks);    //获取当前时间的刻度数
                TimeSpan abs = end.Subtract(spendTime).Duration();      //时间差的绝对值
                                                                        //removeWaterTimer.Enabled = false;

                processing = false;
                this.btnDoit.Enabled = true;
                this.btnSetScale.Enabled = true;
                this.btnResetThumbnail.Enabled = true;
                this.btnStop.Enabled = false;

                this.picBox.Load(currentImageFile);
                this.picBox.Refresh();
                this.Text = "淡化视频水印       " + this.fileName + " 总时长：" + duration + "秒,分辨率" + this.picWidth + " * " + this.picHeight;
                notifyIcon1.ShowBalloonTip(0, "水印处理完成,耗时" + ((int)abs.TotalSeconds) + " 秒", filePath, ToolTipIcon.Info);
                //MessageBox.Show("处理完成,用时 " + ((int)abs.TotalSeconds) + " 秒", f.Name);
            }
            this.Text = "淡化视频水印       " + this.fileName + " 总时长：" + duration + "秒,分辨率" + this.picWidth + " * " + this.picHeight;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (processing)
            {
                //removeWaterTimer.Enabled = false;
                process.Kill();
                processing = false;
                string path = Path.GetDirectoryName(filePath);
                FileInfo f = new FileInfo(filePath);
                spendTime = new TimeSpan(DateTime.Now.Ticks);
                this.btnDoit.Enabled = true;
                this.btnSetScale.Enabled = true;
                this.btnResetThumbnail.Enabled = true;
                this.btnStop.Enabled = false;
                this.Text = "淡化视频水印       " + this.fileName;
            }
        }

        private void btnPrev_Leave(object sender, EventArgs e)
        {
            this.picBoxPrev.Hide();
            this.picBoxPrev.Image = null;
        }

        private void btnOpenOldVideo_Click(object sender, EventArgs e)
        {
            if (filePath == null || filePath.Length == 0)
            {
                return;
            }
            Player player = new Player(filePath);
            player.ShowDialog();
        }

        private void btnOpenNewVideo_Click(object sender, EventArgs e)
        {
            FileInfo f = new FileInfo(filePath);
            string path = Path.GetDirectoryName(filePath);
            string tempName = f.Name.Remove(f.Name.LastIndexOf("."));
            string outPath = path + "\\" + tempName + guding + ".mp4";

            if (outPath == null || outPath.Length == 0 || !File.Exists(outPath))
            {
                return;
            }
            Player player = new Player(outPath);
            player.ShowDialog();
        }

        private void method_Click(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.ShowDialog();
        }

        private void btnPrevVideo_Click(object sender, EventArgs e)
        {
            Process ffplay = new Process();
            ffplay.StartInfo.FileName = "ffplay";
            ffplay.StartInfo.WorkingDirectory = "./";
            ffplay.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            string param = "-i \"" + filePath + "\" -window_title \"预览处理效果视频\" -vf  \"delogo=x=" + x + ":y=" + y + ":w=" + width + ":h=" + height + ":show=0\" ";
            ffplay.StartInfo.Arguments = param;
            ffplay.StartInfo.CreateNoWindow = true;
            ffplay.StartInfo.RedirectStandardOutput = true;
            ffplay.StartInfo.UseShellExecute = false;
            ffplay.EnableRaisingEvents = true;

            ffplay.OutputDataReceived += (o, e) => Debug.WriteLine(e.Data ?? "NULL", "ffplay");
            ffplay.ErrorDataReceived += (o, e) => Debug.WriteLine(e.Data ?? "NULL", "ffplay");
            ffplay.Exited += (o, e) => Debug.WriteLine("Exited", "ffplay");
            ffplay.Start();
            Thread.Sleep(500);
            SetParent(ffplay.MainWindowHandle, this.Handle);
            MoveWindow(ffplay.MainWindowHandle, 0, 0, width, height, true);
        }

        private void WaterMark_Paint(object sender, PaintEventArgs e)
        {
            graphics = e.Graphics;
        }

        private void test_Click(object sender, EventArgs e)
        {
            //removeWaterTimer.Enabled = true;
            //notifyIcon1.ShowBalloonTip(0, "消息标题-Error", "这是一个错误类型的消息内容", ToolTipIcon.Info);
            TestForm me = new TestForm();
            me.ShowDialog();
        }

        private void WaterMark_Load(object sender, EventArgs e)
        {
            //removeWaterTimer.Enabled = true;
        }

        private void notifyIcon1_BalloonTipClosed(object sender, EventArgs e)
        {
            removeWaterTimer.Enabled = false;
            notifyIcon1.Icon = redIcon;
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            removeWaterTimer.Enabled = false;
            notifyIcon1.Icon = redIcon;
        }

        private void WaterMark_DragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            //MessageBox.Show("drag");
        }

        private void WaterMark_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 1)
            {
                MessageBox.Show("只能拖放一个视频文件");
                return;
            }
            string f = files[0];
            if (!f.ToLower().EndsWith(".mp4") && !f.ToLower().EndsWith(".mkv"))
            {
                MessageBox.Show("只能视频文件(.mp4和.mkv)");
                return;
            }
            filePath = f;
            openVideoHander();


        }

        private void btnSetScale_Click(object sender, EventArgs e)
        {
            if (filePath == null)
            {
                ScaleForm scaleForm = new ScaleForm();
                scaleForm.ShowDialog();
                return;
            }
            if (this.tbxWidth.Text.Trim().Equals(""))
            {
                MessageBox.Show("新的宽度不能为空");
                return;
            }
            if (this.tbxHeight.Text.Trim().Equals(""))
            {
                MessageBox.Show("新的高度不能为空");
                return;
            }
            if (Convert.ToInt32(this.tbxWidth.Text) == this.picWidth && Convert.ToInt32(this.tbxHeight.Text) == this.picHeight)
            {
                MessageBox.Show("新的宽度和高度不能和原视频一样");
                return;
            }

            /*bool Debug = true;
            if(Debug == true)
            {
                return;
            }*/

            string path = Path.GetDirectoryName(filePath);
            FileInfo f = new FileInfo(filePath);
            //MessageBox.Show(f.Name);
            spendTime = new TimeSpan(DateTime.Now.Ticks);
            string tempName = f.Name.Remove(f.Name.LastIndexOf("."));
            process = new Process();
            process.StartInfo.FileName = "ffmpeg";
            process.StartInfo.WorkingDirectory = path;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            string param = "-i \"" + filePath + "\" -vf  scale=" + this.tbxWidth.Text + ":" + this.tbxHeight.Text + " \"" + path + "\\" + tempName + scale + ".mp4\" -y";
            if (rbtnGpu.Checked)
            {
                param = "-i \"" + filePath + "\" -vf  scale=" + this.tbxWidth.Text + ":" + this.tbxHeight.Text + " -c:v h264_nvenc -gpu 0 \"" + path + "\\" + tempName + scale + ".mp4\" -y";
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
            this.btnSetScale.Enabled = false;
            this.btnResetThumbnail.Enabled = false;
            this.btnStop.Enabled = true;
        }

        void scale_OutputDataReceived(Object sender, DataReceivedEventArgs e)
        {
            //这里是正常的输出
            //Log.Information("out " + e.Data);
        }


        void scale_ErrorDataReceived(Object sender, DataReceivedEventArgs e)
        {
            //这里得到的是错误信息
            string info = e.Data;
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
                    double rx = Math.Round((Convert.ToDouble(cur) / Convert.ToDouble(duration)), 2) * 100;
                    int percent = Convert.ToInt32(rx);

                    if (lastPercent != percent)
                    {
                        int curLineWidth = (lastPercent + 1) * perMaskWidth;
                        int curLineCenter = (lastPercent + 1) * perMaskWidth;// + perAlphaWidth / 2;
                        Pen pen = new Pen(maskColor, curLineWidth);
                        //Log.Information("lastAlpha:" + lastPercent + ",percent:" + percent + ",curLineCenter:" + curLineCenter + ",picHeight:" + picHeight + ",lastPercent:" + lastPercent);
                        this.picBox.Load(currentImageFile);
                        Graphics.FromImage(this.picBox.Image).DrawLine(pen, new Point(curLineWidth / 2, 0), new Point(curLineWidth / 2, pic.Height));
                        lastPercent = percent;
                        this.picBox.Refresh();
                    }
                    TimeSpan end = new TimeSpan(DateTime.Now.Ticks);    //获取当前时间的刻度数
                    TimeSpan abs = end.Subtract(spendTime).Duration();      //时间差的绝对值
                    this.Text = "分辨率调整        总时长：" + duration + "秒   当前时长：" + cur + "秒   进度：" + percent + "%   用时：" + ((int)abs.TotalSeconds) + "秒   " + this.fileName + "  " + time;
                }
            }
        }

        void scale_Exited(Object sender, EventArgs e)
        {
            if (processing)
            {
                graphics.Dispose();
                FileInfo f = new FileInfo(filePath);
                TimeSpan end = new TimeSpan(DateTime.Now.Ticks);    //获取当前时间的刻度数
                TimeSpan abs = end.Subtract(spendTime).Duration();      //时间差的绝对值
                                                                        //removeWaterTimer.Enabled = false;

                processing = false;
                this.btnDoit.Enabled = true;
                this.btnSetScale.Enabled = true;
                this.btnResetThumbnail.Enabled = true;
                this.btnStop.Enabled = false;

                this.picBox.Load(currentImageFile);
                this.picBox.Refresh();
                this.Text = "淡化视频水印       " + this.fileName + " 总时长：" + duration + "秒,分辨率" + this.picWidth + " * " + this.picHeight;
                notifyIcon1.ShowBalloonTip(0, "分辨率调整完成,耗时" + ((int)abs.TotalSeconds) + " 秒", filePath, ToolTipIcon.Info);
                //MessageBox.Show("处理完成,用时 " + ((int)abs.TotalSeconds) + " 秒", f.Name);
            }
            this.Text = "淡化视频水印       " + this.fileName + " 总时长：" + duration + "秒,分辨率" + this.picWidth + " * " + this.picHeight;
        }

        private void rbt12_Click(object sender, EventArgs e)
        {
            setScaleSize(1, 2);
        }

        private void rbt23_Click(object sender, EventArgs e)
        {
            setScaleSize(2, 3);
        }

        private void rbt34_Click(object sender, EventArgs e)
        {
            setScaleSize(3, 4);
        }

        private void rbt14_Click(object sender, EventArgs e)
        {
            setScaleSize(1, 4);
        }

        private void rbt13_Click(object sender, EventArgs e)
        {
            setScaleSize(1, 3);
        }
        private void setScaleSize(int min, int max)
        {
            if (filePath == null)
            {
                return;
            }
            this.tbxWidth.Text = Convert.ToString(this.picWidth * min / max);
            this.tbxHeight.Text = Convert.ToString(this.picHeight * min / max);
        }
    }
}