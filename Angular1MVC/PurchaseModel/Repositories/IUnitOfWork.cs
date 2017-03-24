using PurchaseModel.Repositories.AccountRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseModel.Repositories
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository Users { get; }

        IRoleRepository Roles { get; }

        IUserRoleRepository UserRoles { get; }

        IProductRepository Products { get; }

        IImageFileRepository ImageFiles { get; }

        int Commit();
    }
}
