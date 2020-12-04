using System;
using System.Threading.Tasks;

namespace JCP.Ordering.Infrastructure.Repositories.Interfaces
{
    public interface IOrderingRepository
    {
        // TODO - Cambiar por el correcto
        Task<bool> SaveOrderItemAsync(Guid id);
    }
}
