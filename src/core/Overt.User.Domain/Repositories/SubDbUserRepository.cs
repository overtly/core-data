using Overt.Core.Data;
using Overt.User.Domain.Contracts;
using Overt.User.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Overt.User.Domain.Repositories
{
    public class SubDbUserRepository : BaseRepository<SubDbUserEntity>, ISubDbUserRepository
    {
        public SubDbUserRepository(IConfiguration configuration)
            : base(configuration, "subdb")
        {
        }
    }
}
