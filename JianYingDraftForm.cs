using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RemoveWaterMar
{
    public partial class JianYingDraftForm : Form
    {
        private List<Draft> drafts = new List<Draft>();
        private System.Threading.Timer taskTimer = null;
        private readonly int percentColumIndex = 3;
        private Process process;
        string saveDir = null;
        string proj = null;
        private int index = 0;
        private int doneCount = 0;

        public JianYingDraftForm()
        {
            InitializeComponent();

            lvDraftList.HeaderStyle = ColumnHeaderStyle.Clickable;
            lvDraftList.View = View.Details;

            this.lvDraftList.Columns.Add("文件", 360, HorizontalAlignment.Left);
            this.lvDraftList.Columns.Add("开始时间", 150, HorizontalAlignment.Left);
            this.lvDraftList.Columns.Add("持续时间", 150, HorizontalAlignment.Left);
            this.lvDraftList.Columns.Add("状态", 100, HorizontalAlignment.Left);

            this.lvDraftList.ProgressTextColor = Color.Red;
            this.lvDraftList.ProgressColor = Color.YellowGreen;

            Application.EnableVisualStyles();
            this.DoubleBuffered = true;
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("./log.txt", rollingInterval: RollingInterval.Month).CreateLogger();

        }

        private void btnDoit_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "请选择一个文件夹";
                DialogResult dialogResult = folderBrowserDialog.ShowDialog();

                if (dialogResult != DialogResult.OK)
                {
                    return;
                }
                saveDir = folderBrowserDialog.SelectedPath;
            }
            taskTimer = new System.Threading.Timer(new TimerCallback(checkAndRunTask));

            int count = drafts.Count;
            if (count == 0)
            {
                Log.Debug("click,列表中没有任务");
                return;
            }
            bool doneAll = true;
            bool debug = true;
            if (debug)
            {
                for (int i = 0; i < count; i++)
                {
                    string sStart = lvDraftList.Items[i].SubItems[1].Text;
                    string duation = lvDraftList.Items[i].SubItems[2].Text;
                    double st = double.Parse(sStart);    
                    double et = double.Parse(duation);    
                    TimeSpan start = TimeSpan.FromMilliseconds(st/1000);
                    TimeSpan end = TimeSpan.FromMilliseconds(et/1000);
                    end = end.Add(start);

                    // 输出时间间隔的毫秒数
                    /*MessageBox.Show(
                        "start:" + sStart +
                        "\nduation:" + duation +
                        "\nstart:" + start.ToString() +
                        "\nduation:" + end.ToString() +
                        "\nstart:" + start.Hours + ":" + start.Minutes + ":" +start.Seconds + "." + start.Milliseconds+
                        "\nend:" + end.Hours + ":" + end.Minutes + ":" + end.Seconds + "." + end.Milliseconds
                        );*/
                }
            }
            for (int i = 0; i < count; i++)
            {
                string percentColum = lvDraftList.Items[i].SubItems[percentColumIndex].Text;
                if (!percentColum.Contains("完成"))
                {
                    doneAll = false;
                }
            }
            if (doneAll)
            {
                return;
            }
            taskTimer.Change(0, 1000);
        }
        private void checkAndRunTask(object v)
        {
            //Log.Debug("runTask,开始");
            int count = lvDraftList.Items.Count;
            if (count == 0)
            {
                Log.Debug("runTask,列表中没有任务");
                return;
            }
            bool debug = true;
            if(debug)
            {
                for (int i = 0; i < count; i++)
                {
                    string sStart = lvDraftList.Items[i].SubItems[1].Text;
                    string duation = lvDraftList.Items[i].SubItems[2].Text;
                    double st = double.Parse(sStart);
                    double et = double.Parse(duation);
                    TimeSpan start = TimeSpan.FromMilliseconds(st / 1000);
                    TimeSpan end = TimeSpan.FromMilliseconds(et / 1000);
                    //end = end.Add(start);

                }
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
                //Log.Debug("runTask,有ffmpeg");
                return;
            }
            bool doneAll = true;
            for (int i = 0; i < count; i++)
            {
                index = i;
                string filePath = lvDraftList.Items[i].SubItems[0].Text;
                string sStart = lvDraftList.Items[i].SubItems[1].Text;
                string duation = lvDraftList.Items[i].SubItems[2].Text;
                double st = double.Parse(sStart);
                double et = double.Parse(duation);
                TimeSpan start = TimeSpan.FromMilliseconds(st / 1000);
                TimeSpan end = TimeSpan.FromMilliseconds(et / 1000);
                string percentColum = lvDraftList.Items[i].SubItems[percentColumIndex].Text;
                if (!percentColum.Contains("完成"))
                {
                    //ffmpeg -ss 00:08:00.0333330 -t 00:06:00.1666660 -i "D:/影视/甄嬛传/甄嬛传.E10.mp4"
                    //-q:a 0 -map a -y "C:\Users\mcncl\Desktop\0.wav"
                    doneAll = false;
                    Log.Debug("runTask,添加任务" + (i + 1));
                    FileInfo f = new FileInfo(filePath);
                    string path = Path.GetDirectoryName(filePath);
                    string tempName = f.Name.Remove(f.Name.LastIndexOf("."));
                    process = new Process();
                    process.StartInfo.FileName = "ffmpeg";
                    process.StartInfo.WorkingDirectory = path;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    string outFile = saveDir + "\\" + proj + "_" + i + ".wav";
                    string param = "-ss " + start.ToString();
                        param += " -t " + end.ToString();
                    param += " -i \"" + filePath + "\"";
                    
                    param += " -q:a 0 -map a -y \"" + outFile + "\"";

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
                    break;
                }
            }
            if (doneAll)
            {
                taskTimer.Change(Timeout.Infinite, 1000);
            }
        }
        void batch_OutputDataReceived(Object sender, DataReceivedEventArgs e)
        {
            //这里是正常的输出
            //执行过程中停止了
            Log.Information("out " + e.Data);
            int count = lvDraftList.Items.Count;
        }


        void batch_ErrorDataReceived(Object sender, DataReceivedEventArgs e)
        {
            //这里得到的是错误信息
            string info = e.Data;
            //Log.Information("error " + e.Data);
            if (info != null && info.Contains("video") && info.Contains("audio") && info.Contains("subtitle"))
            {
            }
        }

        void batch_Exited(Object sender, EventArgs e)
        {
            Log.Information("已完成 " + lvDraftList.Items[index].SubItems[0].Text);
            lvDraftList.Items[index].SubItems[3].Text = "(完成)";


            doneCount++;
            this.lblDoneCount.Text = "已完成 " + doneCount;
        }

        private void btnDraftDir_Click(object sender, EventArgs e)
        {
            drafts.Clear();
            lvDraftList.Items.Clear();
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "请选择一个文件夹";
                DialogResult dialogResult = folderBrowserDialog.ShowDialog();

                if (dialogResult != DialogResult.OK)
                {
                    return;
                }
                string selectedFolder = folderBrowserDialog.SelectedPath;
                string[] infos = selectedFolder.Split('\\');
                proj = infos[infos.Length - 1];
                
                if (!File.Exists(selectedFolder + "\\draft_content.json"  )){
                    MessageBox.Show("该目录下不存在draft_content.json文件", "不是剪映项目文件夹");
                    return;
                }
                string content = File.ReadAllText(selectedFolder + "\\draft_content.json");
                JianYingDraft jydraft = JsonConvert.DeserializeObject<JianYingDraft>(content);
                Dictionary<string, string> materialDict = new Dictionary<string, string>();

                foreach(Video v in jydraft.materials.videos)
                {
                    materialDict.Add(v.id,v.path);
                }
                
                foreach (Track track in jydraft.tracks)
                {
                    if (!track.type.Equals("video"))
                    {
                        continue;
                    }
                    foreach (Segment seg in track.segments)
                    {
                        string material_id = seg.material_id;
                        string path = null;
                        materialDict.TryGetValue(material_id, out path);
                        Draft draft = new Draft();
                        draft.start = seg.source_timerange.start;
                        draft.duration = seg.source_timerange.duration;
                        draft.path = path;
                        drafts.Add(draft);
                    }
                }
                
                for (int i = 0; i < drafts.Count; i++)
                {
                    Draft draft = drafts[i];
                    List<ListViewItem> listItems = new List<ListViewItem>();

                    ListViewItem item = new ListViewItem(draft.toListItem());
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
                    this.lvDraftList.Items.AddRange(listItems.ToArray());
                }
                this.lblTaskCount.Text = "总任务 " + drafts.Count;
                this.lblDoneCount.Text = "已完成 " + 0;
            }
        }
    }
}
