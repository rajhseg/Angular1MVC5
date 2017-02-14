using PurchaseEntities;
using PurchaseModel.Configurations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseModel
{
    public class PurchaseDBContext:DbContext
    {
        public PurchaseDBContext():base("purchasecontext")
        {
            Database.SetInitializer(new PurchaseDbInitializer());
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<User> Users { set; get; }

        public DbSet<Role> Roles { set; get; }

        public DbSet<UserRole> UserRoles { set; get; }

        public void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
        }
    }
}
