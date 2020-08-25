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
        public ActionResult Index(string sortOrder, string searchString)
        {
            if (Session["AdminId"] != null)
            {
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                var visitors = from s in db.VisitorTable
                             select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    visitors = visitors.Where(s => s.LastName.Contains(searchString)
                                           || s.FirstName.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        visitors = visitors.OrderByDescending(s => s.FirstName);
                        break;
                    default:
                        visitors = visitors.OrderBy(s => s.FirstName);
                        break;
                }
                return View(visitors.ToList());
            }
            return RedirectToAction("Login", "Admin");
        }



        /***********************************************/
        /*           Visitor Checkout List             */
        /***********************************************/
        public ActionResult CheckoutList(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var visitors = from s in db.VisitorTable
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                visitors = visitors.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
                ViewBag.Checker = "Searchfound";
            }
            switch (sortOrder)
            {
                case "name_desc":
                    visitors = visitors.OrderByDescending(s => s.FirstName);
                    break;
                default:
                    visitors = visitors.OrderBy(s => s.FirstName);
                    break;
            }
            return View(visitors.ToList());
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
                
                ModelState.Clear();
                ViewBag.Message = "You have checked in successfully.";
            }

            return View();
        }


        /***********************************************/
        /*           Visitor Checkout method           */
        /***********************************************/
        public ActionResult Checkout(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitor visitor = db.VisitorTable.Find(id);
            if (visitor == null)
            {
                return HttpNotFound();
            }
            return View(visitor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout([Bind(Include = "VisitorId,FirstName,LastName,Email,Phone,Address,Purpose,TimeIn,TimeOut")] Visitor visitor)
        {
            if (ModelState.IsValid)
            {
                visitor.TimeOut = DateTime.Now.ToString();
                db.Entry(visitor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            
            return View(visitor);
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