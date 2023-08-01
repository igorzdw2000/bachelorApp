using Core.Entities;
using Core.Interfaces;
using Infrastracture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context) 
        {
            _context = context;
        }

        public bool AddProject(Project project)
        {
            if(project !=  null)
            {
                if(!CheckIfProjectExists(project.ProjectId))
                {
                    _context.Projects.Add(project);
                }
            }
            return _context.SaveChanges() > 1 ? true : false;
        }

        public void AssignCustomerToProject(int projectId, int customerId)
        {
            if(!CheckIfProjectExists(projectId))
            {
                var customerToAssign = _context.Customers.FirstOrDefault(p=>p.CustomerId==customerId);
                if(customerToAssign != null)
                {
                    var project = _context.Projects.FirstOrDefault(p=>p.ProjectId==projectId);
                    project.Customer = customerToAssign;
                    project.CustomerId = customerToAssign.CustomerId;
                    _context.SaveChanges();
                }
            }
        }

        public bool CheckIfProjectExists(int id)
        {
            return _context.Projects.Any(p=>p.ProjectId == id);
        }

        public Project GetProject(int id)
        {
            return _context.Projects
                .Include(p => p.Customer)
                .FirstOrDefault(p=>p.ProjectId==id);
        }

        public List<Project> GetProjects()
        {
            return _context.Projects
                .Include(p=>p.Customer)
                .OrderBy(o=>o.CustomerId).ToList();
        }

        public List<Project> GetProjectsByCustomerName(string name)
        {
            return _context.Projects.Where(p=>p.Customer.Name == name).ToList();
        }

        public bool RemoveProject(int id)
        {
            var projectToDelete = _context.Projects.FirstOrDefault(p => p.ProjectId == id);
            if(projectToDelete != null)
            {
                _context.Projects.Remove(projectToDelete);
            }
            return _context.SaveChanges() > 1 ? true : false;
        }


        public bool UpdateProjectEndDate(int id, DateTime newEndDate)
        {
            if (CheckIfProjectExists(id))
            {
                var projectForUpdate = _context.Projects.Where(p => p.ProjectId == id).FirstOrDefault();
                if (projectForUpdate != null)
                {
                    projectForUpdate.EndDate = newEndDate;
                }
            }
            return _context.SaveChanges() > 1 ? true : false;
        }


        public void UpdateProjectValue(int id, double value)
        {
            var projectForUpdate = _context.Projects.FirstOrDefault(p=>p.ProjectId==id);
            if (projectForUpdate != null)
            {
                projectForUpdate.ProjectValue = value;
            }
            _context.SaveChanges();
        }
    }
}
