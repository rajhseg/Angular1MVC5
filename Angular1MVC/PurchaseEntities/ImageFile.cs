using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseEntities
{
    public class ImageFile : IBaseEntity
    {
        public long FileSize { set; get; }

        public string FileName { set; get; }

        public string DownloadUrl { set; get; }

        public DateTime? CreatedDate { set; get; }
        
        public int Id { set; get; }

        public bool IsActive { set; get; }

        public bool IsDeleted { set; get; }

        public DateTime? ModifiedDate { set; get; }

    }
}
