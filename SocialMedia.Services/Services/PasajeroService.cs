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
            _unitOfWork = unitOfWork;
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
            await _unitOfWork.PasajeroRepository.InsertPasajero(pasajero);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdatePasajero(Pasajero pasajero)
        {
            await _unitOfWork.PasajeroRepository.UpdatePasajero(pasajero);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeletePasajero(Pasajero pasajero)
        {
            await _unitOfWork.PasajeroRepository.DeletePasajero(pasajero);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}