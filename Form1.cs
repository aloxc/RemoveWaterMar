using FFmpeg.NET;
using Serilog;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
namespace RemoveWaterMar
{
    public partial class WaterMark : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        private int picWidth = 0;
        private int picHeight = 0;
        private static string imageFileStart = "Snipaste";
        private static string logFileStart = "log";
        private string currentImageFile = "snip.jpg";
        private static string guding = "_out";

        private int x;
        private int y;
        private int width;
        private int height;
        private static int lastPercent = 0;
        private static int lastX = 0;
        private Color maskFromColor = Color.FromArgb(100, Color.Red);
        private Color maskToColor = Color.FromArgb(0, Color.Green);


        Point startPoint;  //起始点
        Point endPoint;   //结束点
        bool blnDraw;
        bool read = false;
        bool draw = false;
        private string filePath = null;
        private int duration = 0;
        private TimeSpan spendTime;
        private Process process;
        private bool processing = false;
        private string prevfile;
        private Graphics graphics;
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
            openFileDialog.Filter = "视频文件(*.mp4)|*.mp4";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            else
            {
                return;
            }

            var inputFile = new InputFile(filePath);
            Engine ffmpeg = new Engine(@"ffmpeg.exe");

            // 保存位于视频第 15 秒的帧。
            CancellationToken token = new CancellationToken();
            GetVideoInfo(ffmpeg, inputFile, token);
            read = true;
        }
        public async void GetVideoInfo(Engine ffmpeg, InputFile inputFile, CancellationToken token)
        {
            //MetaData data = await ffmpeg.GetMetaDataAsync(inputFile, token).GetAwaiter().GetResult();
            MetaData data = await ffmpeg.GetMetaDataAsync(inputFile, token);
            TimeSpan durat = data.Duration;//00:01:02.3200000
            duration = (int)data.Duration.TotalSeconds;
            string frameSize = data.VideoData.FrameSize;
            string[] frameInfo = frameSize.Split("x");
            picWidth = Int32.Parse(frameInfo[0]);
            picHeight = Int32.Parse(frameInfo[1]);

            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            currentImageFile = "./" + imageFileStart + Convert.ToInt64(ts.TotalMilliseconds).ToString() + ".jpg";
            var outputFile = new OutputFile(currentImageFile);
            MediaFile file = await ffmpeg.GetThumbnailAsync(inputFile, outputFile, token);
            this.picBox.Load(currentImageFile);
            lastX = this.picBox.Location.X;
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

        private void btnDoit_MouseClick(object sender, MouseEventArgs e)
        {
            if (read == false) return;
            if (draw == false)
            {
                return;
            }
            string path = Path.GetDirectoryName(filePath);
            FileInfo f = new FileInfo(filePath);
            //MessageBox.Show(path);
            //MessageBox.Show(filePath);
            //MessageBox.Show(f.Name);
            spendTime = new TimeSpan(DateTime.Now.Ticks);

            string tempName = f.Name.Split(".")[0];
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
            this.btnStop.Enabled = true;
        }

        private void removeWaterTimerCall(object sender, EventArgs e)
        {
            Point point = this.picBox.Location;
        }

        private void btnOut_CheckedChanged(object sender, EventArgs e)
        {
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
         
                    TimeSpan end = new TimeSpan(DateTime.Now.Ticks);    //获取当前时间的刻度数
                    TimeSpan abs = end.Subtract(spendTime).Duration();      //时间差的绝对值
                    this.Text = "淡化视频水印        总时长：" + duration + "秒   当前时长：" + cur + "秒   进度：" + percent + "%   用时：" + ((int)abs.TotalSeconds) + "秒";
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
                MessageBox.Show("处理完成,用时 " + ((int)abs.TotalSeconds) + " 秒", f.Name);
                processing = false;
                this.btnDoit.Enabled = true;
                this.btnStop.Enabled = false;
            }
            this.Text = "淡化视频水印";
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
                string tempName = f.Name.Split(".")[0];
                tempName = path + "\\" + tempName + guding + ".mp4";
                this.btnDoit.Enabled = true;
                this.btnStop.Enabled = false;
            }
        }

        private void btnPrev_Leave(object sender, EventArgs e)
        {
            this.picBoxPrev.Hide();
            this.picBoxPrev.Image = null;
        }

        private void btnOpenOld_Click(object sender, EventArgs e)
        {
            if (filePath == null || filePath.Length == 0)
            {
                return;
            }
            Player player = new Player(filePath);
            player.ShowDialog();
        }

        private void btnOpenNew_Click(object sender, EventArgs e)
        {
            FileInfo f = new FileInfo(filePath);
            string path = Path.GetDirectoryName(filePath);
            string tempName = f.Name.Split(".")[0];
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
            TestForm me = new TestForm();
            me.ShowDialog();
        }
    }
}