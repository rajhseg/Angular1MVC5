using PurchaseEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseModel.Repositories.AccountRepositories
{
    public class ImageFileRepository:Repository<ImageFile>,IImageFileRepository
    {
        public ImageFileRepository(DbContext context, IContextFactory factory) : base(context, factory)
        {

        }
    }
}
