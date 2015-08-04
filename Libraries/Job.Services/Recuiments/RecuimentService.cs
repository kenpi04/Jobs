using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Job.Core;
using Job.Core.Domain;
using Job.Data;
using PagedList;

namespace Job.Services.Recuiments
{
    public   class RecuimentService:IRecuimentService
    {
        private readonly IRepository<Recuitment> _recuimentRepository;
        public RecuimentService(IRepository<Recuitment> _recuimentRepository)
        {
            this._recuimentRepository = _recuimentRepository;
        }
        public IPagedList<Core.Domain.Recuitment> GetAll(int cateId = 0,int locationId=0, int pageIndex = 1, int pageSize = 20)
        {
            var q = _recuimentRepository.Table;
            if (cateId > 0)
                q = q.Where(x => x.CateId == cateId);
            if (locationId != 0)
                q = q.Where(x => x.LocationId == locationId);
            q = q.OrderBy(x => x.DateCreate);
            return new PagedList<Recuitment>(q, pageIndex - 1, pageSize);

        }

        public void Insert(Core.Domain.Recuitment entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            _recuimentRepository.Insert(entity);
        }

        public void Update(Core.Domain.Recuitment entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            _recuimentRepository.Update(entity);
        }

        public void Delete(Core.Domain.Recuitment entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            _recuimentRepository.Delete(entity);
        }

        public IPagedList<Recuitment> GetAll(int cateId = 0, int pageIndex = 1, int pageSize = 20)
        {
            throw new NotImplementedException();
        }
    }
}
