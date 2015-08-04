using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Job.Services
{
  public  class RegisterDependencyService
    {
      public static IEnumerable<Type> GetListServiceAssembly()
      {
          var typesToRegisterService = Assembly.GetExecutingAssembly().GetTypes()
   .Where(type => !String.IsNullOrEmpty(type.Namespace) && type.Namespace.Contains("Job.Services."))
           .Where(type => type.BaseType != null && type.BaseType.IsClass&&type.IsVisible);
          return typesToRegisterService;
      }
    }
}
