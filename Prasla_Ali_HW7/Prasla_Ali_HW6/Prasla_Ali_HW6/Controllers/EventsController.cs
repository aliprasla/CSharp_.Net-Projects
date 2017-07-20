using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Prasla_Ali_HW6.Models;
using System.Data.Entity.Validation;
namespace Prasla_Ali_HW6.Controllers
{
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Events
        public ActionResult Index()
        {
            ViewBag.IsAdmin = User.IsInRole("Admin");
            return View(db.Events.ToList());
        }
        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }


        public SelectList GetAllCommittees(Event @event)
        {
            var query = from c in db.Committees orderby c.Name select c;
            List<Committee> allCommittees = query.ToList();
            if (@event.Title == null)
            {
                return new SelectList(allCommittees, "CommitteeID", "Name");
            }
            else {
                SelectList outlist = new SelectList(allCommittees, "CommitteeID", "Name",@event.Committee.CommitteeID);
                return outlist;
            }
        }


        public MultiSelectList GetAllMembers(Event @event) {
            var query = from c in db.Users orderby c.Email select c;
            List<AppUser> allMembers = query.ToList();
            List<int> SelectedMembers = new List<int>();
            /*
            if (@event.AppUsers != null)
            {
                int whichMember = 0;
                foreach (AppUser m in @event.AppUsers)
                {
                    SelectedMembers.Add(Convert.ToInt32(m.Id));
                }
            }
            */
            return new MultiSelectList(allMembers, "Id", "Email", SelectedMembers);
        }
        // GET: Events/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            Event e = new Event();
            ViewBag.AllCommittees = GetAllCommittees(e);
            return View();
        }
        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "EventID,Title,Date,Location,MembersOnly,SelectedCommittee")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@event);
        }
        // GET: Events/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            ViewBag.AllCommittees = GetAllCommittees(@event);
            ViewBag.AllMembers = GetAllMembers(@event);
            if (@event == null)
            {
                return HttpNotFound();
            }
            Event e = new Event();
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "EventID,Title,Date,Location,MembersOnly")] Event @event, int CommitteeID, int[] SelectedMembers)
        {
            Event eventToChange = db.Events.Find(@event.EventID);


            if (eventToChange.Committee.CommitteeID != CommitteeID) {
                Committee SelectedCommittee = db.Committees.Find(CommitteeID);
                eventToChange.Committee = SelectedCommittee;
            }


            eventToChange.Committee = db.Committees.Find(CommitteeID);
            eventToChange.AppUsers.Clear();
            ViewBag.AllCommittees = GetAllCommittees(eventToChange);
            ViewBag.AllMembers = GetAllMembers(@event);
            /*
            if (SelectedMembers != null)
            {
                //Add members
                foreach (int memberID in SelectedMembers)
                {
                    //Convert position in MultiSelectList to UserId.
                    int pos = 0;
                    var query = from c in db.Users select c;
                    String UserID = null;
                    foreach (SelectListItem item in GetAllMembers(@event)) {
                        if (memberID == pos)
                        {
                            query.Where(c => c.Email == item.Text);
                            var temp = from c in query select c.Id;
                            UserID = temp.First();
                            break;
                        }
                        pos = pos + 1;
                    }
                    
                    eventToChange.AppUsers.Add(db.Users.Find(UserID));
                }
            }
            */
            //reassign data
            {
                eventToChange.Title = @event.Title;
                eventToChange.Date = @event.Date;
                eventToChange.Location = @event.Location;
                eventToChange.MembersOnly = @event.MembersOnly;
            }
            db.Entry(@eventToChange).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }


            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
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
