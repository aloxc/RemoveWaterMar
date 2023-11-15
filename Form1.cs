using FFmpeg.NET;
using Serilog;
using System.Diagnostics;
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
        private int picHight = 0;
        private static string imageFileStart = "Snipaste";
        private static string logFileStart = "log";
        private string currentImageFile = "snip.jpg";
        private static string guding = "_out";

        private int x;
        private int y;
        private int width;
        private int height;


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
            //openFileDialog.InitialDirectory = "c:\\";//注意这里写路径时要用c:\\而不是c:\
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

            // MessageBox.Show("aaaa");
            // FileDialog fileDialog = new FileDialog();
            var inputFile = new InputFile(filePath);
            Engine ffmpeg = new Engine(@"ffmpeg.exe");


            // 保存位于视频第 15 秒的帧。

            CancellationToken token = new CancellationToken();
            GetVideoInfo(ffmpeg, inputFile, token);
            read = true;
            //MessageBox.Show("bbbb");
            //await ffmpeg.GetThumbnailAsync(inputFile, outputFile, options);
            //var metadata = ffmpeg.GetMetadataAsync(inputFile);
        }
        public async void GetVideoInfo(Engine ffmpeg, InputFile inputFile, CancellationToken token)
        {
            //MetaData data = await ffmpeg.GetMetaDataAsync(inputFile, token).GetAwaiter().GetResult();

            MetaData data = await ffmpeg.GetMetaDataAsync(inputFile, token);
            //MessageBox.Show("ccc" + data.Duration + "\n" + data.VideoData.ToString() + "\n" + data.Duration.TotalSeconds);
            TimeSpan durat = data.Duration;//00:01:02.3200000
            duration = (int)data.Duration.TotalSeconds;
            // double bb = (DateTime.Now - DateTime.Parse(durat)).Ticks / 10000000;//转换成秒
            string frameSize = data.VideoData.FrameSize;
            string[] frameInfo = frameSize.Split("x");
            picWidth = Int32.Parse(frameInfo[0]);
            picHight = Int32.Parse(frameInfo[1]);

            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);

            currentImageFile = "./" + imageFileStart + Convert.ToInt64(ts.TotalMilliseconds).ToString() + ".jpg";
            var outputFile = new OutputFile(currentImageFile);
            MediaFile file = await ffmpeg.GetThumbnailAsync(inputFile, outputFile, token);
            this.picBox.Load(currentImageFile);
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
                //Log.Information("mouse up");
            }

        }


        private void btnPrevPic_Click(object sender, EventArgs e)
        {
            //player2 player2 = new player2(filePath, x, y, width, height);
            // player2.ShowDialog();
            //bool debug = true;
            /*
            if (debug)
            {
                return;
            }
            */
            //准备更改为使用ffplay来实现，直接播放
            //ffplay -i D:\download\测试.mp4 -vf "delogo=x=21:y=21:w=315:h=166:show=0,delogo=x=21:y=600:w=315:h=166:show=0"


            Process preProcess = new Process();
            preProcess.StartInfo.FileName = "ffmpeg";
            preProcess.StartInfo.WorkingDirectory = "./";
            preProcess.StartInfo.CreateNoWindow = false;//显示命令行窗口
            preProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            prevfile = currentImageFile.Replace(".jpg", "__out__" + Convert.ToInt64(ts.TotalMilliseconds).ToString() + ".jpg");
            //prevfile = currentImageFile.Replace(".jpg", "__out.jpg");
            string param = "-i \"" + currentImageFile + "\" -filter_complex  \"delogo=x=" + x + ":y=" + y + ":w=" + width + ":h=" + height + ":show=0\" \"" + prevfile + "\" -y";

            //MessageBox.Show(param);
            //cmd.StartInfo.Arguments = param1 + " " + param2;
            preProcess.StartInfo.Arguments = param;
            preProcess.StartInfo.UseShellExecute = true;
            preProcess.StartInfo.CreateNoWindow = true;
            //不使用操作系统使用的shell启动进程
            preProcess.StartInfo.UseShellExecute = false;
            //将输出信息重定向
            preProcess.StartInfo.RedirectStandardOutput = true;


            preProcess.StartInfo.RedirectStandardInput = true;
            preProcess.StartInfo.RedirectStandardOutput = true;
            preProcess.StartInfo.RedirectStandardError = true;
            preProcess.EnableRaisingEvents = true;

            preProcess.Exited += new EventHandler(prevPicExited);


            preProcess.Start();
            //开始异步读取输出
            preProcess.BeginOutputReadLine();
            preProcess.BeginErrorReadLine();


            //ffmpeg -i D:\download\xxxx.png -filter_complex delogo=x=49:y=81:w=864:h=121:show=0 D:\download\xxxx__out.png
            System.Drawing.Point mouse = System.Windows.Forms.Control.MousePosition;
            picBoxPrev.Location = mouse;
            //picture.Location = e.Lo);
            picBoxPrev.Size = new Size(picWidth, picHight);
            picBoxPrev.SizeMode = PictureBoxSizeMode.AutoSize;

            picBoxPrev.TabIndex = 11;
            picBoxPrev.TabStop = false;
            //process.WaitForExit();
            //Log.Information(prevfile);
            //picBoxPrev.Image = System.Drawing.Image.FromFile(prevfile);


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
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;     //绘制线的格式
            if (blnDraw)
            {
                //Log.Information("paint up");
                //此处是为了在绘制时可以由上向下绘制，也可以由下向上
                x = Math.Min(startPoint.X, endPoint.X);
                y = Math.Min(startPoint.Y, endPoint.Y);
                width = Math.Abs(startPoint.X - endPoint.X);
                height = Math.Abs(startPoint.Y - endPoint.Y);
                e.Graphics.DrawRectangle(pen, x, y, width, height);
                draw = true;

                blnDraw = true;

            }
            pen.Dispose();
            //

        }

        private void btnDoit_MouseClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("位置\nx=" + x + "\ny=" + y + "\nwidth=" + width + "\nheight=" + height);
            if (read == false) return;
            if (draw == false)
            {
                //可以做压缩
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
            process.StartInfo.CreateNoWindow = false;//显示命令行窗口
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            string param = "-i \"" + filePath + "\" -filter_complex  \"delogo=x=" + x + ":y=" + y + ":w=" + width + ":h=" + height + ":show=0\" \"" + path + "\\" + tempName + guding + ".mp4\" -y";
            if (rbtnGpu.Checked)
            {
                param = "-i \"" + filePath + "\" -vf  \"delogo=x=" + x + ":y=" + y + ":w=" + width + ":h=" + height + ":show=0\" -c:v h264_nvenc -gpu 0 \"" + path + "\\" + tempName + guding + ".mp4\" -y";
            }
            //MessageBox.Show(param);
            //cmd.StartInfo.Arguments = param1 + " " + param2;
            process.StartInfo.Arguments = param;
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.CreateNoWindow = true;
            //不使用操作系统使用的shell启动进程
            process.StartInfo.UseShellExecute = false;
            //将输出信息重定向
            process.StartInfo.RedirectStandardOutput = true;


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
            //cmd.WaitForExit();


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
            //frame= 2700 fps=178 q=26.0 size=   15616kB time=00:01:31.90 bitrate=1391.9kbits/s speed=6.07x
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
                    int per = Convert.ToInt32(rx);

                    //Log.Information(info);
                    TimeSpan end = new TimeSpan(DateTime.Now.Ticks);    //获取当前时间的刻度数
                    TimeSpan abs = end.Subtract(spendTime).Duration();      //时间差的绝对值
                    this.Text = "淡化视频水印        总时长：" + duration + "秒   当前时长：" + cur + "秒   进度：" + per + "%   用时：" + ((int)abs.TotalSeconds) + "秒";

                }
            }

        }

        void p_Exited(Object sender, EventArgs e)
        {
            if (processing)
            {
                FileInfo f = new FileInfo(filePath);
                TimeSpan end = new TimeSpan(DateTime.Now.Ticks);    //获取当前时间的刻度数
                TimeSpan abs = end.Subtract(spendTime).Duration();      //时间差的绝对值

                MessageBox.Show("处理完成,用时 " + ((int)abs.TotalSeconds) + " 秒", f.Name);
                processing = false;
            }
            this.Text = "淡化视频水印";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (processing)
            {
                process.Kill();
                processing = false;
                string path = Path.GetDirectoryName(filePath);
                FileInfo f = new FileInfo(filePath);
                spendTime = new TimeSpan(DateTime.Now.Ticks);

                string tempName = f.Name.Split(".")[0];
                tempName = path + "\\" + tempName + guding + ".mp4";


            }
        }

        private void btnPrev_Leave(object sender, EventArgs e)
        {
            //Log.Information("leave prev");
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
            //Log.Information("click " + radioButton.Name + "\tcpu:" + rbtnCpu.Checked + "\t gpu:" + rbtnGpu.Checked);
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
            string param = "-i \"" + filePath + "\" -vf  \"delogo=x=" + x + ":y=" + y + ":w=" + width + ":h=" + height + ":show=0\" ";

            ffplay.StartInfo.Arguments = param;
            ffplay.StartInfo.CreateNoWindow = true;
            ffplay.StartInfo.RedirectStandardOutput = true;
            ffplay.StartInfo.UseShellExecute = false;
            ffplay.EnableRaisingEvents = true;

            ffplay.OutputDataReceived += (o, e) => Debug.WriteLine(e.Data ?? "NULL", "ffplay");
            ffplay.ErrorDataReceived += (o, e) => Debug.WriteLine(e.Data ?? "NULL", "ffplay");
            ffplay.Exited += (o, e) => Debug.WriteLine("Exited", "ffplay");
            ffplay.Start();

            Thread.Sleep(500); // you need to wait/check the process started, then...

            SetParent(ffplay.MainWindowHandle, this.Handle);

            // window, x, y, width, height, repaint
            // move the ffplayer window to the top-left corner and set the size to 320x280
            MoveWindow(ffplay.MainWindowHandle, 0, 0, width, height, true);


        }
    }
}