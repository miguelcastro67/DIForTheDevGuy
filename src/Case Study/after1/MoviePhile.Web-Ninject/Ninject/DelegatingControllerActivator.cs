using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Linq;

namespace MoviePhile.Web.Ninject
{
    public sealed class DelegatingControllerActivator : IControllerActivator
    {
        private readonly Func<ControllerContext, object> controllerCreator;
        private readonly Action<ControllerContext, object> controllerReleaser;

        public DelegatingControllerActivator(Func<ControllerContext, object> controllerCreator,
            Action<ControllerContext, object> controllerReleaser = null)
        {
            this.controllerCreator = controllerCreator ??
                throw new ArgumentNullException(nameof(controllerCreator));
            this.controllerReleaser = controllerReleaser ?? ((_, __) => { });
        }

        public object Create(ControllerContext context) => this.controllerCreator(context);
        public void Release(ControllerContext context, object controller) =>
            this.controllerReleaser(context, controller);
    }
}
