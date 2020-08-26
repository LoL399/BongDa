using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoccerWorld.Models
{
    public class Carts
    {
        private int id;
        private int sp;
        private int ms;
        private string kc;

        public Carts(int id, int sp, int ms, string kc)
        {
            this.id = id;
            this.sp = sp;
            this.ms = ms;
            this.kc = kc;
        }
        public int ID { get { return id; } set {id = value; } }
        public int Sp { get {return sp; } set { sp = value; } }
        public int Ms { get {return ms; } set {ms=value; } }
        public string Kc { get { return kc; } set {kc=value; } }
    }
}