using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLTruck.Domain.Models;
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
            var query = _context.Trucks.AsQueryable();
            query = ApplyFilter(query, request.Filter);
            query = ApplySorting(query, request.SortBy);

            return await query.Select(t => t.Adapt<TruckDto>()).ToListAsync(cancellationToken);
        }
        private IQueryable<Truck> ApplyFilter(IQueryable<Truck> query, string? filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(t => t.Name.Contains(filter) ||
                                         t.Description.Contains(filter) ||
                                         t.Code.Contains(filter));
            }
            return query;
        }
        private IQueryable<Truck> ApplySorting(IQueryable<Truck> query, string? sortBy)
        {
            return sortBy switch
            {
                "name" => query.OrderBy(t => t.Name),
                "description" => query.OrderBy(t => t.Description),
                "code" => query.OrderBy(t => t.Code),
                "status" => query.OrderBy(t => t.Status),
                _ => query
            };
        }
    }
}
