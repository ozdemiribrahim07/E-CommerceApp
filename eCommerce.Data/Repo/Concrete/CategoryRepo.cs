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
    public class CategoryRepo : Repo<Category>, ICategoryRepo
    {

        private readonly ApplicationDbContext _context;

        public CategoryRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
