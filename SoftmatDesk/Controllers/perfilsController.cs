using SoftmatDesk.Models.DB_Context;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SoftmatDesk.Controllers
{
    public class perfilsController : Controller
    {
        private softmatdeskEntities db = new softmatdeskEntities();

        // GET: perfils
        public async Task<ActionResult> Index()
        {
            if (Session["Sesion"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (Session["Rol"].ToString() == "Administrador" || Session["Rol"].ToString() == "Admin" || Session["Rol"].ToString() == "Soporte" ||
                        Session["Rol"].ToString() == "Sop" || Session["Rol"].ToString() == "Cliente" || Session["Rol"].ToString() == "Client")
            {
                return View(await db.perfil.ToListAsync());
            }
            return View("Acceso no autorizado");
        }

        // GET: perfils/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["Sesion"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (Session["Rol"].ToString() == "Administrador" || Session["Rol"].ToString() == "Admin" || Session["Rol"].ToString() == "Soporte" ||
                        Session["Rol"].ToString() == "Sop" || Session["Rol"].ToString() == "Cliente" || Session["Rol"].ToString() == "Client")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                perfil perfil = await db.perfil.FindAsync(id);
                if (perfil == null)
                {
                    return HttpNotFound();
                }
                return View(perfil);
            }
            return View("Acceso no autorizado");
        }

        // GET: perfils/Create
        public ActionResult Create()
        {
            if (Session["Sesion"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (Session["Rol"].ToString() == "Administrador" || Session["Rol"].ToString() == "Admin")
            {
                return View();
            }
            return View("Acceso no autorizado");
        }

        // POST: perfils/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idPerfil,Tipo,Descripcion")] perfil perfil)
        {
            if (ModelState.IsValid)
            {
                db.perfil.Add(perfil);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(perfil);
        }

        // GET: perfils/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["Sesion"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (Session["Rol"].ToString() == "Administrador" || Session["Rol"].ToString() == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                perfil perfil = await db.perfil.FindAsync(id);
                if (perfil == null)
                {
                    return HttpNotFound();
                }
                return View(perfil);
            }
            return View("Acceso no autorizado");
        }

        // POST: perfils/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idPerfil,Tipo,Descripcion")] perfil perfil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(perfil).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(perfil);
        }

        // GET: perfils/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["Sesion"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (Session["Rol"].ToString() == "Administrador" || Session["Rol"].ToString() == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                perfil perfil = await db.perfil.FindAsync(id);
                if (perfil == null)
                {
                    return HttpNotFound();
                }
                return View(perfil);
            }
            return View("Acceso no autorizado");
        }

        // POST: perfils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            perfil perfil = await db.perfil.FindAsync(id);
            db.perfil.Remove(perfil);
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
