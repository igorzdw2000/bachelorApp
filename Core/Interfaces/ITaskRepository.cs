using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Core.Entities.Task>> GetAllTasksAsync();
        Task<Core.Entities.Task> GetTaskByIdAsync(int id);
        Task AddTaskAsync(Core.Entities.Task task);
        Task UpdateTaskAsync(Core.Entities.Task task);
        Task DeleteTaskAsync(int id);
        Task<IEnumerable<Core.Entities.Task>> GetTasksByProjectIdAsync(int projectId);
    }
}
