using System;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPasajeroRepository PasajeroRepository { get; }
        IConductorRepository ConductorRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}