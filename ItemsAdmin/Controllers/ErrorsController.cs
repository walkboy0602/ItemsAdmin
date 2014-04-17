using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MonsterAdmin.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult General(Exception exception)
        {
            return View();
        }

        public ActionResult Http404()
        {
            return Content("Not found", "text/plain");
        }

        public ActionResult Http403()
        {
            return Content("Forbidden", "text/plain");
        }

        public JsonResult Redirect()
        {
            return Json(new { result = "Redirect", url = Url.Action("General", "Errors") });
        }

    }
}
