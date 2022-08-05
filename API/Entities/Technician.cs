using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Technician
    {
        [Key]
        public Guid TechnicianId { get; set; } 
         [Required]
        public string FirstName { get; set; }
         [Required]
        public string LastName { get; set; }
         [Required]
        public bool Status { get; set; }
        public ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
    }
}