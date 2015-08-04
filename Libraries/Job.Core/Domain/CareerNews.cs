using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Core.Domain
{
   public class CareerNews:BaseEntityModel
    {
        public int CateId { get; set; }
        public int LocationId { get; set; }
        public int Quantity { get; set; }
        public string FullDes { get; set; }
        public string Title { get; set; }
        public decimal Salary { get; set; }
        public DateTime CreateDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
        public bool Deleted { get; set; }
        public int CreateBy { get; set; }
        public virtual CareerNewCate CareerNewCate { get; set; }
    }
}
