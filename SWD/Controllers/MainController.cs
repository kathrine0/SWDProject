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
using SWD.Models;
using System.Web.Caching;
using System.Web;

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

            var input = new Fact("3^16");

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

                    var nextForm = new QuestionForm();
                    return JsonResponse.OkResponse(ViewHelper.RenderPartialToString("ThirdStep", null, ControllerContext));
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