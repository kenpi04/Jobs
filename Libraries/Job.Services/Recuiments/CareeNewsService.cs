using Job.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Job.Core.Domain;

namespace Job.Services.Recuiments
{
  public  class CareeNewsService:ICareerNewsService
    {
      private readonly IRepository<Job.Core.Domain.CareerNews> _careerNewsRepository;
      private readonly IRepository<CareerNewCate> _careerNewCateRepository;
      public CareeNewsService(IRepository<Job.Core.Domain.CareerNews> careerNewsRepository, IRepository<CareerNewCate> careerNewCateRepository)
      {
          this._careerNewCateRepository = careerNewCateRepository;

          this._careerNewsRepository = careerNewsRepository;
      }
        public PagedList.IPagedList<Core.Domain.CareerNews> GetAll(int pageIndex = 1, int pageSize = 20)
        {
            var q=_careerNewsRepository.Table;
            return new PagedList.PagedList<Core.Domain.CareerNews>(q, pageIndex, pageSize);
        }

        public void Insert(Core.Domain.CareerNews entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            _careerNewsRepository.Insert(entity);
        }

        public void Update(Core.Domain.CareerNews entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            _careerNewsRepository.Update(entity);
        }

        public void Delete(Core.Domain.CareerNews entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            _careerNewsRepository.Delete(entity);
        }


        public Core.Domain.CareerNews GetById(int id)
        {
            return _careerNewsRepository.GetById(id);
        }
        public List<CareerNewCate> GetAllCareerNewsCate()
        {
            return _careerNewCateRepository.Table.Where(x => x.Active).OrderBy(x => x.Name).ToList();
        }
    }
}
