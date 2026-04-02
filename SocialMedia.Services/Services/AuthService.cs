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
            // Buscar en Pasajeros primero a través de la Unidad de Trabajo
            var pasajero = await _unitOfWork.PasajeroRepository.GetPasajeroByCorreoAsync(loginDto.Correo);

            if (pasajero != null && pasajero.Contrasena == loginDto.Contrasena)
            {
                return new LoginResponseDto
                {
                    Id = pasajero.Id,
                    Nombres = $"{pasajero.Nombres} {pasajero.Apellidos}",
                    Correo = pasajero.Correo,
                    Rol = "Pasajero"
                };
            }

            // Si no es pasajero, buscar en Conductores a través de la Unidad de Trabajo
            var conductor = await _unitOfWork.ConductorRepository.GetConductorByCorreoAsync(loginDto.Correo);

            if (conductor != null && conductor.Contrasena == loginDto.Contrasena)
            {
                return new LoginResponseDto
                {
                    Id = conductor.Id,
                    Nombres = $"{conductor.Nombres} {conductor.Apellidos}",
                    Correo = conductor.Correo,
                    Rol = "Conductor"
                };
            }

            // Si no coincide nada, lanzamos una excepción
            throw new Exception("Correo o contraseña incorrectos.");
        }
    }
}