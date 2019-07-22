using Sodao.Core.Data;
using Sodao.User.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sodao.User.Domain.Contracts
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        /// <summary>
        /// 做什么东西
        /// </summary>
        /// <returns></returns>
        UserEntity DoSomething();
    }
}
