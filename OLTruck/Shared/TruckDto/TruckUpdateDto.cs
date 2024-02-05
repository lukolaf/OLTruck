using OLTruck.Domain.Enums;

namespace OLTruck.Shared.TruckDto
{
    public class TruckUpdateDto
    {
        public string Name { get; set; }
        public TruckStatus Status { get; set; }
        public string Description { get; set; }
    }
}
