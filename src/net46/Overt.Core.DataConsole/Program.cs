using Overt.User.Application.Constracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Overt.Core.DataConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // autofac
            AutofacContainer.Register();
            
            #region 单表
            var _userService = AutofacContainer.Container.Resolve<IUserService>();

            // 新增
            var userId = _userService.AddAsync(new User.Application.Models.UserPostModel()
            {
                UserName = "TEST1",
                RealName = "TEST1",
                Password = "123456",
                IsSex = false,
                JsonValue = "{}"
            }).Result;

            // 修改
            var setResult = _userService.UpdateAsync(userId, true).Result;

            // 单条查询
            var getResult = _userService.GetAsync(userId).Result;

            // 多条查询
            var listResult = _userService.GetListAsync(new List<int>() { userId }).Result;

            // 分页查询
            var pageResult = _userService.GetPageAsync(new User.Application.Models.UserSearchModel()
            {
                Page = 1,
                Size = 10,
                UserIds = new List<int> { userId }
            }).Result;

            // 自定义SQL
            var otherResult = _userService.OtherSqlAsync().Result;

            // 删除
            var delResult = _userService.DeleteAsync(userId).Result;

            // ... 其他更多用法详见Readme，可有很多组合方式，并不局限于目前案例所示
            #endregion

            #region 分表
            var _subUserService = AutofacContainer.Container.Resolve<ISubUserService>();

            // 添加
            var addResult1 = _subUserService.AddAsync(new User.Application.Models.UserPostModel()
            {
                UserName = "TEST_Sub",
                RealName = "TEST_Sub",
                Password = "123456",
                IsSex = false,
                JsonValue = "{}"
            }).Result;

            // 获取
            var getResult1 = _subUserService.GetAsync(addResult1).Result;
            #endregion

            #region 分库
            var _subDbUserService = AutofacContainer.Container.Resolve<ISubDbUserService>();

            // 添加
            var addResult2 = _subDbUserService.AddAsync(new User.Application.Models.UserPostModel()
            {
                UserName = "TEST_SubDb",
                RealName = "TEST_SubDb",
                Password = "123456",
                IsSex = false,
                JsonValue = "{}"
            }).Result;

            // 获取
            var getResult2 = _subDbUserService.GetAsync(addResult2).Result;
            #endregion

            #region 分库2
            var _subDbUser2Service = AutofacContainer.Container.Resolve<ISubDbUser2Service>();

            // 添加
            var addResult3 = _subDbUser2Service.AddAsync(new User.Application.Models.UserPostModel()
            {
                UserName = "TEST_SubDb",
                RealName = "TEST_SubDb",
                Password = "123456",
                IsSex = false,
                JsonValue = "{}"
            }).Result;

            // 获取
            var getResult3 = _subDbUser2Service.GetAsync(addResult3).Result;
            #endregion

            #region 事务
            var transResult = _userService.ExecuteInTransactionAsync().Result;
            #endregion
        }
    }
}
