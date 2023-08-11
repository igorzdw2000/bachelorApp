using Core.Entities;
using Core.Interfaces;
using Infrastracture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Infrastracture.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task AddProject(Project project)
        {
            if(project !=  null)
            {
                 _context.Projects.Add(project);
               
            }
            _context.SaveChanges();
        }

        public async Task AssignCustomerToProject(int projectId, int customerId)
        {
            if(!CheckIfProjectExists(projectId))
            {
                var customerToAssign = await _context.Customers.FirstOrDefaultAsync(p=>p.CustomerId==customerId);
                if(customerToAssign != null)
                {
                    var project = await _context.Projects.FirstOrDefaultAsync(p=>p.ProjectId==projectId);
                    project.Customer = customerToAssign;
                    project.CustomerId = customerToAssign.CustomerId;
                    _context.SaveChanges();
                }
            }
        }

        public CalculatedMaterialCost CalculateProjectMaterialCosts(int projectId)
        {
            var project = _context.ProjectMaterials
                .Where(pm=>pm.PId ==projectId)
                .Select(pm => new
                {
                    MaterialCost = pm.Quantity * pm.MaterialSupplier.UnitPrice
                })
                .ToList();

            if (project == null)
            {
                throw new ArgumentException("Project was not found");
            }
            var totalCost = project.Sum(pm => pm.MaterialCost);

            return new CalculatedMaterialCost { CalculatedCost = (decimal)totalCost };
        }

        public bool CheckIfProjectExists(int id)
        {
            return _context.Projects.Any(p=>p.ProjectId == id);
        }

        public async Task<IEnumerable<ProjectMaterial>> GetMaterialsUsedInProject(int projectId)
        {
            var project = await _context.ProjectMaterials
                .Include(p => p.MaterialSupplier)
                .Where(pm => pm.PId == projectId)
                .ToListAsync();
            return project;
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(p=>p.ProjectId==id);
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            return await _context.Projects
                .Include(p=>p.Customer)
                .OrderBy(o=>o.CustomerId).ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetProjectsByCustomerName(string name)
        {
            return await _context.Projects.Where(p=>p.Customer.Name == name).ToListAsync();
        }

        public async Task RemoveProject(int id)
        {
            var projectToDelete = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);
            if(projectToDelete != null)
            {
                _context.Projects.Remove(projectToDelete);
            }
            _context.SaveChanges();
        }


        public async Task UpdateProjectEndDate(int id, DateTime newEndDate)
        {
            if (CheckIfProjectExists(id))
            {
                var projectForUpdate = _context.Projects.Where(p => p.ProjectId == id).FirstOrDefaultAsync();
                if (projectForUpdate.Result != null)
                {
                    projectForUpdate.Result.EndDate = newEndDate;
                }
            }
            _context.SaveChanges();
        }


        public async Task UpdateProjectValue(int id, double value)
        {
            var projectForUpdate = _context.Projects.FirstOrDefaultAsync(p=>p.ProjectId==id);
            if (projectForUpdate.Result != null)
            {
                projectForUpdate.Result.ProjectValue = value;
            }
            _context.SaveChanges();
        }
    }
}
