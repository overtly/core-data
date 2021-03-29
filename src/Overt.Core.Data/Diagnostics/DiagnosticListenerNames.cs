using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overt.Core.Data.Diagnostics
{
    /// <summary>
    /// Diagnostic常量
    /// </summary>
    public static class DiagnosticListenerNames
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public const string DiagnosticSourceName = "CoreDataDiagnosticListener";
        
        public const string ConnectionStart = "CoreDataConnectionStart";
       
        public const string ConnectionStop = "CoreDataConnectionStop";

        public const string ConnectionException = "CoreDataConnectionException";

        public const string CommandStart = "CoreDataCommandStart";

        public const string CommandStop = "CoreDataCommandStop";

        public const string CommandException = "CoreDataCommandException";

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
}
