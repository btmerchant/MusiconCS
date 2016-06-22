using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Musicon.DAL;
using Musicon.Models;
using Microsoft.AspNet.Identity;
using System.Security.Principal;

namespace Musicon.Controllers
{
    public class HomeController : Controller
    {
        public MusiconRepository Repo = new MusiconRepository();

        public ActionResult Index()
        {          
            return View(Repo.GetAllSongs());
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

        // GET: Song/Details/5
        // MethodHomeController   Details-Get
        public ActionResult Details(int? id)
        {

            Song song = Repo.GetSongOrNull(Convert.ToInt32(id));
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }
    }
}