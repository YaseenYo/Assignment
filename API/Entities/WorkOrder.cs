using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class WorkOrder
    {
        [Key]
        public Guid WorkOrderId { get; set; }
        [Required]
        public string Place { get; set; }
        [Required]
        public DateTime DateTime { get; set; }

        [ForeignKey("Technician")]
        public Guid? TechnicianRefId { get; set; }
    }
}