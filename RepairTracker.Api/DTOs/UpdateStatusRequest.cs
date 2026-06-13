using System.ComponentModel.DataAnnotations;
using RepairTracker.Api.Models;

namespace RepairTracker.Api.DTOs
{
    public class UpdateStatusRequest
    {
        [Required]
        [EnumDataType(typeof(RepairStatus), ErrorMessage = "Invalid repair status provided.")]
        public RepairStatus Status { get; set; }
    }
}