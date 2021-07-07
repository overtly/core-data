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
    public class SubDbUser2Repository : BaseRepository<SubDbUser2Entity>, ISubDbUser2Repository
    {
        public SubDbUser2Repository(IConfiguration configuration)
            : base(configuration)
        {
        }


        // Service层进行赋值即可
        public DateTime SubDbAddTime { get; set; }

        public override Func<bool, string> ConnectionFunc => (isMaster) =>
        {
            var connectionString = string.Empty;

            var dbName = $"TestDb_{SubDbAddTime.ToString("yyyy")}";
            if (isMaster)
                connectionString = $"Data Source=127.0.0.1;Initial Catalog={dbName};Persist Security Info=True;User ID=sa;Password=123465;DbType=SqlServer";
            else
                connectionString = $"Data Source=127.0.0.1;Initial Catalog={dbName};Persist Security Info=True;User ID=sa;Password=123465;DbType=SqlServer";
            return connectionString;
        };
    }
}
