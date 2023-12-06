﻿using MobilePhoneService.DataAccess.Date;
using MobilePhoneService.DataAccess.Repository.IRepository;
using MobilePhoneService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhoneService.DataAccess.Repository
{
    public class RequestForRepairRepository: Repository<Request_for_repair>, IRequestForRepairRepository
    {
        private readonly ApplicationDbContext _db;
        public RequestForRepairRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
