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
    public class ticketsController : Controller
    {
        private softmatdeskEntities db = new softmatdeskEntities();

        // GET: tickets
        public async Task<ActionResult> Index()
        {
            var tickets = db.tickets.Include(t => t.categorias).Include(t => t.cliente).Include(t => t.nivel_prioridad).Include(t => t.sedes).Include(t => t.smusuarios).Include(t => t.usuario);
            return View(await tickets.ToListAsync());
        }

        // GET: tickets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tickets tickets = await db.tickets.FindAsync(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }
            return View(tickets);
        }

        // GET: tickets/Create
        public ActionResult Create()
        {
            ViewBag.Categorias_idCategorias = new SelectList(db.categorias, "idCategorias", "Categoria");
            ViewBag.Cliente_idCliente = new SelectList(db.cliente, "idCliente", "Razon_social");
            ViewBag.Nivel_prioridad_idNivel_prioridad = new SelectList(db.nivel_prioridad, "idNivel_prioridad", "Prioridad");
            ViewBag.Sedes_idSedes = new SelectList(db.sedes, "idSedes", "Nom_Sede");
            ViewBag.SmUsuarios_idsmUsuarios = new SelectList(db.smusuarios, "idsmUsuarios", "Nombres");
            ViewBag.Usuario_idUsuario = new SelectList(db.usuario, "idUsuario", "Nombres");
            return View();
        }

        // POST: tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "NoTickets,Categorias_idCategorias,Usuario_idUsuario,Nivel_prioridad_idNivel_prioridad,Cliente_idCliente,Sedes_idSedes,Descripcion_falla,Apertura,Cierre,SmUsuarios_idsmUsuarios")] tickets tickets)
        {
            if (ModelState.IsValid)
            {
                db.tickets.Add(tickets);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Categorias_idCategorias = new SelectList(db.categorias, "idCategorias", "Categoria", tickets.Categorias_idCategorias);
            ViewBag.Cliente_idCliente = new SelectList(db.cliente, "idCliente", "Razon_social", tickets.Cliente_idCliente);
            ViewBag.Nivel_prioridad_idNivel_prioridad = new SelectList(db.nivel_prioridad, "idNivel_prioridad", "Prioridad", tickets.Nivel_prioridad_idNivel_prioridad);
            ViewBag.Sedes_idSedes = new SelectList(db.sedes, "idSedes", "Nom_Sede", tickets.Sedes_idSedes);
            ViewBag.SmUsuarios_idsmUsuarios = new SelectList(db.smusuarios, "idsmUsuarios", "Nombres", tickets.SmUsuarios_idsmUsuarios);
            ViewBag.Usuario_idUsuario = new SelectList(db.usuario, "idUsuario", "Nombres", tickets.Usuario_idUsuario);
            return View(tickets);
        }

        // GET: tickets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tickets tickets = await db.tickets.FindAsync(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categorias_idCategorias = new SelectList(db.categorias, "idCategorias", "Categoria", tickets.Categorias_idCategorias);
            ViewBag.Cliente_idCliente = new SelectList(db.cliente, "idCliente", "Razon_social", tickets.Cliente_idCliente);
            ViewBag.Nivel_prioridad_idNivel_prioridad = new SelectList(db.nivel_prioridad, "idNivel_prioridad", "Prioridad", tickets.Nivel_prioridad_idNivel_prioridad);
            ViewBag.Sedes_idSedes = new SelectList(db.sedes, "idSedes", "Nom_Sede", tickets.Sedes_idSedes);
            ViewBag.SmUsuarios_idsmUsuarios = new SelectList(db.smusuarios, "idsmUsuarios", "Nombres", tickets.SmUsuarios_idsmUsuarios);
            ViewBag.Usuario_idUsuario = new SelectList(db.usuario, "idUsuario", "Nombres", tickets.Usuario_idUsuario);
            return View(tickets);
        }

        // POST: tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "NoTickets,Categorias_idCategorias,Usuario_idUsuario,Nivel_prioridad_idNivel_prioridad,Cliente_idCliente,Sedes_idSedes,Descripcion_falla,Apertura,Cierre,SmUsuarios_idsmUsuarios")] tickets tickets)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tickets).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Categorias_idCategorias = new SelectList(db.categorias, "idCategorias", "Categoria", tickets.Categorias_idCategorias);
            ViewBag.Cliente_idCliente = new SelectList(db.cliente, "idCliente", "Razon_social", tickets.Cliente_idCliente);
            ViewBag.Nivel_prioridad_idNivel_prioridad = new SelectList(db.nivel_prioridad, "idNivel_prioridad", "Prioridad", tickets.Nivel_prioridad_idNivel_prioridad);
            ViewBag.Sedes_idSedes = new SelectList(db.sedes, "idSedes", "Nom_Sede", tickets.Sedes_idSedes);
            ViewBag.SmUsuarios_idsmUsuarios = new SelectList(db.smusuarios, "idsmUsuarios", "Nombres", tickets.SmUsuarios_idsmUsuarios);
            ViewBag.Usuario_idUsuario = new SelectList(db.usuario, "idUsuario", "Nombres", tickets.Usuario_idUsuario);
            return View(tickets);
        }

        // GET: tickets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tickets tickets = await db.tickets.FindAsync(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }
            return View(tickets);
        }

        // POST: tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tickets tickets = await db.tickets.FindAsync(id);
            db.tickets.Remove(tickets);
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
