using Job.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Services.Account
{
   public interface IUserService
    {
       User GetUserByUserName(string username);
    }
}
