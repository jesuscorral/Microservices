using System;
using System.Threading.Tasks;
using JCP.Ordering.Infrastructure.Repositories.Interfaces;

namespace JCP.Ordering.Infrastructure.Repositories
{
    public class OrderingRepository : IOrderingRepository
    {
        // Guardar el order item en la base de datos.
        public async Task<bool> SaveOrderItemAsync(Guid id)
        {
            return true;
        }
    }
}
