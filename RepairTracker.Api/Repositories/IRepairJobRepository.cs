using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairTracker.Api.Models;

namespace RepairTracker.Api.Repositories
{
    public interface IRepairJobRepository
    {
        Task<IEnumerable<RepairJob>> GetAllAsync();
        Task<RepairJob?> GetByIdAsync(Guid id);
        Task<bool> UpdateStatusAsync(Guid id, RepairStatus status);
    }
}