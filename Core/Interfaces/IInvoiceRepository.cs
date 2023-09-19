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
        Task<IEnumerable<InvoiceDetail>> GetInvoiceDetails(int invoiceId);
        Task<Invoice> GetInvoiceByName(string name);
        Task<IEnumerable<Invoice>> GetInvoicesByDate(DateTime date);
        Task AddInvoice(Invoice invoice);
        Task UpdateInvoice(Invoice invoice);
        
        
        Task<IEnumerable<Invoice>> GetInvoicesForProject(int projectId);
        Task<IEnumerable<Invoice>> GetInvoicesForMaterialsForProject(int projectId);
        bool isEditable(int id);

        
    }
}
