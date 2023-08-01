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

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            try
            {
                var customers = _customerRepository.GetCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Wystąpił błąd podczas pobierania listy klientów: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            try
            {
                var customer = _customerRepository.GetCustomerById(id);
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
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _customerRepository.AddCustomer(customer);
                return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Wystąpił błąd podczas dodawania klienta: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] Customer customer)
        {
            try
            {
                //var existingCustomer = _customerRepository.GetCustomerById(id);
                //if (existingCustomer == null)
                //{
                //    return BadRequest("Niepoprawny identyfikator klienta.");
                //}

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                customer.CustomerId = id;
                _customerRepository.UpdateCustomer(id,customer);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Wystąpił błąd podczas aktualizowania klienta: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                var existingCustomer = _customerRepository.GetCustomerById(id);
                if (existingCustomer == null)
                {
                    return NotFound();
                }

                _customerRepository.DeleteCustomer(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Wystąpił błąd podczas usuwania klienta: {ex.Message}");
            }
        }
        [HttpGet("{id}/projects")]
        public IActionResult GetProjectAssignedToCustomer(int id)
        {
            var projects = _customerRepository.GetProjectsForCustomer(id);
            return Ok(projects);
        }


    }
}
