using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [ForeignKey("Parent")]
        public int? ParentId { get; set; }
        public Category Parent { get; set; }
        public ICollection<Category> ChildrenCategory { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
