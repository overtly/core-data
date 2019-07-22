using Sodao.Core.Data;
using Sodao.User.Domain.Entities;

namespace Sodao.User.Domain.Contracts
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
    }
}
