using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Job.Core.Domain;
using Job.Core;
using PagedList;

namespace Job.Services.CareerNews
{
   public interface INewsService
    {
       IPagedList<News> GetAllNews(int pageIndex = 1, int pageSize = 20);
       void Insert(News entity);
       void Update(News entity);
       void Delete(News entity);
     


       News GetById(int id);
    }
}
