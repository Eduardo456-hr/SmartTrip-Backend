using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            _conductorRepository = conductorRepository;
        }

        public async Task InsertConductor(Conductore conductor)
        {
            await _conductorRepository.InsertConductor(conductor);
        }
        public async Task<IEnumerable<Conductore>> GetAllConductoresAsync()
        {
            return await _conductorRepository.GetAllConductoresAsync();
        }
        public async Task UpdateConductor(int id, Conductore conductor)
        {
            var existing = await _conductorRepository.GetConductorByIdAsync(id);

            if (existing == null)
                throw new Exception("Conductor no encontrado");

            // Actualizar campos
            existing.Nombres = conductor.Nombres;
            existing.Apellidos = conductor.Apellidos;
            existing.Correo = conductor.Correo;
            existing.Contrasena = conductor.Contrasena;
            existing.Telefono = conductor.Telefono;
            existing.NumeroLicencia = conductor.NumeroLicencia;

            await _conductorRepository.UpdateConductor(existing);
        }

        public async Task DeleteConductor(int id)
        {
            var existing = await _conductorRepository.GetConductorByIdAsync(id);

            if (existing == null)
                throw new Exception("Conductor no encontrado");

            await _conductorRepository.DeleteConductor(existing);
        }
    }
}
