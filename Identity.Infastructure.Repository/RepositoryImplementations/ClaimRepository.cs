using Identity.Domain.Entities;
using Identity.Domain.RepositoryAbstraction;
using Identity.Infastructure.Contexts;

namespace Identity.Infastructure.RepositoryImplementations
{
    public class ClaimRepository : BaseRepository<Claim>, IClaimRepository
    {
        public ClaimRepository(IdentityDbContext context) : base(context)
        {

        }
    }
}
