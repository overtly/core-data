using Overt.User.Application.Constracts;
using Overt.User.Domain.Contracts;
using Overt.User.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Overt.User.Application.Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserEntity DoSomething()
        {
            return _userRepository.DoSomething();
        }

        public bool DoSomethingWithTrans()
        {
            using (var scope = new TransactionScope())
            {
                var entity = new UserEntity()
                {
                    AddTime = DateTime.Now,
                    IsSex = false,
                    UserName = DateTime.Now.ToString("yyyyMMddHHmmss")
                };
                var addRes = _userRepository.Add(entity, true);
                _userRepository.Set(() => new { RealName = $"R_{DateTime.Now.ToString("yyyyMMddHHmmss")}" }, oo => oo.UserId == entity.UserId);
                scope.Complete();
                return true;
            }
        }

        public List<UserEntity> GetList()
        {
            var list = _userRepository.GetList(1, 10);
            return list.ToList();
        }


        public List<UserEntity> GetByIds()
        {
            var ids = new List<int>();
            for (int i = 0; i < 999; i++)
            {
                ids.Add(i + 1);
            }
            var list = _userRepository.GetList(1, int.MaxValue, oo => ids.Contains(oo.UserId));
            return list.ToList();
        }
    }
}
