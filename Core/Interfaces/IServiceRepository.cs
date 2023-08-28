using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Core.Interfaces
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetServices();
        Task<Service> GetServiceById(int id);
        Task<Service> GetServiceByName(string name);
        Task AddService(Service service);
        Task UpdateService(Service service);
        Task DeleteService(int id);
    }
}
