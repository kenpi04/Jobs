using Job.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Job.Core.Domain;

namespace Job.Services.Recuiments
{
   public interface IRecuimentService
    {
       IPagedList<Recuitment> GetAll(int cateId = 0, int pageIndex = 1, int pageSize = 20);
       void Insert(Recuitment entity);
       void Update(Recuitment entity);
       void Delete(Recuitment entity);
      
    }
}
