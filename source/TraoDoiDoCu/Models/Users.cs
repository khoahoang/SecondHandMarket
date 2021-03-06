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
    
    public partial class Users
    {
        public Users()
        {
            this.Product = new HashSet<Product>();
            this.Comment = new HashSet<Comment>();
            this.FollowProduct = new HashSet<FollowProduct>();
            this.Messages = new HashSet<Messages>();
            this.Messages1 = new HashSet<Messages>();
        }
    
        public int ID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<bool> Admin { get; set; }
        public Nullable<bool> Ban { get; set; }
        public Nullable<int> Rating { get; set; }
        public string Phone { get; set; }
        public Nullable<int> ReportID { get; set; }
        public string ActiveCode { get; set; }
        public string ResetPassword { get; set; }
        public Nullable<System.DateTime> DateRequest { get; set; }
        public Nullable<System.DateTime> BanDate { get; set; }
        public Nullable<int> BanTime { get; set; }
    
        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<FollowProduct> FollowProduct { get; set; }
        public virtual ICollection<Messages> Messages { get; set; }
        public virtual ICollection<Messages> Messages1 { get; set; }
    }
}
