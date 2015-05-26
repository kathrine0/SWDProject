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

        [HttpGet]
        public ActionResult FirstStep()
        {
            var form = new PersonalForm();
            
            return View(form);
        }

        [HttpPost]
        public ActionResult FirstStep(PersonalForm personalForm)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult SecondStep()
        {
            throw new NotImplementedException();
        }

        //[HttpPost]
        //public ActionResult SecondStep()
        //{
        //    throw new NotImplementedException();
        //}

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