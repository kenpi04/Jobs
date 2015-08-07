using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Job.Core.Domain
{
   public class CareerNews:BaseEntityModel
    {
       public CareerNews()
       {
           CareerNewsShop = new HashSet<CareerNews_Shop_Mapping>();
       }      
       [Required(ErrorMessage="Chọn vị trí tuyển dụng")]      
        public int CateId { get; set; }
       [Required(ErrorMessage="Nhập nội dung")]
       [AllowHtml]
        public string FullDes { get; set; }
          
        public decimal Salary { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Deleted { get; set; }
        public int CreateBy { get; set; }
        public virtual CareerNewCate CareerNewCate { get; set; }
       
        public string Image { get; set; }
        public bool IsHot { get; set; }
        public virtual ICollection<CareerNews_Shop_Mapping> CareerNewsShop { get; set; }
      
    }
}
