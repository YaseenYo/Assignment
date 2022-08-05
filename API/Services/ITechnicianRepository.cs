using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Services
{
    public interface ITechnicianRepository : IRepository<Technician>
    {
        Task<Technician> GetTechnicianById(Guid id);
        Task<IEnumerable<WorkOrder>> GetWorkOrdersForTechnician(Guid technicianId);
        Task DeactivateTechnician(Technician technician);
    }
}