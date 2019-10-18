using Overt.User.Application.Constracts;
using Overt.User.Domain.Contracts;
using Overt.User.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            return null;
        }

        public bool DoSomethingWithTrans()
        {
            using (var transaction = _userRepository.BeginTransaction())
            {
                try
                {
                    var entity = new UserEntity()
                    {
                        AddTime = DateTime.Now,
                        IsSex = false,
                        UserName = "20190109154925"
                    };
                    var addRes = _userRepository.Add(entity, true);
                }
                catch (Exception ex)
                {

                    throw;
                }

                //_userRepository.Set(() => new { RealName = $"R_{DateTime.Now.ToString("yyyyMMddHHmmss")}" }, oo => oo.UserId == entity.UserId);
                transaction.Commit();
            }
            return true;
        }

        public List<UserEntity> GetList()
        {
            var a = new List<int> { 1 };
            var list = _userRepository.GetOffsets(2, 1, oo => a.Contains(oo.UserId));
            return list?.ToList();
        }


        public List<UserEntity> GetByIds()
        {
            var ids = new List<int>();
            for (int i = 0; i < 999; i++)
            {
                ids.Add(i + 1);
            }
            var list = _userRepository.GetList(1, int.MaxValue, oo => ids.Contains(oo.UserId));

            var ids1 = ids.ToArray();
            var list1 = _userRepository.GetList(1, int.MaxValue, oo => ids1.Contains(oo.UserId));

            var list2 = _userRepository.GetList(1, int.MaxValue, oo => new List<int> { 1, 2, 3, 4, 5 }.Contains(oo.UserId));

            return list.ToList();
        }
    }
}
