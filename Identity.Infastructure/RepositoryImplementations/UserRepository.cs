using Identity.Domain.Entities;
using Identity.Domain.RepositoryAbstraction;
using Identity.Infastructure.Contexts;

namespace Identity.Infastructure.RepositoryImplementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IdentityDbContext context) : base(context)
        {

        }
    }
}
