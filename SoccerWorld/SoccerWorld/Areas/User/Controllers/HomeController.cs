using SoccerWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoccerWorld.DAO;


namespace SoccerWorld.Areas.User.Controllers
{
    public class HomeController : Controller
    {
        SoccerStoreEntities dbm = new SoccerStoreEntities();
        // GET: User/Home
        public ActionResult Index()
        {
            List<PhanLoai> phanloai = dbm.PhanLoais.ToList();
            List<PhanLoai> phanloai1 = new List<PhanLoai>();
            foreach (var x in phanloai)
            {
                if (checkAvibility(x.IDPhanLoai))
                    phanloai1.Add(x);
                
            }
            return View(phanloai1);
        }
        public ActionResult GetCardInfo(int id)
        {
            List<SanPham> sp = dbm.SanPhams.Where(x => x.IDPhanLoai == id).Take(5).ToList();
            return PartialView(sp);
        }
        public ActionResult Category()
        {
            List<PhanLoai> phanLoai = dbm.PhanLoais.ToList();
            return PartialView("_Category", phanLoai);
        }

        public ActionResult Product(int id)
        {
            SanPham sp = new SanPham();
            sp = dbm.SanPhams.Where(x => x.ID.ToString() == id.ToString()).FirstOrDefault();
            var phanloai = dbm.PhanLoais.FirstOrDefault(x => x.IDPhanLoai == sp.IDPhanLoai);
            
            
            ViewBag.PhanLoai = phanloai.TenPhanLoai.ToString();
            return View(sp);
        }
        public ActionResult GetSize(int id)
        {
            List<CTKichCo> lstkichco = dbm.CTKichCoes.Where(x => x.IDSP == id && x.TinhTrang == true).ToList();
            List<Kichco> kichco = new List<Kichco>();
            foreach (var x in lstkichco)
            {
                kichco.Add(dbm.Kichcoes.Where(o => o.IDKichCo == x.IDKC).FirstOrDefault());
            }
            return PartialView(kichco);
        }
        public ActionResult GetImg(int id)
        {
            List<HinhAnh> hinhanh = dbm.HinhAnhs.Where(x => x.IDSP == id).ToList();
            return PartialView(hinhanh);
        }

        public ActionResult GetMau(int id)
        {
            List<ChiTietMau> lstmau = dbm.ChiTietMaus.Where(x => x.IDSP == id).ToList();
            List<MauSac> mau = new List<MauSac>();
            foreach (var x in lstmau)
            {
                mau.Add(dbm.MauSacs.Where(o => o.IDMau == x.IDMau).FirstOrDefault());
            }
            return PartialView(mau);
        }
        public bool checkAvibility(int id)
        {
            return dbm.SanPhams.Any(x => x.IDPhanLoai == id);
            
        }
    }
}