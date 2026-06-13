using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RepairTracker.Api.DTOs;
using RepairTracker.Api.Services;

namespace RepairTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RepairJobsController : ControllerBase
    {
        private readonly IRepairJobService _service;

        public RepairJobsController(IRepairJobService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jobs = await _service.GetAllJobsAsync();
            return Ok(jobs);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateStatusRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _service.UpdateJobStatusAsync(id, request.Status);
            if (!success)
            {
                return NotFound(new { message = $"Repair job with ID {id} not found." });
            }

            return Ok(new { message = "Status updated successfully." });
        }
    }
}