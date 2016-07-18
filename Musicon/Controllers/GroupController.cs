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
using Microsoft.AspNet.Identity;

namespace Musicon.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        public MusiconRepository Repo = new MusiconRepository();
        public MusiconContext db = new MusiconContext();

        // GET: Groups
        // MethodGroupController   Index-Get
        public ActionResult Index()
        {
            return View(Repo.GetAllGroups());
        }

        // GET: Groups/Details/5
        // MethodGroupController   Details-Get
        public ActionResult Details(int? id)
        {
            string user_id = User.Identity.GetUserId();
            ApplicationUser member = Repo.GetUser(user_id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = Repo.GetGroupByIdOrNull((int)id);
            if (group == null)
            {
                return HttpNotFound();
            }
            List<ApplicationUser> memberList = Repo.GetGroupMemberList((int)id, member);
            ViewBag.UserName = member.NameFirst;
            ViewBag.group = group;
            ViewBag.memberList = memberList;
            ViewBag.Error = false;
            return View(group);

        }

        // GET: Groups/Create
        // MethodGroupController   Create-Get
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // MethodGroupController   Create-Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupId,Name,DateFormed,Style")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(group);
        }

        // GET: Groups/Edit/5
        // MethodGroupController   Edit-Get
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Edit/5
        // MethodGroupController   Edit-Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupId,Name,DateFormed,Style")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: Groups/Delete/5
        // MethodGroupController   Delete-Get
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        // MethodGroupController   Delete-Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = db.Groups.Find(id);
            db.Groups.Remove(group);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        // GET: Groups/Join/5
        // MethodGroupController   Join-Get
        public ActionResult Join(int? id)
        {
            string user_id = User.Identity.GetUserId();
            ApplicationUser member = Repo.GetUser(user_id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            List<ApplicationUser> memberList = Repo.GetGroupMemberList((int)id, member);
            ViewBag.UserName = member.NameFirst;
            ViewBag.group = group;
            ViewBag.memberList = memberList;
            ViewBag.Error = false;
            return View(group);
        }

        // POST: Groups/Join/5
        // MethodGroupController   Join-Post
        [HttpPost, ActionName("Join")]
        [ValidateAntiForgeryToken]
        public ActionResult JoinConfirmed(int id)
        {
            string user_id = User.Identity.GetUserId();
            ApplicationUser member = Repo.GetUser(user_id);

            List<ApplicationUser> memberList = Repo.GetGroupMemberList((int)id, member);
            ViewBag.memberList = memberList;
            Group found_group = Repo.GetGroupByIdOrNull(id);
            bool alreadyAMember = Repo.IsUserAMember((string)found_group.Name, member);
            if (alreadyAMember)
            {
                ViewBag.ErrorMessage = "You are already a member of this group.";
                ViewBag.Error = true;
            }
            else
            {
                ViewBag.Error = false;
                Repo.JoinGroupById(id, member);
                return RedirectToAction("Index");
            }
            return View(found_group);
        }




        // GET: Groups/Quit/5
        // MethodGroupController   Quit-Get
        public ActionResult Quit(int? id)
        {
            string user_id = User.Identity.GetUserId();
            ApplicationUser member = Repo.GetUser(user_id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            List<ApplicationUser> memberList = Repo.GetGroupMemberList((int)id, member);
            ViewBag.UserName = member.NameFirst;
            ViewBag.group = group;
            ViewBag.memberList = memberList;
            ViewBag.Error = false;
            return View(group);
        }

        // POST: Groups/Quit/5
        // MethodGroupController   Quit-Post
        [HttpPost, ActionName("Quit")]
        [ValidateAntiForgeryToken]
        public ActionResult QuitConfirmed(int id)
        {
            string user_id = User.Identity.GetUserId();
            ApplicationUser member = Repo.GetUser(user_id);

            List<ApplicationUser> memberList = Repo.GetGroupMemberList((int)id, member);
            ViewBag.memberList = memberList;
            ViewBag.QuitResult = false;
            Group found_group = Repo.GetGroupByIdOrNull(id);
            GroupMember found_group_member = Repo.GetGroupMemberRelationById(id);
            bool alreadyAMember = Repo.IsUserAMember((string)found_group.Name, member);
            if (!alreadyAMember || found_group_member == null)
            {
                ViewBag.ErrorMessage = "You can not quit this group you are not a member.";
                ViewBag.Error = true;
            }
            else
            {
                ViewBag.Error = false;
                ViewBag.QuitResult = Repo.QuitGroupById(id, member);
            }
            if (ViewBag.QuitResult)
            {
                return RedirectToAction("Index");
            }else
            {
                return View(found_group);
            }
            
        }





        // MethodGroupController   Dispose
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
