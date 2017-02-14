using PurchaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseService
{
    public interface IUserService
    {
        AuthenticationData Validate(string username, string password);

        User CreateUser(string username, string password, string displayname, string email, int phone, int[] roles);

        User GetUser(int userid);

        User GetUser(string username);

        List<Role> GetRoles(int userid);

        List<Role> GetRoles(string username);

        User ActivateUser(int userid);

        User ActiveUser(string username);

        bool DeleteUser(string username);

        bool DeleteUser(int userid);

        string GetOAuthData(AuthenticationData data);

        AuthenticationData GetMembershipData(string Oauthdata);

    }
}
