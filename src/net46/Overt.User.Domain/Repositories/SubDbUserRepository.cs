using Overt.Core.Data;
using Overt.User.Domain.Contracts;
using Overt.User.Domain.Entities;

namespace Overt.User.Domain.Repositories
{
    public class SubDbUserRepository : BaseRepository<SubDbUserEntity>, ISubDbUserRepository
    {
        public SubDbUserRepository()
            : base("subdb")
        {
        }
    }
}
