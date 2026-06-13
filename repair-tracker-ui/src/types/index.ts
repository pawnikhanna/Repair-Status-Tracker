export const RepairStatus = {
  Received: 0,
  InProgress: 1,
  WaitingOnParts: 2,
  QualityCheck: 3,
  ReadyForPickup: 4,
  Completed: 5
} as const;

export type RepairStatus = typeof RepairStatus[keyof typeof RepairStatus];

export const RepairStatusLabels: Record<RepairStatus, string> = {
  [RepairStatus.Received]: "Received",
  [RepairStatus.InProgress]: "In Progress",
  [RepairStatus.WaitingOnParts]: "Waiting on Parts",
  [RepairStatus.QualityCheck]: "Quality Check",
  [RepairStatus.ReadyForPickup]: "Ready for Pickup",
  [RepairStatus.Completed]: "Completed"
};

export interface RepairJob {
  id: string;
  customerName: string;
  vehicleDetails: string;
  repairCenter: string;
  status: RepairStatus;
  lastUpdated: string;
}