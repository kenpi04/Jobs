using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Core.Domain
{
   public class CareerNews_Shop_Mapping:BaseEntityModel
    {
        public int NewsId { get; set; }
        public int ShopId { get; set; }
        public int Quantity { get; set; }
        public DateTime EndDate { get; set; }
        public virtual CareerNews CareerNews { get; set; }
        public virtual Shop Shop { get; set; }
    }
}
