using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SocialMedia.Core.Entities;
using SocialMedia.Core.QueryFilters;

namespace SocialMedia.Core.Interfaces
{
    public interface IViajeRepository
    {
        Task InsertViaje(Viaje viaje);
        Task<Viaje?> GetViajeByIdAsync(int id);
        Task<IEnumerable<Viaje>> GetViajesDisponiblesAsync(ViajeQueryFilter filters);
        Task<IEnumerable<Viaje>> GetHistorialByConductorAsync(int conductorId);
    }
}