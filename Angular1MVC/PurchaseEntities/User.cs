using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseEntities
{
    public class User : IBaseEntity
    {
        public User()
        {
            Roles = new List<UserRole>();
        }
        public int Id { set; get; }

        public string Name { set; get; }

        public string DisplayName { set; get; }

        public string Password { set; get; }

        public string EncPassword { set; get; }

        public int PhoneNo { set; get; }

        public string Email { set; get; }

        public DateTime? CreatedDate { set; get; }

        public DateTime? ModifiedDate { set; get; }

        public bool IsActive { set; get; }

        public bool IsDeleted { set; get; }

        public virtual ICollection<UserRole> Roles { set; get; }

    }
}
