using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Job.Core
{
  public  class CoreConfig
    {
      public static IList<Type>GetAllEntityClass()
      {
          return Assembly.GetExecutingAssembly().GetTypes().Where(x => x.Namespace == " Job.Core.Domain" && x.IsClass && x.IsVisible).ToList();

      }
    }
}
