using Overt.User.Application.Models;
using Overt.User.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Overt.User.Application.Constracts
{
    public interface IUserLongIdService
    {
        #region SyncMethod
        UserModel Get(long userId, bool isMaster = false);

        List<UserModel> GetList(List<long> userIds, bool isMaster = false);

        long Add(UserPostModel model);

        bool Add(params UserPostModel[] models);
        #endregion

        #region AsyncMethod
        Task<UserModel> GetAsync(long userId, bool isMaster = false);

        Task<List<UserModel>> GetListAsync(List<long> userIds, bool isMaster = false);

        Task<long> AddAsync(UserPostModel model);

        Task<bool> AddAsync(params UserPostModel[] models);
        #endregion
    }
}
