using Microsoft.VisualStudio.TestTools.UnitTesting;
using Overt.User.Application.Constracts;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Overt.Core.Test
{
    [TestClass]
    public class ApplicationTest : BaseTest
    {
        public ApplicationTest() : base()
        {
        }

        [TestMethod]
        public void DoSomethingTest()
        {
            var _userService = provider.GetService<IUserService>();
            //_userService.DoSomethingWithTrans();

            _userService.GetList();

            //Parallel.For(0, 100, (i) =>
            //{
            //    if (i % 2 == 0)
            //    {
            //        System.Diagnostics.Debug.WriteLine("Add: " + i);
            //        var _userService = provider.GetService<IUserService>();
            //        _userService.Add();
            //    }
            //    else
            //    {
            //        System.Diagnostics.Debug.WriteLine("GetList: " + i);
            //        var _userService = provider.GetService<IUserService>();
            //        _userService.GetList();
            //    }
            //});

            //var tasks = new List<Task>();
            //for (int i = 0; i < 2; i++)
            //{
            //    var task = Task.Run(() =>
            //    {
            //        var _userService = provider.GetService<IUserService>();
            //        _userService.DoSomethingWithTrans();
            //    });
            //    tasks.Add(task);
            //}
            //Task.WaitAll(tasks.ToArray());
        }

        [TestMethod]
        public void DoSomeTest()
        {
            var _userService = provider.GetService<IUserService>();
            _userService.DoSomethingWithTrans();
        }



        [TestMethod]
        public void GetByIdsTest()
        {
            var _userService = provider.GetService<IUserService>();
            _userService.GetByIds();
        }
    }
}
