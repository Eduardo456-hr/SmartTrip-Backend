using System;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPasajeroRepository PasajeroRepository { get; }
        IConductorRepository ConductorRepository { get; }
        IViajeRepository ViajeRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}