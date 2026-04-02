using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SmartTripContext _context;
        private readonly IPasajeroRepository _pasajeroRepository;
        private readonly IConductorRepository _conductorRepository;

        public UnitOfWork(SmartTripContext context)
        {
            _context = context;
        }

        public IPasajeroRepository PasajeroRepository => _pasajeroRepository ?? new PasajeroRepository(_context);
        public IConductorRepository ConductorRepository => _conductorRepository ?? new ConductorRepository(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}