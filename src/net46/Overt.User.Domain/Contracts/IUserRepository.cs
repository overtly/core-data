using Overt.Core.Data;
using Overt.User.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Overt.User.Domain.Contracts
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        /// <summary>
        /// 其他sql 本案例中 统计UserName去重个数
        /// </summary>
        /// <returns></returns>
        Task<List<string>> OtherSqlAsync();
    }
}
