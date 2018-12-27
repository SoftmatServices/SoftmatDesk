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
    public class categoriasController : Controller
    {
        private softmatdeskEntities db = new softmatdeskEntities();

        // GET: categorias
        public async Task<ActionResult> Index()
        {
            var categorias = db.categorias.Include(c => c.nivel_soporte);
            return View(await categorias.ToListAsync());
        }

        // GET: categorias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categorias categorias = await db.categorias.FindAsync(id);
            if (categorias == null)
            {
                return HttpNotFound();
            }
            return View(categorias);
        }

        // GET: categorias/Create
        public ActionResult Create()
        {
            ViewBag.Nivel_Soporte_idNivel_Soporte = new SelectList(db.nivel_soporte, "Nivel_Soporte_idNivel_Soporte", "Nivel_Sop");
            return View();
        }

        // POST: categorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idCategorias,Nivel_Soporte_idNivel_Soporte,Categoria,Descripcion")] categorias categorias)
        {
            if (ModelState.IsValid)
            {
                db.categorias.Add(categorias);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Nivel_Soporte_idNivel_Soporte = new SelectList(db.nivel_soporte, "Nivel_Soporte_idNivel_Soporte", "Nivel_Sop", categorias.Nivel_Soporte_idNivel_Soporte);
            return View(categorias);
        }

        // GET: categorias/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categorias categorias = await db.categorias.FindAsync(id);
            if (categorias == null)
            {
                return HttpNotFound();
            }
            ViewBag.Nivel_Soporte_idNivel_Soporte = new SelectList(db.nivel_soporte, "Nivel_Soporte_idNivel_Soporte", "Nivel_Sop", categorias.Nivel_Soporte_idNivel_Soporte);
            return View(categorias);
        }

        // POST: categorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idCategorias,Nivel_Soporte_idNivel_Soporte,Categoria,Descripcion")] categorias categorias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categorias).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Nivel_Soporte_idNivel_Soporte = new SelectList(db.nivel_soporte, "Nivel_Soporte_idNivel_Soporte", "Nivel_Sop", categorias.Nivel_Soporte_idNivel_Soporte);
            return View(categorias);
        }

        // GET: categorias/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categorias categorias = await db.categorias.FindAsync(id);
            if (categorias == null)
            {
                return HttpNotFound();
            }
            return View(categorias);
        }

        // POST: categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            categorias categorias = await db.categorias.FindAsync(id);
            db.categorias.Remove(categorias);
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
