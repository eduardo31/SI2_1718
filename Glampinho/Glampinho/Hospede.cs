//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Glampinho
{
    using System;
    using System.Collections.Generic;
    
    public partial class Hospede
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hospede()
        {
            this.HospEstAti = new HashSet<HospEstAti>();
            this.Estada = new HashSet<Estada>();
        }
    
        public string nIdentificacao { get; set; }
        public decimal nif { get; set; }
        public string nome { get; set; }
        public string morada { get; set; }
        public string mail { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HospEstAti> HospEstAti { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Estada> Estada { get; set; }
    }
}