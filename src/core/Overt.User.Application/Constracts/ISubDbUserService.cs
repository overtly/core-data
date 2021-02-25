using Overt.User.Application.Models;
using Overt.User.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Overt.User.Application.Constracts
{
    public interface ISubDbUserService
    {

        int Add(UserPostModel model);

        UserModel Get(int userId, bool isMaster = false);

        Task<int> AddAsync(UserPostModel model);

        Task<UserModel> GetAsync(int userId, bool isMaster = false);
    }
}
