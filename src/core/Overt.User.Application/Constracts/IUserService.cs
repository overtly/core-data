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
        #region SyncMethod
        UserModel Get(int userId, bool isMaster = false);

        List<UserModel> GetList(List<int> userIds, bool isMaster = false);

        (int, List<UserModel>) GetPage(UserSearchModel model);

        int Add(UserPostModel model);

        bool Add(params UserPostModel[] models);

        bool Update(int userId, bool isSex);

        bool Delete(int userId);

        List<string> OtherSql();

        /// <summary>
        /// 在事务中执行
        /// </summary>
        /// <returns></returns>
        bool ExecuteInTransaction();
        #endregion

        #region AsyncMethod
        Task<UserModel> GetAsync(int userId, bool isMaster = false);

        Task<List<UserModel>> GetListAsync(List<int> userIds, bool isMaster = false);

        Task<(int, List<UserModel>)> GetPageAsync(UserSearchModel model);

        Task<int> AddAsync(UserPostModel model);

        Task<bool> AddAsync(params UserPostModel[] models);

        Task<bool> UpdateAsync(int userId, bool isSex);

        Task<bool> DeleteAsync(int userId);

        Task<List<string>> OtherSqlAsync();

        /// <summary>
        /// 在事务中执行
        /// </summary>
        /// <returns></returns>
        Task<bool> ExecuteInTransactionAsync();
        #endregion
    }
}
