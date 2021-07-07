using Overt.Core.Data;
using Overt.User.Domain.Contracts;
using Overt.User.Domain.Entities;
using System;
using System.Configuration;

namespace Overt.User.Domain.Repositories
{
    public class SubDbUser2Repository : BaseRepository<SubDbUser2Entity>, ISubDbUser2Repository
    {
        public SubDbUser2Repository()
            : base()
        {
        }


        // Service层进行赋值即可
        public DateTime SubDbAddTime { get; set; }

        public override Func<bool, ConnectionStringSettings> ConnectionFunc => (isMaster) =>
        {
            var connectionString = string.Empty;

            var dbName = $"TestDb_{SubDbAddTime.ToString("yyyy")}";
            if (isMaster)
                connectionString = $"Data Source=127.0.0.1;Initial Catalog={dbName};Persist Security Info=True;User ID=sa;Password=123465";
            else
                connectionString = $"Data Source=127.0.0.1;Initial Catalog={dbName};Persist Security Info=True;User ID=sa;Password=123465";
            return new ConnectionStringSettings(dbName, connectionString, "System.Data.SqlClient");
        };
    }
}
