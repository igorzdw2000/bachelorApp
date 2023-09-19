using API.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ICustomerRepository _customerRepository;

        public ProjectsController(IProjectRepository projectRepository, ICustomerRepository customerRepository)
        {
            _projectRepository = projectRepository;
            _customerRepository = customerRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Project>))]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _projectRepository.GetProjectsAsync();
            return Ok(projects);
        }
        [HttpGet("{projectId}")]
        [ProducesResponseType(200, Type = typeof(Project))]
        public async Task<IActionResult> GetProject(int projectId)
        {
            var project = await _projectRepository.GetProjectByIdAsync(projectId);
            return Ok(project);
        }
        //[HttpGet("{projectId}/materials")]
        //public async Task<IActionResult> GetMaterialUsedInProject(int projectId)
        //{
        //    try
        //    {
        //        JsonSerializerOptions options = new JsonSerializerOptions
        //        {
        //            ReferenceHandler = ReferenceHandler.Preserve
        //        };
        //        var materials = await _projectRepository.GetMaterialsUsedInProject(projectId);
        //        if (materials == null)
        //        {
        //            return NotFound("Project with this id was not found");
        //        }
        //        return Ok(JsonSerializer.Serialize(materials, options));
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "Internal server error");
        //    }

        //}
        //[HttpGet("{projectId}/material-cost")]
        //public async Task<IActionResult> GetMaterialCostInProject(int projectId)
        //{
        //    try
        //    {
        //        var material = _projectRepository.CalculateProjectMaterialCosts(projectId);
        //        if (material == null)
        //        {
        //            return NotFound("Project with this id was not found");
        //        }
        //        return Ok(material.CalculatedCost);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Internal server error");
        //    }
        //}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProjectValue(int id,[FromBody]double projectValue)
        {
            var existingProject = await _projectRepository.GetProjectByIdAsync(id);
            if (existingProject != null)
            {
                await _projectRepository.UpdateProjectValue(id, projectValue);
                return Ok("Project value was successfuly updated");
            }
            else
            {
                return NotFound("Project with this id was not found");
            }
            
            
        }
        [HttpPost]
        public async Task<IActionResult> AddProject([FromBody] ProjectAddDto projectDto)
        {
            var customer = _customerRepository.GetCustomerByIdAsync(projectDto.CustomerId);
            if(customer.Result != null)
            {
                var project = new Project
                {
                    Name = projectDto.Name,
                    Description = projectDto.Description,
                    Street = projectDto.Street,
                    City = projectDto.City,
                    StartDate = projectDto.StartDate,
                    EndDate = projectDto.EndDate,
                    ProjectValue = projectDto.ProjectValue,
                    Customer = customer.Result

                };
                await _projectRepository.AddProject(project);
                return Ok("Project successfuly added");
            }
            return BadRequest();
            
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var existingProject = await _projectRepository.GetProjectByIdAsync(id);
            if (existingProject != null)
            {
                await _projectRepository.RemoveProject(id);
                return Ok("Project successfuly removed");
            }
            return NotFound("Project with this id was not found");
            
        }
       

    }
}
