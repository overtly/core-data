using AutoMapper;
using Overt.User.Application.Constracts;
using Overt.User.Application.Models;
using Overt.User.Domain.Contracts;
using Overt.User.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overt.User.Application.Services
{
    public class UserLongIdService : IUserLongIdService
    {
        IMapper _mapper;
        IUserLongIdRepository _userRepository;
        public UserLongIdService(
            IMapper mapper,
            IUserLongIdRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        #region SyncMethod
        public long Add(UserPostModel model)
        {
            if (!model.IsValid(out Exception ex))
                throw ex;

            var entity = _mapper.Map<UserLongIdEntity>(model);
            entity.AddTime = DateTime.Now;
            var result = _userRepository.Add(entity, true);
            if (!result)
                throw new Exception($"新增失败");

            var entity2 = _mapper.Map<UserLongIdEntity>(model);
            entity2.AddTime = DateTime.Now;
            var result2 = _userRepository.Add(entity2);
            if (!result2)
                throw new Exception($"新增失败");

            return entity.UserId;
        }

        public bool Add(params UserPostModel[] models)
        {
            if ((models?.Count() ?? 0) <= 0)
                throw new Exception("必须提供");

            var entities = _mapper.Map<List<UserLongIdEntity>>(models);
            entities.ForEach(oo =>
            {
                oo.AddTime = DateTime.Now;
            });
            var result = _userRepository.Add(entities.ToArray());
            if (!result)
                throw new Exception($"新增失败");
            return result;
        }

        public bool Delete(long userId)
        {
            if (userId <= 0)
                throw new Exception($"UserId必须大于0");

            return _userRepository.Delete(oo => oo.UserId == userId);
        }

        public UserModel Get(long userId, bool isMaster = false)
        {
            if (userId <= 0)
                throw new Exception($"UserId必须大于0");

            var entity = _userRepository.Get(oo => oo.UserId == userId, isMaster: isMaster);
            return _mapper.Map<UserModel>(entity);
        }

        public List<UserModel> GetList(List<long> userIds, bool isMaster = false)
        {
            if ((userIds?.Count ?? 0) <= 0)
                throw new Exception($"UserIds至少提供一个");

            var entities = _userRepository.GetList(1, userIds.Count, oo => userIds.Contains(oo.UserId), isMaster: isMaster);
            return _mapper.Map<List<UserModel>>(entities);
        }
        #endregion

        #region AsyncMethod
        public async Task<long> AddAsync(UserPostModel model)
        {
            if (!model.IsValid(out Exception ex))
                throw ex;

            var entity = _mapper.Map<UserLongIdEntity>(model);
            entity.AddTime = DateTime.Now;
            var result = await _userRepository.AddAsync(entity, true);
            if (!result)
                throw new Exception($"新增失败");
            return entity.UserId;
        }

        public async Task<bool> AddAsync(params UserPostModel[] models)
        {
            if ((models?.Count() ?? 0) <= 0)
                throw new Exception("必须提供");

            var entities = _mapper.Map<List<UserLongIdEntity>>(models);
            entities.ForEach(oo =>
            {
                oo.AddTime = DateTime.Now;
            });
            var result = await _userRepository.AddAsync(entities.ToArray());
            if (!result)
                throw new Exception($"新增失败");
            return result;
        }

        public async Task<bool> DeleteAsync(long userId)
        {
            if (userId <= 0)
                throw new Exception($"UserId必须大于0");

            return await _userRepository.DeleteAsync(oo => oo.UserId == userId);
        }

        public async Task<UserModel> GetAsync(long userId, bool isMaster = false)
        {
            if (userId <= 0)
                throw new Exception($"UserId必须大于0");

            var entity = await _userRepository.GetAsync(oo => oo.UserId == userId, isMaster: isMaster);
            return _mapper.Map<UserModel>(entity);
        }

        public async Task<List<UserModel>> GetListAsync(List<long> userIds, bool isMaster = false)
        {
            if ((userIds?.Count ?? 0) <= 0)
                throw new Exception($"UserIds至少提供一个");

            var entities = await _userRepository.GetListAsync(1, userIds.Count, oo => userIds.Contains(oo.UserId), isMaster: isMaster);
            return _mapper.Map<List<UserModel>>(entities);
        }
        #endregion

    }
}
