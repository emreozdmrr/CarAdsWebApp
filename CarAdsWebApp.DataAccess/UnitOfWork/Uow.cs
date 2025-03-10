using CarAdsWebApp.DataAccess.Contexts;
using CarAdsWebApp.DataAccess.Interfaces;
using CarAdsWebApp.DataAccess.Repositories;
using CarAdsWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.DataAccess.UnitOfWork
{
    public class Uow:IUow
    {
        private readonly Context _context;

        public Uow(Context context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return new Repository<T>(_context);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
