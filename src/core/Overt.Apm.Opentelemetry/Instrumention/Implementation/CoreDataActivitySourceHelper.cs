using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Overt.Apm.Opentelemetry.Instrumention.Implementation
{
    internal class CoreDataActivitySourceHelper
    {
        public const string ActivitySourceName = "OpenTelemetry.CoreData";

        private static readonly Version Version = typeof(CoreDataActivitySourceHelper).Assembly.GetName().Version;

        internal static readonly ActivitySource ActivitySource = new ActivitySource(ActivitySourceName, Version.ToString());
    }
}
