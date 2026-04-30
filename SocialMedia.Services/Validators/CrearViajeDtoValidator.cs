using FluentValidation;
using SocialMedia.Core.DTOs;

namespace SocialMedia.Services.Validators
{
    public class CrearViajeDtoValidator : AbstractValidator<CrearViajeDto>
    {
        public CrearViajeDtoValidator()
        {
            RuleFor(x => x.ConductorId)
                .GreaterThan(0).WithMessage("El conductor es obligatorio.");

            RuleFor(x => x.Origen)
                .NotEmpty().WithMessage("El origen es obligatorio.")
                .MaximumLength(100).WithMessage("El origen no debe superar los 100 caracteres.");

            RuleFor(x => x.Destino)
                .NotEmpty().WithMessage("El destino es obligatorio.")
                .MaximumLength(100).WithMessage("El destino no debe superar los 100 caracteres.");

            RuleFor(x => x.FechaViaje)
                .GreaterThan(DateTime.Now).WithMessage("La fecha del viaje debe ser futura.");

            RuleFor(x => x.AsientosDisponibles)
                .GreaterThan(0).WithMessage("Debe existir al menos un asiento disponible.");

            RuleFor(x => x.Precio)
                .GreaterThanOrEqualTo(0).WithMessage("El precio no puede ser negativo.");
        }
    }
}