using SoftmatDesk.Models.DB_Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftmatDesk.Models
{
    public class Controlacces
    {
        usuario us = new usuario();

        public int idUsuario;
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
        public string Contraseña { get; set; }
    }
}