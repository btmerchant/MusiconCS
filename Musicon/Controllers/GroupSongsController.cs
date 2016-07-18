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
    public class GroupSongsController : Controller
    {
        public MusiconRepository Repo = new MusiconRepository();
        private MusiconContext db = new MusiconContext();

        // GET: GroupSongs
        public ActionResult Index()
        {
            return View(db.GroupSongRelations.ToList());
        }

        // GET: GroupSongs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupSong groupSong = db.GroupSongRelations.Find(id);
            if (groupSong == null)
            {
                return HttpNotFound();
            }
            return View(groupSong);
        }

        // GET: GroupSongs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GroupSongs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupSongId")] GroupSong groupSong)
        {
            if (ModelState.IsValid)
            {
                db.GroupSongRelations.Add(groupSong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(groupSong);
        }

        // GET: GroupSongs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupSong groupSong = db.GroupSongRelations.Find(id);
            if (groupSong == null)
            {
                return HttpNotFound();
            }
            return View(groupSong);
        }

        // POST: GroupSongs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupSongId")] GroupSong groupSong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupSong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(groupSong);
        }

        // GET: GroupSongs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupSong groupSong = db.GroupSongRelations.Find(id);
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
            GroupSong groupSong = db.GroupSongRelations.Find(id);
            db.GroupSongRelations.Remove(groupSong);
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
