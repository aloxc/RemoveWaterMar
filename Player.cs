using FFmpeg.NET;
using Serilog;
using System.Reflection;
using System.Text;

namespace RemoveWaterMar
{
    public partial class Player : Form
    {
        public readonly int pBarPlayerMultiple = 100;
        private int vWidth = 0;
        private int vHeight = 0;
        private readonly string filePath = null;
        private string fileName = null;
        private ToolTip toolTip = null;
        private double fps;
        private int? bitRate;
        //视频时长，单位秒,这时使用ffmpeg读取出来的
        private int duration;
        //视频时长，单位毫秒,这时使用ffmpeg读取出来的
        private System.Diagnostics.Stopwatch watchTime = null;
        private Random random = new Random();
        private readonly static string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly static string imageFileStart = "Snipaste";

        public Player(string filePath)
        {
            InitializeComponent();
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("./log.txt", rollingInterval: RollingInterval.Month).CreateLogger();
            this.filePath = filePath;
            toolTip = new ToolTip();
            // 设置显示样式
            toolTip.AutoPopDelay = 5000;//提示信息的可见时间
            toolTip.InitialDelay = 500;//事件触发多久后出现提示
            toolTip.ReshowDelay = 500;//指针从一个控件移向另一个控件时，经过多久才会显示下一个提示框
            toolTip.ShowAlways = true;//是否显示提示框
            timer1.Enabled = true;
            watchTime = new System.Diagnostics.Stopwatch();   //定义一个计时对象  
            watchTime.Start();


        }

        private void vlcControl1_VlcLibDirectoryNeeded(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
        {
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;

            if (currentDirectory == null)
                return;
            if (IntPtr.Size == 4)
                e.VlcLibDirectory = new DirectoryInfo(Path.GetFullPath(@".\libvlc\win-x86\"));
            else
                e.VlcLibDirectory = new DirectoryInfo(Path.GetFullPath(@".\libvlc\win-x64\"));

            if (!e.VlcLibDirectory.Exists)
            {
                var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
                folderBrowserDialog.Description = "Select Vlc libraries folder.";
                folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
                folderBrowserDialog.ShowNewFolderButton = true;
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    e.VlcLibDirectory = new DirectoryInfo(folderBrowserDialog.SelectedPath);
                }
            }
        }

        private void Player_Load(object sender, EventArgs e)
        {
            //string filePath = "D:\\download\\防清吹洗脑指南\\3.mp4";
            //filePath = "D:\\download\\出局的列强？哈布斯堡之后的奥地利【牧杂谈】.mp4";
            play.SetMedia(new System.IO.FileInfo(filePath));
            FileInfo f = new FileInfo(filePath);
            fileName = f.Name;
            play.Play();

            //前期一切OK，Play方法去播放视频也没问题，结果在做到我想在播放的时候双击全屏，发现播放起来后，
            //鼠标事件和键盘事件都不能生效。又没有官方文档可查，于是墙内墙外查了两天，
            //看起来比较靠谱的方案就是说什么加一个透明的Panel，结果我试了并不能实现也不理想。。。
            //都准备换其他控件了，也很疑问为什么官方会捕获用户的键盘和鼠标事件。。。
            play.Video.IsMouseInputEnabled = false;
            play.Video.IsKeyInputEnabled = false;
            //Log.Information("帧率" + play.Rate);
            //play.Rate = 2.0f;
            var inputFile = new InputFile(filePath);
            Engine ffmpeg = new Engine(@"ffmpeg.exe");

            CancellationToken token = new CancellationToken();
            GetVideoInfo(ffmpeg, inputFile, token);
            //play.Pause();
        }

        private void play_Playing(object sender, Vlc.DotNet.Core.VlcMediaPlayerPlayingEventArgs e)
        {
            //Log.Information("播放ing" + e);
        }

        private void play_PositionChanged(object sender, Vlc.DotNet.Core.VlcMediaPlayerPositionChangedEventArgs e)
        {
            int pos = (int)(play.Position * 100);
            if (pos == 100)
            {
                play.Stop();
                this.Close();
            }
            int volume = play.Audio.Volume;
            this.Text = "分辨率：" + vWidth + "x" + vHeight + "     音量：" + volume + "%"
                + "     帧率：" + fps + "     码率：" + bitRate + "     时长(s)：" + duration +
                "     " + fileName;
            pos = (int)(play.Position * 100 * pBarPlayerMultiple);
            if (pos <= 100 * pBarPlayerMultiple)
            {
                this.pBarPlayer.Value = pos;
            }


        }

        private void time1_Tick(object sender, EventArgs e)
        {
            //Log.Information("tick !");
            int percent = (int.Parse(pBarPlayer.Value.ToString()) / pBarPlayerMultiple);
            //(int)watchTime.Elapsed.TotalSeconds
            int time = (int)play.Time / 1000;//毫秒转秒
            toolTip.SetToolTip(pBarPlayer, "进度：" + time + "s(" + percent + "%)");//toolTip显示进度
        }


        private void btn1x_Click(object sender, EventArgs e)
        {
            play.Rate = 1f;
            setRateButtonForeColor(sender as Button);
        }

        private void btn125x_Click(object sender, EventArgs e)
        {
            play.Rate = 1.25f;
            setRateButtonForeColor(sender as Button);
        }

        private void btn15x_Click(object sender, EventArgs e)
        {
            play.Rate = 1.5f;
            setRateButtonForeColor(sender as Button);
        }

        private void btn2x_Click(object sender, EventArgs e)
        {
            play.Rate = 2f;
            setRateButtonForeColor(sender as Button);
        }

        public async void GetVideoInfo(Engine ffmpeg, InputFile inputFile, CancellationToken token)
        {
            MetaData data = await ffmpeg.GetMetaDataAsync(inputFile, token);
            fps = data.VideoData.Fps;
            bitRate = data.VideoData.BitRateKbs;
            TimeSpan durat = data.Duration;//00:01:02.3200000
            int durationMilliseconds = (int)data.Duration.TotalMilliseconds;
            duration = (int)data.Duration.TotalSeconds;

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
            ConversionOptions conversionOptions = new ConversionOptions()
            {
                Seek = TimeSpan.FromMilliseconds(random.Next(1, durationMilliseconds - 1))
            };

            await ffmpeg.GetThumbnailAsync(inputFile, outputFile, conversionOptions, token);
            Image image = Image.FromFile(currentImageFile);

            vWidth = image.Width;
            vHeight = image.Height;
            play.Width = vWidth;
            play.Height = vHeight;
            this.Width = vWidth;
            this.Height = vHeight;
            this.pBarPlayer.Width = vWidth;
        }

        private void Player_FormClosed(object sender, FormClosedEventArgs e)
        {
            play.Stop();
        }
        private void setRateButtonForeColor(Button btn)
        {
            btn1x.ForeColor = Color.Black;
            btn125x.ForeColor = Color.Black;
            btn15x.ForeColor = Color.Black;
            btn2x.ForeColor = Color.Black;
            btn.ForeColor = Color.Red;

        }

        /// <summary>
        /// 重写ProcessDialogKey，来允许监听方向键
        /// </summary>
        /// <param name="keycode"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keycode)
        {
            switch (keycode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Right:
                case Keys.Down:
                    return false;
            }
            return true;
        }

        private void Player_KeyUp(object sender, KeyEventArgs e)
        {
            int volume = play.Audio.Volume;
            //Log.Information("keyUp:" + e.KeyCode + "\t" + volume);
            switch (e.KeyCode)
            {
                case Keys.Up:
                    volume += 5;
                    if (volume > 100)
                    {
                        volume = 100;
                    }
                    play.Audio.Volume = volume;
                    break;

                case Keys.Down:
                    volume -= 5;
                    if (volume < 0)
                    {
                        volume = 0;
                    }
                    play.Audio.Volume = volume;
                    break;
                case Keys.Right:
                    setTime(5);
                    break;
                case Keys.Left:
                    setTime(-5);
                    break;
                case Keys.PageDown:
                    setTime(10);
                    break;
                case Keys.PageUp:
                    setTime(-10);
                    break;
                case Keys.End:
                    setTime(15);
                    break;
                case Keys.Home:
                    setTime(-15);
                    break;
                case Keys.Space:
                    if (play.IsPlaying)
                    {
                        play.Pause();
                    }
                    else
                    {
                        play.Play();
                    }

                    break;
            }
        }
        private void setTime(int second)
        {
            if (second < 0)
            {
                //后退
                long time = play.Time + second * 1000;
                if (time > 0)
                {
                    play.Time = time;
                }
            }
            else if (second > 0)
            {
                //前进
                long time = play.Time + second * 1000;
                if (time < play.Length)
                {
                    play.Time = time;
                }
            }
        }
        private void pBarPlayer_MouseHover(object sender, EventArgs e)
        {
            /*
                // 创建the ToolTip 
                ToolTip toolTip1 = new ToolTip();

                // 设置显示样式
                toolTip1.AutoPopDelay = 5000;//提示信息的可见时间
                toolTip1.InitialDelay = 500;//事件触发多久后出现提示
                toolTip1.ReshowDelay = 500;//指针从一个控件移向另一个控件时，经过多久才会显示下一个提示框
                toolTip1.ShowAlways = true;//是否显示提示框

                //  设置伴随的对象.
                toolTip1.SetToolTip(this.pBarPlayer, "查询");//设置提示按钮和提示内容
            */
        }

        private void Player_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Help help = new Help();
            help.ShowDialog();
        }

        private void play_DoubleClick(object sender, EventArgs e)
        {
        }
    }
}
