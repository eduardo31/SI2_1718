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
    
    public partial class HospEstAti
    {
        public int num { get; set; }
        public int ano { get; set; }
        public int id { get; set; }
        public string nIdentificacao { get; set; }
    
        public virtual Atividade Atividade { get; set; }
        public virtual Estada Estada { get; set; }
        public virtual Hospede Hospede { get; set; }
    }
}