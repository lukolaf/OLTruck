using MediatR;
using OLTruck.Shared.TruckDto;

namespace OLTruck.Commands
{
    public class CreateTruckCommand : IRequest<TruckCreateDto>
    {
        public TruckCreateDto TruckCreateDto { get; set; }
    }
}
