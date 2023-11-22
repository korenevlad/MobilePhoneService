using MobilePhoneService.DataAccess.Date;
using MobilePhoneService.DataAccess.Repository.IRepository;
using MobilePhoneService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.DataAccess.Repository
{
    public class OperatingSystemRepository: Repository<Operating_system>, IOperatingSystemRepository
    {
        private readonly ApplicationDbContext _db;
        public OperatingSystemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
