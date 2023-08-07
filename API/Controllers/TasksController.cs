using API.Dtos;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;

        public TasksController(ITaskRepository taskRepository, IProjectRepository projectRepository)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetTasks() 
        {
            var tasks = await _taskRepository.GetAllTasksAsync();
            return Ok(tasks);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTasksById(int id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);
            return Ok(task);
        }
        [HttpGet("projects/{projectId}")]
        public async Task<IActionResult> GetTaskByProjectId(int projectId)
        {
            var tasks = await _taskRepository.GetTasksByProjectIdAsync(projectId);
            return Ok(tasks);
        }
        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] TaskAddDto taskToAddDto)
        {
            var project = _projectRepository.GetProjectByIdAsync(taskToAddDto.ProjectId);
            if(project == null)
            {
                return NotFound("Project with specified id was not found");
            }
            var task = new Core.Entities.Task
            {
                TaskName = taskToAddDto.TaskName,
                TaskDescription = taskToAddDto.TaskDescription,
                StartDate = taskToAddDto.StartDate,
                EndDate = taskToAddDto.EndDate,
                Project = project.Result
            };
            await _taskRepository.AddTaskAsync(task);
            return CreatedAtAction(nameof(GetTasksById), new { id = task.TaskId }, task);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id,[FromBody] TaskUpdateDto taskToUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var task = _taskRepository.GetTaskByIdAsync(id);
            if (task.Result == null)
            {
                return NotFound("Task with specified id was not found");
            }
            task.Result.TaskName = taskToUpdateDto.TaskName;
            task.Result.TaskDescription = taskToUpdateDto.TaskDescription;
            task.Result.StartDate = taskToUpdateDto.StartDate;
            task.Result.EndDate = taskToUpdateDto.EndDate;
            
            await _taskRepository.UpdateTaskAsync(task.Result);
            return Ok(task.Result);
        }
        
    }
}
