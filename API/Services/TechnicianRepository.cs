using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DbContexts;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class TechnicianRepository : Repository<Technician>, ITechnicianRepository
    {
        public TechnicianRepository(DatabaseContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets a Technician matching provided technicianId.
        /// </summary>
        public async Task<Technician> GetTechnicianById(Guid id){
            return await _context.Technicians.Where(x => x.TechnicianId == id).Include(x => x.WorkOrders).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets workorders associated with the technician
        /// </summary>
        public async Task<IEnumerable<WorkOrder>> GetWorkOrdersForTechnician(Guid technicianId){
            return await _context.WorkOrders.Where(x=> x.TechnicianRefId == technicianId).ToListAsync();
        }

        /// <summary>
        /// Deactivates the technician.
        /// </summary>
        public async Task DeactivateTechnician(Technician technician){
            technician.Status = false;
            await _context.SaveChangesAsync();
        }
        
    }
}