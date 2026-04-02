using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Services.Interfaces;

namespace SocialMedia.Services.Services
{
    public class ConductorService : IConductorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConductorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IEnumerable<Conductore>> GetAllConductoresAsync()
        {
            return await _unitOfWork.ConductorRepository.GetAllConductoresAsync();
        }

        public async Task InsertConductor(Conductore conductor)
        {
            if (conductor is null) throw new ArgumentNullException(nameof(conductor));

            // Validaciones: Ahora el correo y contraseña están en conductor.Usuario
            if (string.IsNullOrWhiteSpace(conductor.Nombres)) throw new ArgumentException("El nombre es requerido.");
            if (conductor.Usuario == null || string.IsNullOrWhiteSpace(conductor.Usuario.Correo))
                throw new ArgumentException("El correo electrónico es obligatorio.");

            await _unitOfWork.ConductorRepository.InsertConductor(conductor);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateConductor(int id, Conductore conductor)
        {
            if (conductor is null) throw new ArgumentNullException(nameof(conductor));

            // Buscamos el existente incluyendo su Usuario
            var existing = await _unitOfWork.ConductorRepository.GetConductorByIdAsync(id);

            if (existing == null)
                throw new KeyNotFoundException($"Conductor con id {id} no encontrado.");

            // Actualizar campos del Perfil
            existing.Nombres = conductor.Nombres;
            existing.Apellidos = conductor.Apellidos;
            existing.Telefono = conductor.Telefono;
            existing.NumeroLicencia = conductor.NumeroLicencia;

            // Actualizar campos de la Cuenta (Tabla Usuarios)
            if (conductor.Usuario != null)
            {
                existing.Usuario.Correo = conductor.Usuario.Correo;
                existing.Usuario.Contrasena = conductor.Usuario.Contrasena;
            }

            await _unitOfWork.ConductorRepository.UpdateConductor(existing);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteConductor(int id)
        {
            var existing = await _unitOfWork.ConductorRepository.GetConductorByIdAsync(id);

            if (existing == null)
                throw new KeyNotFoundException($"Conductor con id {id} no encontrado.");

            await _unitOfWork.ConductorRepository.DeleteConductor(existing);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}