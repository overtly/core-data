using Overt.Core.Data.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Overt.Apm.Opentelemetry.Instrumention.Implementation
{
    internal class DiagnosticSourceListener : IObserver<KeyValuePair<string, object>>
    {
        private  Activity parent;
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(KeyValuePair<string, object> value)
        {
            Activity activity;
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

            if (value.Key.EndsWith("Start"))
            {
                parent = Activity.Current;
                activity = CoreDataActivitySourceHelper.ActivitySource.StartActivity("CoreDataExecute", ActivityKind.Client);
                if (parent != null)
                    activity.SetParentId(parent.Id);
            }
            else
            {
                activity = Activity.Current;
            }

            if (value.Key == DiagnosticListenerNames.CommandStart)
            {
                activity?.SetTag("start", value.Value);
            }

            if (value.Key == DiagnosticListenerNames.CommandStop)
            {
                activity?.SetTag("stop", value.Value);
                activity?.Stop();
                if (parent != null)
                {
                    Activity.Current = parent;
                }
            }
        }
    }
}
