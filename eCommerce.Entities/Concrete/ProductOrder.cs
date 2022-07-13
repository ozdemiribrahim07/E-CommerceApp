﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities.Concrete
{
    public class ProductOrder
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public double OrderPrice { get; set; }
        public string AppUserId { get; set; }
        public string Phone { get; set; }
        public string FullAdress { get; set; }
        public string Name { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }

    }
}
