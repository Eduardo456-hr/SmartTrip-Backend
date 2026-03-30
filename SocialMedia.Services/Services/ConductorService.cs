using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Services.Interfaces;

namespace SocialMedia.Services.Services
{
    public class ConductorService : IConductorService
    {
        private readonly IConductorRepository _conductorRepository;

        public ConductorService(IConductorRepository conductorRepository)
        {
            _conductorRepository = conductorRepository;
        }

        public async Task InsertConductor(Conductore conductor)
        {
            await _conductorRepository.InsertConductor(conductor);
        }
        public async Task<IEnumerable<Conductore>> GetAllConductoresAsync()
        {
            return await _conductorRepository.GetAllConductoresAsync();
        }
    }
}
