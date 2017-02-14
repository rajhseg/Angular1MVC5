using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseEntities
{
    public class Role : IBaseEntity
    {
        public Role()
        {
            UserRoles = new List<UserRole>();
        }
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }

        public DateTime? CreatedDate { set; get; }

        public DateTime? ModifiedDate { set; get; }

        public bool IsActive { set; get; }

        public bool IsDeleted { set; get; }

        public virtual ICollection<UserRole> UserRoles { set; get; }
    }
}
