using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairTracker.Api.Models;
using RepairTracker.Api.Repositories;

namespace RepairTracker.Api.Services
{
    public class RepairJobService : IRepairJobService
    {
        private readonly IRepairJobRepository _repository;

        public RepairJobService(IRepairJobRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RepairJob>> GetAllJobsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<bool> UpdateJobStatusAsync(Guid id, RepairStatus newStatus)
        {
            var job = await _repository.GetByIdAsync(id);
            if (job == null)
            {
                return false; // Job does not exist
            }

            // Explicit validation check: ensure value falls directly within enum bounds
            if (!Enum.IsDefined(typeof(RepairStatus), newStatus))
            {
                throw new ArgumentException("The specified state change validation failed due to an unknown status.");
            }

            return await _repository.UpdateStatusAsync(id, newStatus);
        }
    }
}