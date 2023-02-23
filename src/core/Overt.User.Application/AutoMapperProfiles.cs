using AutoMapper;
using Overt.User.Application.Models;
using Overt.User.Domain.Entities;

namespace Overt.User.Application
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            #region Input
            CreateMap<UserPostModel, UserEntity>();
            CreateMap<UserPostModel, SubUserEntity>();
            CreateMap<UserPostModel, SubDbUserEntity>();
            CreateMap<UserPostModel, SubDbUser2Entity>();
            CreateMap<UserPostModel, UserLongIdEntity>();
            #endregion

            #region Output
            CreateMap<UserEntity,UserModel>();
            CreateMap<SubUserEntity, UserModel>();
            CreateMap<SubDbUserEntity, UserModel>();
            CreateMap<SubDbUser2Entity, UserModel>();
            CreateMap<UserLongIdEntity, UserModel>();
            #endregion
        }
    }
}
