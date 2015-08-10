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


        public IList<User> GetAll()
        {
            return _userRepository.Table.ToList() ;
        }


        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }


        public void Insert(User model)
        {
            _userRepository.Insert(model);
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }
    }
}
