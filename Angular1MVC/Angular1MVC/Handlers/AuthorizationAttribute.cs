using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Angular1MVC.Handlers
{
    public class AuthorizationAttribute:AuthorizeAttribute
    {

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            ClaimsPrincipal currentPrincipal = HttpContext.Current.User as ClaimsPrincipal;

            if (currentPrincipal != null && Check(currentPrincipal))
                return true;
            else
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                return false;
            }            
        }      

        private bool Check(ClaimsPrincipal principal)
        {
            string[] roles = Split(Roles);
            if (roles.Length == 0) return true;
            return roles.Any(principal.IsInRole);
        }

        protected static string[] Split(string roles)
        {
            if (string.IsNullOrWhiteSpace(roles))
                return new string[0];

            var result = roles.Split(',').Where(s => !String.IsNullOrWhiteSpace(s.Trim()));
            return result.Select(s => s.Trim()).ToArray();
        }
    }
}