using AutoMapper;
using Overt.User.Application.Constracts;
using Overt.User.Application.Models;
using Overt.User.Domain.Contracts;
using Overt.User.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Overt.User.Application.Services
{
    public class SubUserService : ISubUserService
    {
        IMapper _mapper;
        ISubUserRepository _repository;
        public SubUserService(
            IMapper mapper,
            ISubUserRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<int> AddAsync(UserPostModel model)
        {
            if (!model.IsValid(out Exception ex))
                throw ex;

            // 分表标识赋值
            _repository.AddTime = DateTime.Now;

            var entity = _mapper.Map<SubUserEntity>(model);
            entity.AddTime = DateTime.Now;
            var result = await _repository.AddAsync(entity, true);
            if (!result)
                throw new Exception($"新增失败");
            return entity.UserId;
        }

        public async Task<UserModel> GetAsync(int userId, bool isMaster = false)
        {
            if (userId <= 0)
                throw new Exception($"UserId必须大于0");

            // 分表标识赋值
            _repository.AddTime = DateTime.Now;
            var entity = await _repository.GetAsync(oo => oo.UserId == userId, isMaster: isMaster);
            return _mapper.Map<UserModel>(entity);
        }
    }
}
