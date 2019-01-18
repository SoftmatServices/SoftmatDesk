using PasswordSecurity;
using SoftmatDesk.Models.DB_Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftmatDesk.Controllers
{
    public class LoginController : Controller
    {
        private softmatdeskEntities db = new softmatdeskEntities();
        public LoginViewModel lvm = new LoginViewModel();

        // GET: Login
        public ActionResult Index()
        {
            return View();
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
                    lvm.NickName = model.NickName;
                    lvm.Contraseña = model.Contraseña;
                    lvm.RememberMe = true;
                    lvm.idUsuario = model.idUsuario;
                    lvm.Perfil_idPerfil = model.Perfil_idPerfil;
                    return RedirectToAction("Index", "tickets");
                }
            }
            return View();
        }
    }
}