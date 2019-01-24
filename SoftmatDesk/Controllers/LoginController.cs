using PasswordSecurity;
using SoftmatDesk.Models.DB_Context;
using System.Linq;
using System.Web.Mvc;

namespace SoftmatDesk.Controllers
{
    public class LoginController : Controller
    {
        private softmatdeskEntities db = new softmatdeskEntities();

        // GET: Login
        public ActionResult Index()
        {
            
            if (Session["Sesion"] != null)
            {
                if (Session["Rol"].ToString() == "Administrador" || Session["Rol"].ToString() == "Admin")
                {
                    return RedirectToAction("Index", "tickets", new { NombreUs = Session["Sesion"].ToString(), id = Session["id"], rol = Session["Rol"] });
                }
                else if (Session["Rol"].ToString() == "Soporte" || Session["Rol"].ToString() == "Sop")
                {
                    return RedirectToAction("Create","tickets", new { NombreUs = Session["Sesion"].ToString(), id = Session["id"], rol = Session["Rol"] });
                }
                else if (Session["Rol"].ToString() == "Usuario" || Session["Rol"].ToString() == "User")
                {
                    return RedirectToAction("Index","tickets", new { NombreUs = Session["Sesion"].ToString(), id = Session["id"], rol = Session["Rol"] });
                }
                else
                {
                    return View();
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(string NickName, string contraseña)
        {
            if (NickName != null && contraseña != null)
            {
                var model = db.usuario.Where(x => x.NickName == NickName).SingleOrDefault();

                if (model == null)
                {
                    return Content("Usuario o contraseña no son correctos");
                }
                bool result = PasswordStorage.VerifyPassword(contraseña, model.Contraseña);
                if (result)
                {
                    var perf = db.perfil.Where(p => p.idPerfil == model.Perfil_idPerfil).FirstOrDefault();
                    Session["Sesion"] = model.Nombres + " " + model.Apellidos;
                    Session["id"] = model.idUsuario;
                    Session["Rol"] = perf.Tipo;

                    return RedirectToAction("Index", "tickets");
                }
            }
            return View();


        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");


        }
    }
}