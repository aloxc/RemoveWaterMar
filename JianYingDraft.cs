using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoveWaterMar
{
    public class JianYingDraft
    {
        public Materials materials { get; set; }
        public Track[] tracks { get; set; }
    }

    public class Materials
    {
        public Video[] videos { get; set; }
    }

    public class Video
    {
        public string id { get; set; }
        public string path { get; set; }
    }

    public class Track
    {
        public Segment[] segments { get; set; }
        public string type {  get; set; }
    }

    public class Segment
    {
        public string material_id { get; set; }
        public Source_Timerange source_timerange { get; set; }
        public Target_Timerange target_timerange { get; set; }
    }

    public class Source_Timerange
    {
        public int duration { get; set; }
        public int start { get; set; }
    }

    public class Target_Timerange
    {
        public int duration { get; set; }
        public int start { get; set; }
    }

    public class Draft
    {
        public string path { get; set; }
        public int duration { get; set; }
        public int start { get; set; }
        public string[] toListItem()
        {
            return new string[4]{
                path,
                start.ToString(),
                duration.ToString(),
                ""
            };
        }
    }

}
