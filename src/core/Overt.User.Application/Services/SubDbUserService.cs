using AutoMapper;
using Overt.User.Application.Constracts;
using Overt.User.Application.Models;
using Overt.User.Domain.Contracts;
using Overt.User.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Overt.User.Application.Services
{
    public class SubDbUserService : ISubDbUserService
    {
        IMapper _mapper;
        ISubDbUserRepository _repository;
        public SubDbUserService(
            IMapper mapper,
            ISubDbUserRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public int Add(UserPostModel model)
        {
            if (!model.IsValid(out Exception ex))
                throw ex;

            var entity = _mapper.Map<SubDbUserEntity>(model);
            entity.AddTime = DateTime.Now;
            var result = _repository.Add(entity, true);
            if (!result)
                throw new Exception($"新增失败");
            return entity.UserId;
        }

        public UserModel Get(int userId, bool isMaster = false)
        {
            if (userId <= 0)
                throw new Exception($"UserId必须大于0");

            var entity = _repository.Get(oo => oo.UserId == userId, isMaster: isMaster);
            return _mapper.Map<UserModel>(entity);
        }

        public async Task<int> AddAsync(UserPostModel model)
        {
            if (!model.IsValid(out Exception ex))
                throw ex;

            var entity = _mapper.Map<SubDbUserEntity>(model);
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

            var entity = await _repository.GetAsync(oo => oo.UserId == userId, isMaster: isMaster);
            return _mapper.Map<UserModel>(entity);
        }
    }
}
