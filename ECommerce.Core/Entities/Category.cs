using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public int ParentId { get; set; }
        public Category category { get; set; }
    }
}
