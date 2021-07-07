using Overt.Core.Data;
using Overt.User.Domain.Entities;
using System;

namespace Overt.User.Domain.Contracts
{
    public interface ISubDbUser2Repository : IBaseRepository<SubDbUser2Entity>
    {
        /// <summary>
        /// 分库标识
        /// </summary>
        DateTime SubDbAddTime { get; set; }
    }
}
