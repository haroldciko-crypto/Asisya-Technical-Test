using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asisya.Domain.Entities
{
    public class Order
    {
        public int OrderID { get; set; }

        public string CustomerID { get; set; } = string.Empty;

        public int EmployeeID { get; set; }

        public int ShipVia { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public Customer Customer { get; set; } = null!;

        public Employee Employee { get; set; } = null!;

        public Shipper Shipper { get; set; } = null!;

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
