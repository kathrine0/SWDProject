using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWD.Helpers
{
    public static class JsonResponse 
    {
        private class Response
        {
            public Response(string status, object view)
            {
                this.status = status;
                this.view = view;
            }
            public string status { get; set; }
            public object view { get; set; }
        }

        public static JsonResult OkResponse(string viewData)
        {
            return new JsonResult() { Data = new Response("Ok", viewData) };
        }

        public static JsonResult ErrorResponse(string viewData)
        {
            return new JsonResult() { Data = new Response("Error", viewData) };
        }
    }
}