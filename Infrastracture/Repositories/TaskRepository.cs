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
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task AddTaskAsync(Core.Entities.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteTaskAsync(int id)
        {
            var taskToDelete = await _context.Tasks.FirstOrDefaultAsync(x => x.TaskId == id);
            if (taskToDelete != null)
            {
                _context.Tasks.Remove(taskToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Core.Entities.Task>> GetAllTasksAsync()
        {
            return await _context.Tasks
                .Include(p=>p.Project)
                .ToListAsync();
        }

        public async Task<Core.Entities.Task> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks
                .Include(p=>p.Project)
                .FirstOrDefaultAsync(x=>x.TaskId == id);
        }

        public async Task<IEnumerable<Core.Entities.Task>> GetTasksByProjectIdAsync(int projectId)
        {
            return await _context.Tasks.Where(t => t.ProjectId == projectId)
                .Include(p=>p.Project)
                .Include(p=>p.Project.Customer)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task UpdateTaskAsync(Core.Entities.Task task)
        {
            var tastToUpdate = await _context.Tasks.FirstOrDefaultAsync(x=>x.TaskId == task.TaskId);
            if (tastToUpdate != null)
            {
                _context.Tasks.Update(tastToUpdate);
                _context.SaveChanges();
            }
        }
    }
}
