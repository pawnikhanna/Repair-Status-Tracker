using System;

namespace RepairTracker.Api.Models
{
    public class RepairJob
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string VehicleDetails { get; set; } = string.Empty; // e.g., "2022 Tesla Model 3"
        public string RepairCenter { get; set; } = string.Empty;
        public RepairStatus Status { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}