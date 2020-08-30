using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoccerWorld.Models
{
    public class Carts
    {
        private int id;
        private SanPham sp;
        private MauSac ms;
        private Kichco kc;
        public Carts(int id,  SanPham sp, MauSac ms, Kichco kc)
        {
            this.id = id;
            this.sp = sp;
            this.ms = ms;
            this.kc = kc;
        }
        public int ID { get { return id; } set {id = value; } }
        public SanPham Sp { get {return sp; } set { sp = value; } }
        public MauSac Ms { get {return ms; } set {ms=value; } }
        public Kichco Kc { get { return kc; } set {kc=value; } }
    }
}