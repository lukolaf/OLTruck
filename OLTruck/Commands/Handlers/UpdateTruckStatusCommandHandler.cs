using MediatR;
using OLTruck.Infrastructure;

namespace OLTruck.Commands.Handlers
{
    public class UpdateTruckStatusCommandHandler : IRequestHandler<UpdateTruckStatusCommand, Unit>
    {
        private readonly OlTruckDbContext _context;

        public UpdateTruckStatusCommandHandler(OlTruckDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTruckStatusCommand request, CancellationToken cancellationToken)
        {
            var truck = await _context.Trucks.FindAsync(new object[] { request.Code }, cancellationToken);
            if (truck == null)
            {
                throw new KeyNotFoundException($"Truck with code {request.Code} not found.");
            }

            try
            {
                truck.UpdateStatus(request.NewStatus);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Invalid attempt to change status. Detailed message:{ex.Message}");
            }

            return Unit.Value;
        }
    }
}
