using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Interfaces
{
    public interface IConductorRepository
    {
        Task<IEnumerable<Conductore>> GetAllConductoresAsync();
        Task<Conductore> GetConductorByIdAsync(int id);
        Task InsertConductor(Conductore conductor);

        Task<Conductore> GetConductorByCorreoAsync(string correo);

        Task UpdateConductor(Conductore conductor);
        Task DeleteConductor(Conductore conductor);

    }
}
