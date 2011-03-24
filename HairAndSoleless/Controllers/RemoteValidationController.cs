using System.Web.Mvc;

namespace HairAndSoleless.Controllers
{
    public class RemoteValidationController : Controller
    {
        public ActionResult ContactEmailExists(string Contact)
        {
            // Just for demo purposes to get an AJAX-call - will say that 
            // no customer can be in the @avega.se domain
            var endsInAvega = Contact.EndsWith("@avega.se");
            return Json(!endsInAvega, JsonRequestBehavior.AllowGet);
        }
    }
}
