using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Angular1MVC.App_Start
{
    public class BootstrapAutofac
    {
        public static void Run()
        {
            AutofacConfig.Initialize(GlobalConfiguration.Configuration);            
        }
    }
}