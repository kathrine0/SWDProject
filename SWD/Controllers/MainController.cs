using SWD.Helpers;
using SWD.Model.Userform;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWD.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Index()
        {
            return View();
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