using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities.Concrete
{
    public class AppUser: IdentityUser
    {
        [Required(ErrorMessage = "Bu alan boş bırakılmamalı..")]
        public string FullName { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string FullAdress { get; set; }

        [NotMapped]
        public string Role { get; set; }

    }
}
