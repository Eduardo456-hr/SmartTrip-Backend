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
        private readonly IConductorRepository _conductorRepository;

        public ConductorService(IConductorRepository conductorRepository)
        {
            _conductorRepository = conductorRepository ?? throw new ArgumentNullException(nameof(conductorRepository));
        }

        public async Task InsertConductor(Conductore conductor)
        {
            if (conductor is null) throw new ArgumentNullException(nameof(conductor));

            // Validaciones básicas (ampliar según reglas del negocio)
            if (string.IsNullOrWhiteSpace(conductor.Nombres)) throw new ArgumentException("Nombres inválidos.", nameof(conductor));
            if (string.IsNullOrWhiteSpace(conductor.Correo)) throw new ArgumentException("Correo inválido.", nameof(conductor));

            await _conductorRepository.InsertConductor(conductor).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Conductore>> GetAllConductoresAsync()
        {
            return await _conductorRepository.GetAllConductoresAsync().ConfigureAwait(false);
        }

        public async Task UpdateConductor(int id, Conductore conductor)
        {
            if (conductor is null) throw new ArgumentNullException(nameof(conductor));

            var existing = await _conductorRepository.GetConductorByIdAsync(id).ConfigureAwait(false);

            if (existing == null)
                throw new KeyNotFoundException($"Conductor con id {id} no encontrado.");

            // Actualizar campos permitidos
            existing.Nombres = conductor.Nombres;
            existing.Apellidos = conductor.Apellidos;
            existing.Correo = conductor.Correo;
            existing.Contrasena = conductor.Contrasena;
            existing.Telefono = conductor.Telefono;
            existing.NumeroLicencia = conductor.NumeroLicencia;

            await _conductorRepository.UpdateConductor(existing).ConfigureAwait(false);
        }

        public async Task DeleteConductor(int id)
        {
            var existing = await _conductorRepository.GetConductorByIdAsync(id).ConfigureAwait(false);

            if (existing == null)
                throw new KeyNotFoundException($"Conductor con id {id} no encontrado.");

            await _conductorRepository.DeleteConductor(existing).ConfigureAwait(false);
        }
    }
}
