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
        public IOperatingSystemRepository OperatingSystem { get; private set; }
        public ICpuRepository Cpu { get; private set; }
        public IPhoneSpecificationRepository PhoneSpecification { get; private set; }
        public IPhoneModelRepository PhoneModel { get; private set; }
        public IServiceRepository Service { get; private set; }
        public IClientRepository Client { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            Manufacturer = new ManufacturerRepository(_db);
            OperatingSystem = new OperatingSystemRepository(_db);
            Cpu = new CpuRepository(_db);
            PhoneSpecification = new PhoneSpecificationRepository(_db);
            PhoneModel = new PhoneModelRepository(_db);
            Service = new ServiceRepository(_db);
            Client = new ClientRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
