using eCommerce.Data.Repo.Abstract;
using eCommerce.Entities.Concrete;
using eCommerceApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Data.Repo.Concrete
{
    public class ProductRepo : Repo<Product> , IProductRepo
    {
        private readonly ApplicationDbContext _context;

        public ProductRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
