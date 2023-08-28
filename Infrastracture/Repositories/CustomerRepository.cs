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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomer(int id,Customer customer)
        {
           
           _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }
        

        public async Task DeleteCustomer(int id)
        {
            var customerToDelete = await _context.Customers.FindAsync(id);
            if (customerToDelete != null)
            {
                _context.Customers.Remove(customerToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Project>> GetProjectsForCustomer(int customerId)
        {
            return await _context.Projects
                .Include(p=>p.Customer)
                .Where(p => p.CustomerId == customerId).ToListAsync();
        }

        public async Task AssignProjectToCustomer(int customerId, int projectId)
        {
            var project = _context.Projects.FindAsync(projectId);
            if(project.Result != null)
            {
                project.Result.CustomerId = customerId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UnassignProjectToCustomer(int customerId, int projectId)
        {
            var project = _context.Projects.FindAsync(projectId);
            if(project.Result != null && project.Result.CustomerId == customerId)
            {
                project.Result.CustomerId = null;
                await _context.SaveChangesAsync();
            }
        }
    }
}
