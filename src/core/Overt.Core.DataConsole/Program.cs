using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Overt.User.Application;
using Overt.User.Application.Constracts;
using System.Collections.Generic;

namespace Overt.Core.DataConsole
{
    class Program
    {
        static IServiceProvider provider;
        static Program()
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(AppContext.BaseDirectory)
                   .AddJsonFile("appsettings.json", true);

            var configuration = builder.Build();

            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddApplicationDI();

            provider = services.BuildServiceProvider();

        }
        static void Main(string[] args)
        {
            #region 单表
            var _userService = provider.GetService<IUserService>();

            // 新增
            var userId = _userService.AddAsync(new User.Application.Models.UserPostModel()
            {
                UserName = "TEST1",
                RealName = "TEST1",
                Password = "123456",
                IsSex = false,
                JsonValue = "{}"
            }).Result;

            // 批量新增
            var batchAddResult = _userService.AddAsync(new User.Application.Models.UserPostModel()
            {
                UserName = "TEST1" + DateTime.Now.ToString("fffff"),
                RealName = "TEST1" + DateTime.Now.ToString("fffff"),
                Password = "123456",
                IsSex = false,
                JsonValue = "{}"
            }, new User.Application.Models.UserPostModel()
            {
                UserName = "TEST2" + DateTime.Now.ToString("fffff"),
                RealName = "TEST2" + DateTime.Now.ToString("fffff"),
                Password = "123456",
                IsSex = false,
                JsonValue = "{}"
            }).Result;

            // 修改
            var setResult = _userService.UpdateAsync(userId, true).Result;

            // 单条查询
            var getResult = _userService.GetAsync(userId, true).Result;

            // 多条查询
            var listResult = _userService.GetListAsync(new List<int>() { userId }, true).Result;

            // 分页查询
            var pageResult = _userService.GetPageAsync(new User.Application.Models.UserSearchModel()
            {
                Page = 1,
                Size = 10,
                UserIds = new List<int> { userId },
                IsMaster = true
            }).Result;

            // 自定义SQL
            var otherResult = _userService.OtherSqlAsync().Result;

            // 删除
            var delResult = _userService.DeleteAsync(userId).Result;

            // ... 其他更多用法详见Readme，可有很多组合方式，并不局限于目前案例所示
            #endregion

            #region 分表
            var _subUserService = provider.GetService<ISubUserService>();

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
            var _subDbUserService = provider.GetService<ISubDbUserService>();

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

            #region 事务
            var transResult = _userService.ExecuteInTransactionAsync().Result;
            #endregion
        }
    }
}
