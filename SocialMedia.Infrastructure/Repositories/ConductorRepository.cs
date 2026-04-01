using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;

namespace SocialMedia.Infrastructure.Repositories
{
    public class ConductorRepository : IConductorRepository
    {
        private readonly SmartTripContext _context;

        public ConductorRepository(SmartTripContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Conductore>> GetAllConductoresAsync()
        {
            return await _context.Conductores.ToListAsync();
        }

        public async Task<Conductore> GetConductorByIdAsync(int id)
        {
            return await _context.Conductores.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertConductor(Conductore conductor)
        {
            _context.Conductores.Add(conductor);
            await _context.SaveChangesAsync();
        }
        public async Task<Conductore> GetConductorByCorreoAsync(string correo)
        {
            return await _context.Conductores.FirstOrDefaultAsync(x => x.Correo == correo);
        }
        public async Task UpdateConductor(Conductore conductor)
        {
            _context.Conductores.Update(conductor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteConductor(Conductore conductor)
        {
            _context.Conductores.Remove(conductor);
            await _context.SaveChangesAsync();
        }
    }
}
