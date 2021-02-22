using Autofac;
using Autofac.Integration.Web;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace WebFormClient.Core
{
    public class PageBase : Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            var cpa = (IContainerProviderAccessor)HttpContext.Current.ApplicationInstance;
            var cp = cpa.ContainerProvider;
            cp.RequestLifetime.InjectProperties(this);
        }
    }
}