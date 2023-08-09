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
    public class SubcontractorRepository : ISubcontractorRepository
    {
        private readonly AppDbContext _context;

        public SubcontractorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task AddSubcontractor(Subcontractor subcontractor)
        {
            _context.Subcontractors.Add(subcontractor);
            _context.SaveChanges();
        }

        public async System.Threading.Tasks.Task DeleteSubcontractor(int id)
        {
            var subcontractorToDelete = await _context.Subcontractors.FirstOrDefaultAsync(p=>p.SubcontractorId == id);
            if(subcontractorToDelete != null)
            {
                _context.Subcontractors.Remove(subcontractorToDelete);
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<Subcontractor>> GetAllSubcontractors()
        {
            return await _context.Subcontractors.ToListAsync();
        }

        public async Task<Subcontractor> GetSubcontractorById(int id)
        {
            return await _context.Subcontractors.FirstOrDefaultAsync(p => p.SubcontractorId == id);
        }

        public async System.Threading.Tasks.Task UpdateSubcontractor(Subcontractor subcontractor)
        {
            var existingSubcontractor = await _context.Subcontractors.FirstOrDefaultAsync(p=>p.SubcontractorId==subcontractor.SubcontractorId);
            if(existingSubcontractor != null)
            {
                _context.Subcontractors.Update(existingSubcontractor);
                _context.SaveChanges();
            }
        }
    }
}
