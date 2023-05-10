using Transferencias.Persistence.Entities;

namespace Transferencias.Persistence.Repositories
{
    public class TransferenciaRepository : CoreRepository<Transferencia,ApplicationDbContext>
    {
        public TransferenciaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
