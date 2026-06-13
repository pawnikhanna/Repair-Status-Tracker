using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairTracker.Api.Models;

namespace RepairTracker.Api.Services
{
    public interface IRepairJobService
    {
        Task<IEnumerable<RepairJob>> GetAllJobsAsync();
        Task<bool> UpdateJobStatusAsync(Guid id, RepairStatus newStatus);
    }
}