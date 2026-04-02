using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Services.Interfaces;

namespace SocialMedia.Services.Services
{
    public class PasajeroService : IPasajeroService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PasajeroService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IEnumerable<Pasajero>> GetAllPasajerosAsync()
        {
            return await _unitOfWork.PasajeroRepository.GetAllPasajerosAsync();
        }

        public async Task<Pasajero> GetPasajeroByIdAsync(int id)
        {
            return await _unitOfWork.PasajeroRepository.GetPasajeroByIdAsync(id);
        }

        public async Task<Pasajero> GetPasajeroByCorreoAsync(string correo)
        {
            return await _unitOfWork.PasajeroRepository.GetPasajeroByCorreoAsync(correo);
        }

        public async Task InsertPasajero(Pasajero pasajero)
        {
            if (pasajero is null) throw new ArgumentNullException(nameof(pasajero));

            if (string.IsNullOrWhiteSpace(pasajero.Nombres)) throw new ArgumentException("Nombres requeridos.");
            if (pasajero.Usuario == null || string.IsNullOrWhiteSpace(pasajero.Usuario.Correo))
                throw new ArgumentException("Credenciales de usuario incompletas.");

            await _unitOfWork.PasajeroRepository.InsertPasajero(pasajero);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdatePasajero(Pasajero pasajero)
        {
            if (pasajero is null) throw new ArgumentNullException(nameof(pasajero));

            var existing = await _unitOfWork.PasajeroRepository.GetPasajeroByIdAsync(pasajero.UsuarioId);

            if (existing == null) throw new KeyNotFoundException("Pasajero no encontrado.");

            existing.Nombres = pasajero.Nombres;
            existing.Apellidos = pasajero.Apellidos;
            existing.Telefono = pasajero.Telefono;

            if (pasajero.Usuario != null)
            {
                existing.Usuario.Correo = pasajero.Usuario.Correo;
                existing.Usuario.Contrasena = pasajero.Usuario.Contrasena;
            }

            await _unitOfWork.PasajeroRepository.UpdatePasajero(existing);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeletePasajero(Pasajero pasajero)
        {

            await _unitOfWork.PasajeroRepository.DeletePasajero(pasajero);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}