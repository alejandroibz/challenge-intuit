using Application.Features.Cliente.Commands;
using FluentValidation;

namespace Application.Validators
{
    public class CreateClientValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("El apellido es obligatorio");

            RuleFor(x => x.CUIT)
                .NotEmpty().WithMessage("El CUIT es obligatorio.")
                .Matches(@"^\d{11}$").WithMessage("CUIT must be 11 numeric characters.");

            RuleFor(x => x.Cellphone)
                .NotEmpty().WithMessage("El numero de celular es obligatorio");

            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("El email es obligatorio")
                 .EmailAddress().WithMessage("Formato de email invalido");
        }
    }
}
