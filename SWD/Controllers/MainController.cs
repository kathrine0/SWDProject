using SWD.Model.Userform;
using System;
using System.Collections.Generic;
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