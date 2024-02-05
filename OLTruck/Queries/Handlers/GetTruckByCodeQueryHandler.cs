using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLTruck.Infrastructure;
using OLTruck.Shared.TruckDto;

namespace OLTruck.Queries.Handlers
{
    public class GetTruckByCodeQueryHandler : IRequestHandler<GetTruckByCodeQuery, TruckDto>
    {
        private readonly OlTruckDbContext _context;

        public GetTruckByCodeQueryHandler(OlTruckDbContext context)
        {
            _context = context;
        }

        public async Task<TruckDto> Handle(GetTruckByCodeQuery request, CancellationToken cancellationToken)
        {
            var truck = await _context.Trucks
                .Where(t => t.Code == request.Code)
                .Select(t => t.Adapt<TruckDto>())
                .FirstOrDefaultAsync(cancellationToken);

            if (truck == null)
            {
                throw new KeyNotFoundException($"Truck with code {request.Code} not found.");
            }

            return truck;
        }
    }
}
