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
    
    public partial class Telefone
    {
        public int telefone1 { get; set; }
        public string parque { get; set; }
    
        public virtual ParqueCampismo ParqueCampismo { get; set; }
    }
}
