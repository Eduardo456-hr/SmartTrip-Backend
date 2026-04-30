using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SocialMedia.Core.Entities;
using SocialMedia.Core.QueryFilters;

namespace SocialMedia.Services.Interfaces
{
    public interface IViajeService
    {
        Task InsertViaje(Viaje viaje);
        Task<IEnumerable<Viaje>> GetViajesDisponiblesAsync(ViajeQueryFilter filters);
        Task<IEnumerable<Viaje>> GetHistorialByConductorAsync(int conductorId);
    }
}