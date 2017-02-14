using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseEntities
{
    public interface IBaseEntity
    {
        int Id { set; get; }

        DateTime? CreatedDate { set; get; }

        DateTime? ModifiedDate { set; get; }

        bool IsActive { set; get; }

        bool IsDeleted { set; get; }
    }
}
