using Identity.Domain.Entities;
using Identity.Domain.RepositoryAbstraction;
using Identity.Infastructure.Contexts;

namespace Identity.Infastructure.RepositoryImplementations
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(IdentityDbContext context) : base(context)
        {

        }
    }
}
