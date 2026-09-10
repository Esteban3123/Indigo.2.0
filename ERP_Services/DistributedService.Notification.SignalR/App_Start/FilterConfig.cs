using System.Web;
using System.Web.Mvc;

namespace DistributedService.Notification.SignalR
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}