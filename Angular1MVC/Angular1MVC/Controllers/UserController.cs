using Angular1MVC.Handlers;
using Angular1MVC.Models;
using PurchaseModel.Repositories;
using PurchaseModel.Services;
using PurchaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Angular1MVC.Controllers
{
    [Authorization(Roles = "Customer,Admin")]    
    [RoutePrefix("api/user")]
    public class UserController : BaseController
    {

        private IUserService userService;

        public UserController(IUnitOfWork unitOfwork, IEncryptService encService, IUserService userService) : base(unitOfwork, encService)
        {
            this.userService = userService;
        }
      

        [Route("getbyid")]        
        [HttpGet]
        public HttpResponseMessage Getbyid()
        {
            IEnumerable<string> _headers;
            AuthenticationData authData = null;

            Request.Headers.TryGetValues("Authorization", out _headers);
            if (_headers != null)
            {
                var authtoken = _headers.FirstOrDefault();
                var token = authtoken.Replace("Bearer", "").Trim(); if (token != null && token != string.Empty)
                {                    
                    authData = userService.GetMembershipData(token);
                }

            }                
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, new { user = new UserModel() {id = authData?.User?.Id, displayname = authData?.User?.DisplayName, username = authData?.User?.Name,email= authData?.User?.Email,phone= authData?.User?.PhoneNo }  });
        }


        [Route("edituser")]
        [HttpPost]
        public HttpResponseMessage Edituser(UserModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _user = unitOfWork.Users.Get(user.id.Value);
                    if (_user != null)
                    {
                        _user.DisplayName = user.displayname;
                        _user.Name = user.username;
                        _user.Email = user.email;                        
                        _user.PhoneNo = user.phone.Value;
                        unitOfWork.Users.Update(_user);
                        unitOfWork.Commit();
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(System.Net.HttpStatusCode.NotFound);
                }
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, new { user = user });
            }
            else
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.NotAcceptable);
            }
        }

        [Route("deactivate")]
        public HttpResponseMessage Deactivate()
        {
            IEnumerable<string> _headers = null;
            Request.Headers.TryGetValues("Authorization", out _headers);
            if (_headers != null)
            {
                var authToken = _headers.FirstOrDefault();
                var token = authToken.Replace("Bearer", "").Trim();
                if(token!=null && token != string.Empty)
                {
                    var format = base.encryptService.Decrypt(token);
                    String[] data = format.Split(new char[] { ' ' });
                    string username = string.Empty, password = string.Empty;
                    
                    if (data[0] == "OAUTH-TOKEN" && data.Length == 3)
                    {
                        username = data[1];
                        password = data[2];
                        var _user = base.unitOfWork.Users.Fetch(x => x.Name == username && x.Password == password).FirstOrDefault();

                        if (_user != null)
                        {
                            _user.IsActive = false;
                            base.unitOfWork.Users.Update(_user);
                            base.unitOfWork.Commit();                            
                            return Request.CreateResponse(System.Net.HttpStatusCode.OK, new { Status = true });
                        }
                        else
                        {
                            return Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, new { Status = false });
                        }
                    }
                }
            }
            return Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, new { Status = false });
        }

        [Route("updatepassword")]
        [HttpPost]
        public HttpResponseMessage UpdatePassword(ChangePasswordModel passobj)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<string> _headers = null;
                Request.Headers.TryGetValues("Authorization", out _headers);

                if (_headers != null)
                {
                    var authToken = _headers.FirstOrDefault();
                    var token = authToken.Replace("Bearer", "").Trim();

                    if(token!=null && token != string.Empty)
                    {
                        var format = base.encryptService.Decrypt(token);
                        String[] data = format.Split(new char[] { ' ' });
                        string username = string.Empty, password = string.Empty;

                        if (passobj.OldPassword != data[2])
                        {
                            return Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);                            
                        }

                        if (data[0] == "OAUTH-TOKEN" && data.Length == 3 && passobj.OldPassword == data[2])
                        {
                            username = data[1];
                            password = data[2];
                            var _user = base.unitOfWork.Users.Fetch(x => x.Name == username && x.Password == password).FirstOrDefault();

                            if (_user != null)
                            {

                                _user.Password = passobj.NewPassword;
                                _user.EncPassword = base.encryptService.Encrypt(passobj.NewPassword);
                                base.unitOfWork.Users.Update(_user);
                                base.unitOfWork.Commit();
                                var authcode  = this.userService.GetOAuthData(username, passobj.NewPassword);
                                return Request.CreateResponse(System.Net.HttpStatusCode.OK,new { Status = true, Username = username, Authcode = authcode });

                            }
                            else
                            {
                                return Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
                            }
                        }


                    }

                }

            }

            return Request.CreateResponse(System.Net.HttpStatusCode.NotAcceptable);

        }


    }

}