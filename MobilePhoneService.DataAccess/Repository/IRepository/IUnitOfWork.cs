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
        void Save();
    }
}
