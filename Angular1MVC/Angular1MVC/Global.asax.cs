using Angular1MVC.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Angular1MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            BootstrapAutofac.Run();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configuration.EnsureInitialized();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                RequestContext context = ((MvcHandler)httpContext.Handler).RequestContext;
                if (context.HttpContext.Request.IsAjaxRequest())
                {
                    httpContext.Response.Clear();
                    var controllername = context.RouteData.GetRequiredString("controller");
                    IControllerFactory factory =  ControllerBuilder.Current.GetControllerFactory();
                    IController controller = factory.CreateController(context, controllername);
                    ControllerContext controllerContext = new ControllerContext(context, (ControllerBase)controller);

                    JsonResult json = new JsonResult();
                    json.Data = new { status = false, serverError = "500" };
                    json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    json.ExecuteResult(controllerContext);
                    httpContext.Response.End();

                }
                else
                {
                    httpContext.Response.Redirect("~/Error");
                }
            }
        }
    }
}
