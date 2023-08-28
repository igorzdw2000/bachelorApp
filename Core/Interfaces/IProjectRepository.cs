using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Core.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetProjectsAsync();
        Task<Project> GetProjectByIdAsync(int id);
        Task<IEnumerable<Project>> GetProjectsByCustomerName(string name);
        Task AddProject(Project project);
        Task RemoveProject(int id);
        Task UpdateProjectValue(int id,double value);
        Task UpdateProjectEndDate(int id, DateTime newEndDate);
        Task AssignCustomerToProject(int projectId, int customerId);
        bool CheckIfProjectExists(int id);
        CalculatedMaterialCost CalculateProjectMaterialCosts(int projectId);
        Task<IEnumerable<ProjectMaterial>> GetMaterialsUsedInProject(int projectId);
        
        
    }
}
