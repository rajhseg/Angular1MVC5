using PurchaseEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseModel.Repositories.AccountRepositories
{
    public class ProductRepository:Repository<Product>,IProductRepository
    {
        public ProductRepository(DbContext context, IContextFactory factory) : base(context, factory)
        {
                
        }
    }

}
