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
    public class VisitorController : Controller
    {
        private CompanyContext db = new CompanyContext();

        /***********************************************/
        /*         Visitor Log viewed By admin         */
        /***********************************************/
        public ActionResult Index()
        {
            if (Session["AdminId"] != null)
            {
                var visitor = db.VisitorTable.ToList();
                return View(visitor);
            }
            return RedirectToAction("Login", "Admin");
        }



        /***********************************************/
        /*      Visitor Details viewed By admin        */
        /***********************************************/
        public ActionResult Details(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Visitor account = db.VisitorTable.Find(id);
                if (account == null)
                {
                    return HttpNotFound();
                }
                return View(account);
            }
            return RedirectToAction("Login", "Admin");
        }


        /***********************************************/
        /*            Visitor Checkin method           */
        /***********************************************/
        public ActionResult Checkin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkin([Bind(Include = "VisitorId,FirstName,LastName,Email,Phone,Address,Purpose,TimeIn,TimeOut")] Visitor account)
        {
            if (ModelState.IsValid)
            {
                account.TimeIn = DateTime.Now.ToString();
                account.TimeOut = null;
                db.VisitorTable.Add(account);
                db.SaveChanges();
                /*
                 * Use this instead when success view page is set up
                 return RedirectToAction("Success", "Keycode");
                 */
                return RedirectToAction("Index");
            }

            return View();
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