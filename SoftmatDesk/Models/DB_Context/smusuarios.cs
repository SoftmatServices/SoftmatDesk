//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SoftmatDesk.Models.DB_Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class smusuarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public smusuarios()
        {
            this.tickets = new HashSet<tickets>();
        }
    
        public int idsmUsuarios { get; set; }
        public string Login { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int Nivel_Soporte_Nivel_Soporte_idNivel_Soporte { get; set; }
        public byte[] ImgPerfil { get; set; }
        public string NickName { get; set; }
        public string Contraseña { get; set; }
    
        public virtual nivel_soporte nivel_soporte { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tickets> tickets { get; set; }
    }
}
