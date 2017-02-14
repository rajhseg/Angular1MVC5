using PurchaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PurchaseModel.Repositories.AccountRepositories
{
    public class UserRoleRepository : Repository<UserRole>,IUserRoleRepository
    {
        public UserRoleRepository(DbContext context, IContextFactory factory) : base(context, factory)
        {
        }
    }
}
