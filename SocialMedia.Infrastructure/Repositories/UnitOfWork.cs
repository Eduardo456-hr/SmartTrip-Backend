using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Repositories;
namespace SocialMedia.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SmartTripContext _context;
        private readonly IDbConnectionFactory _connectionFactory;

        private IPasajeroRepository? _pasajeroRepository;
        private IConductorRepository? _conductorRepository;
        private IViajeRepository? _viajeRepository;

        public UnitOfWork(SmartTripContext context, IDbConnectionFactory connectionFactory)
        {
            _context = context;
            _connectionFactory = connectionFactory;
        }

        public IPasajeroRepository PasajeroRepository =>
            _pasajeroRepository ??= new PasajeroRepository(_context);

        public IConductorRepository ConductorRepository =>
            _conductorRepository ??= new ConductorRepository(_context);

        public IViajeRepository ViajeRepository =>
    _viajeRepository ??= new ViajeRepository(_context, _connectionFactory);

        public void Dispose()
        {
            _context.Dispose();
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