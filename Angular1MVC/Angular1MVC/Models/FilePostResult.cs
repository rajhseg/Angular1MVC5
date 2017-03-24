using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Angular1MVC.Models
{
    public class FilePostResult
    {
        public string TrackingId { set; get; }

        public string DownloadUrl { set; get; }

        public string Size { set; get; }

        public string FileName { set; get; }

        public string Extn { set; get; }       

    }
}