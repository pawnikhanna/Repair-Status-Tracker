using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairTracker.Api.Models;

namespace RepairTracker.Api.Repositories
{
    public class InMemoryRepairJobRepository : IRepairJobRepository
    {
        private static readonly ConcurrentDictionary<Guid, RepairJob> _jobs = new();

        // Seed mock data inside the constructor
        public InMemoryRepairJobRepository()
        {
            if (_jobs.IsEmpty)
            {
                var seedData = new List<RepairJob>
                {
                    new() { Id = Guid.NewGuid(), CustomerName = "Pawni Khanna", VehicleDetails = "2019 Honda Civic", RepairCenter = "Westmont Center", Status = RepairStatus.Received, LastUpdated = DateTime.UtcNow },
                    new() { Id = Guid.NewGuid(), CustomerName = "Sarah James", VehicleDetails = "2023 Tesla Model Y", RepairCenter = "Chicago Downtown", Status = RepairStatus.InProgress, LastUpdated = DateTime.UtcNow },
                    new() { Id = Guid.NewGuid(), CustomerName = "Michael Yang", VehicleDetails = "2021 Ford F-150", RepairCenter = "Westmont Center", Status = RepairStatus.WaitingOnParts, LastUpdated = DateTime.UtcNow },
                    new() { Id = Guid.NewGuid(), CustomerName = "Emily Johnson", VehicleDetails = "2022 BMW X5", RepairCenter = "Chicago Downtown", Status = RepairStatus.QualityCheck, LastUpdated = DateTime.UtcNow }
                };

                foreach (var job in seedData)
                {
                    _jobs[job.Id] = job;
                }
            }
        }

        public Task<IEnumerable<RepairJob>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<RepairJob>>(_jobs.Values);
        }

        public Task<RepairJob?> GetByIdAsync(Guid id)
        {
            _jobs.TryGetValue(id, out var job);
            return Task.FromResult(job);
        }

        public Task<bool> UpdateStatusAsync(Guid id, RepairStatus status)
        {
            if (_jobs.TryGetValue(id, out var job))
            {
                job.Status = status;
                job.LastUpdated = DateTime.UtcNow;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}