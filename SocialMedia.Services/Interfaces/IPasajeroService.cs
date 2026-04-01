using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMedia.Core.Entities;

namespace SocialMedia.Services.Interfaces
{
    public interface IPasajeroService
    {
        Task InsertPasajero(Pasajero pasajero);
        Task<Pasajero> GetPasajeroByCorreoAsync(string correo);
        Task<Pasajero> GetPasajeroByIdAsync(int id);
        Task DeletePasajero(Pasajero pasajero);
        Task UpdatePasajero(Pasajero pasajero);

        Task<IEnumerable<Pasajero>> GetAllPasajerosAsync();

    }
}
