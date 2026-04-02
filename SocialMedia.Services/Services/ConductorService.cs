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

        public async Task InsertConductor(Conductore conductor)
        {
            if (conductor is null) throw new ArgumentNullException(nameof(conductor));

            if (string.IsNullOrWhiteSpace(conductor.Nombres)) throw new ArgumentException("Nombres inválidos.", nameof(conductor));
            if (string.IsNullOrWhiteSpace(conductor.Correo)) throw new ArgumentException("Correo inválido.", nameof(conductor));

            await _unitOfWork.ConductorRepository.InsertConductor(conductor);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Conductore>> GetAllConductoresAsync()
        {
            return await _unitOfWork.ConductorRepository.GetAllConductoresAsync();
        }

        public async Task UpdateConductor(int id, Conductore conductor)
        {
            if (conductor is null) throw new ArgumentNullException(nameof(conductor));

            var existing = await _unitOfWork.ConductorRepository.GetConductorByIdAsync(id);

            if (existing == null)
                throw new KeyNotFoundException($"Conductor con id {id} no encontrado.");

            existing.Nombres = conductor.Nombres;
            existing.Apellidos = conductor.Apellidos;
            existing.Correo = conductor.Correo;
            existing.Contrasena = conductor.Contrasena;
            existing.Telefono = conductor.Telefono;
            existing.NumeroLicencia = conductor.NumeroLicencia;

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