namespace Product.API.DTOs.Payloads.Validators
{
    public class CreateProductPayloadValidator : AbstractValidator<CreateProductPayload>
    {
        public CreateProductPayloadValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Product price must be greater than zero");
        }
    }
}

