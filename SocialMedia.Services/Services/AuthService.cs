using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Interfaces;
using SocialMedia.Services.Interfaces;

namespace SocialMedia.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
       
            var pasajero = await _unitOfWork.PasajeroRepository.GetPasajeroByCorreoAsync(loginDto.Correo);

            if (pasajero != null && pasajero.Usuario.Contrasena == loginDto.Contrasena)
            {
                return new LoginResponseDto
                {
                    Id = pasajero.UsuarioId,
                    Nombres = $"{pasajero.Nombres} {pasajero.Apellidos}",
                    Correo = pasajero.Usuario.Correo,
                    Rol = pasajero.Usuario.Rol
                };
            }

            var conductor = await _unitOfWork.ConductorRepository.GetConductorByCorreoAsync(loginDto.Correo);

            if (conductor != null && conductor.Usuario.Contrasena == loginDto.Contrasena)
            {
                return new LoginResponseDto
                {
                    Id = conductor.UsuarioId,
                    Nombres = $"{conductor.Nombres} {conductor.Apellidos}",
                    Correo = conductor.Usuario.Correo,
                    Rol = conductor.Usuario.Rol
                };
            }

            throw new Exception("Correo o contraseña incorrectos.");
        }
    }
}