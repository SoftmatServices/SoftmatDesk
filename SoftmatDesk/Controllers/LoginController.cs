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
                    return RedirectToAction("Create","ticketsSop", new { NombreUs = Session["Sesion"].ToString(), id = Session["id"], rol = Session["Rol"] });
                }
                else if (Session["Rol"].ToString() == "Cliente" || Session["Rol"].ToString() == "Client")
                {
                    return RedirectToAction("Index", "TicketsCl", new { NombreUs = Session["Sesion"].ToString(), id = Session["id"], rol = Session["Rol"] });
                }
                else if (Session["Rol"].ToString() == "Usuario" || Session["Rol"].ToString() == "User")
                {
                    return RedirectToAction("Index","TicketsUs", new { NombreUs = Session["Sesion"].ToString(), id = Session["id"], rol = Session["Rol"] });
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
                var modelS = db.smusuarios.Where(x => x.NickName == NickName).SingleOrDefault();

                if (model == null && modelS == null)
                {
                    return Content("Usuario o contraseña no son correctos");
                }
                else if (model != null)
                {
                    bool result = PasswordStorage.VerifyPassword(contraseña, model.Contraseña);
                    if (result)
                    {
                        var per = db.perfil.Where(p => p.idPerfil == model.Perfil_idPerfil).FirstOrDefault();
                        Session["Sesion"] = model.Nombres + " " + model.Apellidos;
                        Session["id"] = model.idUsuario;
                        Session["Rol"] = per.Tipo;
                        if (Session["Rol"].ToString() == "Cliente" || Session["Rol"].ToString() == "Client")
                        {   
                            var client = db.cliente.Where(c => c.idCliente == model.Cliente_idCliente).FirstOrDefault();
                            Session["idC"] = model.Cliente_idCliente;
                            Session["N_US"] = client.Num_Usuarios;
                            return RedirectToAction("Index", "TicketsCl");
                        }
                        else if(Session["Rol"].ToString() == "Usuario" || Session["Rol"].ToString() == "User")
                        {
                            return RedirectToAction("Index", "TicketsUs");
                        }
                    }
                }
                else if (modelS != null && modelS.Nivel_Soporte_Nivel_Soporte_idNivel_Soporte == 1)
                {
                    bool result = PasswordStorage.VerifyPassword(contraseña, modelS.Contraseña);
                    if (result)
                    {
                        var sopAdmin = db.nivel_soporte.Where(sa => sa.Nivel_Soporte_idNivel_Soporte == modelS.Nivel_Soporte_Nivel_Soporte_idNivel_Soporte).FirstOrDefault();
                        Session["Sesion"] = modelS.Nombres + " " + modelS.Apellidos;
                        Session["id"] = modelS.idsmUsuarios;
                        Session["Rol"] = "Sop";
                        Session["LvlSop"] = sopAdmin.Nivel_Soporte_idNivel_Soporte;

                        return RedirectToAction("Index", "TicketsSop");
                    }
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