using Overt.Core.Data;
using Overt.User.Domain.Contracts;
using Overt.User.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Overt.User.Domain.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(IConfiguration configuration) 
            : base(configuration) // dbStoreKey 可用于不同数据库切换，连接字符串key前缀：xxx.master xxx.secondary
        {
        }

        public async Task<List<string>> OtherSqlAsync()
        {
            // 表名最好使用这个方法获取，支持分表，分表案例详见其他案例
            var tableName = GetTableName();
            var sql = $"select distinct([UserName]) from [{tableName}]";
            return await Execute(async connecdtion =>
            {
                var task = await connecdtion.QueryAsync<string>(sql);
                return task.ToList();
            });
        }
    }
}
