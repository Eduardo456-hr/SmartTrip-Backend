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
    public class PasajeroService : IPasajeroService
    {
        private readonly IPasajeroRepository _pasajeroRepository;

        public PasajeroService(IPasajeroRepository pasajeroRepository)
        {
            _pasajeroRepository = pasajeroRepository;
        }

        public async Task InsertPasajero(Pasajero pasajero)
        {
            await _pasajeroRepository.InsertPasajero(pasajero);
        }

        public async Task<Pasajero> GetPasajeroByCorreoAsync(string correo)
        {
            return await _pasajeroRepository.GetPasajeroByCorreoAsync(correo);
        }

        public async Task<Pasajero> GetPasajeroByIdAsync(int id)
        {
            return await _pasajeroRepository.GetPasajeroByIdAsync(id);
        }

        public async Task UpdatePasajero(Pasajero pasajero)
        {
            await _pasajeroRepository.UpdatePasajero(pasajero);
        }

        public async Task DeletePasajero(Pasajero pasajero)
        {
            await _pasajeroRepository.DeletePasajero(pasajero);
        }

        public async Task<IEnumerable<Pasajero>> GetAllPasajerosAsync()
        {
            return await _pasajeroRepository.GetAllPasajerosAsync();
        }

    }
}