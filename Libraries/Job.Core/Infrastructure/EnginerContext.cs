using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Integration.Mvc;

namespace Job.Core.Infrastructure
{
   public class EnginerContext
    {

       public static void SetBuilder(IContainer container)
       {
           Container = container;
       }


       private static IContainer Container { get; set; }
      
       public static T Resolve<T>() where T : class
       {
           return AutofacDependencyResolver.Current.ApplicationContainer.Resolve<T>();
       }
    }
}
