using SoftmatDesk.Models.DB_Context;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SoftmatDesk.Controllers
{
    public class categoriasController : Controller
    {
        private softmatdeskEntities db = new softmatdeskEntities();

        // GET: categorias
        public async Task<ActionResult> Index()
        {
            if (Session["Sesion"].ToString() == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var categorias = db.categorias.Include(c => c.nivel_soporte);
            return View(await categorias.ToListAsync());
        }

        // GET: categorias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["Sesion"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
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
            if (Session["Sesion"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (Session["Sesion"].ToString() == "Administrador" || Session["Sesion"].ToString()=="Admin")
            {
                ViewBag.Nivel_Soporte_idNivel_Soporte = new SelectList(db.nivel_soporte, "Nivel_Soporte_idNivel_Soporte", "Nivel_Sop");
                return View();
            }
            return View("Acceso o autorizado");
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
            if (Session["Sesion"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (Session["Sesion"].ToString() == "Administrador" || Session["Sesion"].ToString() == "Admin") {
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
            return View("Acceso no autorizado");
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
            if (Session["Sesion"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (Session["Sesion"].ToString() == "Administrador" || Session["Sesion"].ToString() == "Admin")
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

            return View("Acceso no autorizado");
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
