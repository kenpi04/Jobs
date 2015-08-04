using Job.Core.Domain;
using Job.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Services.Account
{
  public  class UserService:IUserService
    {
      private readonly IRepository<User> _userRepository;
      public UserService(IRepository<User> userRepository)
      {
          this._userRepository = userRepository;
      }
        public Core.Domain.User GetUserByUserName(string username)
        {
            return _userRepository.First(x => x.UserName == username);
        }
    }
}
