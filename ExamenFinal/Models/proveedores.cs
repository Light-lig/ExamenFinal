//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExamenFinal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class proveedores
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public proveedores()
        {
            this.productos = new HashSet<productos>();
        }
    
        public int idProveedor { get; set; }
        public string nombreCompañia { get; set; }
        public string nombrecontacto { get; set; }
        public string cargocontacto { get; set; }
        public string direccion { get; set; }
        public string ciudad { get; set; }
        public string region { get; set; }
        public string codPostal { get; set; }
        public string pais { get; set; }
        public string telefono { get; set; }
        public string fax { get; set; }
        public string paginaprincipal { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<productos> productos { get; set; }
    }
}
