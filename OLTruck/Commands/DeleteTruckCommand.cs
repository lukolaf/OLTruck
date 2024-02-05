using MediatR;

namespace OLTruck.Commands
{
    public class DeleteTruckCommand : IRequest<Unit>
    {
        public string Code { get; }

        public DeleteTruckCommand(string code)
        {
            Code = code;
        }
    }
}
