using eCommerce.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities.VMs
{
    public class OrderVM
    {
        public ProductOrder ProductOrder { get; set; }
        public IEnumerable<OrderDetail> OrderDetail { get; set; }

    }
}
