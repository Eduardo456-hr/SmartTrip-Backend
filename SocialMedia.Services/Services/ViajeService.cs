using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Services.Interfaces;
using System.Net;

namespace SocialMedia.Services.Services
{
    public class ViajeService : IViajeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ViajeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task InsertViaje(Viaje viaje)
        {
            var conductor = await _unitOfWork.ConductorRepository.GetConductorByIdAsync(viaje.ConductorId);

            if (conductor == null)
                throw new BusinessException("El conductor no existe.", HttpStatusCode.NotFound);

            if (viaje.FechaViaje <= DateTime.Now)
                throw new BusinessException("La fecha debe ser futura.");

            if (viaje.AsientosDisponibles <= 0)
                throw new BusinessException("Debe haber asientos disponibles.");
            if (viaje.Origen.Trim().ToLower() == viaje.Destino.Trim().ToLower())
                throw new BusinessException("El origen y el destino no pueden ser iguales.");

            if (viaje.Precio < 0)
                throw new BusinessException("El precio no puede ser negativo.");

            viaje.Estado = "Disponible";

            await _unitOfWork.ViajeRepository.InsertViaje(viaje);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Viaje>> GetViajesDisponiblesAsync(ViajeQueryFilter filters)
        {
            return await _unitOfWork.ViajeRepository.GetViajesDisponiblesAsync(filters);
        }

        public async Task<IEnumerable<Viaje>> GetHistorialByConductorAsync(int conductorId)
        {
            return await _unitOfWork.ViajeRepository.GetHistorialByConductorAsync(conductorId);
        }
    }
}