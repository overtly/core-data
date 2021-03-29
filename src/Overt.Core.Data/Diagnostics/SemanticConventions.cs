using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overt.Core.Data.Diagnostics
{
    internal static class SemanticConventions
    {
        public const string AttributeDbSystem = "db.system";
        public const string AttributeConnectionString = "db.connection_string";
        public const string AttributeDbUser = "db.user";
        public const string AttributePeerIp = "net.peer.ip";
        public const string AttributePeerName = "net.peer.name";
        public const string AttributeDbName = "db.name";
        public const string AttributeDbCommand = "db.statement";
        public const string AttributeOperation = "db.operation";
        public const string AttributeTable = "db.sql.table";
    }
}
