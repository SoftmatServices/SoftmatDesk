using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SoftmatDesk.Models.DB_Context;

namespace SoftmatDesk.Controllers
{
    public class nivel_soporteController : Controller
    {
        private softmatdeskEntities db = new softmatdeskEntities();

        // GET: nivel_soporte
        public async Task<ActionResult> Index()
        {
            return View(await db.nivel_soporte.ToListAsync());
        }

        // GET: nivel_soporte/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nivel_soporte nivel_soporte = await db.nivel_soporte.FindAsync(id);
            if (nivel_soporte == null)
            {
                return HttpNotFound();
            }
            return View(nivel_soporte);
        }

        // GET: nivel_soporte/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: nivel_soporte/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Nivel_Soporte_idNivel_Soporte,Nivel_Sop,Descripcion")] nivel_soporte nivel_soporte)
        {
            if (ModelState.IsValid)
            {
                db.nivel_soporte.Add(nivel_soporte);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(nivel_soporte);
        }

        // GET: nivel_soporte/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nivel_soporte nivel_soporte = await db.nivel_soporte.FindAsync(id);
            if (nivel_soporte == null)
            {
                return HttpNotFound();
            }
            return View(nivel_soporte);
        }

        // POST: nivel_soporte/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Nivel_Soporte_idNivel_Soporte,Nivel_Sop,Descripcion")] nivel_soporte nivel_soporte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nivel_soporte).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(nivel_soporte);
        }
        
        // GET: nivel_soporte/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nivel_soporte nivel_soporte = await db.nivel_soporte.FindAsync(id);
            if (nivel_soporte == null)
            {
                return HttpNotFound();
            }
            return View(nivel_soporte);
        }

        // POST: nivel_soporte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            nivel_soporte nivel_soporte = await db.nivel_soporte.FindAsync(id);
            db.nivel_soporte.Remove(nivel_soporte);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
