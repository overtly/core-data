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
            _userService.DoSomethingWithTrans();
            _userService.GetList();
        }
    }
}
