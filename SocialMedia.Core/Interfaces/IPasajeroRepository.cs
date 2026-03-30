using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Interfaces
{
    public interface IPasajeroRepository
    {
        Task InsertPasajero(Pasajero pasajero);

        Task<Pasajero> GetPasajeroByCorreoAsync(string correo);
    }
}