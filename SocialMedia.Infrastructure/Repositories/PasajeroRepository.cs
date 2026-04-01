using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task InsertPasajero(Pasajero pasajero)
        {
            _context.Pasajeros.Add(pasajero);
            await _context.SaveChangesAsync();
        }
        public async Task<Pasajero> GetPasajeroByCorreoAsync(string correo)
        {
            return await _context.Pasajeros.FirstOrDefaultAsync(x => x.Correo == correo);
        }

        public async Task<IEnumerable<Pasajero>> GetAllPasajerosAsync()
        {
            return await _context.Pasajeros.ToListAsync();
        }

        public async Task<Pasajero> GetPasajeroByIdAsync(int id)
        {
            return await _context.Pasajeros.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdatePasajero(Pasajero pasajero)
        {
            _context.Pasajeros.Update(pasajero);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePasajero(Pasajero pasajero)
        {
            _context.Pasajeros.Remove(pasajero);
            await _context.SaveChangesAsync();
        }
    }
}