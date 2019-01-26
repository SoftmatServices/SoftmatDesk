using SoftmatDesk.Models;
using SoftmatDesk.Models.DB_Context;
using System.Web.Mvc;

namespace SoftmatDesk.Controllers
{
    public class LoginController : Controller
    {
        private softmatdeskEntities db = new softmatdeskEntities();
        private Acces ac = new Acces();

        // GET: Login
        public ActionResult Index()
        {

            if (Session["Sesion"] != null)
            {
                //if (Session["Rol"].ToString() == "Administrador" || Session["Rol"].ToString() == "Admin")
                //{
                //    return RedirectToAction("Index", "tickets", new { NombreUs = Session["Sesion"].ToString(), id = Session["id"], rol = Session["Rol"] });
                //}
                //else if (Session["Rol"].ToString() == "Soporte" || Session["Rol"].ToString() == "Sop")
                //{
                //    return RedirectToAction("Create", "ticketsSop", new { NombreUs = Session["Sesion"].ToString(), id = Session["id"], rol = Session["Rol"] });
                //}
                //else if (Session["Rol"].ToString() == "Cliente" || Session["Rol"].ToString() == "Client")
                //{
                //    return RedirectToAction("Index", "TicketsCl", new { NombreUs = Session["Sesion"].ToString(), id = Session["id"], rol = Session["Rol"] });
                //}
                //else if (Session["Rol"].ToString() == "Usuario" || Session["Rol"].ToString() == "User")
                //{
                //    return RedirectToAction("Index", "TicketsUs", new { NombreUs = Session["Sesion"].ToString(), id = Session["id"], rol = Session["Rol"] });
                //}
                return RedirectToAction("Index", "tickets");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(string NickName, string contraseña)
        {
            ac.ValidacionAccesoU(NickName, contraseña);
            if (NickName != null && contraseña != null)
            {

                if (ac.Sesion == null)
                {
                    return Content("Usuario o contraseña no son correctos");
                }
                else if (ac.Sesion != null)
                {
                    Session["Sesion"] = ac.Sesion;
                    Session["id"] = ac.idUs;
                    Session["Rol"] = ac.Rol;
                    if (Session["Rol"].ToString() == "Cliente" || Session["Rol"].ToString() == "Client")
                    {
                        Session["idC"] = ac.idC;
                        //return RedirectToAction("ListCl", "tickets");
                        return RedirectToAction("Index", "tickets");
                    }
                    else if (Session["Rol"].ToString() == "Usuario" || Session["Rol"].ToString() == "User")
                    {
                        //return RedirectToAction("ListUs", "tickets");
                        return RedirectToAction("Index", "tickets");
                    }

                }
                else if (ac.Rol == "Administrador" || ac.Rol == "Admin")
                {
                    Session["Sesion"] = ac.Sesion;
                    Session["id"] = ac.idUs;
                    Session["Rol"] = ac.Rol;
                    Session["IdRol"] = ac.IdRol;

                    return RedirectToAction("Index", "tickets");


                }
                else if (ac.Rol == "Sop" || ac.Rol == "Soporte")
                {
                    Session["Sesion"] = ac.Sesion;
                    Session["id"] = ac.idUs;
                    Session["Rol"] = ac.Rol;
                    Session["IdRol"] = ac.IdRol;
                    return RedirectToAction("Index", "tickets");
                    //return RedirectToAction("ListSop", "tickets");
                }
            }

            return View("No tiene acceso");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");


        }
    }
}