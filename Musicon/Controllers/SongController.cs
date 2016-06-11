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
    public class SongController : Controller
    {
        public MusiconRepository Repo = new MusiconRepository();

        public IEnumerable<Song> Get(string command)
        {
            //Temporary!
            string group = "Fade2Blue";

            string user = User.Identity.GetUserId();
            // string user = Repo.GetUser(Repo.GetUserId());
            switch (command)
            {
                case "Users":
                    {
                        List<Song> songs = Repo.GetSongs(user);
                        return songs;
                    }
                //case "Groups":
                //    {
                //        List<Song> songs = Repo.GetMemberSongs(user);
                //        return songs;
                //    }
                default:
                    {
                        List<Song> songs = new List<Song>();
                        return songs;
                    }
            }
        }

        // GET: Song
        public ActionResult Index()
        {
            string user = User.Identity.GetUserId();
            //ViewBag.Songs = Repo.GetSongs();
            return View(Repo.GetSongs(user));
        }

        // GET: Song/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = Repo.GetSongOrNull(Convert.ToInt32(id));
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }


        // GET: Poll/Create
        public ActionResult Create()
        {
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

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Artist,Composer,Key,Tempo,Length,Status,Vocal,EntryDate,Genre")] Song song)
        {

            //Get User ID form the HTTP context
            string user_id = User.Identity.GetUserId();
            ApplicationUser member = Repo.GetUser(user_id);


            if (member != null && ModelState.IsValid)
            {
                Repo.AddSong(song.Title,song.Artist,song.Composer,song.Key,song.Tempo,song.Length,song.Status,song.Vocal,song.EntryDate,song.Genre,member);             
            }
        return RedirectToAction("Index");
        }

    //// GET: Song/Edit/5
    //public ActionResult Edit(int? id)
    //{
    //    string StatusSelected;
    //    string TempoSelected;
    //    try
    //    {
    //        StatusSelected = db.Songs.Select(s => new SelectListItem { Value = s.Status, Text = s.Status }).ToString();
    //    }
    //    catch (Exception)
    //    {
    //        StatusSelected = "Preliminary";
    //    }

    //    try
    //    {
    //        TempoSelected = db.Songs.Select(s => new SelectListItem { Value = s.Tempo, Text = s.Tempo }).ToString();
    //    }
    //    catch (Exception)
    //    {
    //        TempoSelected = "Slow";
    //    }

    //    ViewBag.StatusSelected = StatusSelected;
    //    ViewBag.TempoSelected = TempoSelected;

    //    IEnumerable<SelectListItem> StatusList = db.Statuses.Select(s => new SelectListItem { Value = s.StatusType, Text = s.StatusType });
    //    IEnumerable<SelectListItem> TempoList = db.Tempos.Select(s => new SelectListItem { Value = s.TempoType, Text = s.TempoType });
    //    ViewBag.StatusList = StatusList;
    //    ViewBag.TempoList = TempoList;

    //    if (id == null)
    //    {
    //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //    }
    //    Song song = db.Songs.Find(id);
    //    if (song == null)
    //    {
    //        return HttpNotFound();
    //    }
    //    return View(song);
    //}

    //// POST: Song/Edit/5
    //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[System.Web.Http.HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Edit([Bind(Include = "Member,Title,Artist,Composer,Key,Tempo,Length,Status,Vocal,EntryDate,Genre")] Song song)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        db.Entry(song).State = EntityState.Modified;
    //        db.SaveChanges();
    //        return RedirectToAction("Index");
    //    }
    //    return View(song);
    //}

    //// GET: Song/Delete/5
    //public ActionResult Delete(int? id)
    //{
    //    if (id == null)
    //    {
    //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //    }
    //    Song song = db.Songs.Find(id);
    //    if (song == null)
    //    {
    //        return HttpNotFound();
    //    }
    //    return View(song);
    //}

    //// POST: Song/Delete/5
    //[System.Web.Http.HttpPost, System.Web.Http.ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public ActionResult DeleteConfirmed(int id)
    //{
    //    Song song = db.Songs.Find(id);
    //    db.Songs.Remove(song);
    //    db.SaveChanges();
    //    return RedirectToAction("Index");
    //}

    //protected override void Dispose(bool disposing)
    //{
    //    if (disposing)
    //    {
    //        db.Dispose();
    //    }
    //    base.Dispose(disposing);
    //}
}
}
