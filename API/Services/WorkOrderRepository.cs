using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DbContexts;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class WorkOrderRepository : Repository<WorkOrder>, IWorkOrderRepository
    {
        public WorkOrderRepository(DatabaseContext context) : base(context)
        {
        }

        /// <summary>
        /// Filters the workorders based on provided datetime.
        /// </summary>
        public async Task<IEnumerable<WorkOrder>> GetWorkOrdersByDateAsync(DateTime time)
        {
            return await _context.WorkOrders.Where(x => x.DateTime == time).ToListAsync();
        }


        /// <summary>
        /// Gets workorder matching the provided workorderId.
        /// </summary>
        public async Task<WorkOrder> GetWorkOrderById(Guid id){
            return await _context.WorkOrders.FirstOrDefaultAsync(x => x.WorkOrderId == id);
        }


        /// <summary>
        /// Deletes the workorder matching the provided workorderId.
        /// </summary>
        public async Task DeleteWorkOrderById(WorkOrder workOrder){
            _context.WorkOrders.Remove(workOrder);
            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Gets a Technician matching provided technicianId.
        /// </summary>
        public async Task<Technician> GetTechnicianById(Guid id){
            return await _context.Technicians.FirstOrDefaultAsync(x => x.TechnicianId == id);
        }


        /// <summary>
        /// Assigns a technician to the provided workorder.
        /// </summary>
        public async Task<Technician?> AssignTechnician(WorkOrder workOrder,Technician technician){
            if(!technician.Status){
                return null;
            }
            workOrder.TechnicianRefId = technician.TechnicianId;
            technician.WorkOrders.Add(workOrder);
            _context.UpdateRange(workOrder,technician);
            await _context.SaveChangesAsync();
            return technician;
        }
    }
}