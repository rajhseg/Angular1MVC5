using PurchaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseModel.Configurations
{
    public class UserRoleConfiguration:BaseEntityConfiguration<UserRole>
    {
        public UserRoleConfiguration()
        {
            Property(x => x.RoleId).IsRequired();
            Property(x => x.UserId).IsRequired();
        }
    }
}
