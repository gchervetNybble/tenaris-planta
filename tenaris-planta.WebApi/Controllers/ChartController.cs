using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tenaris_planta.WebApi.Controllers
{
    public class ChartController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Tenaris Planta";

            return View();
        }
    }
}
