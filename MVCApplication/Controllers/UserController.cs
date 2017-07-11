using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCApplication.Models;

namespace MVCApplication.Controllers
{
    public class UserController : Controller
    {

        // GET: http://localhost:15304/user/index
        [HttpGet]
        public ActionResult Index()
        {
            UserRepository db = new UserRepository();
            return View(db.GetAllUsers().ToList());
        }

        // GET: User/Create -- Blank Screen
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Password,ConfirmPassword,FirstName,LastName,PhoneNumber,EmailAddress,Gender,TermCondition,CreationDate")] User user)
        {
            //var errors = ModelState
            //    .Where(x => x.Value.Errors.Count > 0)
            //    .Select(x => new { x.Key, x.Value.Errors })
            //    .ToArray();

            if (ModelState.IsValid)
            {
                UserRepository db = new UserRepository();
                Boolean stat = db.AddUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            UserRepository db = new UserRepository();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: http://localhost:15304/User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Password,ConfirmPassword,FirstName,LastName,PhoneNumber,EmailAddress,Gender,TermCondition,CreationDate")] User user)
        {
            //var errors = ModelState
            //    .Where(x => x.Value.Errors.Count > 0)
            //    .Select(x => new { x.Key, x.Value.Errors })
            //    .ToArray();

            if (ModelState.IsValid)
            {
                UserRepository db = new UserRepository();
                Boolean stat = db.UpdateUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            UserRepository db = new UserRepository();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRepository db = new UserRepository();
            User user = db.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserRepository db = new UserRepository();
            User user = db.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            Boolean stat = db.DeleteUser(id);
            return RedirectToAction("Index");
        }

    }
}
