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
    
    public partial class tickets
    {
        public int NoTickets { get; set; }
        public int Categorias_idCategorias { get; set; }
        public int Usuario_idUsuario { get; set; }
        public int Nivel_prioridad_idNivel_prioridad { get; set; }
        public int Cliente_idCliente { get; set; }
        public int Sedes_idSedes { get; set; }
        public string Descripcion_falla { get; set; }
        public System.DateTime Apertura { get; set; }
        public Nullable<System.DateTime> Cierre { get; set; }
        public int SmUsuarios_idsmUsuarios { get; set; }
    
        public virtual categorias categorias { get; set; }
        public virtual cliente cliente { get; set; }
        public virtual nivel_prioridad nivel_prioridad { get; set; }
        public virtual sedes sedes { get; set; }
        public virtual smusuarios smusuarios { get; set; }
        public virtual usuario usuario { get; set; }
    }
}