using Job.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Services.Directory
{
  public  interface IBannerService
    {
      IList<Banner> GetAll();
      void Insert(Banner entity);
      void Update(Banner entity);
      void Delete(Banner entity);
      
    }
}
