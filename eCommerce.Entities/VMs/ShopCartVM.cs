using eCommerce.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities.VMs
{
    public class ShopCartVM
    {
        public IEnumerable<ShopCart> CartList { get; set; }
        public ProductOrder ProductOrder { get; set; }

    }
}
