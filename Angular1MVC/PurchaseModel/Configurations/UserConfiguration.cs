using PurchaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseModel.Configurations
{
    public class UserConfiguration:BaseEntityConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users");
            Property(x => x.Name).IsRequired().HasMaxLength(40);
            Property(x => x.DisplayName).IsRequired().HasMaxLength(70);
            Property(x => x.Email).IsRequired().HasMaxLength(150);
            Property(x => x.EncPassword).IsRequired();
            Property(x => x.Password).IsRequired();
            Property(x => x.PhoneNo).IsRequired();
        }
    }
}
