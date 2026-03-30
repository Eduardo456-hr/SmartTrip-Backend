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

    }
}