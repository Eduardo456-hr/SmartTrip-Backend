using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using SocialMedia.Core.DTOs;

namespace SocialMedia.Services.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Correo).NotEmpty().EmailAddress().WithMessage("Correo inválido.");
            RuleFor(x => x.Contrasena).NotEmpty().WithMessage("La contraseña es obligatoria.");
        }
    }
}
