using Ninject.Infrastructure.Disposal;
using System;

namespace Ext
{
    public static class LifetimeScope
    {
        public static ScopeObject Current { get; set; }
    }

    public class ScopeObject : IDisposable, INotifyWhenDisposed
    {
        public bool IsDisposed { get; private set; }

        public event EventHandler Disposed;

        public void Dispose()
        {
            IsDisposed = true;
            Disposed?.Invoke(this, EventArgs.Empty);
        }
    }
}
