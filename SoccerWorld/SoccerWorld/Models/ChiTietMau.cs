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
    
    public partial class ChiTietMau
    {
        public int IDCT { get; set; }
        public bool TinhTrang { get; set; }
        public float Gia { get; set; }
        public int IDSP { get; set; }
        public int IDMau { get; set; }
    
        public virtual MauSac MauSac { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
