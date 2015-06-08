using SWD.Model.Userform;
using System;
using System.Data;
using System.ServiceModel.PeerResolvers;
using System.Web.Mvc;
using SWD.DataAccess;
using SWD.DataAccess.Algoritm;
using SWD.DataAccess.Model;
using SWD.Model;

namespace SWD.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Index()
        {


            return View();
        }

        public ActionResult TestAlgoritm()
        {
            var repo = new Repository();
            var facts = repo.GetFacts();

            var input = new Fact("!1˄!3");

            var result = Algoritm.Run(facts, input);
            return View(new StringResult{Result= result});
        }

        public ActionResult FirstStep()
        {
            var form = new PersonalForm();
            return PartialView(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FirstStep(PersonalForm form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return PartialView("SecondStep");
                }
            }
            catch (DataException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return PartialView(form);
        }

        public ActionResult SecondStep()
        {
            throw new NotImplementedException();
        }

        public ActionResult ThirdStep()
        {
            throw new NotImplementedException();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}