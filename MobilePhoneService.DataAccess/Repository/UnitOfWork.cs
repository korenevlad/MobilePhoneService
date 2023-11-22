using MobilePhoneService.DataAccess.Date;
using MobilePhoneService.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.DataAccess.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private ApplicationDbContext _db;

        public IManufacturerIRepository Manufacturer { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Manufacturer = new ManufacturerRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
