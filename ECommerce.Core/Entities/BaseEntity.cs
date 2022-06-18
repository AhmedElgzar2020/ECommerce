using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModefiedBy { get; set; }
        public DateTime? ModefiedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
