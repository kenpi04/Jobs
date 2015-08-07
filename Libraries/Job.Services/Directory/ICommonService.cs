using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Job.Core.Domain;

namespace Job.Services.Directory
{
  public  interface ICommonService
    {
      IList<StateProvice> GetAllStateProvince();
      IList<District> GetAllDistrictByStateId(int stateId);
      IList<Shop> GetAllShop(int stateId = 0);
      void InsertShop(Shop entity);
      void DeleteShop(Shop entity);
      void UpdateShop(Shop entity);
      Shop GetShopById(int id);

      
    }
}
