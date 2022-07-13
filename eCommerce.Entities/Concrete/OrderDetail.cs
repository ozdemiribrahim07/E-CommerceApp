using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities.Concrete
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public int ProductId { get; set; }
        public int ProductOrderId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("ProductOrderId")]
        public ProductOrder ProductOrder { get; set; }

      

    }
}
