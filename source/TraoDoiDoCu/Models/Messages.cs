//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TraoDoiDoCu.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Messages
    {
        public int ID { get; set; }
        public int ID_Sender { get; set; }
        public int ID_Reciver { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> Datepost { get; set; }
        public Nullable<bool> IsRead { get; set; }
    
        public virtual Users Users { get; set; }
        public virtual Users Users1 { get; set; }
    }
}
