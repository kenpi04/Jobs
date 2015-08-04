using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Job.Data;
using Job.Core.Domain;
using Job.Core;
using PagedList;

namespace Job.Services.CareerNews
{
    public class NewsService:INewsService
    {
        private readonly IRepository<News> _newsRepository;
       
        public NewsService(IRepository<News> newsRepository)
        {
            this._newsRepository = newsRepository;
        }
        public IPagedList<News> GetAllNews(int pageIndex = 1, int pageSize = 20)
        {
            var q = _newsRepository.Table.OrderBy(x=>x.CreateDate);
            return new PagedList<News>(q, pageIndex, pageSize);

           
        }

        public void Insert(Core.Domain.News entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            _newsRepository.Insert(entity);
        }

        public void Update(Core.Domain.News entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            _newsRepository.Update(entity);
        }

        public void Delete(Core.Domain.News entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            _newsRepository.Delete(entity);
        }


        public News GetById(int id)
        {
            return _newsRepository.GetById(id);
        }


     
    }
}
