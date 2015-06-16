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
        AlgoritmHelper algoritmHelper;

        public MainController()
        {
            algoritmHelper = new AlgoritmHelper();
        }

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
                var input = new Fact(-1, model.Input);

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

                 dictionary = new Dictionary<int, bool>();


                //var result = Algoritm.RunWithDecomposition(facts, input, dictionary);

                var result = Algoritm.RunWithDecompositionsynthesis(facts, input, dictionary);
 
                res = result;
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
                    SessionHelper.AddElement<PersonalForm>("PersonalForm", form);

                    var nextForm = new QuestionForm();
                    return JsonResponse.OkResponse(ViewHelper.RenderPartialToString("SecondStep", nextForm, ControllerContext));
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

           return JsonResponse.ErrorResponse(ViewHelper.RenderPartialToString("FirstStep", form, ControllerContext));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SecondStep(QuestionForm form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SessionHelper.AddElement<QuestionForm>("QuestionForm", form);

                    var result = algoritmHelper.GetResult(SessionHelper.GetElement<PersonalForm>("PersonalForm"), form);
                    return JsonResponse.OkResponse(ViewHelper.RenderPartialToString("ThirdStep", result, ControllerContext));
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return JsonResponse.ErrorResponse(ViewHelper.RenderPartialToString("SecondStep", form, ControllerContext));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThirdStep()
        {
            throw new NotImplementedException();
        }
    }
}