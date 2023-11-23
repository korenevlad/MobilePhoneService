using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T: class
    {
        void Add(T obj);
        void Remove(T obj);
        IEnumerable<T> GetAll(string? includeProperties = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);

        void Update(T obj);
    }
}
