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
    [Route("api/technicians")]
    [ApiController]
    public class TechniciansController : ControllerBase
    {
        private readonly ITechnicianRepository _technicianRepository;

        public TechniciansController(ITechnicianRepository technicianRepository)
        {
            _technicianRepository = technicianRepository;
        }   


        /// <summary>
        /// Action to get all existing technicians.
        /// </summary>
        /// <returns>Returns a list of all technicians</returns>
        /// <response code="200">Returned if the technicians were loaded</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<List<Technician>> Technicians()
        {
            return _technicianRepository.GetAll().ToList();
        }
        

        /// <summary>
        /// Action to get all existing workorders for a technician.
        /// </summary>
        /// <returns>Returns a list of all workorders</returns>
        /// <response code="200">Returned if the workorders were loaded</response>
        /// <response code="400">Returned if provided technicianId is incorrect</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}/workorders")]
        public async Task<ActionResult<List<WorkOrder>>> WorkOrdersForTechnician(Guid id){
            var technician = await _technicianRepository.GetTechnicianById(id);

            if(technician == null){
                return BadRequest($"No technician found with the id {id}");
            }

            return technician.WorkOrders.ToList();
        } 


        /// <summary>
        /// Action to get technician by Id.
        /// </summary>
        /// <returns>Returns a technician</returns>
        /// <response code="200">Returned if the technician is loaded</response>
        /// <response code="404">Returned if technician was not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}", Name = "GetTechnician")]
        public async Task<ActionResult<Technician>> TechnicianById(Guid id)
        {
            var technician = await _technicianRepository.GetTechnicianById(id);
            if(technician == null){
                return NotFound($"No technician found with the id {id}");
            }
             return technician;
        }


        /// <summary>
        /// Action to deactivate a technician.
        /// </summary>
        /// <returns>Returns no content</returns>
        /// <response code="204">Returned if the technician is deactivated</response>
        /// <response code="404">Returned if technician was not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateTechnician(Guid id){
            var technician = await _technicianRepository.GetTechnicianById(id);
            if(technician == null){
                return NotFound($"No technician found with the id {id}");
            }
            await _technicianRepository.DeactivateTechnician(technician);
            return NoContent();
        }


        /// <summary>
        /// Action to create a technician.
        /// </summary>
        /// <returns>Returns a newly created technician</returns>
        /// <response code="201">Returned if the technician is created successfully</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult<Technician>> Technician(Technician technician)
        {
            var createdTechnician = await _technicianRepository.AddAsync(technician);

            return CreatedAtRoute("GetTechnician",
                        new { id = createdTechnician.TechnicianId }, createdTechnician);

        }
    }
}