using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VK

{
    public class VkAlbum
    {
        public AlbumResponse[] restonse { get; set; }
    }
    public class AlbumResponse
    {
        public int aid { get; set; }
        public int owned_id { get; set; }
        public string artist { get; set; }
        public string title { get; set; }
        public int duration { get; set; }
        public string url { get; set; }
        public string lurics_id { get; set; }
        public int genre { get; set; }
    }
    public class VkAlbumSup : AlbumResponse
    {
        public string durationString { get; set; }
    }
}
