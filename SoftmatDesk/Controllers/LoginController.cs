using PasswordSecurity;
using SoftmatDesk.Models;
using SoftmatDesk.Models.DB_Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
                return RedirectToAction("Index","tickets", new { NombreUs = Session["Sesion"].ToString()});
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login (string NickName, string contraseña)
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
                    Session["Sesion"] = model.Nombres + " " + model.Apellidos;
                    return RedirectToAction("Index", "tickets");
                }
            }
            return View();


        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction ("Index","Login");


        }
    }
}