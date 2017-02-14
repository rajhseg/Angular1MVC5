using PurchaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PurchaseModel.Repositories.AccountRepositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context, IContextFactory factory) : base(context, factory)
        {
        }

        public void AssignRole(int id, UserRole role)
        {
            var user = Get(id);
            if (user != null)
            {
                user.Roles.Add(role);
            }
        }

        public void AssignRoles(int id, List<UserRole> roles)
        {
            var user = Get(id);
            if (user != null)
            {
                foreach (var item in roles)
                {
                    user.Roles.Add(item);
                }
            }
        }
    }
}
