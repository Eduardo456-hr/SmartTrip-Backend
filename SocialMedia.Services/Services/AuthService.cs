using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Interfaces;
using SocialMedia.Services.Interfaces;

namespace SocialMedia.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPasajeroRepository _pasajeroRepository;
        private readonly IConductorRepository _conductorRepository;

        public AuthService(IPasajeroRepository pasajeroRepository, IConductorRepository conductorRepository)
        {
            _pasajeroRepository = pasajeroRepository;
            _conductorRepository = conductorRepository;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            // Buscar en Pasajeros primero
            var pasajero = await _pasajeroRepository.GetPasajeroByCorreoAsync(loginDto.Correo);
            if (pasajero != null && pasajero.Contrasena == loginDto.Contrasena)
            {
                return new LoginResponseDto { Id = pasajero.Id, Nombres = $"{pasajero.Nombres} {pasajero.Apellidos}", Correo = pasajero.Correo, Rol = "Pasajero" };
            }

            // Si no es pasajero, buscar en Conductores
            var conductor = await _conductorRepository.GetConductorByCorreoAsync(loginDto.Correo);
            if (conductor != null && conductor.Contrasena == loginDto.Contrasena)
            {
                return new LoginResponseDto { Id = conductor.Id, Nombres = $"{conductor.Nombres} {conductor.Apellidos}", Correo = conductor.Correo, Rol = "Conductor" };
            }

            // Si no coincide nada, lanzamos una excepción
            throw new Exception("Correo o contraseña incorrectos.");
        }
    }
}
