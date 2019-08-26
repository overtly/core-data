using Microsoft.Extensions.Configuration;
using Overt.Core.Data;
using Overt.User.Domain.Contracts;
using Overt.User.Domain.Entities;
using System;

namespace Overt.User.Domain.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(IConfiguration configuration)
            : base(configuration, "ss")
        {
        }

        public override Func<string> TableNameFunc => () =>
        {
            var tableName = $"{GetMainTableName()}_Test";
            return tableName;
        };

        public override Func<string, string> CreateScriptFunc => (tableName) =>
        {
            // MySql
            //return "CREATE TABLE `" + tableName + "` (" +
            //       "  `UserId` int(11) NOT NULL AUTO_INCREMENT," +
            //       "  `UserName` varchar(200) DEFAULT NULL," +
            //       "  `Password` varchar(200) DEFAULT NULL," +
            //       "  `RealName` varchar(200) DEFAULT NULL," +
            //       "  `AddTime` datetime DEFAULT NULL," +
            //       "  `IsSex` bit(1) DEFAULT NULL," +
            //       "  `JsonValue` json DEFAULT NULL," +
            //       "  `Join` varchar(255) DEFAULT NULL," +
            //       "  `ENValue` int(11) DEFAULT NULL," +
            //       "  PRIMARY KEY(`UserId`)" +
            //       ") ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8mb4; ";

            // MSSql
            return "CREATE TABLE [" + tableName + "] (" +
                   "  [UserId] int IDENTITY(1,1) NOT NULL," +
                   "  [UserName] varchar(200) DEFAULT NULL," +
                   "  [Password] varchar(200) DEFAULT NULL," +
                   "  [RealName] varchar(200) DEFAULT NULL," +
                   "  [AddTime] datetime DEFAULT NULL," +
                   "  [IsSex] bit DEFAULT NULL," +
                   "  [ENValue] int DEFAULT NULL," +
                   "  PRIMARY KEY CLUSTERED ([UserId])" +
                   ")";
        };
    }
}
