using PurchaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseModel.Repositories.AccountRepositories
{
    public interface IUserRepository:IRepository<User>
    {
        void AssignRole(int id, UserRole role);

        void AssignRoles(int id, List<UserRole> roles);
    }
}
