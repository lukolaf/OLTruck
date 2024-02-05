using FluentValidation;

namespace OLTruck.Commands.Validators
{
    public class UpdateTruckCommandValidator : AbstractValidator<UpdateTruckCommand>
    {
        public UpdateTruckCommandValidator()
        {
            RuleFor(truck => truck.TruckUpdateDto.Name).NotEmpty().MinimumLength(6).WithMessage("Truck name need to have atleast 6 characters.");
        }
    }
}
