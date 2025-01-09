using DataLayer.Context;
using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly ApplicationDbContext _context;
        

        public IdentityService(ApplicationDbContext context)
        {
            _context = context;
        }

    }
}
