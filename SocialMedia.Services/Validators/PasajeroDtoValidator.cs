using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using SocialMedia.Core.DTOs;

namespace SocialMedia.Services.Validators
{
    public class PasajeroDtoValidator : AbstractValidator<PasajeroDto>
    {
        public PasajeroDtoValidator()
        {
            RuleFor(x => x.Nombres).NotEmpty().WithMessage("El nombre es obligatorio.");
            RuleFor(x => x.Apellidos).NotEmpty().WithMessage("Los apellidos son obligatorios.");
            RuleFor(x => x.Correo)
                .NotEmpty().WithMessage("El correo no puede estar vacío.")
                .EmailAddress().WithMessage("Debe ser una dirección de correo válida.");
            RuleFor(x => x.Contrasena)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");
            RuleFor(x => x.Telefono).NotEmpty().WithMessage("El teléfono es obligatorio.");
        }
    }
}

