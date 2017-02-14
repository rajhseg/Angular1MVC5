using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseModel.Repositories
{
    public interface IContextFactory
    {
        T GetContext<T>(DbContext context) where T:class;

    }
}
