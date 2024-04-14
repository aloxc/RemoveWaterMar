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
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RemoveWaterMar
{
    public partial class JianYingDraftForm : Form
    {
        
        private readonly int percentColumIndex = 1;
        private Process process;
        private MainConfig mainConfig;
        private readonly string jianyingDraftSet = "C:\\Users\\mcncl\\AppData\\Local\\JianyingPro\\User Data\\Projects\\com.lveditor.draft\\root_meta_info.json";
        public JianYingDraftForm()
        {
            InitializeComponent();

            lvProjectList.HeaderStyle = ColumnHeaderStyle.Clickable;
            lvProjectList.View = View.Details;

            this.lvProjectList.Columns.Add("项目", 529, HorizontalAlignment.Left);
            this.lvProjectList.Columns.Add("状态", 70, HorizontalAlignment.Left);

            this.lvProjectList.ProgressTextColor = Color.Red;
            this.lvProjectList.ProgressColor = Color.YellowGreen;

            Application.EnableVisualStyles();
            this.DoubleBuffered = true;
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("./log.txt", rollingInterval: RollingInterval.Month).CreateLogger();
            mainConfig = MainConfig.Load();
        }
        private void btnSetOutPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "请选择导出文件夹";
                DialogResult dialogResult = folderBrowserDialog.ShowDialog();

                if (dialogResult != DialogResult.OK)
                {
                    return;
                }
                string saveDir = folderBrowserDialog.SelectedPath;
                mainConfig.JianYingOutPath = saveDir;
                MainConfig.Save(mainConfig);
            }

        }
        
        private void btnDoit_Click(object sender, EventArgs e)
        {
            if (mainConfig.JianYingOutPath == null || mainConfig.JianYingOutPath.Length == 0)
            {
                btnSetOutPath_Click(null, null);
            }
            bool debug = true;
            System.Windows.Forms.ListView.SelectedListViewItemCollection selectedItems = lvProjectList.SelectedItems;
            if(selectedItems.Count == 0)
            {
                MessageBox.Show("请选择需要导出的项目，按住ctrl多选");
                return;
            }
            foreach (ListViewItem item in selectedItems)
            {
                string project = item.SubItems[0].Text;
                string content = File.ReadAllText(mainConfig.JianYingProjectPath + "\\" + item.SubItems[0].Text + "\\draft_content.json");
                JianYingDraft jydraft = JsonConvert.DeserializeObject<JianYingDraft>(content);
                Dictionary<string, string> materialDict = new Dictionary<string, string>();
                foreach (Video v in jydraft.materials.videos)
                {
                    materialDict.Add(v.id, v.path);
                }
                List<Draft> drafts = new List<Draft>();
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
                        materialDict.TryGetValue(material_id, out path);
                        Draft draft = new Draft();
                        draft.start = seg.source_timerange.start;
                        draft.duration = seg.source_timerange.duration;
                        draft.path = path;
                        drafts.Add(draft);
                    }
                    for (int i = 0; i < drafts.Count; i++)
                    {
                        if (!Directory.Exists(mainConfig.JianYingOutPath + "\\" + project))
                        {
                            Directory.CreateDirectory(mainConfig.JianYingOutPath + "\\" + project);
                        }
                        Draft draft = drafts[i];
                        double st = draft.start;
                        double et = draft.duration;
                        TimeSpan start = TimeSpan.FromMilliseconds(st / 1000);
                        TimeSpan end = TimeSpan.FromMilliseconds(et / 1000);
                        //ffmpeg -ss 00:08:00.0333330 -t 00:06:00.1666660 -i "D:/影视/甄嬛传/甄嬛传.E10.mp4"
                        //-q:a 0 -map a -y "C:\Users\mcncl\Desktop\0.wav"
                        Log.Debug("runTask,添加任务" + (i + 1));
                        FileInfo f = new FileInfo(draft.path);
                        process = new Process();
                        process.StartInfo.FileName = "ffmpeg";
                        process.StartInfo.WorkingDirectory = f.DirectoryName;
                        process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        string outFile = mainConfig.JianYingOutPath + "\\"+ project + "\\" + project + "_" + i + ".wav";
                        string param = "-ss " + start.ToString();
                        param += " -t " + end.ToString();
                        param += " -i \"" + draft.path + "\"";

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
                    }
                }
            }
            if(selectedItems.Count == 1)
            {
                System.Diagnostics.Process.Start("Explorer", mainConfig.JianYingOutPath + "\\" + selectedItems[0].SubItems[0].Text);
            }
            else
            {
                System.Diagnostics.Process.Start("Explorer", mainConfig.JianYingOutPath);
            }

        }

        void batch_OutputDataReceived(Object sender, DataReceivedEventArgs e)
        {
            //这里是正常的输出
            //执行过程中停止了
            Log.Information("out " + e.Data);
            int count = lvProjectList.Items.Count;
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
        }

        private void btnDraftDir_Click(object sender, EventArgs e)
        {

            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "请选择一个文件夹";
                DialogResult dialogResult = folderBrowserDialog.ShowDialog();

                if (dialogResult != DialogResult.OK)
                {
                    return;
                }
                string selectedFolder = folderBrowserDialog.SelectedPath;
                mainConfig.JianYingProjectPath = selectedFolder;
                MainConfig.Save(mainConfig);
                setProjectList();
            }
        }

        private void JianYingDraftForm_Load(object sender, EventArgs e)
        {
            if(mainConfig.JianYingProjectPath == null || mainConfig.JianYingProjectPath.Length == 0)
            {
                using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                {
                    folderBrowserDialog.Description = "请选择一个文件夹";
                    DialogResult dialogResult = folderBrowserDialog.ShowDialog();

                    if (dialogResult != DialogResult.OK)
                    {
                        return;
                    }
                    string selectedFolder = folderBrowserDialog.SelectedPath;
                    mainConfig.JianYingProjectPath = selectedFolder;
                    MainConfig.Save(mainConfig);
                }
            }
            setProjectList();
        }

        private void setProjectList()
        {
            if (!Directory.Exists(mainConfig.JianYingProjectPath))
            {
                return;
            }
            lvProjectList.Items.Clear();
            DirectoryInfo dir = new DirectoryInfo(mainConfig.JianYingProjectPath);
            //获取当前目录JPG文件列表 GetFiles获取指定目录中文件的名称(包括其路径)
            DirectoryInfo[] directoryInfos = dir.GetDirectories();
            List<string> covers = new List<string>();
        a: foreach (DirectoryInfo sub in directoryInfos)
            {
                FileInfo[] files = sub.GetFiles();
                if (files.Length == 0)
                {
                    continue;
                }
                string fs = "";
                foreach (FileInfo file in files)
                {
                    fs += file.Name;
                }
                if (fs.Contains("draft_content.json") && fs.Contains("draft_cover.jpg"))
                {
                    covers.Add(sub.Name);
                    string f = sub.GetFiles("draft_content.json")[0].FullName;
                    string content = File.ReadAllText(f);
                    JianYingDraft jydraft = JsonConvert.DeserializeObject<JianYingDraft>(content);
                    Dictionary<string, string> materialDict = new Dictionary<string, string>();
                    foreach (Video v in jydraft.materials.videos)
                    {
                        materialDict.Add(v.id, v.path);
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
                            if (path == null || !File.Exists(path))
                            {
                                covers.Remove(sub.Name);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < covers.Count; i++)
            {
                string[] infos = covers[i].Split('\\');
                string p = infos[infos.Length - 1];
                List<ListViewItem> listItems = new List<ListViewItem>();

                ListViewItem item = new ListViewItem(new string[] { p, "--" });
                listItems.Add(item);
                item.UseItemStyleForSubItems = false;
                if (i % 2 == 0)
                {
                    item.SubItems[0].BackColor = Color.YellowGreen;
                    item.SubItems[1].BackColor = Color.YellowGreen;
                }
                else
                {
                    item.SubItems[0].BackColor = Color.MistyRose;
                    item.SubItems[1].BackColor = Color.MistyRose;
                }
                this.lvProjectList.Items.AddRange(listItems.ToArray());
                this.lblTaskCount.Text = "总任务 " + covers.Count;
            }
        }
    }
}
