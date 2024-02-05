using FluentValidation;

namespace OLTruck.Commands.Validators
{
    public class CreateTruckCommandValidator : AbstractValidator<CreateTruckCommand>
    {
        public CreateTruckCommandValidator()
        {
            RuleFor(truck => truck.TruckCreateDto.Code).NotEmpty().WithMessage("Truck code is required.");
            RuleFor(truck => truck.TruckCreateDto.Name).NotEmpty().WithMessage("Truck name is required.");
            RuleFor(truck => truck.TruckCreateDto.Description).NotEmpty().WithMessage("Truck description is required.");
        }
    }
}
