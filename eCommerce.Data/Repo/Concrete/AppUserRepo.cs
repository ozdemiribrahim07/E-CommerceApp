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
    public class AppUserRepo : Repo<AppUser> , IAppUserRepo
    { 
        private readonly ApplicationDbContext _context;

        public AppUserRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
