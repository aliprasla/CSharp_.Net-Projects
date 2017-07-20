using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Prasla_Ali_HW6.Models;
using Microsoft.AspNet.Identity;

namespace Prasla_Ali_HW6.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AppUsers
        [Authorize(Roles = "Admin,User")]
        public ActionResult Index()
        {
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.IsAdmin = User.IsInRole("Admin");
            return View(db.Users.ToList());
        }

        // GET: AppUsers/Details/54
        [Authorize(Roles = "Admin,User")]
        public ActionResult Details(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser AppUser = db.Users.Find(id);
            if (AppUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.AllEvents = GetAllEvents(AppUser);
            return View(AppUser);
        }

        // GET: AppUsers/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,Phone,OkToText,Major")] AppUser AppUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(AppUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(AppUser);
        }
        public MultiSelectList GetAllAppUsers(Event e) {
            //Gets all AppUsers from DB.
            var query = from c in db.Users orderby c.Email select c;
            List<AppUser> allAppUsers = query.ToList();
            //Figures out selected Numbers
            List<int> SelectedAppUsers = new List<int>();
            foreach (AppUser m in e.AppUsers) {
                SelectedAppUsers.Add(Convert.ToInt32(m.Id));
            }
            return new MultiSelectList (allAppUsers,"AppUserID","Email",SelectedAppUsers);
        }
        public MultiSelectList GetAllEvents(AppUser m)
        {
            var query = from c in db.Events select c;
            List<Event> allEvents = query.ToList();
            List<int> SelectedEvents = new List<int>();
            if (m.Events != null)
            {
                foreach (Event e in m.Events)
                {
                    SelectedEvents.Add(e.EventID);
                }
            }
            return new MultiSelectList(allEvents, "EventID", "Title", SelectedEvents);
        }
        // GET: AppUsers/Edit/5
        public ActionResult Edit(String id)
        {
            if (id == null || id != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser AppUser = db.Users.Find(id);
            if (AppUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.AllEvents = GetAllEvents(AppUser);
            return View(AppUser);
        }

        // POST: AppUsers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,Phone,OkToText,Major")] AppUser AppUser, int[] SelectedEvents)
        {
            AppUser AppUserToChange = db.Users.Find(AppUser.Id);
            AppUserToChange.Events.Clear();
            if (SelectedEvents != null)
            {
                foreach (int eventID in SelectedEvents)
                {
                    AppUserToChange.Events.Add(db.Events.Find(eventID));
                }
            }
            //reassign new data
            {
                AppUserToChange.FirstName = AppUser.FirstName;
                AppUserToChange.LastName = AppUser.LastName;
                AppUserToChange.Email = AppUser.Email;
                AppUserToChange.PhoneNumber = AppUser.PhoneNumber;
                AppUserToChange.Major = AppUser.Major;
                AppUserToChange.OkToText = AppUser.OkToText;
            }
            ViewBag.AllEvents = GetAllEvents(AppUserToChange);
            db.Entry(@AppUserToChange).State = EntityState.Modified;
            db.SaveChanges();
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(AppUser);
        }

        // GET: AppUsers/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser AppUser = db.Users.Find(id);
            if (AppUser == null)
            {
                return HttpNotFound();
            }
            return View(AppUser);
        }

        // POST: AppUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            AppUser AppUser = db.Users.Find(id);
            db.Users.Remove(AppUser);
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
