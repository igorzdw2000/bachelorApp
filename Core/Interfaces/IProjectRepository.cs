using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProjectRepository
    {
        List<Project> GetProjects();
        Project GetProject(int id);
        List<Project> GetProjectsByCustomerName(string name);
        bool AddProject(Project project);
        bool RemoveProject(int id);
        void UpdateProjectValue(int id,double value);
        bool UpdateProjectEndDate(int id, DateTime newEndDate);
        void AssignCustomerToProject(int projectId, int customerId);
        bool CheckIfProjectExists(int id);
        
    }
}
