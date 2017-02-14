using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseEntities
{
    public class UserRole : IBaseEntity
    {
        public int Id { set; get; }

        public int UserId { set; get; }

        public DateTime? CreatedDate { set; get; }

        public DateTime? ModifiedDate { set; get; }

        public bool IsActive { set; get; }

        public bool IsDeleted { set; get; }

        public int RoleId { set; get; }

        [ForeignKey("RoleId")]
        public virtual Role Role { set; get; }

        [ForeignKey("UserId")]
        public virtual User User { set; get; }
    }
}
