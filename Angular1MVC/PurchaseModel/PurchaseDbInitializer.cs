using PurchaseEntities;
using PurchaseModel.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseModel
{
    public class PurchaseDbInitializer:DropCreateDatabaseIfModelChanges<PurchaseDBContext>
    {

        IEncryptService encService;
        public PurchaseDbInitializer()
        {
            encService = new EncryptService();
        }

        protected override void Seed(PurchaseDBContext context)
        {

            var adminRole = new Role() { Name = "Admin", Description = "Role is for admin Activities", CreatedDate = DateTime.Now, IsActive = true };
            var userRole = new Role() { Name = "Customer", Description = "Role is for Customer Activities", CreatedDate = DateTime.Now, IsActive = true };

            var adminuser = new User() { Name = "Rajesh", DisplayName = "Rajhseg", Email = "rajhseg@gmail.com", CreatedDate = DateTime.Now, Password = "Password12*", IsActive = true, PhoneNo = 5451231 };
            adminuser.EncPassword = encService.Encrypt(adminuser.Password);

            var cusUser = new User() { Name = "Suresh", DisplayName = "Suresh", Email = "suresh@gmail.com", CreatedDate = DateTime.Now, Password = "Password13*", IsActive = true, PhoneNo = 2341221 };
            cusUser.EncPassword = encService.Encrypt(cusUser.Password);

            var adminuserMap1 = new UserRole() { UserId = 1, RoleId = 1, IsActive = true, CreatedDate = DateTime.Now };
            var adminuserMap2 = new UserRole() { UserId = 1, RoleId = 1, IsActive = true, CreatedDate = DateTime.Now };
            var cusUserMap1 = new UserRole() { UserId = 2, RoleId = 2, IsActive = true, CreatedDate = DateTime.Now };

            context.Roles.Add(adminRole);
            context.Roles.Add(userRole);
            context.Commit();

            context.Users.Add(adminuser);
            context.Users.Add(cusUser);
            context.Commit();

            context.UserRoles.Add(adminuserMap1);
            context.UserRoles.Add(adminuserMap2);
            context.UserRoles.Add(cusUserMap1);

            context.Commit();

            base.Seed(context);
        }
    }
}
