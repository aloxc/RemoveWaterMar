using FFmpeg.NET;
using RemoveWaterMar.Properties;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vlc.DotNet.Forms;

namespace RemoveWaterMar
{
    public partial class Player : Form
    {
        private int vWidth = 0;
        private int vHight = 0;
        private readonly string filePath = null;
        private string fileName = null;

        public Player(string filePath)
        {
            InitializeComponent();
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("./log.txt", rollingInterval: RollingInterval.Month).CreateLogger();
            this.filePath = filePath;
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
            this.Text = fileName + "%         音量：" + volume + "%";
            pos = (int)(play.Position * 10000);
            this.pBarPlayer.Value = pos;
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
            //MetaData data = await ffmpeg.GetMetaDataAsync(inputFile, token).GetAwaiter().GetResult();

            MetaData data = await ffmpeg.GetMetaDataAsync(inputFile, token);
            string frameSize = data.VideoData.FrameSize;
            string[] frameInfo = frameSize.Split("x");
            vWidth = Int32.Parse(frameInfo[0]);
            vHight = Int32.Parse(frameInfo[1]);
            play.Width = vWidth;
            play.Height = vHight;
            this.Width = vWidth;
            this.Height = vHight;
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
            }
        }
    }
}
