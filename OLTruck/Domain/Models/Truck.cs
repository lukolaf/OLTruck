using OLTruck.Domain.Enums;

namespace OLTruck.Domain.Models
{
    public class Truck
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public TruckStatus Status { get; set; }
        public string? Description { get; set; }
        public void UpdateStatus(TruckStatus newStatus)
        {
            if (CanChangeStatus(newStatus))
            {
                this.Status = newStatus;
            }
            else
            {
                throw new InvalidOperationException($"Cannot change status from {Status} to {newStatus}");
            }
        }

        public bool CanChangeStatus(TruckStatus newStatus)
        {
            if (newStatus == TruckStatus.OutOfService)
            {
                return true;
            }

            return Status switch
            {
                TruckStatus.OutOfService => true,
                TruckStatus.Loading => newStatus == TruckStatus.ToJob,
                TruckStatus.ToJob => newStatus == TruckStatus.AtJob,
                TruckStatus.AtJob => newStatus == TruckStatus.Returning,
                TruckStatus.Returning => newStatus == TruckStatus.Loading,
                _ => false
            };
        }
    }
}
