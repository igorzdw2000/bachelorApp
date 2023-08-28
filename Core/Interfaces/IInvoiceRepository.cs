using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Core.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<IEnumerable<Invoice>> GetInvoices();
        Task<Invoice> GetInvoiceById(int id);
        Task AddInvoice(Invoice invoice);
        Task UpdateInvoice(Invoice invoice);
        
        Task<IEnumerable<Invoice>> GetInvoiceForCustomer(int customerId);
        Task<IEnumerable<Invoice>> GetInvoicesForMaterialSupplier(int materialSupplierId);
        Task<IEnumerable<Invoice>> GetInvoicesForProject(int projectId);

        
    }
}
