using Angular1MVC.Models;
using PurchaseEntities;
using PurchaseModel.Repositories;
using PurchaseModel.Services;
using PurchaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Angular1MVC.Controllers
{
    [Authorize(Roles ="Admin")]
    [RoutePrefix("api/account")]
    public class AccountController : BaseController
    {
        private IUserService userService;

        public AccountController(IUnitOfWork unitOfwork, IEncryptService encService, IUserService userService) : base(unitOfwork, encService)
        {
           this.userService = userService;
        }


        [Route("adduser")]
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Register(RegistrationModel model)
        {
            return Create(() =>
            {
                HttpResponseMessage response = null;

                if (ModelState.IsValid)
                {
                    User _user = userService.CreateUser(model.UserName, model.Password, model.DisplayName, model.Email, model.Phone, new int[] { 2 });

                    if (_user != null)
                    {
                        response = Request.CreateResponse(HttpStatusCode.OK, new { status = true });
                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.OK, new { status = false });
                    }
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, new { status = false });
                }

                return response;
            });
        }



        [HttpPost]
        [AllowAnonymous]
        [ResponseType(typeof(LoginAuthData))]
        [Route("login")]
        public LoginAuthData Login(LoginModel model)
        {
            LoginAuthData _response = null;
                if (ModelState.IsValid)
                {
                    AuthenticationData _data = userService.Validate(model.UserName, model.Password);
                    if(_data!=null && _data.User != null)
                    {
                        _response =  new LoginAuthData(){ Status = true,Username = model.UserName, Authcode = userService.GetOAuthData(_data) };
                    }
                    else
                    {
                        _response = new LoginAuthData() { Status = false ,Username = model.UserName};
                    }
                }
                else
                {
                    _response = new LoginAuthData() { Status = false, Username = model.UserName };
                }
                return _response;           
        }

    }

    

}
