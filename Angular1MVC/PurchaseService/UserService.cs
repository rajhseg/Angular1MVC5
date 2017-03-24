using PurchaseEntities;
using PurchaseModel.Repositories;
using PurchaseModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseService
{
    public class UserService : IUserService
    {
        #region variable

        private readonly IUnitOfWork _unitofwork;
        private readonly IEncryptService _encService;

        #endregion

        public UserService(IUnitOfWork unitofWork, IEncryptService _encService)
        {
            this._unitofwork = unitofWork;
            this._encService = _encService;
        }

        public AuthenticationData Validate(string username, string password)
        {
            var _authData = new AuthenticationData();
            
            var _user = _unitofwork.Users.Fetch(x => x.Name == username && x.Password == password).FirstOrDefault();

            if (_user != null)
            {
                var _roles = _user.Roles;
                _authData.User = _user;
                _authData.Principal = new GenericPrincipal(new GenericIdentity(username), _roles.Select(x => x.Role.Name).ToArray());
            }
            
            return _authData;
        }

        public User CreateUser(string username, string password, string displayname, string email, int phone, int[] roles)
        {
            User _user = null;

           
            _user = _unitofwork.Users.FirstOrDefault(x => x.Name == username);

            if (_user != null)
                throw new Exception("username is not available");

            _user = new User();
            _user.Name = username;
            _user.Password = password;
            _user.EncPassword = _encService.Encrypt(password);
            _user.Email = email;
            _user.DisplayName = displayname;
            _user.CreatedDate = DateTime.Now;
            _user.IsActive = true;
            _user.PhoneNo = phone;

            /* Saving the New User */
            _unitofwork.Users.Insert(_user);
            _unitofwork.Commit();

            /* Adding Roles to the new user */
            foreach (var item in roles)
            {
                var _role = _unitofwork.Roles.Get(item);
                if (_role == null)
                    throw new Exception("Role doesnt exists");

                var _userRole = new UserRole() { RoleId = _role.Id, UserId = _user.Id, CreatedDate = DateTime.Now, IsActive = true };
                _unitofwork.UserRoles.Insert(_userRole);
            }

            _unitofwork.Commit();
            
            return _user;
        }

        public User GetUser(int userid)
        {
            return _unitofwork.Users.Get(userid);
        }

        public User GetUser(string username)
        {
            return _unitofwork.Users.Fetch(x => x.Name == username).FirstOrDefault();
        }

        public List<Role> GetRoles(int userid)
        {
            return _unitofwork.UserRoles.Fetch(x => x.UserId == userid).Select(x => x.Role).ToList();
        }

        public List<Role> GetRoles(string username)
        {
            return _unitofwork.UserRoles.Fetch(x => x.User.Name == username).Select(x => x.Role).ToList();
        }

        public User ActivateUser(int userid)
        {
           
            var _user = _unitofwork.Users.Get(userid);
            if (_user == null)
                throw new Exception("user not available");

            _user.IsActive = true;
            _unitofwork.Users.Update(_user);
            _unitofwork.Commit();
            return _user;
            

        }

        public User ActiveUser(string username)
        {            
            var _user = _unitofwork.Users.Fetch(x => x.Name == username).FirstOrDefault();
            if (_user == null)
                throw new Exception("user not available");

            _user.IsActive = true;
            _unitofwork.Users.Update(_user);
            _unitofwork.Commit();
            return _user;
          
        }

        public bool DeleteUser(string username)
        {
            bool result;
            
            var _user = _unitofwork.Users.Fetch(x => x.Name == username).FirstOrDefault();

            if (_user == null)
                throw new Exception("User not available");

            _unitofwork.Users.Delete(_user);
            _unitofwork.Commit();
            result = true;
            
            return result;
        }

        public bool DeleteUser(int userid)
        {

            bool result;
            
            var _user = _unitofwork.Users.Get(userid);

            if (_user == null)
                throw new Exception("User not available");

            _unitofwork.Users.Delete(_user);
            _unitofwork.Commit();
            result = true;
            
            return result;
        }

        public string GetOAuthData(AuthenticationData data)
        {
            var format = string.Format("OAUTH-TOKEN {0} {1}", data.User.Name, data.User.Password);
            var oauthdata = _encService.Encrypt(format);
            return oauthdata;
        }

        public string GetOAuthData(string username,string password)
        {
            var format = string.Format("OAUTH-TOKEN {0} {1}", username, password);
            var oauthdata = _encService.Encrypt(format);
            return oauthdata;
        }

        public AuthenticationData GetMembershipData(string Oauthdata)
        {
            var format = _encService.Decrypt(Oauthdata);
            String[] data = format.Split(new char[] { ' ' });
            if (data[0] == "OAUTH-TOKEN" && data.Length == 3)
            {
                return Validate(data[1], data[2]);
            }
            return null;
        }
    }
}
