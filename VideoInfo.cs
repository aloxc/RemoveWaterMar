using FFmpeg.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoveWaterMar
{
    public class VideoInfo
    {
        public string path { get; set; }
        public string frameSize { get; set; }
        public int percent { get; set; }
        public string duration { get; set; }
        public int spend { get; set; }

        public VideoInfo()
        {

        }

        public VideoInfo(string path, string frameSize, int percent, string duration, int spend)
        {
            this.path = path;
            this.frameSize = frameSize;
            this.percent = percent;
            this.duration = duration;
            this.spend = spend;
        }

        public override bool Equals(object? obj)
        {
            return obj is VideoInfo info &&
                   path == info.path &&
                   frameSize == info.frameSize &&
                   percent == info.percent &&
                   duration == info.duration &&
                   spend == info.spend;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(path, frameSize, percent, duration, spend);
        }

        public bool isErrorVideo()
        {
            if(spend == -1)
            {
                return true;
            }
            return false;
        }

        public string[] toListItem()
        {
            return new string[]
            {
                this.path,
                this.duration,
                this.frameSize,
                "0",
                ""
            };
        }
    }
}
