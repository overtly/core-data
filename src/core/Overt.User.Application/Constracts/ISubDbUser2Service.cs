using Overt.User.Application.Models;
using System.Threading.Tasks;

namespace Overt.User.Application.Constracts
{
    public interface ISubDbUser2Service
    {
        int Add(UserPostModel model);
        UserModel Get(int userId, bool isMaster = false);

        Task<int> AddAsync(UserPostModel model);

        Task<UserModel> GetAsync(int userId, bool isMaster = false);
    }
}
