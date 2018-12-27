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
    public class smusuariosController : Controller
    {
        private softmatdeskEntities db = new softmatdeskEntities();
        
        // GET: smusuarios
        public async Task<ActionResult> Index()
        {
            var smusuarios = db.smusuarios.Include(s => s.nivel_soporte);
            return View(await smusuarios.ToListAsync());
        }

        // GET: smusuarios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            smusuarios smusuarios = await db.smusuarios.FindAsync(id);
            if (smusuarios == null)
            {
                return HttpNotFound();
            }
            return View(smusuarios);
        }

        // GET: smusuarios/Create
        public ActionResult Create()
        {
            
            ViewBag.Nivel_Soporte_Nivel_Soporte_idNivel_Soporte = new SelectList(db.nivel_soporte, "Nivel_Soporte_idNivel_Soporte", "Nivel_Sop");
            return View();
        }

        // POST: smusuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idsmUsuarios,Nombres,Apellidos,TipoIdentificacion,Identificacion,Direccion,Telefono,Nivel_Soporte_Nivel_Soporte_idNivel_Soporte,ImgPerfil,NickName,Contraseña")] smusuarios smusuarios)
        {
            if (ModelState.IsValid)
            {
                db.Database.SqlQuery<smusuarios>("call CrearUS");
                await db.SaveChangesAsync();
                //db.smusuarios.Add(smusuarios);
                //await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Nivel_Soporte_Nivel_Soporte_idNivel_Soporte = new SelectList(db.nivel_soporte, "Nivel_Soporte_idNivel_Soporte", "Nivel_Sop", smusuarios.Nivel_Soporte_Nivel_Soporte_idNivel_Soporte);
            return View(smusuarios);
        }

        // GET: smusuarios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            smusuarios smusuarios = await db.smusuarios.FindAsync(id);
            if (smusuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.Nivel_Soporte_Nivel_Soporte_idNivel_Soporte = new SelectList(db.nivel_soporte, "Nivel_Soporte_idNivel_Soporte", "Nivel_Sop", smusuarios.Nivel_Soporte_Nivel_Soporte_idNivel_Soporte);
            return View(smusuarios);
        }

        // POST: smusuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idsmUsuarios,Nombres,Apellidos,TipoIdentificacion,Identificacion,Direccion,Telefono,Nivel_Soporte_Nivel_Soporte_idNivel_Soporte,ImgPerfil,NickName,Contraseña")] smusuarios smusuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smusuarios).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Nivel_Soporte_Nivel_Soporte_idNivel_Soporte = new SelectList(db.nivel_soporte, "Nivel_Soporte_idNivel_Soporte", "Nivel_Sop", smusuarios.Nivel_Soporte_Nivel_Soporte_idNivel_Soporte);
            return View(smusuarios);
        }

        // GET: smusuarios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            smusuarios smusuarios = await db.smusuarios.FindAsync(id);
            if (smusuarios == null)
            {
                return HttpNotFound();
            }
            return View(smusuarios);
        }

        // POST: smusuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            smusuarios smusuarios = await db.smusuarios.FindAsync(id);
            db.smusuarios.Remove(smusuarios);
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
