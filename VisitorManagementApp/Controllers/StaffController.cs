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
    public class StaffController : Controller
    {
        private CompanyContext db = new CompanyContext();

        /***********************************************/
        /*      Main index page for Staff              */
        /***********************************************/
        public ActionResult Index()
        {
            if (Session["AdminId"] != null)
            {
                using (CompanyContext db = new CompanyContext())
                {
                    var staff = db.StaffTable.ToList();
                    return View(staff);
                }
            }
            return RedirectToAction("Login", "Admin");
        }


        /***********************************************/
        /*        Staff Details viewed By admin        */
        /***********************************************/
        public ActionResult Details(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Staff account = db.StaffTable.Find(id);
                if (account == null)
                {
                    return HttpNotFound();
                }
                return View(account);
            }
            return RedirectToAction("Login", "Admin");
        }


        /***********************************************/
        /*                 Edit Staff                  */
        /***********************************************/
        public ActionResult Edit(int? id)
        {
            if (Session["StaffId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Staff account = db.StaffTable.Find(id);
                if (account == null)
                {
                    return HttpNotFound();
                }
                return View(account);
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffId,Name,Email,Username,Password,Phone,OfficeLocation")] Staff account)
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
        /*      Staff Registration method              */
        /***********************************************/
        public ActionResult Registration()
        {
            if (Session["AdminId"] != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Admin");
            
        }

        [HttpPost]
        public ActionResult Registration(Staff account)
        {
            if (ModelState.IsValid)
            {
                using (CompanyContext db = new CompanyContext())
                {
                    db.StaffTable.Add(account);
                    db.SaveChanges();
                }

                ModelState.Clear();

                ViewBag.Message = "New Account Successfully Registered";
            }
            return View();
        }


        /***********************************************/
        /*             Staff Login method              */
        /***********************************************/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Staff account)
        {
            using (CompanyContext db = new CompanyContext())
            {
                try
                {
                    var mystaff = db.StaffTable.Single(s => s.Username == account.Username && s.Password == account.Password);
                    if (mystaff != null)
                    {
                        Session["StaffId"] = mystaff.StaffId.ToString();
                        Session["Username"] = mystaff.Username.ToString();

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
        /*            Staff Logout method              */
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
                Staff account = db.StaffTable.Find(id);
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
            Staff account = db.StaffTable.Find(id);
            db.StaffTable.Remove(account);
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