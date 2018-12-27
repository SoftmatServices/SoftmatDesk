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
    public class tipo_fallasController : Controller
    {
        private softmatdeskEntities db = new softmatdeskEntities();

        // GET: tipo_fallas
        public async Task<ActionResult> Index()
        {
            var tipo_fallas = db.tipo_fallas.Include(t => t.categorias);
            return View(await tipo_fallas.ToListAsync());
        }

        // GET: tipo_fallas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_fallas tipo_fallas = await db.tipo_fallas.FindAsync(id);
            if (tipo_fallas == null)
            {
                return HttpNotFound();
            }
            return View(tipo_fallas);
        }

        // GET: tipo_fallas/Create
        public ActionResult Create()
        {
            ViewBag.Categorias_idCategorias = new SelectList(db.categorias, "idCategorias", "Categoria");
            return View();
        }

        // POST: tipo_fallas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idFallas,Categorias_idCategorias,Fuente,Descripcion,Link")] tipo_fallas tipo_fallas)
        {
            if (ModelState.IsValid)
            {
                db.tipo_fallas.Add(tipo_fallas);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Categorias_idCategorias = new SelectList(db.categorias, "idCategorias", "Categoria", tipo_fallas.Categorias_idCategorias);
            return View(tipo_fallas);
        }

        // GET: tipo_fallas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_fallas tipo_fallas = await db.tipo_fallas.FindAsync(id);
            if (tipo_fallas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categorias_idCategorias = new SelectList(db.categorias, "idCategorias", "Categoria", tipo_fallas.Categorias_idCategorias);
            return View(tipo_fallas);
        }

        // POST: tipo_fallas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idFallas,Categorias_idCategorias,Fuente,Descripcion,Link")] tipo_fallas tipo_fallas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_fallas).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Categorias_idCategorias = new SelectList(db.categorias, "idCategorias", "Categoria", tipo_fallas.Categorias_idCategorias);
            return View(tipo_fallas);
        }

        // GET: tipo_fallas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_fallas tipo_fallas = await db.tipo_fallas.FindAsync(id);
            if (tipo_fallas == null)
            {
                return HttpNotFound();
            }
            return View(tipo_fallas);
        }

        // POST: tipo_fallas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tipo_fallas tipo_fallas = await db.tipo_fallas.FindAsync(id);
            db.tipo_fallas.Remove(tipo_fallas);
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
