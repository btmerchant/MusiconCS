using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Musicon.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Create your personal song list, Join with other users to form a group, Create your groups song list from its members personal song lists.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "For more information about this Application contact Brian@btmerchant.com.";

            return View();
        }
    }
}