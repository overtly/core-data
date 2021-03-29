using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Overt.Apm.Opentelemetry.Instrumention.Implementation
{
    internal class CoreDataSubscriber : IObserver<DiagnosticListener>
    {
        private readonly Func<DiagnosticListener, bool> isEnableListener;
        public CoreDataSubscriber(Func<DiagnosticListener, bool> isEnableListener)
        {
            this.isEnableListener = isEnableListener;
        }

        public void Subscribe()
        {
            DiagnosticListener.AllListeners.Subscribe(this);
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(DiagnosticListener value)
        {
            var listener = new DiagnosticSourceListener();
            if (isEnableListener(value))
                value.Subscribe(listener);
        }
    }
}
