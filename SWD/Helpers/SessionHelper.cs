using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SWD.Helpers
{
    public static class SessionHelper
    {
        public static string CookieName { get; set; }


        public static void AddElement<T>(string name, T element)
        {

            if (System.Web.HttpContext.Current.Session[name] != null)
                System.Web.HttpContext.Current.Session[name] = element;
            else
                System.Web.HttpContext.Current.Session.Add(name, element);
        }

        public static T GetElement<T>(string name)
        {
            if (System.Web.HttpContext.Current.Session[name] != null)
                return (T) System.Web.HttpContext.Current.Session[name];
            else throw new KeyNotFoundException();
        }
    }
}