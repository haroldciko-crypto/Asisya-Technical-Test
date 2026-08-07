using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asisya.Domain.Entities
{
    public class Category
    {
        public int CategoryID { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Picture { get; set; }

        // Relación 1:N
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
