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
    
    public partial class Extra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Extra()
        {
            this.EstAlojExtra = new HashSet<EstAlojExtra>();
            this.HistoricoExtra = new HashSet<HistoricoExtra>();
        }
    
        public int id { get; set; }
        public string descricao { get; set; }
        public decimal precoDia { get; set; }
        public string tipo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstAlojExtra> EstAlojExtra { get; set; }
        public virtual ExtraAloj ExtraAloj { get; set; }
        public virtual ExtraPessoa ExtraPessoa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistoricoExtra> HistoricoExtra { get; set; }
    }
}
