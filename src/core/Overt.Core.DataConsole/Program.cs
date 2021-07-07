using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Overt.User.Application;
using Overt.User.Application.Constracts;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            ExecuteMethod();

            ExecuteMethodAsync().GetAwaiter().GetResult();
        }

        #region Static Method
        private static async Task ExecuteMethodAsync()
        {
            var _userService = provider.GetService<IUserService>();

            #region 单表
            // 新增
            var userId = await _userService.AddAsync(new User.Application.Models.UserPostModel()
            {
                UserName = "TEST1",
                RealName = "TEST1",
                Password = "123456",
                IsSex = false,
                JsonValue = "{}"
            });

            // 批量新增
            var batchAddResult = await _userService.AddAsync(new User.Application.Models.UserPostModel()
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
            });

            // 修改
            var setResult = await _userService.UpdateAsync(userId, true);

            // 单条查询
            var getResult = await _userService.GetAsync(userId, true);

            // 多条查询
            var listResult = await _userService.GetListAsync(new List<int>() { userId }, true);

            // 分页查询
            var pageResult = await _userService.GetPageAsync(new User.Application.Models.UserSearchModel()
            {
                Page = 1,
                Size = 10,
                UserIds = new List<int> { userId },
                IsMaster = true
            });

            // 自定义SQL
            var otherResult = await _userService.OtherSqlAsync();

            // 删除
            var delResult = await _userService.DeleteAsync(userId);

            // ... 其他更多用法详见Readme，可有很多组合方式，并不局限于目前案例所示
            #endregion

            #region 分表
            var _subUserService = provider.GetService<ISubUserService>();

            // 添加
            var addResult1 = await _subUserService.AddAsync(new User.Application.Models.UserPostModel()
            {
                UserName = "TEST_Sub",
                RealName = "TEST_Sub",
                Password = "123456",
                IsSex = false,
                JsonValue = "{}"
            });

            // 获取
            var getResult1 = await _subUserService.GetAsync(addResult1);
            #endregion

            #region 分库
            var _subDbUserService = provider.GetService<ISubDbUserService>();

            // 添加
            var addResult2 = await _subDbUserService.AddAsync(new User.Application.Models.UserPostModel()
            {
                UserName = "TEST_SubDb",
                RealName = "TEST_SubDb",
                Password = "123456",
                IsSex = false,
                JsonValue = "{}"
            });

            // 获取
            var getResult2 = await _subDbUserService.GetAsync(addResult2);
            #endregion

            #region 分库2
            var _subDbUser2Service = provider.GetService<ISubDbUser2Service>();

            // 添加
            var addResult3 = await _subDbUser2Service.AddAsync(new User.Application.Models.UserPostModel()
            {
                UserName = "TEST_SubDb2",
                RealName = "TEST_SubDb2",
                Password = "123456",
                IsSex = false,
                JsonValue = "{}"
            });

            // 获取
            var getResult3 = await _subDbUser2Service.GetAsync(addResult3);
            #endregion

            #region 事务
            var transResult = await _userService.ExecuteInTransactionAsync();
            #endregion
        }

        /// <summary>
        /// 同步方法
        /// </summary>
        private static void ExecuteMethod()
        {
            var _userService = provider.GetService<IUserService>();

            #region 单表
            // 新增
            var userId = _userService.Add(new User.Application.Models.UserPostModel()
            {
                UserName = "TEST1",
                RealName = "TEST1",
                Password = "123456",
                IsSex = false,
                JsonValue = "{}"
            });

            // 批量新增
            var batchAddResult = _userService.Add(new User.Application.Models.UserPostModel()
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
            });

            // 修改
            var setResult = _userService.Update(userId, true);

            // 单条查询
            var getResult = _userService.Get(userId, true);

            // 多条查询
            var listResult = _userService.GetList(new List<int>() { userId }, true);

            // 分页查询
            var pageResult = _userService.GetPage(new User.Application.Models.UserSearchModel()
            {
                Page = 1,
                Size = 10,
                UserIds = new List<int> { userId },
                IsMaster = true
            });

            // 自定义SQL
            var otherResult = _userService.OtherSql();

            // 删除
            var delResult = _userService.Delete(userId);

            // ... 其他更多用法详见Readme，可有很多组合方式，并不局限于目前案例所示
            #endregion

            #region 分表
            var _subUserService = provider.GetService<ISubUserService>();

            // 添加
            var addResult1 = _subUserService.Add(new User.Application.Models.UserPostModel()
            {
                UserName = "TEST_Sub",
                RealName = "TEST_Sub",
                Password = "123456",
                IsSex = false,
                JsonValue = "{}"
            });

            // 获取
            var getResult1 = _subUserService.Get(addResult1);
            #endregion

            #region 分库
            var _subDbUserService = provider.GetService<ISubDbUserService>();

            // 添加
            var addResult2 = _subDbUserService.Add(new User.Application.Models.UserPostModel()
            {
                UserName = "TEST_SubDb",
                RealName = "TEST_SubDb",
                Password = "123456",
                IsSex = false,
                JsonValue = "{}"
            });

            // 获取
            var getResult2 = _subDbUserService.Get(addResult2);
            #endregion

            #region 分库2
            var _subDbUser2Service = provider.GetService<ISubDbUser2Service>();

            // 添加
            var addResult3 = _subDbUser2Service.Add(new User.Application.Models.UserPostModel()
            {
                UserName = "TEST_SubDb2",
                RealName = "TEST_SubDb2",
                Password = "123456",
                IsSex = false,
                JsonValue = "{}"
            });

            // 获取
            var getResult3 = _subDbUser2Service.Get(addResult3);
            #endregion

            #region 事务
            var transResult = _userService.ExecuteInTransaction();
            #endregion
        }
        #endregion
    }
}
