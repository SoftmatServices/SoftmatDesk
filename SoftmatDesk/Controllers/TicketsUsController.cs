using SoftmatDesk.Models.DB_Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftmatDesk.Controllers
{
    public class TicketsUsController : Controller
    {
        private softmatdeskEntities db = new softmatdeskEntities();

        // GET: TicketsUs
        public ActionResult Index()
        {
            return View();
        }
    }
}