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
    public class UserService : IUserService
    {
        IMapper _mapper;
        IUserRepository _userRepository;
        ISubUserRepository _subUserRepository;
        public UserService(
            IMapper mapper,
            IUserRepository userRepository,
            ISubUserRepository subUserRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _subUserRepository = subUserRepository;
        }

        public async Task<int> AddAsync(UserPostModel model)
        {
            if (!model.IsValid(out Exception ex))
                throw ex;

            var entity = _mapper.Map<UserEntity>(model);
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

            var entities = _mapper.Map<List<UserEntity>>(models);
            entities.ForEach(oo =>
            {
                oo.AddTime = DateTime.Now;
            });
            var result = await _userRepository.AddAsync(entities.ToArray());
            if (!result)
                throw new Exception($"新增失败");
            return result;
        }

        public async Task<bool> DeleteAsync(int userId)
        {
            if (userId <= 0)
                throw new Exception($"UserId必须大于0");

            return await _userRepository.DeleteAsync(oo => oo.UserId == userId);
        }

        public async Task<UserModel> GetAsync(int userId, bool isMaster = false)
        {
            if (userId <= 0)
                throw new Exception($"UserId必须大于0");

            var entity = await _userRepository.GetAsync(oo => oo.UserId == userId, isMaster: isMaster);
            return _mapper.Map<UserModel>(entity);
        }

        public async Task<List<UserModel>> GetListAsync(List<int> userIds, bool isMaster = false)
        {
            if ((userIds?.Count ?? 0) <= 0)
                throw new Exception($"UserIds至少提供一个");

            var entities = await _userRepository.GetListAsync(1, userIds.Count, oo => userIds.Contains(oo.UserId), isMaster: isMaster);
            return _mapper.Map<List<UserModel>>(entities);
        }

        public async Task<(int, List<UserModel>)> GetPageAsync(UserSearchModel model)
        {
            var expression = model.GetExpression();
            var orders = model.GetOrder();
            var count = await _userRepository.CountAsync(expression, model.IsMaster);
            var entities = await _userRepository.GetListAsync(model.Page, model.Size, expression, isMaster: model.IsMaster, orderByFields: orders);
            var models = _mapper.Map<List<UserModel>>(entities);
            return (count, models);
        }

        public async Task<List<string>> OtherSqlAsync()
        {
            var result = await _userRepository.OtherSqlAsync();
            return result;
        }

        public async Task<bool> UpdateAsync(int userId, bool isSex)
        {
            if (userId <= 0)
                throw new Exception($"UserId必须大于0");

            // 第一种
            var dic = new Dictionary<string, object>()
            {
                { nameof(UserEntity.IsSex), isSex }
            };
            var updateResult1 = await _userRepository.SetAsync(() => dic, oo => oo.UserId == userId);

            // 第二种
            var updateResult2 = await _userRepository.SetAsync(() => new { IsSex = isSex }, oo => oo.UserId == userId);

            // 第三种
            var entity = await _userRepository.GetAsync(oo => oo.UserId == userId, isMaster: true);
            if (entity?.UserId != userId)
                throw new Exception($"无可更新数据");
            entity.IsSex = isSex;
            var updateResult3 = await _userRepository.SetAsync(entity);

            // 第三种
            var updateResult4 = await _userRepository.SetAsync(nameof(UserEntity.Age), 10, oo => oo.UserId == userId);

            return updateResult3;
        }

        public async Task<bool> ExecuteInTransactionAsync()
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await _userRepository.AddAsync(new UserEntity()
                {
                    UserName = "11111111111",
                    RealName = "11111111111",
                    Password = "123456",
                    IsSex = false,
                    JsonValue = "{}",
                    AddTime = DateTime.Now
                });
                result &= await _subUserRepository.AddAsync(new SubUserEntity());

                scope.Complete();
            }
            //await _userRepository.BeginTransactionAsync(async transaction =>
            //{
            //    // 传递事务
            //    _subUserRepository.Transaction = transaction;

            //    var result = await _userRepository.AddAsync(new UserEntity());
            //    result &= await _subUserRepository.AddAsync(new SubUserEntity());

            //    transaction.Commit();
            //    return result;
            //});
            return true;
        }
    }
}
