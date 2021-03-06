using ECommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.ViewModels
{
    public class CategoryVM : BaseEntityVM
    {
        [Required]
        public string Name { get; set; }
        public int ProductId { get; set; }
        public Category Parent { get; set; }
        public ICollection<Category> ChildrenCategory { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
