using API.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcontractorsController : Controller
    {
        private readonly ISubcontractorRepository _subcontractorRepository;
        private readonly ILogger<SubcontractorsController> _logger;

        public SubcontractorsController(ISubcontractorRepository subcontractorRepository, ILogger<SubcontractorsController> logger)
        {
            _subcontractorRepository = subcontractorRepository;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Task<IEnumerable<Subcontractor>>))]
        public async Task<IActionResult> GetSubcontractors()
        {
            try
            { 
                var subcontractors = await _subcontractorRepository.GetAllSubcontractors();
                if(subcontractors.Any())
                {
                    return Ok(subcontractors);
                }
                return NotFound();
                
            }
            catch (Exception)
            {
                _logger.LogError($"Something went wrong in retrieving Subcontractors data");
                return StatusCode(500, "Internal server error");
            }
            
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubcontractorById(int id)
        {
            try
            {
                var subcontractor = await _subcontractorRepository.GetSubcontractorById(id);
                if(subcontractor == null)
                {
                    _logger.LogWarning($"Cannot find subcontractor with id : {id}");
                    return NotFound($"Cannot find subcontractor with id : {id}");
                }
                return Ok(subcontractor);
            }
            catch (Exception)
            {
                _logger.LogError($"Something went wrong in retrieving Subcontractors data");
                return StatusCode(500, "Internal server error");
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> AddSubcontractor([FromBody] Subcontractor subcontractor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                if(subcontractor == null)
                {
                    return BadRequest("Object Subcontractor is null");
                }
                _subcontractorRepository.AddSubcontractor(subcontractor);
                return CreatedAtAction(nameof(GetSubcontractorById), new { id = subcontractor.SubcontractorId }, subcontractor);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AddSubcontractor action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubcontractor(int id,[FromBody] SubcontractorUpdateDto subcontractorDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                if (subcontractorDto == null)
                {
                    return BadRequest("Object Subcontractor is null");
                }
                var subcontractor = await _subcontractorRepository.GetSubcontractorById(id);
                if (subcontractor == null)
                {
                    _logger.LogWarning($"Cannot find subcontractor with id : {id}");
                    return NotFound($"Cannot find subcontractor with id : {id}");
                }
                subcontractor.CompanyName = subcontractorDto.CompanyName;
                subcontractor.ContactName = subcontractorDto.ContactName;
                subcontractor.PhoneNumber = subcontractorDto.PhoneNumber;
                subcontractor.NIP = subcontractorDto.NIP;
                subcontractor.Email = subcontractorDto.Email;
                await _subcontractorRepository.UpdateSubcontractor(subcontractor);
                return Ok(subcontractor);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the UpdateSubcontractor action: {ex}");
                return StatusCode(500, "Internal server error");
            }
           


        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubcontractor(int id)
        {
            try
            {
                await _subcontractorRepository.DeleteSubcontractor(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the DeleteSubcontractor action: {ex}");
                return StatusCode(500, "Internal server error");
            }
            
        }
    }
}
