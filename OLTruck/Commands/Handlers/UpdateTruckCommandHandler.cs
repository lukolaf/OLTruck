using FluentValidation;
using MediatR;
using OLTruck.Infrastructure;

namespace OLTruck.Commands.Handlers
{
    public class UpdateTruckCommandHandler : IRequestHandler<UpdateTruckCommand, Unit>
    {
        private readonly OlTruckDbContext _context;
        private readonly IValidator<UpdateTruckCommand> _validator;
        public UpdateTruckCommandHandler(OlTruckDbContext context, IValidator<UpdateTruckCommand> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdateTruckCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var truck = await _context.Trucks.FindAsync(request.Code);
            if (truck == null)
            {
                throw new KeyNotFoundException("Truck not found");
            }

            truck.Name = request.TruckUpdateDto.Name;
            truck.Status = request.TruckUpdateDto.Status;
            truck.Description = request.TruckUpdateDto.Description;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
