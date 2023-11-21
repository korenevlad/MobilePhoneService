using Microsoft.EntityFrameworkCore;
using MobilePhoneService.DataAccess.Date;
using MobilePhoneService.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.DataAccess.Repository
{
    public class Repository<T>: IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> _dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = _dbSet;
            return query;
        }
    }
}
