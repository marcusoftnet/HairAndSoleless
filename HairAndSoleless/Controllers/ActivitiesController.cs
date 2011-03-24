using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HairAndSoleless.Models;
using HairAndSoleless.Models.Storage;

namespace HairAndSoleless.Controllers
{   
    public class ActivitiesController : Controller
    {
		private readonly ICoachRepository coachRepository;
		private readonly ICustomerRepository customerRepository;
		private readonly IActivityRepository activityRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public ActivitiesController() : this(new CoachRepository(), new CustomerRepository(), new ActivityRepository())
        {
        }

        public ActivitiesController(ICoachRepository coachRepository, ICustomerRepository customerRepository, IActivityRepository activityRepository)
        {
			this.coachRepository = coachRepository;
			this.customerRepository = customerRepository;
			this.activityRepository = activityRepository;
        }

        //
        // GET: /Activity/

        public ViewResult Index()
        {
            return View(activityRepository.GetAllActivities(activity => activity.Coach, activity => activity.Customer));
        }

        //
        // GET: /Activity/Details/5

        public ViewResult Details(int id)
        {
            return View(activityRepository.GetById(id));
        }

        //
        // GET: /Activity/Create

        public ActionResult Create()
        {
			ViewBag.PossibleCoaches = coachRepository.GetAllCoaches();
			ViewBag.PossibleCustomers = customerRepository.GetAllCustomers();
            return View();
        } 

        //
        // POST: /Activity/Create

        [HttpPost]
        public ActionResult Create(Activity activity)
        {
            if (ModelState.IsValid) {
                activityRepository.InsertOrUpdate(activity);
                activityRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleCoaches = coachRepository.GetAllCoaches();
				ViewBag.PossibleCustomers = customerRepository.GetAllCustomers();
				return View();
			}
        }
        
        //
        // GET: /Activity/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleCoaches = coachRepository.GetAllCoaches();
			ViewBag.PossibleCustomers = customerRepository.GetAllCustomers();
             return View(activityRepository.GetById(id));
        }

        //
        // POST: /Activity/Edit/5

        [HttpPost]
        public ActionResult Edit(Activity activity)
        {
            if (ModelState.IsValid) {
                activityRepository.InsertOrUpdate(activity);
                activityRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleCoaches = coachRepository.GetAllCoaches();
				ViewBag.PossibleCustomers = customerRepository.GetAllCustomers();
				return View();
			}
        }

        //
        // GET: /Activity/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(activityRepository.GetById(id));
        }

        //
        // POST: /Activity/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            activityRepository.Delete(id);
            activityRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

