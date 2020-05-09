using Overt.User.Application.Models;
using Overt.User.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Overt.User.Application.Constracts
{
    public interface IUserService
    {
        Task<UserModel> GetAsync(int userId, bool isMaster = false);

        Task<List<UserModel>> GetListAsync(List<int> userIds, bool isMaster = false);

        Task<(int, List<UserModel>)> GetPageAsync(UserSearchModel model);

        Task<int> AddAsync(UserPostModel model);

        Task<bool> AddAsync(params UserPostModel[] models);

        Task<bool> UpdateAsync(int userId, bool isSex);

        Task<bool> DeleteAsync(int userId);

        Task<List<string>> OtherSqlAsync();
    }
}
