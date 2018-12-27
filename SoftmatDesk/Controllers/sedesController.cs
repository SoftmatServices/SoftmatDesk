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
    public class sedesController : Controller
    {
        private softmatdeskEntities db = new softmatdeskEntities();

        // GET: sedes
        public async Task<ActionResult> Index()
        {
            var sedes = db.sedes.Include(s => s.cliente);
            return View(await sedes.ToListAsync());
        }

        // GET: sedes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sedes sedes = await db.sedes.FindAsync(id);
            if (sedes == null)
            {
                return HttpNotFound();
            }
            return View(sedes);
        }

        // GET: sedes/Create
        public ActionResult Create()
        {
            ViewBag.Cliente_idCliente = new SelectList(db.cliente, "idCliente", "Razon_social");
            return View();
        }

        // POST: sedes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idSedes,Cliente_idCliente,Nom_Sede,Pais,Ciudad,Direccion,Telefono")] sedes sedes)
        {
            if (ModelState.IsValid)
            {
                db.sedes.Add(sedes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Cliente_idCliente = new SelectList(db.cliente, "idCliente", "Razon_social", sedes.Cliente_idCliente);
            return View(sedes);
        }

        // GET: sedes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sedes sedes = await db.sedes.FindAsync(id);
            if (sedes == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cliente_idCliente = new SelectList(db.cliente, "idCliente", "Razon_social", sedes.Cliente_idCliente);
            return View(sedes);
        }

        // POST: sedes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idSedes,Cliente_idCliente,Nom_Sede,Pais,Ciudad,Direccion,Telefono")] sedes sedes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sedes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Cliente_idCliente = new SelectList(db.cliente, "idCliente", "Razon_social", sedes.Cliente_idCliente);
            return View(sedes);
        }

        // GET: sedes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sedes sedes = await db.sedes.FindAsync(id);
            if (sedes == null)
            {
                return HttpNotFound();
            }
            return View(sedes);
        }

        // POST: sedes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            sedes sedes = await db.sedes.FindAsync(id);
            db.sedes.Remove(sedes);
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
