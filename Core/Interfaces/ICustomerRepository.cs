using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Core.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task AddCustomer(Customer customer);
        Task UpdateCustomer(int id, Customer customer);
        Task DeleteCustomer(int id);
        Task<IEnumerable<Project>> GetProjectsForCustomer(int customerId);
        Task<IEnumerable<Invoice>> GetInvoiceForCustomer(int customerId);
        Task AssignProjectToCustomer(int customerId, int projectId);
        Task UnassignProjectToCustomer(int customerId, int projectId);
    }
}
