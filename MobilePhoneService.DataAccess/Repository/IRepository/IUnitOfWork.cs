using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IManufacturerIRepository Manufacturer { get; }
        IOperatingSystemRepository OperatingSystem { get; }
        ICpuRepository Cpu { get; }
        IPhoneSpecificationRepository PhoneSpecification { get; }
        IPhoneModelRepository PhoneModel { get; }
        IServiceRepository Service { get; }
        IClientRepository Client { get; }
        IRequestForRepairRepository RequestForRepair { get; }
        IHistoryOfRepairRepository HistoryOfRepair { get; }
        void Save();
    }
}
