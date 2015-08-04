using Job.Core;
using Job.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Web.Security;
using Job.Services.Account;

namespace Job.Services.WorkContext
{
  public  class WorkContext:IWorkContext
    {
      private readonly HttpContextBase _httpContext;
      private readonly IUserService _userService;
      private User _cachedUser;
      public WorkContext(HttpContextBase httpContext, IUserService userService)
      {
          this._httpContext = httpContext;
          this._userService = userService;
      }
      public virtual User CurrentUser
        {
            get
            {
               
                return GetAuthenticatedUser();
            }
            set{
                _cachedUser=value;
            }
           
        }
        public virtual User GetAuthenticatedUser()
        {
            if (_cachedUser != null)
                return _cachedUser;

            if (_httpContext == null ||
                _httpContext.Request == null ||
                !_httpContext.Request.IsAuthenticated ||
                !(_httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }

            var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
            var user = GetAuthenticatedUserFromTicket(formsIdentity.Ticket);
            if (user != null)
                _cachedUser = user;
            return _cachedUser ;
        }

        public virtual User GetAuthenticatedUserFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            var username = ticket.Name;
            var customer = _userService.GetUserByUserName(username);
            return customer;
        }


        public void SetUser(User user, bool isCrossDomain = false)
        {
            _cachedUser = user;
        }
    }
}
