using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/{controller}")]
    public class ServicesController : Controller
    {
        private readonly IServiceRepository _serviceRepository;

        public ServicesController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetServices() 
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var services = _serviceRepository.GetServices();
            return Ok(services);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();

            }
            var service = _serviceRepository.GetServiceById(id);
            return Ok(service);
        }
    }
}
