using PasswordSecurity;
using SoftmatDesk.Models.DB_Context;
using System.Linq;

namespace SoftmatDesk.Models
{
    public class Acces
    {
        private softmatdeskEntities db = new softmatdeskEntities();

        public string Sesion { get; set; }
        public int idUs { get; set; }
        public string Rol { get; set; }
        public int idC { get; set; }
        public int N_Us { get; set; }
        public int idS { get; set; }
        public int IdLvl { get; set; }
        public int IdRol { get; set; }

        public void ValidacionAccesoU(string NickName, string contraseña)
        {

            if (NickName != null && contraseña != null)
            {
                bool result = false;
                var model = db.usuario.Where(x => x.NickName == NickName).FirstOrDefault();
                if (model != null)
                {
                    result = PasswordStorage.VerifyPassword(contraseña, model.Contraseña);
                }

                if (model != null)
                {
                    if (result)
                    {
                        var per = db.perfil.Where(p => p.idPerfil == model.Perfil_idPerfil).FirstOrDefault();
                        Sesion = model.Nombres + " " + model.Apellidos;
                        idUs = model.idUsuario;
                        Rol = per.Tipo;
                        if (Rol == "Cliente" || Rol == "Client")
                        {
                            var client = db.cliente.Where(c => c.idCliente == model.Cliente_idCliente).FirstOrDefault();
                            idC = model.Cliente_idCliente;
                            N_Us = client.Num_Usuarios;
                        }
                    }
                }
                else if (model == null)
                {
                    ValidacionAccesoS(NickName, contraseña);
                }
            }
        }

        public void ValidacionAccesoS(string NickName, string contraseña)
        {

            if (NickName != null && contraseña != null)
            {
                bool result = false;
                var modelS = db.smusuarios.Where(x => x.NickName == NickName).FirstOrDefault();
                var modelLvl = db.nivel_soporte.Where(lvl => lvl.Nivel_Soporte_idNivel_Soporte == modelS.Nivel_Soporte_Nivel_Soporte_idNivel_Soporte).FirstOrDefault();
                
                if (modelS != null)
                {
                    result = PasswordStorage.VerifyPassword(contraseña, modelS.Contraseña);
                }

                else if (modelLvl != null && modelLvl.Nivel_Sop == "Administrador" || modelLvl.Nivel_Sop == "Admin")
                {
                    Sesion = modelS.Nombres + " " + modelS.Apellidos;
                    idUs = modelS.idsmUsuarios;
                    Rol = modelLvl.Nivel_Sop;
                    if (result)
                    {
                        IdLvl = modelLvl.Nivel_Soporte_idNivel_Soporte;
                    }

                }
                else if (modelLvl != null && modelLvl.Nivel_Sop == "Soporte" || modelLvl.Nivel_Sop == "Sop")
                {
                    Sesion = modelS.Nombres + " " + modelS.Apellidos;
                    idUs = modelS.idsmUsuarios;
                    
                    Rol = modelLvl.Nivel_Sop;
                    if (result)
                    {
                        IdRol = modelLvl.Nivel_Soporte_idNivel_Soporte;
                    }
                }
            }
        }

        public bool DisponivilidadUsuario(string Nickname)
        {
            bool dis;
            var modelDs = db.usuario.Where(vu => vu.NickName == Nickname).FirstOrDefault();
            if (modelDs == null)
            {
                dis = true;
                return dis;
            }
            else
            {
                dis = false;
                return dis;
            }

        }

        public bool CantidadUsuarios(int Cantidad, int IdCliente)
        {
            bool dis;

            var modelDs = db.usuario.Where(u => u.Cliente_idCliente == IdCliente).Count();

            if (modelDs < Cantidad)
            {
                dis = true;
                return dis;
            }
            else
            {
                dis = false;
                return dis;
            }

        }

        public bool DisponivilidadSoporte(string Nickname)
        {
            bool dis;
            var modelDs = db.smusuarios.Where(vu => vu.NickName == Nickname).FirstOrDefault();
            if (modelDs == null)
            {
                dis = true;
                return dis;
            }
            else
            {
                dis = false;
                return dis;
            }

        }
    }
}