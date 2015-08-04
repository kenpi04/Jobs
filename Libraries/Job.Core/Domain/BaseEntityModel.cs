using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Core.Domain
{
   public class BaseEntityModel
    {
       public BaseEntityModel()
       {
           this.CustomProperties = new Dictionary<string, object>();
       }
       public int Id { get; set; }
       public virtual Dictionary<string,object> CustomProperties  { get; set; }
       public T GetCustomValue<T>(string keyName)
       {
           object a=null;
           if(this.CustomProperties.TryGetValue(keyName,out a))
           {
               return (T)a;
           }
           return (T)a;
       }
    }
}
