using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            return await _context.Conductores
                .Include(x => x.Usuario)
                .ToListAsync();
        }

        public async Task<Conductore> GetConductorByIdAsync(int id)
        {
            return await _context.Conductores
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.UsuarioId == id);
        }

        public async Task<Conductore> GetConductorByCorreoAsync(string correo)
        {
            return await _context.Conductores
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Usuario.Correo == correo);
        }

        public async Task InsertConductor(Conductore conductor)
        {

            _context.Conductores.Add(conductor);

        }

        public async Task UpdateConductor(Conductore conductor)
        {
            _context.Conductores.Update(conductor);
        }

        public async Task DeleteConductor(Conductore conductor)
        {
            _context.Conductores.Remove(conductor);
        }
    }
}
