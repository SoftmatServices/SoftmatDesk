using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftmatDesk.Controllers
{
    public class TicketsClController : Controller
    {
        // GET: TicketsCl
        public ActionResult Index()
        {
            return View();
        }

        // GET: TicketsCl/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TicketsCl/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketsCl/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TicketsCl/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TicketsCl/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TicketsCl/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TicketsCl/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
