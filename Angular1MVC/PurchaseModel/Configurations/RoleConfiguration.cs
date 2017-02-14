using PurchaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseModel.Configurations
{
    public class RoleConfiguration:BaseEntityConfiguration<Role>
    {
        public RoleConfiguration()
        {
            Property(x => x.Name).IsRequired();
            Property(x => x.Description).IsRequired();
        }
    }
}
