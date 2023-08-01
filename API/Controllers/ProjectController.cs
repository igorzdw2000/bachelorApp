using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Project>))]
        public IActionResult GetProjects()
        {
            var projects = _projectRepository.GetProjects();
            return Ok(projects);
        }
        [HttpGet("{projectId}")]
        [ProducesResponseType(200,Type = typeof(Project))]
        public IActionResult GetProject(int projectId)
        {
            var project = _projectRepository.GetProject(projectId);
            return Ok(project);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProjectValue(int id,[FromBody]double projectValue)
        {
            var existingProject = _projectRepository.GetProject(id);
            if (existingProject != null)
            {
                _projectRepository.UpdateProjectValue(id, projectValue);
                return Ok("Project value was successfuly updated");
            }
            else
            {
                return NotFound("Project with this id was not found");
            }
            
            
        }
        [HttpPost]
        public IActionResult AddProject([FromBody] Project project)
        {
            _projectRepository.AddProject(project);
            return Ok("Project successfuly added");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            var existingProject = _projectRepository.GetProject(id);
            if (existingProject != null)
            {
                _projectRepository.RemoveProject(id);
                return Ok();
            }
            return NotFound("Project with this id was not found");
            
        }
        [HttpPut("{projectId}/assign-customer/{customerId}")]
        public IActionResult AssignCustomerToProject(int projectId, int customerId)
        {
            var existingProject = _projectRepository.GetProject(projectId);
            if (existingProject != null)
            {
                _projectRepository.AssignCustomerToProject(projectId, customerId);
                return Ok("Customer successfuly assigned to project");
            }
            return NotFound("Cannot find Project with this id");
        }
    }
}
