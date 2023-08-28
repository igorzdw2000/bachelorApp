using Core.Entities;
using Core.Interfaces;
using Infrastracture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Infrastracture.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _context;

        public ServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddService(Service service)
        {
            if(service == null) throw new ArgumentNullException(
                nameof(service));
            _context.Services.Add(service);
            _context.SaveChanges();

        }

        public async Task DeleteService(int id)
        {
            var serviceToDelete = await _context.Services.FindAsync(id);
            if(serviceToDelete != null)
            {
                _context.Services.Remove(serviceToDelete);
                _context.SaveChanges();
            }
        }

        public async Task<Service> GetServiceById(int id)
        {
            var service = await _context.Services.FindAsync(id);
            return service;
        }

        public async Task<Service> GetServiceByName(string name)
        {
            var service = await _context.Services.FirstOrDefaultAsync(x => x.Name == name);
            return service;
        }

        public async Task<IEnumerable<Service>> GetServices()
        {
            if( _context.Services == null )
            {
                return Enumerable.Empty<Service>();
            }
            return await _context.Services.ToListAsync();
        }

        public async Task UpdateService(Service service)
        {
            if(service == null) throw new ArgumentNullException(nameof(service));
            _context.Services.Update(service);
            _context.SaveChanges();
        }
    }
}
