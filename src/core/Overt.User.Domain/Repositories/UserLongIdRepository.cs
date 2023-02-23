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
    public class UserLongIdRepository : BaseRepository<UserLongIdEntity>, IUserLongIdRepository
    {
        public UserLongIdRepository(IConfiguration configuration)
            : base(configuration) // dbStoreKey 可用于不同数据库切换，连接字符串key前缀：xxx.master xxx.secondary
        {
        }

    }
}
