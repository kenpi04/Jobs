using Job.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Job.Core.Domain;
using PagedList;

namespace Job.Services.Recuiments
{
   public interface IRecuimentService
    {
       IPagedList<Core.Domain.Recuitment> GetAll(int cateId = 0, int locationId = 0, int pageIndex = 1, int pageSize = 20);
       void Insert(Recuitment entity);
       void Update(Recuitment entity);
       void Delete(Recuitment entity);


       Recuitment GetById(int id);
    }
}
