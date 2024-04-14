using Newtonsoft.Json;
using System.IO;

namespace RemoveWaterMar
{

    [Serializable]
    public class MainConfig
    {
        public static readonly string UserDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RemoveWaterMar";

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public static readonly string MainConfigPath = UserDirectory + "\\Config.json";


        public string JianYingOutPath { get; set; }
        public string JianYingProjectPath { get; set; }

        public MainConfig() 
        { 
        
        } 

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="path">配置文件路径</param>
        /// <returns></returns>
        public static MainConfig Load(string path)
        {
            MainConfig config = null;
            try
            {
                string content = File.ReadAllText(path);
                config = JsonConvert.DeserializeObject(content, typeof(MainConfig)) as MainConfig;
            }
            catch (Exception err)
            {
            }
            return config;
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <returns></returns>
        public static MainConfig Load()
        {
       
            // 检查目录是否存在
            if (Directory.Exists(UserDirectory) == false)
            {
                Directory.CreateDirectory(UserDirectory);
            }
            MainConfig config = Load(MainConfigPath);
            if (config == null)
            {
                config = new MainConfig();
                MainConfig.Save(config);
            }
            return config;
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <param name="config"></param>
        public static void Save(MainConfig config)
        {
            try
            {
                string content = JsonConvert.SerializeObject(config, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                File.WriteAllText(MainConfigPath, content);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
        }
    }
}
