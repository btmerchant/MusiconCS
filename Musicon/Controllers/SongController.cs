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
    [Authorize]
    public class SongController : Controller
    {
        public MusiconRepository Repo = new MusiconRepository();

        // MethodSongController   GetSongs
        public IEnumerable<Song> GetSongs(string command)
        {
            //Get User ID form the HTTP context
            string user_id = User.Identity.GetUserId();
            ApplicationUser member = Repo.GetUser(user_id);
            switch (command)
            {
                case "Users":
                    {
                        List<Song> songs = Repo.GetUserSongs(member);
                        return songs;
                    }

                default:
                    {
                        List<Song> songs = new List<Song>();
                        return songs;
                    }
            }
        }

        // GET: Song
        // MethodSongController   Index-Get
        public ActionResult Index()
        {
            //Get User ID form the HTTP context
            string user_id = User.Identity.GetUserId();
            ApplicationUser member = Repo.GetUser(user_id);

            ViewBag.UserName = member.NameFirst;
            return View(Repo.GetUserSongs(member));
        }

        // GET: Song/Details/5
        // MethodSongController   Details-Get
        public ActionResult Details(int? id)
        {
            string user_id = User.Identity.GetUserId();
            ApplicationUser member = Repo.GetUser(user_id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = Repo.GetSongOrNull(Convert.ToInt32(id));
            if (song == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserName = member.NameFirst;

            return View(song);
        }


        // GET: Song/Create
        // MethodSongController   Create-Get
        public ActionResult Create()
        {
            string user_id = User.Identity.GetUserId();
            ApplicationUser member = Repo.GetUser(user_id);

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
            ViewBag.UserName = member.NameFirst;

            return View();
        }

        // MethodSongController   Create-Post
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

        // GET: Song/Edit/5
        // MethodSongController   Edit-Get
        public ActionResult Edit(int? id)
        {
            string user_id = User.Identity.GetUserId();
            ApplicationUser member = Repo.GetUser(user_id);

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
            ViewBag.UserName = member.NameFirst;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = Repo.GetSongOrNull((int)id);
            //Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // POST: Song/Edit/5
        // MethodSongController   Edit-Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SongId,Title,Artist,Composer,Key,Tempo,Length,Status,Vocal,EntryDate,Genre")] Song song_to_edit)
        {
            if (ModelState.IsValid)
            {
                Repo.EditSong(song_to_edit);
                return RedirectToAction("Index");
            }
            return View(song_to_edit);
        }

        // GET: Song/Delete/5
        // MethodSongController   Delete-Get
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = Repo.GetSong((int)id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // POST: Song/Delete/5
        // MethodSongController   Delete-Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Repo.DeleteSelectedSong(id);
            return RedirectToAction("Index");
        }
    }
}
