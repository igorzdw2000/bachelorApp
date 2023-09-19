using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : Controller
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _customerRepository.GetCustomersAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Wystąpił błąd podczas pobierania listy klientów: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerByIdAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Wystąpił błąd podczas pobierania klienta: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _customerRepository.AddCustomer(customer);
                return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Wystąpił błąd podczas dodawania klienta: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer customer)
        {
            try
            {
                //var existingCustomer = await _customerRepository.GetCustomerByIdAsync(id);
                //if (existingCustomer == null)
                //{
                //    return BadRequest("Niepoprawny identyfikator klienta.");
                //}

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                customer.CustomerId = id;
                await _customerRepository.UpdateCustomer(id,customer);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Wystąpił błąd podczas aktualizowania klienta: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                var existingCustomer = await _customerRepository.GetCustomerByIdAsync(id);
                if (existingCustomer == null)
                {
                    return NotFound();
                }

                await _customerRepository.DeleteCustomer(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Wystąpił błąd podczas usuwania klienta: {ex.Message}");
            }
        }
        [HttpGet("{id}/projects")]
        public async Task<IActionResult> GetProjectAssignedToCustomer(int id)
        {
            var projects = await _customerRepository.GetProjectsForCustomer(id);
            return Ok(projects);
        }
        [HttpGet("{id}/invoices")]
        public async Task<IActionResult> GetCustomerInvoices(int id)
        {
            try
            {
                var invoices = await _customerRepository.GetInvoiceForCustomer(id);
                var mapped = _mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceToReturnDto>>(invoices);
                return Ok(mapped);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Wystąpił błąd podczas pobierania faktury: {ex.Message}");
            }
            
        }
        [HttpPost("{customerId}/projects/{projectId}/assign")]
        public async Task<IActionResult> AssignProjectToCustomer(int customerId, int projectId)
        {
            try
            {
                await _customerRepository.AssignProjectToCustomer(customerId, projectId);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An error occured during assign project to customer: {ex.Message}");
            }
        }
        [HttpPost("{customerId}/projects/{projectId}/unassign")]
        public async Task<IActionResult> UnassignProjectToCustomer(int customerId, int projectId)
        {
            try
            {
                await _customerRepository.UnassignProjectToCustomer(customerId, projectId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured during unassign project to customer: {ex.Message}");
            }
        }
        



    }
}
