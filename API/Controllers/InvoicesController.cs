using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Runtime.CompilerServices;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : Controller
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProjectRepository _projectRepository;

        public InvoicesController(IInvoiceRepository invoiceRepository, IMapper mapper, ICustomerRepository customerRepository, IProjectRepository projectRepository)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
            _customerRepository = customerRepository;
            _projectRepository = projectRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetInvoices()
        {
            try
            {
                var invoices = await _invoiceRepository.GetInvoices();
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occured during retrieving invoices: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoicesById(int id)
        {
            try
            {
                var invoice = await _invoiceRepository.GetInvoiceById(id);
                return Ok(invoice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occured during retrieving invoice: {ex.Message}");
            }
        }
        [HttpGet("{invoiceId}/details")]
        public async Task<IActionResult> GetInvoiceDetails(int invoiceId)
        {
            try
            {
                var details = await _invoiceRepository.GetInvoiceDetails(invoiceId);

                return Ok(_mapper.Map<IEnumerable<InvoiceDetail>, IEnumerable<InvoiceDetailDto>>(details));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occured during retrieving invoice details: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceAddDto invoiceDto)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerByIdAsync(invoiceDto.CustomerId);
                var project = await _projectRepository.GetProjectByIdAsync(invoiceDto.ProjectId);
                if (customer != null && project != null)
                {
                    var invoice = new Invoice
                    {
                        InvoiceNumber = invoiceDto.InvoiceNumber,
                        CreationDate = invoiceDto.CreationDate,
                        SaleDate = invoiceDto.SaleDate,
                        DateOfPayment = invoiceDto.DateOfPayment,
                        PaymentStatus = invoiceDto.PaymentStatus,
                        PaymentMethod = invoiceDto.PaymentMethod,
                        Comments = invoiceDto.Comments,
                        Customer = customer,
                        Project = project,
                        InvoiceType = invoiceDto.InvoiceType
                    };
                    await _invoiceRepository.AddInvoice(invoice);
                    return Ok();
                }
                return NotFound();

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occuered during creating invoice: {ex.Message}");
            }
        }
        [HttpPut("{id}/publish")]
        public async Task<IActionResult> PublishInvoice(int id)
        {
            try
            {
                var invoice = await _invoiceRepository.GetInvoiceById(id);
                if (invoice != null)
                {
                    invoice.Published = true;
                    await _invoiceRepository.UpdateInvoice(invoice);
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occured during publishing invoice: {ex.Message}");
                throw;
            }
        }
       
    }
}
