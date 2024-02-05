using OLTruck.Domain.Enums;

namespace OLTruck.Domain.Models
{
    public class Truck
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public TruckStatus Status { get; set; }
        public string? Description { get; set; }
    }
}
