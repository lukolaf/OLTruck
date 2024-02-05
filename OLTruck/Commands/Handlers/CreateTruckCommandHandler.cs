using FluentValidation;
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
        private readonly IValidator<CreateTruckCommand> _validator;

        public CreateTruckCommandHandler(OlTruckDbContext context, IValidator<CreateTruckCommand> validator)
        {
            _context = context;
            _validator = validator;
        }
        public async Task<TruckCreateDto> Handle(CreateTruckCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

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
