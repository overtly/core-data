using Sodao.User.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sodao.User.Application.Constracts
{
    public interface IUserService
    {
        UserEntity DoSomething();

        List<UserEntity> GetList();

        bool DoSomethingWithTrans();

        List<UserEntity> GetByIds();
    }
}
