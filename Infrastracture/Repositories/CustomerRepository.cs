using Core.Entities;
using Core.Interfaces;
using Infrastracture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customer GetCustomerById(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.CustomerId == id);
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void UpdateCustomer(int id,Customer customer)
        {
            //var customerForUpdate = _context.Customers.Find(customer);
            //if (customerForUpdate != null)
            //{
                _context.Customers.Update(customer);
            //}
            _context.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            var customerToDelete = _context.Customers.Find(id);
            if (customerToDelete != null)
            {
                _context.Customers.Remove(customerToDelete);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Project> GetProjectsForCustomer(int customerId)
        {
            return _context.Projects
                .Include(p=>p.Customer)
                .Where(p => p.CustomerId == customerId).ToList();
        }
    }
}
