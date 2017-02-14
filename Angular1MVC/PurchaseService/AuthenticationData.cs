using PurchaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseService
{
    public class AuthenticationData
    {
        public User User { set; get; }

        public IPrincipal Principal { set; get; }

        public bool isValid()
        {
            return Principal != null;
        }
    }
}
