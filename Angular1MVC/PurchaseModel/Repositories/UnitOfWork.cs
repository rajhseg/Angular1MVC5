using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurchaseModel.Repositories.AccountRepositories;
using System.Data.Entity;

namespace PurchaseModel.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext Context;

        public IRoleRepository Roles { get; private set; }

        public IUserRoleRepository UserRoles { get; private set; }

        public IUserRepository Users { get; private set; }

        public UnitOfWork(DbContext context, IContextFactory factory)
        {
            this.Context = context;
            Users = new UserRepository(context, factory);
            Roles = new RoleRepository(context, factory);
            UserRoles = new UserRoleRepository(context, factory);
        }

        public int Commit()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
        }

    }
}
