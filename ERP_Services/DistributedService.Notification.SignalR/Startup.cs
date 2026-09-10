using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DistributedService.Notification.SignalR.Startup))]

namespace DistributedService.Notification.SignalR
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR("/Chat", new Microsoft.AspNet.SignalR.HubConfiguration());
            app.MapSignalR("/Management", new Microsoft.AspNet.SignalR.HubConfiguration());
        }
    }
}
