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
            return View();
        }
        public ActionResult Product()
        {
            int id = 1;
            SanPham sp = new SanPham();
            sp = dbm.SanPhams.Where(x => x.ID.ToString() == id.ToString()).FirstOrDefault();
            var phanloai = dbm.PhanLoais.FirstOrDefault(x => x.IDPhanLoai == sp.IDPhanLoai);
            List<CTKichCo> kichco = dbm.CTKichCoes.Where(x => x.IDSP == sp.ID && x.TinhTrang==true).ToList();
            ViewBag.PhanLoai = phanloai.TenPhanLoai.ToString();
            ViewBag.KichCo = kichco;
            return View(sp);
        }
    }
}