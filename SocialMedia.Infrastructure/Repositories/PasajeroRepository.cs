using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PasajeroRepository : IPasajeroRepository
    {
        private readonly SmartTripContext _context;

        public PasajeroRepository(SmartTripContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pasajero>> GetAllPasajerosAsync()
        {
            return await _context.Pasajeros
                .Include(x => x.Usuario)
                .ToListAsync();
        }

        public async Task<Pasajero> GetPasajeroByIdAsync(int id)
        {
            return await _context.Pasajeros
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.UsuarioId == id);
        }

        public async Task<Pasajero> GetPasajeroByCorreoAsync(string correo)
        {
            return await _context.Pasajeros
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Usuario.Correo == correo);
        }

        public async Task InsertPasajero(Pasajero pasajero)
        {
            _context.Pasajeros.Add(pasajero);

        }

        public async Task UpdatePasajero(Pasajero pasajero)
        {
            _context.Pasajeros.Update(pasajero);
        }

        public async Task DeletePasajero(Pasajero pasajero)
        {
            _context.Pasajeros.Remove(pasajero);
        }
    }
}