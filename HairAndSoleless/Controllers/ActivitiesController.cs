using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HairAndSoleless.Models;
using HairAndSoleless.Models.Storage;

namespace HairAndSoleless.Controllers
{   
    public class ActivitiesController : Controller
    {
        private HairAndSolelessContext context = new HairAndSolelessContext();

        //
        // GET: /Activity/

        public ViewResult Index()
        {
            return View(context.Activities.Include(activity => activity.Coach).Include(activity => activity.Customer).ToList());
        }

        //
        // GET: /Activity/Details/5

        public ViewResult Details(int id)
        {
			Activity activity = context.Activities.Single(x => x.ActivityId == id);
            return View(activity);
        }

        //
        // GET: /Activity/Create

        public ActionResult Create()
        {
			ViewBag.PossibleCoaches = context.Coaches;
			ViewBag.PossibleCustomers = context.Customers;
            return View();
        } 

        //
        // POST: /Activity/Create

        [HttpPost]
        public ActionResult Create(Activity activity)
        {
            if (ModelState.IsValid)
            {
				context.Activities.Add(activity);
				context.SaveChanges();
				return RedirectToAction("Index");  
            }

			ViewBag.PossibleCoaches = context.Coaches;
			ViewBag.PossibleCustomers = context.Customers;
            return View(activity);
        }
        
        //
        // GET: /Activity/Edit/5
 
        public ActionResult Edit(int id)
        {
			Activity activity = context.Activities.Single(x => x.ActivityId == id);
			ViewBag.PossibleCoaches = context.Coaches;
			ViewBag.PossibleCustomers = context.Customers;
			return View(activity);
        }

        //
        // POST: /Activity/Edit/5

        [HttpPost]
        public ActionResult Edit(Activity activity)
        {
            if (ModelState.IsValid)
            {
				context.Entry(activity).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
			ViewBag.PossibleCoaches = context.Coaches;
			ViewBag.PossibleCustomers = context.Customers;
            return View(activity);
        }

        //
        // GET: /Activity/Delete/5
 
        public ActionResult Delete(int id)
        {
			Activity activity = context.Activities.Single(x => x.ActivityId == id);
            return View(activity);
        }

        //
        // POST: /Activity/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = context.Activities.Single(x => x.ActivityId == id);
            context.Activities.Remove(activity);
			context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}