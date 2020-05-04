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
            return "CREATE TABLE `" + tableName + "` (" +
                   "  `UserId` int(11) NOT NULL AUTO_INCREMENT," +
                   "  `UserName` varchar(200) DEFAULT NULL," +
                   "  `Password` varchar(200) DEFAULT NULL," +
                   "  `RealName` varchar(200) DEFAULT NULL," +
                   "  `AddTime` datetime DEFAULT NULL," +
                   "  `IsSex` bit(1) DEFAULT NULL," +
                   "  `JsonValue` json DEFAULT NULL," +
                   "  `Join` varchar(255) DEFAULT NULL," +
                   "  `ENValue` int(11) DEFAULT NULL," +
                   "  PRIMARY KEY(`UserId`)" +
                   ") ENGINE = InnoDB AUTO_INCREMENT = 3748 DEFAULT CHARSET = utf8mb4; ";
        };
    }
}
