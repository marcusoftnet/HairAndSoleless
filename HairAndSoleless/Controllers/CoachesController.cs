using System.Web.Mvc;
using HairAndSoleless.Models;
using HairAndSoleless.Models.Storage;

namespace HairAndSoleless.Controllers
{   
    public class CoachesController : Controller
    {
		private readonly ICoachRepository coachRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public CoachesController() : this(new CoachRepository())
        {
        }

        public CoachesController(ICoachRepository coachRepository)
        {
			this.coachRepository = coachRepository;
        }

        //
        // GET: /Coach/

        public ViewResult Index()
        {
            return View(coachRepository.GetAllCoaches(coach => coach.Activities));
        }

        //
        // GET: /Coach/Details/5

        public ViewResult Details(int id)
        {
            return View(coachRepository.GetById(id));
        }

        //
        // GET: /Coach/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Coach/Create

        [HttpPost]
        public ActionResult Create(Coach coach)
        {
            if (ModelState.IsValid) {
                coachRepository.InsertOrUpdate(coach);
                coachRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Coach/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(coachRepository.GetById(id));
        }

        //
        // POST: /Coach/Edit/5

        [HttpPost]
        public ActionResult Edit(Coach coach)
        {
            if (ModelState.IsValid) {
                coachRepository.InsertOrUpdate(coach);
                coachRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Coach/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(coachRepository.GetById(id));
        }

        //
        // POST: /Coach/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            coachRepository.Delete(id);
            coachRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

