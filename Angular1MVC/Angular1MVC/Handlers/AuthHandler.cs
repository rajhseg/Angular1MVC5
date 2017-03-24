using PurchaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Dependencies;

namespace Angular1MVC.Handlers
{
    public class AuthHandler:DelegatingHandler
    {
        IEnumerable<string> _headers=null;
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            
            try
            {
                request.Headers.TryGetValues("Authorization", out _headers);
                if (_headers != null)
                {
                    var authtoken = _headers.FirstOrDefault();
                    var token = authtoken.Replace("Bearer", "").Trim();
                   
                    if(token != null && token != string.Empty)
                    {
                        IDependencyScope scope = request.GetDependencyScope();
                        IUserService userService = (IUserService)scope.GetService(typeof(IUserService));
                        AuthenticationData authData = userService.GetMembershipData(token);
                        if(authData!=null && authData.User != null)
                        {
                            Thread.CurrentPrincipal = authData.Principal;
                            HttpContext.Current.User = authData.Principal;
                        }
                        else
                        {
                            var response =new  TaskCompletionSource<HttpResponseMessage>();
                            response.SetResult(request.CreateResponse(System.Net.HttpStatusCode.Unauthorized));
                            return response.Task;
                        }
                    }
                    else
                    {
                        var response = new TaskCompletionSource<HttpResponseMessage>();
                        response.SetResult(request.CreateResponse(System.Net.HttpStatusCode.Forbidden));
                        return response.Task;
                    }
                    return base.SendAsync(request, cancellationToken);
                }
                else
                {
                    return base.SendAsync(request, cancellationToken);
                }
            }
            catch(Exception ex)
            {
                var response = new TaskCompletionSource<HttpResponseMessage>();
                response.SetResult(request.CreateResponse(System.Net.HttpStatusCode.InternalServerError));
                return response.Task;
            }

        }
    }
}