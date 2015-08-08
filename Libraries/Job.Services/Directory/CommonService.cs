using Job.Core.Domain;
using Job.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Services.Directory
{
    public class CommonService : ICommonService
    {
        private readonly IRepository<StateProvice> _stateProvinceRepository;
        private readonly IRepository<District> _districtRepository;
        private readonly IRepository<Shop> _shopRepository;
        public CommonService(IRepository<StateProvice> stateProvinceRepository,
       IRepository<District> districtRepository,
        IRepository<Shop> shopRepository)
        {
            this._districtRepository = districtRepository;
            this._stateProvinceRepository = stateProvinceRepository;
            this._shopRepository = shopRepository;
        }
        public IList<Core.Domain.StateProvice> GetAllStateProvince()
        {
            return _stateProvinceRepository.Table.ToList();
        }

        public IList<Core.Domain.District> GetAllDistrictByStateId(int stateId)
        {
            var q = _districtRepository.Table.Where(x => x.StateProviceId == stateId).ToList();
            return q;
            
        }

        public IList<Core.Domain.Shop> GetAllShop(int stateId = 0)
        {
            var q = _shopRepository.Table;
            if (stateId != 0)
                q = q.Where(x => x.LocationId == stateId);
            return q.ToList();
        }


        public void InsertShop(Shop entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null");
            _shopRepository.Insert(entity);
        }

        public void DeleteShop(Shop entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null");
            _shopRepository.Delete(entity);
        }

        public void UpdateShop(Shop entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null");
            _shopRepository.Update(entity);
        }


        public Shop GetShopById(int id)
        {
            return _shopRepository.GetById(id);
        }
    }
}
