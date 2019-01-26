using SoftmatDesk.Models.DB_Context;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SoftmatDesk.Controllers
{
    public class sedesController : Controller
    {
        private softmatdeskEntities db = new softmatdeskEntities();

        // GET: sedes
        public async Task<ActionResult> Index()
        {
            if (Session["Session"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (Session["Rol"].ToString() == "Administrador" || Session["Rol"].ToString() == "Admin")
            {
                var sedes = db.sedes.Include(s => s.cliente);
                return View(await sedes.ToListAsync());
            }
            else if (Session["Rol"].ToString() == "Cliente" || Session["Rol"].ToString() == "Client")
            {
                int idC = Int32.Parse(Session["idC"].ToString());
                var sedes = db.sedes.Where(s => s.Cliente_idCliente == idC);
                return View(await sedes.ToListAsync());
            }
            return View("Acceso no autorizado");

        }

        // GET: sedes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["Sesion"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (Session["Rol"].ToString() == "Administrador" || Session["Rol"].ToString() == "Admin" || Session["Rol"].ToString() == "Cliente" || Session["Rol"].ToString() == "Client")
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
            return View("Acceso no autorizado");
        }

        // GET: sedes/Create
        public async Task<ActionResult> Create()
        {
            if (Session["Sesion"] == null)
            {
                return View("Index", "Login");
            }
            else if (Session["Rol"].ToString() == "Administrador" || Session["Rol"].ToString() == "Admin")
            {
                ViewBag.Cliente_idCliente = new SelectList(db.cliente, "idCliente", "Razon_social");
                return View();
            }
            else if (Session["Rol"].ToString() == "Cliente" || Session["Rol"].ToString() == "Client")
            {
                var cliente = await db.cliente.Where(c => c.idCliente == Int32.Parse(Session["idC"].ToString())).ToListAsync();
                //ViewBag.Cliente_idCliente = new SelectList(db.cliente, "idCliente", "Razon_social");
                ViewBag.Cliente_idCliente = new SelectList(cliente, "idCliente", "Razon_social");
                return View();
            }
            return View("Acceso no autorizado");
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
            if (Session["Sesion"] == null)
            {
                return View("Index", "Login");
            }
            else if (Session["Rol"].ToString() == "Administrador" || Session["Rol"].ToString() == "Admin")
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
            return View("Acceso no autorizado");
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
            if (Session["Sesion"] == null)
            {
                return View("Index", "Login");
            }
            else if (Session["Rol"].ToString() == "Administrador" || Session["Rol"].ToString() == "Admin")
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
            return View("Acceso no autorizado");
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
