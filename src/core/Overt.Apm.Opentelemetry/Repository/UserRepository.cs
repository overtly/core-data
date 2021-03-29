using Dapper;
using Microsoft.Extensions.Configuration;
using Overt.Apm.Opentelemetry.Domain;
using Overt.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Overt.Apm.Opentelemetry.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IConfiguration configuration, string dbStoreKey = "") : base(configuration, dbStoreKey)
        {
            this.Execute((connection, sql, param) =>
            {
                connection.Query(sql, param, commandTimeout: 15);
                return 1;
            }, "select * from users where id=@id;", new { id = 1 });
        }
    }
}
