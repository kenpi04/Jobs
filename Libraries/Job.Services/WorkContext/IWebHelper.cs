using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Services.WorkContext
{
   public interface IWebHelper
    {
       string MapPath(string path);
       string GetHostName();
       string HostName { get; set; }
     
      
    }
}
