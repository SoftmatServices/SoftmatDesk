using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using SoftmatDesk.Models.DB_Context;
using AutenticacionPersonalizada.Utilidades;
using System.ComponentModel.DataAnnotations;

namespace AutenticacionPersonalizada.Seguridad
{
  public class UsuarioMembership:MembershipUser
    {


        public int idUsuario { get; set; }
        public int Cliente_idCliente { get; set; }
        public int idSede { get; set; }
        public string Login { get; set; }
        public int Perfil_idPerfil { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Num_contacto { get; set; }
        public byte[] ImgPerfil { get; set; }
        public string NickName { get; set; }
        [Required]
        [StringLength(100)]
        public string Contraseña { get; set; }


        public UsuarioMembership(usuario us)
        {
            idUsuario = us.idUsuario;
            Cliente_idCliente = us.Cliente_idCliente;
            idSede = us.idSede;
            Login = us.Login;
            per
            password = SeguridadUtilidades.GetSha1(us.password);
            nombre = us.nombre;
            apellidos = us.apellidos;
        }
    }
}
