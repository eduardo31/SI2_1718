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
    
    public partial class Estada
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Estada()
        {
            this.EstAlojExtra = new HashSet<EstAlojExtra>();
            this.Fatura = new HashSet<Fatura>();
            this.HospEstAti = new HashSet<HospEstAti>();
            this.Hospede = new HashSet<Hospede>();
        }
    
        public int id { get; set; }
        public System.DateTime dataInicio { get; set; }
        public System.DateTime dataFim { get; set; }
        public string nIdentificacao { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstAlojExtra> EstAlojExtra { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fatura> Fatura { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HospEstAti> HospEstAti { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hospede> Hospede { get; set; }
        public virtual Hospede Hospede1 { get; set; }
    }
}
