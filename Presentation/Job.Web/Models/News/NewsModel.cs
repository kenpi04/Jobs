using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Job.Web.Models.News
{
    public class NewsModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public string Time { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public int ViewCount { get; set; }
        public string PostBy { get; set; }

        public string ShortDes { get; set; }
    }
}