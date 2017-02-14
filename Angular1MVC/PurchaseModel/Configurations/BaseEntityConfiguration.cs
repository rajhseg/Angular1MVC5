using PurchaseEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseModel.Configurations
{
    public class BaseEntityConfiguration<T>:EntityTypeConfiguration<T> where T:class, IBaseEntity, new()
    {
        public BaseEntityConfiguration()
        {
            HasKey(x => x.Id);
            Property(x => x.IsActive).IsRequired();
            Property(x => x.IsDeleted).IsOptional();
            Property(x => x.CreatedDate).IsRequired();
            Property(x => x.ModifiedDate).IsOptional();
        }
    }
}
