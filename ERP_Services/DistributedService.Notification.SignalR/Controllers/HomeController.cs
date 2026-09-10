using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace DistributedService.Notification.SignalR.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase f)
        {
            try
            {
                if (HttpContext.Request.Files.Count > 0)
                {
                    for (int i = 0; i < HttpContext.Request.Files.Count; i++)
                    {
                        var file = HttpContext.Request.Files[i];
                        var fileName = Path.Combine(Helpers.Helper.GetUpgradeFilesFolder(), Path.GetFileName(file.FileName));
                        if (System.IO.File.Exists(fileName))
                            System.IO.File.Delete(fileName);
                        file.SaveAs(fileName);
                    }
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Created);
                }
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, "InternalServerError:\n " + ex.Message + " \n " + ex.StackTrace);
            }
        }

    }
}
