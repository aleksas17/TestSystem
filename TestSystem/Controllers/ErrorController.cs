using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestSystem.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Http404()
        {
            return View();
        }

        public ActionResult Http405()
        {
            return View();
        }

        public ActionResult General()
        {
            return View();
        }
    }
}