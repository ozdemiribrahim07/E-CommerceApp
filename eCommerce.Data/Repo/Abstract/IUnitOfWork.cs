using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Data.Repo.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IAppUserRepo AppUser { get; }
        ICategoryRepo Category { get; }
        IOrderDetailRepo OrderDetail { get; }
        IProductRepo Product { get; }
        IProductOrderRepo ProductOrder { get; }
        IShopCartRepo ShopCart { get; }

        void Save();

    }
}
