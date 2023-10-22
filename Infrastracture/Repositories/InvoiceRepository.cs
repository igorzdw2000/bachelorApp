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
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;

        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddInvoice(Invoice invoice)
        {
            if(invoice == null) throw new ArgumentNullException(nameof(invoice));
            _context.Invoices.Add(invoice);
            _context.SaveChanges();
        }

        public async Task<Invoice> GetInvoiceById(int id)
        {
            return await _context.Invoices.FirstOrDefaultAsync(x => x.InvoiceId == id);
        }

        public async Task<Invoice> GetInvoiceByName(string name)
        {
            return await _context.Invoices.FindAsync(name);
        }

        public async Task<IEnumerable<InvoiceDetail>> GetInvoiceDetails(int invoiceId)
        {
            return await _context.InvoiceDetails
                .Include(p=>p.Invoice)
                .Where(p=>p.InvoiceId==invoiceId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Invoice>> GetInvoiceForCustomer(int customerId)
        {
            return await _context.Invoices
               .Include(p => p.Customer)
               .Include(c => c.Project)
               .Where(c=>c.CustomerId==customerId)
               .ToListAsync();
        }

        public async Task<IEnumerable<Invoice>> GetInvoices()
        {
            return await _context.Invoices
                .Include(p=>p.Customer)
                .Include(c=>c.Project)
                .ToListAsync();
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesByDate(DateTime date)
        {
            return await _context.Invoices
                .Include(c=>c.Customer)
                .Include(c=>c.Project)
                .Where (c=>c.CreationDate == date.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesForMaterialsForProject(int projectId)
        {
            var query = from invoice in _context.Invoices
                        where invoice.ProjectId == projectId && invoice.InvoiceType == InvoiceType.MaterialSupplier
                        select invoice;
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesForProject(int projectId)
        {
            var query = from invoice in _context.Invoices
                        where invoice.ProjectId == projectId
                        select invoice;
            return await query.ToListAsync();
        }

        public bool isEditable(int id)
        {
            var invoice = _context.Invoices
                    .Where(x=>x.Published == true)
                    .FirstOrDefault(x=>x.InvoiceId == id);
            if(invoice != null)
                return false;
            return true;
        }


        public async Task UpdateInvoice(Invoice invoice)
        {
            if(invoice == null) throw new ArgumentNullException(nameof(invoice));
            if(isEditable(invoice.InvoiceId))
            {
            invoice.Published = true;
                _context.Invoices.Update(invoice);
                _context.SaveChanges();
            }
            
        }
    }
}
