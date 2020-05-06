using Dapper;
using Overt.Core.Data;
using Overt.User.Domain.Contracts;
using Overt.User.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overt.User.Domain.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository()
            : base() // dbStoreKey 可用于不同数据库切换，连接字符串key前缀：xxx.master xxx.secondary
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
