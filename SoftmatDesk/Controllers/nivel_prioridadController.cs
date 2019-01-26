using SoftmatDesk.Models.DB_Context;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SoftmatDesk.Controllers
{
    public class nivel_prioridadController : Controller
    {
        private softmatdeskEntities db = new softmatdeskEntities();

        // GET: nivel_prioridad
        public async Task<ActionResult> Index()
        {
            if (Session["Sesion"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (Session["Rol"].ToString() == "Administrador" || Session["Admin"].ToString() == "Admin" || Session["Rol"].ToString() == "Soporte" || Session["Rol"].ToString() == "Sop")
            {
                return View(await db.nivel_prioridad.ToListAsync());
            }
            return View("Acceso no autorizado");

        }

        // GET: nivel_prioridad/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["Sesion"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (Session["Rol"].ToString() == "Administrador" || Session["Rol"].ToString() == "Admin" || Session["Rol"].ToString() == "Soporte" || Session["Rol"].ToString() == "Sop")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                nivel_prioridad nivel_prioridad = await db.nivel_prioridad.FindAsync(id);
                if (nivel_prioridad == null)
                {
                    return HttpNotFound();
                }
                return View(nivel_prioridad);
            }
            return View("Accesso no autorizado");

        }

        // GET: nivel_prioridad/Create
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

        // POST: nivel_prioridad/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idNivel_prioridad,Prioridad,Tiempo_ejecucion,Descripcion")] nivel_prioridad nivel_prioridad)
        {
            if (ModelState.IsValid)
            {
                db.nivel_prioridad.Add(nivel_prioridad);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(nivel_prioridad);


        }

        // GET: nivel_prioridad/Edit/5
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
                nivel_prioridad nivel_prioridad = await db.nivel_prioridad.FindAsync(id);
                if (nivel_prioridad == null)
                {
                    return HttpNotFound();
                }
                return View(nivel_prioridad);
            }
            return View("Acceso no autorizado");
        }

        // POST: nivel_prioridad/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idNivel_prioridad,Prioridad,Tiempo_ejecucion,Descripcion")] nivel_prioridad nivel_prioridad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nivel_prioridad).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(nivel_prioridad);
        }

        // GET: nivel_prioridad/Delete/5
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
                nivel_prioridad nivel_prioridad = await db.nivel_prioridad.FindAsync(id);
                if (nivel_prioridad == null)
                {
                    return HttpNotFound();
                }
                return View(nivel_prioridad);
            }
            return View("Acceso no autorizado");
        }

        // POST: nivel_prioridad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            nivel_prioridad nivel_prioridad = await db.nivel_prioridad.FindAsync(id);
            db.nivel_prioridad.Remove(nivel_prioridad);
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
