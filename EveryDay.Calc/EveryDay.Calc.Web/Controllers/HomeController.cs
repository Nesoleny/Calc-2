using EveryDay.Calc.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EveryDay.Calc.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int days)
        {
            var x = new IndexModel();

            x.Date = DateTime.Now.AddDays(days);

            var viewName = days % 2 == 0 ? "IndexM" : "PartView";

            return View(viewName, x);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.Text = "asd";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}