using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/workorders")]
    [ApiController] 
    public class WorkOrdersController : ControllerBase
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly INotificationMode _notifier;

        public WorkOrdersController(IWorkOrderRepository workOrderRepository,INotificationMode notifier)
        {
            _workOrderRepository = workOrderRepository;
            _notifier = notifier;
        }


        /// <summary>
        /// Action to get all existing workorders.
        /// </summary>
        /// <returns>Returns a list of all workorders</returns>
        /// <response code="200">Returned if the workorders were loaded</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<List<WorkOrder>> WorkOrders()
        {
            return _workOrderRepository.GetAll().ToList();
        }


        /// <summary>
        /// Action to workorder by id.
        /// </summary>
        /// <returns>Returns a workorder</returns>
        /// <response code="200">Returned if the workorder is loaded</response>
        /// <response code="404">Returned if the workorder couldn't be found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}", Name = "GetWorkOrder")]
        public async Task<ActionResult<WorkOrder>> WorkOrderById(Guid id)
        {
            var workorder = await _workOrderRepository.GetWorkOrderById(id);
            if(workorder == null){
                return NotFound($"No workorder found with the id {id}");
            }
            return workorder;
        }


         /// <summary>
        /// Action to create a workorder.
        /// </summary>
        /// <returns>Returns a newly created workorder</returns>
        /// <response code="200">Returned if the workorder is loaded</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult<WorkOrder>> WorkOrder(WorkOrder workOrder)
        {
            var createdWorkOrder = await _workOrderRepository.AddAsync(workOrder);

            return CreatedAtRoute("GetWorkOrder",
                        new { id = createdWorkOrder.WorkOrderId }, createdWorkOrder);

        }


        /// <summary>
        /// Action to get all existing workorders based on specified datetime.
        /// </summary>
        /// <returns>Returns a list of all workorders</returns>
        /// <response code="200">Returned if the workorders were loaded</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("query")]
        public async Task<ActionResult<List<WorkOrder>>> WorkOrder([FromQuery] DateTime dateTime)
        {
            var workorders =  await _workOrderRepository.GetWorkOrdersByDateAsync(dateTime);
            return Ok(workorders);
        }


        /// <summary>
        /// Action to delete a workorder.
        /// </summary>
        /// <returns>Returns a void</returns>
        /// <response code="200">Returned if the workorder is deleted</response>
        /// <response code="400">Returned if the provided workorderId is incorrect</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> WorkOrder(Guid id)
        {
            var workorder = await _workOrderRepository.GetWorkOrderById(id);
            if (workorder == null)
            {
                return BadRequest($"workorder with Id = {id} not found");
            }
            await _workOrderRepository.DeleteWorkOrderById(workorder);
             return Ok($"Student with id = {id} deleted");
        }


        /// <summary>
        /// Action to assign a technician to a workorder.
        /// </summary>
        /// <returns>Returns a newly assigned technician</returns>
        /// <response code="200">Returned if technician is assigned successfully</response>
        /// <response code="400">Returned if the provided arguments are incorrect</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{workorderid}/assigntechnician/{technicianid}")]
        public async Task<ActionResult<Technician>> AssignTechnician(Guid workorderid,Guid technicianid)
        {
            var workorder = await _workOrderRepository.GetWorkOrderById(workorderid);
            if(workorder == null){
                return BadRequest($"No workorder found with the id {workorderid}");
            }
            var technician = await _workOrderRepository.GetTechnicianById(technicianid);
            if(technician == null){
                return BadRequest($"No technician found with the id {technicianid}");
            }

           var assignedTechnician = await _workOrderRepository.AssignTechnician(workorder,technician); 
           if(assignedTechnician == null){
                return BadRequest($"technician with the id {technicianid} is inactive");
            }

            // Notification is being notified to the technician
            // here logmode is being implemented as a notification mode
            // we can easily replace this by any future notification mode implementation
            _notifier.Notify($"New WorkOrder Has been Assigned {workorderid}");
            return assignedTechnician;
        }

    }
}