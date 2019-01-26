using SoftmatDesk.Models.DB_Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SoftmatDesk.Controllers
{

    public class ticketsController : Controller
    {
        private softmatdeskEntities db = new softmatdeskEntities();


        // GET: tickets
        public ActionResult Index()
        {

            if (Session["Sesion"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (Session["Rol"].ToString() == "Administrador" || Session["Rol"].ToString() == "Admin")
            {
                var tickets = db.tickets.Include(t => t.categorias).Include(t => t.cliente).Include(t => t.nivel_prioridad).Include(t => t.sedes).Include(t => t.smusuarios).Include(t => t.usuario);
                ViewBag.Nombre = Session["Sesion"];
                ViewBag.Id = Session["id"];
                return View(tickets);
            }
            else if (Session["Rol"].ToString() == "Cliente" || Session["Rol"].ToString() == "Client")
            {
                int IdC = Int32.Parse(Session["idC"].ToString());
                var tickets = db.tickets.Where(t => t.Cliente_idCliente == IdC);
                ViewBag.Categorias_idCategorias = new SelectList(db.categorias, "idCategorias", "Categoria");
                ViewBag.Cliente_idCliente = new SelectList(db.cliente, "idCliente", "Razon_social");
                ViewBag.Nivel_prioridad_idNivel_prioridad = new SelectList(db.nivel_prioridad, "idNivel_prioridad", "Prioridad");
                ViewBag.Sedes_idSedes = new SelectList(db.sedes, "idSedes", "Nom_Sede");
                ViewBag.SmUsuarios_idsmUsuarios = new SelectList(db.smusuarios, "idsmUsuarios", "Nombres");
                ViewBag.Usuario_idUsuario = new SelectList(db.usuario, "idUsuario", "Nombres");
                ViewBag.Nombre = Session["Sesion"];
                ViewBag.Id = Session["id"];
                return View(tickets);
            }
            else if (Session["Rol"].ToString() == "Usuario" || Session["Rol"].ToString() == "User")
            {
                int Id = Int32.Parse(Session["id"].ToString());
                var tickets = db.tickets.Where(t => t.Usuario_idUsuario == Id);
                ViewBag.Categorias_idCategorias = new SelectList(db.categorias, "idCategorias", "Categoria");
                ViewBag.Cliente_idCliente = new SelectList(db.cliente, "idCliente", "Razon_social");
                ViewBag.Nivel_prioridad_idNivel_prioridad = new SelectList(db.nivel_prioridad, "idNivel_prioridad", "Prioridad");
                ViewBag.Sedes_idSedes = new SelectList(db.sedes, "idSedes", "Nom_Sede");
                ViewBag.SmUsuarios_idsmUsuarios = new SelectList(db.smusuarios, "idsmUsuarios", "Nombres");
                ViewBag.Usuario_idUsuario = new SelectList(db.usuario, "idUsuario", "Nombres");
                ViewBag.Nombre = Session["Sesion"];
                ViewBag.Id = Session["id"];
                return View(tickets);
            }

            return Content("No tiene acceso");
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

            if (Session["Sesion"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (Session["Rol"].ToString() == "Administrador" || Session["Rol"].ToString() == "Admin" || Session["Rol"].ToString() == "Soporte" || Session["Rol"].ToString() == "Sop")
            {
                ViewBag.Categorias_idCategorias = new SelectList(db.categorias, "idCategorias", "Categoria");
                ViewBag.Cliente_idCliente = new SelectList(db.cliente, "idCliente", "Razon_social");
                ViewBag.Nivel_prioridad_idNivel_prioridad = new SelectList(db.nivel_prioridad, "idNivel_prioridad", "Prioridad");
                ViewBag.Sedes_idSedes = new SelectList(db.sedes, "idSedes", "Nom_Sede");
                ViewBag.SmUsuarios_idsmUsuarios = new SelectList(db.smusuarios, "idsmUsuarios", "Nombres");
                ViewBag.Usuario_idUsuario = new SelectList(db.usuario, "idUsuario", "Nombres");
                return View();
            }
            else if (Session["Rol"].ToString() == "Cliente" || Session["Rol"].ToString() == "Client")
            {
                int idC = Int32.Parse(Session["idC"].ToString());
                ViewBag.Categorias_idCategorias = new SelectList(db.categorias, "idCategorias", "Categoria");
                var Cliente = db.cliente.Where(c => c.idCliente == idC);
                //ViewBag.Cliente_idCliente = new SelectList(db.cliente, "idCliente", "Razon_social");
                ViewBag.Cliente_idCliente = new SelectList(Cliente, "idCliente", "Razon_social");
                ViewBag.Nivel_prioridad_idNivel_prioridad = new SelectList(db.nivel_prioridad, "idNivel_prioridad", "Prioridad");
                var Sedes = db.sedes.Where(s => s.Cliente_idCliente == idC);
                //ViewBag.Sedes_idSedes = new SelectList(db.sedes, "idSedes", "Nom_Sede");
                ViewBag.Sedes_idSedes = new SelectList(Sedes, "idSedes", "Nom_Sede");
                ViewBag.SmUsuarios_idsmUsuarios = new SelectList(db.smusuarios, "idsmUsuarios", "Nombres");
                var Usuario = db.usuario.Where(u => u.Cliente_idCliente == idC);
                //ViewBag.Usuario_idUsuario = new SelectList(db.usuario, "idUsuario", "Nombres");
                ViewBag.Usuario_idUsuario = new SelectList(Usuario, "idUsuario", "Nombres");
                return View();
            }
            else if (Session["Rol"].ToString() == "Usuario" || Session["Rol"].ToString() == "User")
            {
                int idC = Int32.Parse(Session["idC"].ToString());
                int id = Int32.Parse(Session["id"].ToString());
                var Cliente = db.cliente.Where(c => c.idCliente == idC);
                ViewBag.Categorias_idCategorias = new SelectList(db.categorias, "idCategorias", "Categoria");
                //ViewBag.Cliente_idCliente = new SelectList(db.cliente, "idCliente", "Razon_social");
                ViewBag.Cliente_idCliente = new SelectList(Cliente, "idCliente", "Razon_social");
                ViewBag.Nivel_prioridad_idNivel_prioridad = new SelectList(db.nivel_prioridad, "idNivel_prioridad", "Prioridad");
                var Sedes = db.sedes.Where(s => s.Cliente_idCliente == idC);
                //ViewBag.Sedes_idSedes = new SelectList(db.sedes, "idSedes", "Nom_Sede");
                ViewBag.Sedes_idSedes = new SelectList(Sedes, "idSedes", "Nom_Sede");
                ViewBag.SmUsuarios_idsmUsuarios = new SelectList(db.smusuarios, "idsmUsuarios", "Nombres");
                var Usuario = db.usuario.Where(u => u.idUsuario == id);
                //ViewBag.Usuario_idUsuario = new SelectList(db.usuario, "idUsuario", "Nombres");
                ViewBag.Usuario_idUsuario = new SelectList(Usuario, "idUsuario", "Nombres");
                return View();
            }
            return View("Acceso no autorizado");

        }
        // POST: tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "NoTickets,Categorias_idCategorias,Usuario_idUsuario,Nivel_prioridad_idNivel_prioridad,Cliente_idCliente,Sedes_idSedes,Descripcion_falla,Apertura,Cierre,SmUsuarios_idsmUsuarios")] tickets tickets)
        {
            if (Session["Sesion"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (ModelState.IsValid)
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

        // GET: tickets/Edit
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["Sesion"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
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
            if (Session["Sesion"].ToString()==null)
            {
                return ViewBag("Index", "Login");
            }
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
            if (Session["Sesion"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
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
