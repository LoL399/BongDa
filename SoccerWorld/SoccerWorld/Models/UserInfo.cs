//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SoccerWorld.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserInfo
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
    
        public virtual UserTB UserTB { get; set; }
    }
}
