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
    public class usuariosController : Controller
    {
        private softmatdeskEntities db = new softmatdeskEntities();

        // GET: usuarios
        public async Task<ActionResult> Index()
        {
            var usuario = db.usuario.Include(u => u.cliente).Include(u => u.perfil).Include(u => u.sedes);
            return View(await usuario.ToListAsync());
        }

        // GET: usuarios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = await db.usuario.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: usuarios/Create
        public ActionResult Create()
        {
            ViewBag.Cliente_idCliente = new SelectList(db.cliente, "idCliente", "Razon_social");
            ViewBag.Perfil_idPerfil = new SelectList(db.perfil, "idPerfil", "Tipo");
            ViewBag.idSede = new SelectList(db.sedes, "idSedes", "Nom_Sede");
            
            return View();
        }

        public ActionResult GetSedes(int id)
        {
            //var clienteSede = db.cliente.GroupJoin(db.sedes, cli => cli.idCliente,
            //    sd => sd.Cliente_idCliente, (cli, sd) => new { cli, sd }).FirstOrDefault(x => x.cli.idCliente == idCliente);
            List<sedes> sd = db.sedes.Where(x => x.Cliente_idCliente == idCliente).ToList();
            ViewBag.sed = new SelectList(sd, "idSede","Nom_Sede");
            return PartialView("DisplaySedes");
            //var clienteSede = db.cliente.GroupJoin(db.sedes, cli => cli.idCliente,
            //    sd => sd.Cliente_idCliente, (cli, sd) => new { cli, sd }).ToList();
        }

        // POST: usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idUsuario,Cliente_idCliente,idSede,Perfil_idPerfil,Nombres,Apellidos,Correo,Num_contacto,ImgPerfil,NickName,Contraseña")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.usuario.Add(usuario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Cliente_idCliente = new SelectList(db.cliente, "idCliente", "Razon_social", usuario.Cliente_idCliente);
            ViewBag.Perfil_idPerfil = new SelectList(db.perfil, "idPerfil", "Tipo", usuario.Perfil_idPerfil);
            ViewBag.idSede = new SelectList(db.sedes, "idSedes", "Nom_Sede", usuario.idSede);
            return View(usuario);
        }

        // GET: usuarios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = await db.usuario.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cliente_idCliente = new SelectList(db.cliente, "idCliente", "Razon_social", usuario.Cliente_idCliente);
            ViewBag.Perfil_idPerfil = new SelectList(db.perfil, "idPerfil", "Tipo", usuario.Perfil_idPerfil);
            ViewBag.idSede = new SelectList(db.sedes, "idSedes", "Nom_Sede", usuario.idSede);
            return View(usuario);
        }

        // POST: usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idUsuario,Cliente_idCliente,idSede,Perfil_idPerfil,Nombres,Apellidos,Correo,Num_contacto,ImgPerfil,NickName,Contraseña")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Cliente_idCliente = new SelectList(db.cliente, "idCliente", "Razon_social", usuario.Cliente_idCliente);
            ViewBag.Perfil_idPerfil = new SelectList(db.perfil, "idPerfil", "Tipo", usuario.Perfil_idPerfil);
            ViewBag.idSede = new SelectList(db.sedes, "idSedes", "Nom_Sede", usuario.idSede);
            return View(usuario);
        }

        // GET: usuarios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = await db.usuario.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            usuario usuario = await db.usuario.FindAsync(id);
            db.usuario.Remove(usuario);
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
