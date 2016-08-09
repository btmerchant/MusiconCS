using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Musicon.DAL;
using Musicon.Models;


namespace Musicon.Controllers
{
    public class GroupSongController : Controller
    {
        public MusiconRepository Repo = new MusiconRepository();
        private MusiconContext db = new MusiconContext();

        // GET: GroupSongs
        public ActionResult Index(int? id)
        {
            System.Web.HttpContext.Current.Session["currentGroupId"] = id;
            List<GroupSong> groupSongList = Repo.GetGroupSongs();
            return View(groupSongList);
        }

        // GET: GroupSongs/Details/5
        public ActionResult Details(int? id)
        {
            ViewData["currentGroupId"] = System.Web.HttpContext.Current.Session["currentGroupId"];

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupSong groupSong = db.GroupSongs.Find(id);
            if (groupSong == null)
            {
                return HttpNotFound();
            }
            return View(groupSong);
        }

        // GET: GroupSongs/Create
        public ActionResult Create()
        {
            //string user_id = User.Identity.GetUserId();
            //ApplicationUser member = Repo.GetUser(user_id);

            ViewData["currentGroupId"] = System.Web.HttpContext.Current.Session["currentGroupId"];
            ViewBag.Error = false;

            string StatusSelected;
            string TempoSelected;
            try
            {
                StatusSelected = Repo.context.Songs.Select(s => new SelectListItem { Value = s.Status, Text = s.Status }).ToString();
            }
            catch (Exception)
            {
                StatusSelected = "Preliminary";
            }

            try
            {
                TempoSelected = Repo.context.Songs.Select(s => new SelectListItem { Value = s.Tempo, Text = s.Tempo }).ToString();
            }
            catch (Exception)
            {
                TempoSelected = "Slow";
            }

            ViewBag.StatusSelected = StatusSelected; IEnumerable<SelectListItem> StatusList = Repo.context.Statuses.Select(s => new SelectListItem { Value = s.StatusType, Text = s.StatusType });
            ViewBag.TempoSelected = TempoSelected; IEnumerable<SelectListItem> TempoList = Repo.context.Tempos.Select(s => new SelectListItem { Value = s.TempoType, Text = s.TempoType });
            ViewBag.StatusList = StatusList;
            ViewBag.TempoList = TempoList;
            //ViewBag.UserName = member.NameFirst;
            return View();
        }

        // POST: GroupSongs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Artist,Composer,Key,Tempo,Length,Status,Vocal,EntryDate,Genre,Arrangement,Lyric")] GroupSong groupSong)
        {
            ViewData["currentGroupId"] = System.Web.HttpContext.Current.Session["currentGroupId"];
            int currentGroupId = Convert.ToInt32(System.Web.HttpContext.Current.Session["currentGroupId"]);
            if (ModelState.IsValid)
            {
                Repo.AddGroupSong(groupSong.Title, groupSong.Artist, groupSong.Composer, groupSong.Key, groupSong.Tempo, groupSong.Length, groupSong.Status, groupSong.Vocal, groupSong.EntryDate, groupSong.Genre, currentGroupId, groupSong.Arrangement, groupSong.Lyric);
                return RedirectToAction("Index");
            }

            return View(groupSong);
        }

        // GET: GroupSong/Edit/5
        // MethodGroupSongController   Edit-Get
        public ActionResult Edit(int? id)
        {
            int currentGroupId = Convert.ToInt32(System.Web.HttpContext.Current.Session["currentGroupId"]);

            string StatusSelected;
            string TempoSelected;
            try
            {
                StatusSelected = Repo.GetSelectedStatus();
            }
            catch (Exception)
            {
                StatusSelected = "Preliminary";
            }

            try
            {
                TempoSelected = Repo.GetSelectedTempo();
            }
            catch (Exception)
            {
                TempoSelected = "Slow";
            }

            ViewBag.StatusSelected = StatusSelected;
            ViewBag.TempoSelected = TempoSelected;

            IEnumerable<SelectListItem> StatusList = Repo.GetStatusList();
            IEnumerable<SelectListItem> TempoList = Repo.GetTempoList();
            ViewBag.StatusList = StatusList;
            ViewBag.TempoList = TempoList;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupSong song = Repo.GetGroupSongOrNull((int)id);
            //Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // POST: GroupSong/Edit/5
        // MethodGroupSongController   Edit-Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupSongId,Title,Artist,Composer,Key,Tempo,Length,Status,Vocal,EntryDate,Genre,Arrangement,Lyric")] GroupSong group_Song_to_edit)
        {
            int currentGroupId = Convert.ToInt32(System.Web.HttpContext.Current.Session["currentGroupId"]);
            if (ModelState.IsValid)
            {
                Repo.EditGroupSong(group_Song_to_edit);
                return RedirectToActionPermanent("Index",currentGroupId);
            }
            return View(group_Song_to_edit);
        }


        // GET: GroupSongs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupSong groupSong = db.GroupSongs.Find(id);
            if (groupSong == null)
            {
                return HttpNotFound();
            }
            return View(groupSong);
        }

        // POST: GroupSongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GroupSong groupSong = db.GroupSongs.Find(id);
            db.GroupSongs.Remove(groupSong);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
