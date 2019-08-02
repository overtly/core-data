using Overt.Core.Data;
using Overt.User.Domain.Contracts;
using Overt.User.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Overt.User.Domain.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository() : base()
        {
        }

        public override Func<string> TableNameFunc => () =>
        {
            var tableName = $"{GetMainTableName()}_Test";
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

        public UserEntity DoSomething()
        {
            var userIds = new[] { 1, 2, 3, };
            var userName = "浙江啊啊啊";
            var aa = Get(oo => userName.Equals(oo.UserName));

            //var aa = Get(oo => "aa" == oo.UserName);
            return null;

            #region 增
            var model = new UserEntity()
            {
                UserName = "abc",
                RealName = "abc",
                Password = "123456",
            };
            var addResult = Add(model, true);
            #endregion

            #region 查
            var getResult = Get(oo => oo.UserId == model.UserId, oo => new { oo.UserId, oo.UserName });

            var countResult = Count(oo => oo.UserName.Contains("test"));
            #endregion

            #region 改

            model.RealName = "abc_aaa";
            var setResult = Set(model);

            //var setObj = new { RealName = "abc", UserName = "test" };
            //var setDic = new Dictionary<string, object>() { { "RealName", "abc" } };
            //var setResult = Set(() => setDic, oo => oo.UserId == model.UserId);
            #endregion

            #region 删
            var delResult = Delete(oo => oo.UserId == model.UserId);
            #endregion

            return null;
        }
    }

    public class TestValue
    {
        public List<int> Ids { get; set; } = new List<int>() { 3, 4, 5 };
    }
}
