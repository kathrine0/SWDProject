using SWD.Helpers;
using System;
using System.Data;
using System.ServiceModel.PeerResolvers;
using System.Web.Mvc;
using SWD.DataAccess;
using SWD.DataAccess.Algoritm;
using SWD.DataAccess.Model;
using SWD.Model;
using System.Collections.Generic;
using System.Linq;
using SWD.DataAccess.ViewModel;
using SWD.DataAccess.Helpers;

namespace SWD.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestAlgoritm(StringResult model)
        {
            var repo = new Repository();
            var facts = repo.GetFacts();
            var res = "";
            if (model.Input != null)
            {
                //var input = new Fact("!2^3^16");
                var input = new Fact(model.Input);

                var dictionary = new Dictionary<int, bool>
                {
                    {1, true},
                    {6, true},
                    {7, true},
                    {8, true},
                    {9, false},
                    {10, false},
                    {11, false}
                };

                var result = Algoritm.Run(facts, input, dictionary);


                res = AlgoritmHelper.GetTheBest(result);
            }


            return View(new StringResult{Result= res});
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
                    //TODO: logic here
                    return JsonResponse.OkResponse(ViewHelper.RenderPartialToString("SecondStep", form, ControllerContext));
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

           return JsonResponse.ErrorResponse(ViewHelper.RenderPartialToString("FirstStep", form, ControllerContext));
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