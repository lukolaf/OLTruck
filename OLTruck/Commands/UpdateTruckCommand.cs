using MediatR;
using OLTruck.Shared.TruckDto;

namespace OLTruck.Commands
{
    public class UpdateTruckCommand : IRequest<Unit>
    {
        public string Code { get; set; }
        public TruckUpdateDto TruckUpdateDto { get; set; }
        public UpdateTruckCommand(string code, TruckUpdateDto truckUpdateDto)
        {
            Code = code;
            TruckUpdateDto = truckUpdateDto;
        }
    }
}
