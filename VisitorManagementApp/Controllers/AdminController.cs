﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VisitorManagementApp.Models;

namespace VisitorManagementApp.Controllers
{
    public class AdminController : Controller
    {
        private CompanyContext db = new CompanyContext();

        /***********************************************/
        /*      Main index page for admin              */
        /***********************************************/
        public ActionResult Index()
        {
            if (Session["AdminId"] != null)
            {
                using (CompanyContext db = new CompanyContext())
                {
                    var admin = db.AdminTable.ToList();
                    return View(admin);
                }
            }
            return RedirectToAction("Login");
        }



        /***********************************************/
        /*        Admin Details viewed By admin        */
        /***********************************************/
        public ActionResult Details(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Admin account = db.AdminTable.Find(id);
                if (account == null)
                {
                    return HttpNotFound();
                }
                return View(account);
            }
            return RedirectToAction("Login");
        }



        /***********************************************/
        /*                 Edit Admin                  */
        /***********************************************/
        public ActionResult Edit(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Admin account = db.AdminTable.Find(id);
                if (account == null)
                {
                    return HttpNotFound();
                }
                return View(account);
            }
            return RedirectToAction("Login", "Admin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdminId,Name,Email,Username,Password,Phone,OfficeLocation")] Admin account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }



        /***********************************************/
        /*      Admin Registration method              */
        /***********************************************/
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Admin account)
        {
            if (ModelState.IsValid)
            {
                using (CompanyContext db = new CompanyContext())
                {
                    db.AdminTable.Add(account);
                    db.SaveChanges();
                }

                ModelState.Clear();

                ViewBag.Message = "New Account Successfully Registered";
            }
            return View();
        }


        /***********************************************/
        /*             Admin Login method              */
        /***********************************************/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin account)
        {
            using (CompanyContext db = new CompanyContext())
            {
                try
                {
                    var myadmin = db.AdminTable.Single(a => a.Username == account.Username && a.Password == account.Password);
                    if (myadmin != null)
                    {
                        Session["AdminId"] = myadmin.AdminId.ToString();
                        Session["Username"] = myadmin.Username.ToString();

                        return RedirectToAction("Index");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Username or Password is invalid.");
                }
            }
            return View();
        }


        /***********************************************/
        /*            Admin Logout method              */
        /***********************************************/
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }



        /***********************************************/
        /*      Delete Admin account method            */
        /***********************************************/
        public ActionResult Delete(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Admin account = db.AdminTable.Find(id);
                if (account == null)
                {
                    return HttpNotFound();
                }
                return View(account);
            }
            return RedirectToAction("Login");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Admin account = db.AdminTable.Find(id);
            db.AdminTable.Remove(account);
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