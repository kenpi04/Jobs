
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace Job.Services.WorkContext
{
    public class WebHelper : IWebHelper
    {
        private readonly HttpContextBase _httpContext;
        private  bool USE_SSL = false;//setting used ssl
        private string _HostName;//cached HostName
        public WebHelper(HttpContextBase httpContext)
        {
            this._httpContext = httpContext;
        }
        public string HostName {

            get
            {
                if (_HostName == null)
                    _HostName = GetHostName();
                return _HostName;
            }           
            set{
                _HostName=value;
            }
        }
        public string MapPath(string path)
        {
            if (HostingEnvironment.IsHosted)
            {
                //hosted
                return HostingEnvironment.MapPath(path);
            }
            //not hosted. For example, run in unit tests
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            return Path.Combine(baseDirectory, path);
        }


        public string GetHostName()
        {
            var uri = _httpContext.Request.Url;

            string hostName = uri.Scheme+"://" +uri.Host;
            if(uri.Port!=80&&uri.Port!=8080)
            {
                hostName += ":" + uri.Port;
            }
            if (!string.IsNullOrWhiteSpace(HttpRuntime.AppDomainAppVirtualPath))
                hostName += HttpRuntime.AppDomainAppVirtualPath;

            return hostName;
        }




       
    }
}
