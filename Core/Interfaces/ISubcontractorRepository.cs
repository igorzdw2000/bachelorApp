using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ISubcontractorRepository
    {
        Task<IEnumerable<Subcontractor>> GetAllSubcontractors();
        Task<Subcontractor> GetSubcontractorById(int id);
        System.Threading.Tasks.Task AddSubcontractor(Subcontractor subcontractor);
        System.Threading.Tasks.Task UpdateSubcontractor(Subcontractor subcontractor);
        System.Threading.Tasks.Task DeleteSubcontractor(int id);
    }
}
