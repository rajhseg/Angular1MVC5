using PurchaseModel.Repositories;
using PurchaseModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Angular1MVC.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork work;
        private IEncryptService service;

        public HomeController(IUnitOfWork _work,IEncryptService service)
        {
            this.work = _work;
            this.service = service;
        }

        public ActionResult Index()
        {
            var roles = work.Roles.Getall();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}