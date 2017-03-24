using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseEntities
{
    public class Product : IBaseEntity
    {
        public string Brand { set; get; }

        public string Model { set; get; }

        public string Description { set; get; }

        public int ImageId { set; get; }

        [ForeignKey("ImageId")]
        public virtual ImageFile File { set; get; }

        public DateTime? CreatedDate { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
