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
            this.Execute(connection =>
            {
                this.ExecuteScript = "select * from users where id=@id;";
                connection.Query(this.ExecuteScript, new { id = 1 }, commandTimeout: 15);
                return 1;
            });
        }
    }
}
