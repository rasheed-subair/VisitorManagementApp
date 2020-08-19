using System;
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
    public class KeycodeController : Controller
    {
        private CompanyContext db = new CompanyContext();

        /***********************************************/
        /*               Keycode list                 */
        /***********************************************/
        public ActionResult Index()
        {
            if (Session["AdminId"] != null)
            {
                return View(db.KeycodeTable.ToList());
            }
            return RedirectToAction("Login", "Admin");
        }



        /***********************************************/
        /*                 Create Keycode              */
        /***********************************************/
        // GET: Keycodes/Create
        public ActionResult Create()
        {
            if (Session["AdminId"] != null)
            {
                ViewBag.AdminId = new SelectList(db.AdminTable, "AdminId", "Name");
                return View();
            }
            else if (Session["StaffId"] != null)
            {
                ViewBag.StaffId = new SelectList(db.StaffTable, "StaffId", "Name");
                return View();
            }
            else
            {
                //You need to change this to Index method Home controller later
                return RedirectToAction("Login", "Admin");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KeycodeId,Key,AppointmentDay,AppointmentTime,AdminId,StaffId")] Keycode keycode)
        {
            if (ModelState.IsValid)
            {
                db.KeycodeTable.Add(keycode);
                db.SaveChanges();

                ModelState.Clear();

                ViewBag.Message = "New Keycode Successfully Registered";
            }

            ViewBag.AdminId = new SelectList(db.AdminTable, "AdminId", "Name", keycode.AdminId);
            ViewBag.StaffId = new SelectList(db.StaffTable, "StaffId", "Name", keycode.StaffId);

            return View();
        }



        /***********************************************/
        /*           Check key code validity           */
        /***********************************************/
        public ActionResult CheckKey()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckKey(Keycode visitorkey)
        {
            using (CompanyContext db = new CompanyContext())
            {
                try
                {
                    var mykey = db.KeycodeTable.Single(k => k.Key == visitorkey.Key);
                    if (mykey != null)
                    {
                        /*
                         Check this out as you progress

                        Session["AdminId"] = mykey.AdminId.ToString();
                        Session["StaffId"] = mykey.StaffId.ToString();
                         */

                        ViewBag.Message = "Key code is correct. Welcome to Hestia Nig. Ltd.";
                        return RedirectToAction("Success");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Keycode is incorrect, Please try again...");
                }
            }
            return View();
        }



        /***********************************************/
        /*               Success Method                */
        /***********************************************/
        public ActionResult Success()
        {
            return View();
        }


        /***********************************************/
        /*               Delete Keycode                */
        /***********************************************/
        public ActionResult Delete(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Keycode keycode = db.KeycodeTable.Find(id);
                if (keycode == null)
                {
                    return HttpNotFound();
                }
                return View(keycode);
            }
            return RedirectToAction("Login", "Admin");
        }

        // POST: Keycodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Keycode keycode = db.KeycodeTable.Find(id);
            db.KeycodeTable.Remove(keycode);
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