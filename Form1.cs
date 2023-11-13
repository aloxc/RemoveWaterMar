using FFmpeg.NET;
using FFmpeg.NET.Enums;
using FFmpeg.NET.Events;
using Serilog;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace RemoveWaterMar
{
    public partial class WaterMark : Form
    {
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

        Point startPoint;  //��ʼ��
        Point endPoint;   //������
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
            //openFileDialog.InitialDirectory = "c:\\";//ע������д·��ʱҪ��c:\\������c:\
            openFileDialog.Filter = "��Ƶ�ļ�(*.mp4)|*.mp4";
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


            // ����λ����Ƶ�� 15 ���֡��

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
            // double bb = (DateTime.Now - DateTime.Parse(durat)).Ticks / 10000000;//ת������
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
                picBox.Invalidate();//�˴��벻��ʡ��
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

        private void btnPrev_Click(object sender, EventArgs e)
        {

            Process preProcess = new Process();
            preProcess.StartInfo.FileName = "ffmpeg";
            preProcess.StartInfo.WorkingDirectory = "./";
            preProcess.StartInfo.CreateNoWindow = false;//��ʾ�����д���
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
            //��ʹ�ò���ϵͳʹ�õ�shell��������
            preProcess.StartInfo.UseShellExecute = false;
            //�������Ϣ�ض���
            preProcess.StartInfo.RedirectStandardOutput = true;


            preProcess.StartInfo.RedirectStandardInput = true;
            preProcess.StartInfo.RedirectStandardOutput = true;
            preProcess.StartInfo.RedirectStandardError = true;
            preProcess.EnableRaisingEvents = true;

            preProcess.Exited += new EventHandler(prev_Exited);


            preProcess.Start();
            //��ʼ�첽��ȡ���
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

        void prev_Exited(Object sender, EventArgs e)
        {
            picBoxPrev.Load(prevfile);
            picBoxPrev.Show();

        }

        private void picBox_Paint(object sender, PaintEventArgs e)
        {
            if (read == false) return;
            PictureBox pic = sender as PictureBox;
            Pen pen = new Pen(Color.Red, 3);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;     //�����ߵĸ�ʽ
            if (blnDraw)
            {
                //Log.Information("paint up");
                //�˴���Ϊ���ڻ���ʱ�����������»��ƣ�Ҳ������������
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
            //MessageBox.Show("λ��\nx=" + x + "\ny=" + y + "\nwidth=" + width + "\nheight=" + height);
            if (read == false) return;
            if (draw == false)
            {
                //������ѹ��
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
            process.StartInfo.CreateNoWindow = false;//��ʾ�����д���
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
            //��ʹ�ò���ϵͳʹ�õ�shell��������
            process.StartInfo.UseShellExecute = false;
            //�������Ϣ�ض���
            process.StartInfo.RedirectStandardOutput = true;


            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.EnableRaisingEvents = true;

            process.Exited += new EventHandler(p_Exited);
            process.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            process.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);

            process.Start();
            //��ʼ�첽��ȡ���
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
            //���������������
            //Log.Information("out " + e.Data);

        }

        void p_ErrorDataReceived(Object sender, DataReceivedEventArgs e)
        {
            //����õ����Ǵ�����Ϣ
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
                    TimeSpan end = new TimeSpan(DateTime.Now.Ticks);    //��ȡ��ǰʱ��Ŀ̶���
                    TimeSpan abs = end.Subtract(spendTime).Duration();      //ʱ���ľ���ֵ
                    this.Text = "������Ƶˮӡ        ��ʱ����" + duration + "��   ��ǰʱ����" + cur + "��   ���ȣ�" + per + "%   ��ʱ��" + ((int)abs.TotalSeconds) + "��";

                }
            }

        }

        void p_Exited(Object sender, EventArgs e)
        {
            if (processing)
            {
                FileInfo f = new FileInfo(filePath);
                TimeSpan end = new TimeSpan(DateTime.Now.Ticks);    //��ȡ��ǰʱ��Ŀ̶���
                TimeSpan abs = end.Subtract(spendTime).Duration();      //ʱ���ľ���ֵ

                MessageBox.Show("�������,��ʱ " + ((int)abs.TotalSeconds) + " ��", f.Name);
                processing = false;
            }
            this.Text = "������Ƶˮӡ";
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
    }
}