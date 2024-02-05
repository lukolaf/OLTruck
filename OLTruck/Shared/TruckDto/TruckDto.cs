using OLTruck.Domain.Enums;
using OLTruck.Domain.Models;

namespace OLTruck.Shared.TruckDto
{
    public class TruckDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TruckStatus Status { get; set; }
        public TruckDto() { }
        public TruckDto(Truck truck)
        {
            Code = truck.Code;
            Name = truck.Name;
            Status = truck.Status;
            Description = truck.Description;
        }
    }
}
