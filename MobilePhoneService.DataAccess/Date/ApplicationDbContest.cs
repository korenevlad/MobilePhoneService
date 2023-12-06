using Microsoft.EntityFrameworkCore;
using MobilePhoneService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.DataAccess.Date
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        DbSet<Manufacturer> Manufacturer { get; set; }
        DbSet<Operating_system> Operating_system { get; set; }
        DbSet<Cpu> Cpu { get; set; }
        DbSet<Phone_specification> Phone_Specification { get; set; }
        DbSet<Phone_model> Phone_Model { get; set; }
        DbSet<Service> Service { get; set; }
        DbSet<Client> Client { get; set; }
        DbSet<Request_for_repair> Request_for_repair { get; set; }
        DbSet<History_of_repair> History_of_repair { get; set; }
    }
}
