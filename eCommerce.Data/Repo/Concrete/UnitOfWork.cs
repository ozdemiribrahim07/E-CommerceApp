using eCommerce.Data.Repo.Abstract;
using eCommerceApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Data.Repo.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IAppUserRepo AppUser => new AppUserRepo(_context);

        public ICategoryRepo Category => new CategoryRepo(_context);

        public IOrderDetailRepo OrderDetail => new OrderDetailRepo(_context);

        public IProductRepo Product => new ProductRepo(_context);

        public IProductOrderRepo ProductOrder => new ProductOrderRepo(_context);

        public IShopCartRepo ShopCart => new ShopCartRepo(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
