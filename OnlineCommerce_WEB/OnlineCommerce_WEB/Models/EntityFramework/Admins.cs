//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineCommerce_WEB.Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Admins
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public Nullable<int> AccountID { get; set; }
    
        public virtual Accounts Accounts { get; set; }
    }
}