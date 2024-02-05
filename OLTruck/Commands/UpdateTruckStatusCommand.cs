using MediatR;
using OLTruck.Domain.Enums;

namespace OLTruck.Commands
{
    public class UpdateTruckStatusCommand : IRequest<Unit>
    {
        public string Code { get; }
        public TruckStatus NewStatus { get; }

        public UpdateTruckStatusCommand(string code, TruckStatus newStatus)
        {
            Code = code;
            NewStatus = newStatus;
        }
    }
}
