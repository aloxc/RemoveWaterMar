using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoveWaterMar
{
    public partial class M3u8 : Form
    {
        private bool done = false;
        public M3u8()
        {
            InitializeComponent();
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("./log.txt", rollingInterval: RollingInterval.Month).CreateLogger();

        }

        private void btnOpenM3u8_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string filter = "m3u8文件(*.m3u8)|*.m3u8";

            openFileDialog.Filter = filter;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                FileInfo fileInfo = new FileInfo(filePath);
                string name = fileInfo.Name;
                this.tboxPath.Text = filePath;
                name = name.Split(".")[0];
                this.tboxName.Text = name + ".mp4";
            }
            else
            {
                return;
            }
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            done = false;
            string path = this.tboxPath.Text;
            FileInfo f = new FileInfo(path);
            if (!f.Exists)
            {
                this.lblStatus.Text = "请确认是否存在 " + path;
                return;
            }
            if (this.tboxName.Text.Trim().Length == 0) {
                this.lblStatus.Text = "输出文件不能为空";
                return;
            }
            if (!this.tboxName.Text.EndsWith(".mp4"))
            {
                this.lblStatus.Text = "输出文件必须是mp4";
                return;
            }
            string outfile = Path.GetDirectoryName(path) + "\\" +this.tboxName.Text;
            this.lblStatus.Text = outfile;
            FileInfo o = new FileInfo(outfile);

            if (o.Exists)
            {
                MessageBox.Show("输出文件已存在,本次操作取消" );

                return;
               
            }
            //MessageBox.Show(f.Name);
            Process process = new Process();
            process.StartInfo.FileName = "ffmpeg";
            //process.StartInfo.WorkingDirectory = path;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            string param = "-i \"" + path + "\" -vcodec copy -acodec copy \"" + outfile + "\"";
            //将输出信息重定向
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
            //开始异步读取输出
        }

        void p_OutputDataReceived(Object sender, DataReceivedEventArgs e)
        {
            //这里是正常的输出
            Log.Debug("out " + e.Data);
        }


        void p_ErrorDataReceived(Object sender, DataReceivedEventArgs e)
        {
            Log.Debug("error " + e.Data);
            if(!done)
            {
                this.lblStatus.Text = "正在合并";
            }
            
            //这里得到的是错误信息
            string info = e.Data;
            //video:113956kB audio:24191kB subtitle:0kB other streams:0kB global headers:0kB muxing overhead: 2.020919%
            if (info != null && info.Contains("video")
                && info.Contains("audio") 
                && info.Contains("subtitle") 
                && info.Contains("other") 
                && info.Contains("streams") 
                && info.Contains("global")
                && info.Contains("headers")
                && info.Contains("muxing")
                && info.Contains("overhead")
                )
            {
                done = true;
                this.lblStatus.Text = "完成合并";
            }
        }

        void p_Exited(Object sender, EventArgs e)
        {
            
        }
    }
}
