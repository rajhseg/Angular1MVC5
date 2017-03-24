using PurchaseModel.Repositories;
using PurchaseModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Angular1MVC.Controllers
{
    public class BaseController:ApiController
    {
        protected IUnitOfWork unitOfWork;
        protected IEncryptService encryptService;

        public BaseController(IUnitOfWork unitofwork,IEncryptService encServices)
        {
            this.unitOfWork = unitofwork;
            this.encryptService = encServices;
        }
        public HttpResponseMessage Create(Func<HttpResponseMessage> method)
        {
            HttpResponseMessage _response = null;
            try
            {
                if(method!=null)
                    _response = method.Invoke();
            }catch(Exception ex)
            {
                _response = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
            return _response;
        }
    }
}