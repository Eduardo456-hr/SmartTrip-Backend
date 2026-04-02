using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMedia.Core.Entities;

namespace SocialMedia.Services.Interfaces
{
    public interface IConductorService
    {
        Task InsertConductor(Conductore conductor);
        Task<IEnumerable<Conductore>> GetAllConductoresAsync();
        Task UpdateConductor(int id, Conductore conductor);
        Task DeleteConductor(int id);



    }
}