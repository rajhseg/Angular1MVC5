using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseModel.Repositories
{
    public class ContextFactory : IContextFactory
    {
        public T GetContext<T>(DbContext context) where T : class
        {
            return context as T;
        }
    }
}
