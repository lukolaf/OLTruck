using MediatR;
using OLTruck.Infrastructure;

namespace OLTruck.Commands.Handlers
{
    public class DeleteTruckCommandHandler : IRequestHandler<DeleteTruckCommand, Unit>
    {
        private readonly OlTruckDbContext _context;

        public DeleteTruckCommandHandler(OlTruckDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTruckCommand request, CancellationToken cancellationToken)
        {
            var truck = await _context.Trucks.FindAsync(new object[] { request.Code }, cancellationToken);
            if (truck == null)
            {
                throw new KeyNotFoundException($"Truck with code {request.Code} not found.");
            }

            _context.Trucks.Remove(truck);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
