using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HairAndSoleless.Models;
using HairAndSoleless.Models.Storage;

namespace HairAndSoleless.Controllers
{   
    public class CustomersController : Controller
    {
		private readonly ICustomerRepository customerRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public CustomersController() : this(new CustomerRepository())
        {
        }

        public CustomersController(ICustomerRepository customerRepository)
        {
			this.customerRepository = customerRepository;
        }

        //
        // GET: /Customer/

        public ViewResult Index()
        {
            return View(customerRepository.GetAllCustomers(customer => customer.Activities));
        }

        //
        // GET: /Customer/Details/5

        public ViewResult Details(int id)
        {
            return View(customerRepository.GetById(id));
        }

        //
        // GET: /Customer/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Customer/Create

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid) {
                customerRepository.InsertOrUpdate(customer);
                customerRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Customer/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(customerRepository.GetById(id));
        }

        //
        // POST: /Customer/Edit/5

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid) {
                customerRepository.InsertOrUpdate(customer);
                customerRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Customer/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(customerRepository.GetById(id));
        }

        //
        // POST: /Customer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            customerRepository.Delete(id);
            customerRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

