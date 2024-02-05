using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLTruck.Infrastructure;
using OLTruck.Shared.TruckDto;

namespace OLTruck.Queries.Handlers
{
    public class GetAllTrucksQueryHandler : IRequestHandler<GetAllTrucksQuery, IEnumerable<TruckDto>>
    {
        private readonly OlTruckDbContext _context;

        public GetAllTrucksQueryHandler(OlTruckDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TruckDto>> Handle(GetAllTrucksQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Trucks.ToListAsync();
            return result.Adapt<List<TruckDto>>();
        }
    }
}
