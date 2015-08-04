using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Job.Core.Domain;
using Job.Data;

namespace Job.Services.Directory
{
   public class BannerService:IBannerService
    {
       private readonly IRepository<Banner> _bannerRepository;
           public BannerService(IRepository<Banner> bannerRepository)
           {
               this._bannerRepository = bannerRepository;
           }
        public IList<Banner> GetAll()
        {
            return _bannerRepository.Table.ToList();
        }

        public void Insert(Banner entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
             _bannerRepository.Insert(entity);
        }

        public void Update(Banner entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            _bannerRepository.Update(entity);
        }

        public void Delete(Banner entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            _bannerRepository.Delete(entity);
        }
    }
}
