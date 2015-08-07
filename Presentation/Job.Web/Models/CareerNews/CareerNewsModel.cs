using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Job.Web.Models.CareerNews
{
    public class CareerNewsModel
    {
        public int Id { get; set; }
        public string CateName { get; set; }
        public int Quantity { get; set; }
        public string CateGroupName { get; set; }
        public string StateList { get; set; }
        public string ImgUrl { get; set; }
    }
}