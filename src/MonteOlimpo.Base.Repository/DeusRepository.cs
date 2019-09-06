using MonteOlimpo.Base.Core.Data.Repository;
using MonteOlimpo.Base.Core.Domain.UnitOfWork;
using MonteOlimpo.Domain.Models;
using MonteOlimpo.Domain.Repository;

namespace MonteOlimpo.Repository
{
    public class DeusRepository : BaseRepository<Deus>, IDeusRepository
    {
        public DeusRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
