using Dapper;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Infrastructure.Data;

namespace SocialMedia.Infrastructure.Repositories
{
    public class ViajeRepository : IViajeRepository
    {
        private readonly SmartTripContext _context;
        private readonly IDbConnectionFactory _connectionFactory;

        public ViajeRepository(SmartTripContext context, IDbConnectionFactory connectionFactory)
        {
            _context = context;
            _connectionFactory = connectionFactory;
        }

        public async Task InsertViaje(Viaje viaje)
        {
            await _context.Viajes.AddAsync(viaje);
        }

        public async Task<Viaje?> GetViajeByIdAsync(int id)
        {
            return await _context.Viajes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Viaje>> GetViajesDisponiblesAsync(ViajeQueryFilter filters)
        {
            using var connection = _connectionFactory.CreateConnection();

            var sql = @"
                SELECT *
                FROM viajes
                WHERE AsientosDisponibles > 0
            ";

            if (!string.IsNullOrWhiteSpace(filters.Estado))
                sql += " AND Estado = @Estado";
            else
                sql += " AND Estado = 'Disponible'";

            if (!string.IsNullOrWhiteSpace(filters.Origen))
                sql += " AND Origen LIKE CONCAT('%', @Origen, '%')";

            if (!string.IsNullOrWhiteSpace(filters.Destino))
                sql += " AND Destino LIKE CONCAT('%', @Destino, '%')";

            if (filters.Fecha != null)
                sql += " AND DATE(FechaViaje) = DATE(@Fecha)";

            sql += " ORDER BY FechaViaje ASC";

            return await connection.QueryAsync<Viaje>(sql, filters);
        }

        public async Task<IEnumerable<Viaje>> GetHistorialByConductorAsync(int conductorId)
        {
            using var connection = _connectionFactory.CreateConnection();

            var sql = @"
                SELECT *
                FROM viajes
                WHERE ConductorId = @ConductorId
                ORDER BY FechaViaje DESC
            ";

            return await connection.QueryAsync<Viaje>(sql, new { ConductorId = conductorId });
        }
    }
}