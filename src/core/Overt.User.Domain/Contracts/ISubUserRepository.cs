using Overt.Core.Data;
using Overt.User.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Overt.User.Domain.Contracts
{
    public interface ISubUserRepository : IBaseRepository<SubUserEntity>
    {
        /// <summary>
        /// 分表标识 第二种
        /// 第一种为HASH模式，实体标记Submeter
        /// </summary>
        DateTime AddTime { get; set; }
             
    }
}
