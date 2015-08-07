using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Job.Core.Domain;

namespace Job.Services.Recuiments
{
   public interface ICareerNewsService
    {
       IPagedList<Job.Core.Domain.CareerNews> GetAll(int groupid = 0,int stateId=0, bool onlyHaveQuantity = false,bool includePriotyBox=false, int pageIndex = 1, int pageSize = 20);
       void Insert(Job.Core.Domain.CareerNews entity);
       void Update(Job.Core.Domain.CareerNews entity);
       void Delete(Job.Core.Domain.CareerNews entity);

       List<CareerNewCate> GetAllCareerNewsCate();
       Job.Core.Domain.CareerNews GetById(int id);
    }
}
