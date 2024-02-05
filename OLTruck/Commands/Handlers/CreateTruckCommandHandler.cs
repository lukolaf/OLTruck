using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLTruck.Domain.Models;
using OLTruck.Infrastructure;
using OLTruck.Shared.TruckDto;

namespace OLTruck.Commands.Handlers
{
    public class CreateTruckCommandHandler : IRequestHandler<CreateTruckCommand, TruckCreateDto>
    {
        private readonly OlTruckDbContext _context;

        public CreateTruckCommandHandler(OlTruckDbContext context)
        {
            _context = context;
        }
        public async Task<TruckCreateDto> Handle(CreateTruckCommand request, CancellationToken cancellationToken)
        {
            if (!await IsTruckCodeUniqueAsync(request.TruckCreateDto.Code))
            {
                throw new InvalidOperationException("Truck code must be unique.");
            }
            var truck = request.TruckCreateDto.Adapt<Truck>();
            await _context.Trucks.AddAsync(truck);
            await _context.SaveChangesAsync(cancellationToken);

            return truck.Adapt<TruckCreateDto>();
        }
        private async Task<bool> IsTruckCodeUniqueAsync(string code)
        {
            return !await _context.Trucks.AnyAsync(t => t.Code == code);
        }
    }
}
