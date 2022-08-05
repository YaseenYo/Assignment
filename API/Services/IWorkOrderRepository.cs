using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Services
{
    public interface IWorkOrderRepository : IRepository<WorkOrder>
    { 
        Task<IEnumerable<WorkOrder>> GetWorkOrdersByDateAsync(DateTime time);
        Task<Technician> GetTechnicianById(Guid id);
        Task<Technician?> AssignTechnician(WorkOrder workOrder,Technician technician);
        Task<WorkOrder> GetWorkOrderById(Guid id);
        Task DeleteWorkOrderById(WorkOrder workOrder);
    }
}