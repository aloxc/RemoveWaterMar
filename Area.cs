using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoveWaterMar
{

    public class Area
    {

        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }

        public Area()
        {

        }

        public Area(int x, int y, int width, int height)
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("./log.txt", rollingInterval: RollingInterval.Month).CreateLogger();
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            Log.Information("const x:" + x + ",y:" + y + ",width:" + width + ",height:" + height);
        }

        public override bool Equals(object? obj)
        {
            return obj is Area area &&
                   x == area.x &&
                   y == area.y &&
                   width == area.width &&
                   height == area.height;
        }

        public override string ToString()
        {
            return "x:" + x + ",y:" + y + ",width:" + width + ",height:" + height;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, width, height);
        }

        //检出多个处理框内容重叠否
        public static bool checkArea(List<Area> list)
        {
            if (list == null || list.Count == 0) return false;
            if (list.Count == 1) return true;
            return false;
        }
    }
}
