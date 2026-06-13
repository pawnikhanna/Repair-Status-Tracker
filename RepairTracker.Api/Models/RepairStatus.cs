namespace RepairTracker.Api.Models
{
    public enum RepairStatus
    {
        Received,
        InProgress,
        WaitingOnParts,
        QualityCheck,
        ReadyForPickup,
        Completed
    }
}