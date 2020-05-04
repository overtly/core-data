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

            // 删除
            var delResult = _userService.DeleteAsync(userId);

            // ... 其他更多用法详见Readme，可有很多组合方式，并不局限于目前案例所示

        }
    }
}
