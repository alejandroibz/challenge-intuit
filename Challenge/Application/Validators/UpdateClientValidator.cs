using Application.Features.Cliente.Commands;
using FluentValidation;

namespace Application.Features.Cliente.Validators
{
    public class UpdateClientValidator : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage("El ClientId es obligatorio");

        }
    }
}
