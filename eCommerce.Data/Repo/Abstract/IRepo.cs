using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Data.Repo.Abstract
{
    public interface IRepo<T> where T : class
    {
        void Add(T t);
        void Update(T t);
        void Remove(T t);
        void RemoveRange(IEnumerable<T> entities);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? include = null);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? include = null);

    }
}
