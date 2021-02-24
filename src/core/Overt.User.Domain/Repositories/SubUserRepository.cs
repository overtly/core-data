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
    public class SubUserRepository : BaseRepository<SubUserEntity>, ISubUserRepository
    {
        public SubUserRepository(IConfiguration configuration) : base(configuration)
        {
        }

        // Service层进行赋值即可
        public DateTime AddTime { get; set; }

        public override Func<string> TableNameFunc => () =>
        {
            var tableName = $"{GetMainTableName()}_{DateTime.Now.ToString("yyyyMMdd")}";
            return tableName;
        };

        public override Func<string, string> CreateScriptFunc => (tableName) =>
        {
            return $"CREATE TABLE [{tableName}] (" +
                    "  [UserId] int  IDENTITY(1,1) NOT NULL," +
                    "  [UserName] varchar(200) COLLATE Chinese_PRC_CI_AS DEFAULT NULL NULL," +
                    "  [Password] varchar(200) COLLATE Chinese_PRC_CI_AS DEFAULT NULL NULL," +
                    "  [RealName] varchar(200) COLLATE Chinese_PRC_CI_AS DEFAULT NULL NULL," +
                    "  [AddTime] datetime DEFAULT NULL NULL," +
                    "  [IsSex] bit DEFAULT NULL NULL," +
                    "  [Age] int DEFAULT 0 NOT NULL," +
                    "  [JsonValue] varchar(1000) COLLATE Chinese_PRC_CI_AS DEFAULT NULL NULL" +
                    ") ";
        };
    }
}
