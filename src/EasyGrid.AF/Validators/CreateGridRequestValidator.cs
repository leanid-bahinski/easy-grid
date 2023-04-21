using EasyGrid.AF.Requests;
using FluentValidation;

namespace EasyGrid.AF.Validators
{
    public class CreateGridRequestValidator : AbstractValidator<CreateGridRequest>
    {
        public CreateGridRequestValidator()
        {
            RuleFor(x => x.MinLat).NotNull().InclusiveBetween(-90, 90);
            RuleFor(x => x.MinLon).NotNull().InclusiveBetween(-180, 180);
            RuleFor(x => x.MaxLat).NotNull().InclusiveBetween(-90, 90);
            RuleFor(x => x.MaxLon).NotNull().InclusiveBetween(-180, 180);
            RuleFor(x => x.SquareSize).NotNull().GreaterThanOrEqualTo(1);
        }
    }
}
