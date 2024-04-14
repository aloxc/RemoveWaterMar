using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoveWaterMar
{

    public class Rootobject
    {
        public All_Draft_Store[] all_draft_store { get; set; }
    }

    public class All_Draft_Store
    {
        public bool draft_cloud_last_action_download { get; set; }
        public string draft_cloud_purchase_info { get; set; }
        public string draft_cloud_template_id { get; set; }
        public string draft_cloud_tutorial_info { get; set; }
        public string draft_cloud_videocut_purchase_info { get; set; }
        public string draft_cover { get; set; }
        public string draft_fold_path { get; set; }
        public string draft_id { get; set; }
        public bool draft_is_ai_shorts { get; set; }
        public bool draft_is_invisible { get; set; }
        public string draft_json_file { get; set; }
        public string draft_name { get; set; }
        public string draft_new_version { get; set; }
        public string draft_root_path { get; set; }
        public long draft_timeline_materials_size { get; set; }
        public string draft_type { get; set; }
        public string tm_draft_cloud_completed { get; set; }
        public int tm_draft_cloud_modified { get; set; }
        public long tm_draft_create { get; set; }
        public long tm_draft_modified { get; set; }
        public int tm_draft_removed { get; set; }
        public int tm_duration { get; set; }
    }

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
        public string type { get; set; }
    }

    public class Segment
    {
        public string material_id { get; set; }
        public Source_Timerange source_timerange { get; set; }
        public Target_Timerange target_timerange { get; set; }
    }

    public class Source_Timerange
    {
        public long duration { get; set; }
        public long start { get; set; }
    }

    public class Target_Timerange
    {
        public long duration { get; set; }
        public long start { get; set; }
    }

    public class Draft
    {
        public string path { get; set; }
        public long duration { get; set; }
        public long start { get; set; }
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
