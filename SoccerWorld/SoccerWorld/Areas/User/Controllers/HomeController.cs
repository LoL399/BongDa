using SoccerWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoccerWorld.DAO;
using System.Web.UI.WebControls;
using SoccerWorld.Areas.User.Tools;
using System.Web.UI;

namespace SoccerWorld.Areas.User.Controllers
{
    public class HomeController : Controller
    {
        SoccerStoreEntities dbm = new SoccerStoreEntities();
        // GET: User/Home
        public ActionResult Index()
        {
            if (TempData["Error"] != null)
            {
                ViewBag.Error = 123;
                TempData.Remove("Error");
            }
           
            List<PhanLoai> phanloai = dbm.PhanLoais.ToList();
            List<PhanLoai> phanloai1 = new List<PhanLoai>();
            foreach (var x in phanloai)
            {
                if (checkAvibility(x.IDPhanLoai))
                    phanloai1.Add(x);
            }
            return View(phanloai1);
        }
        public ActionResult GetCardInfo(int id,int no)
        {
            List<SanPham> sp = dbm.SanPhams.Where(x => x.IDPhanLoai == id).Take(no).ToList();
            return PartialView(sp);
        }
        public ActionResult Category()
        {
            List<PhanLoai> phanloai = dbm.PhanLoais.ToList();
            return PartialView("_Category", phanloai);
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
        public ActionResult GetImgCard(int id)
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
        public ActionResult ListPageCategory(int id)
        {
            List<SanPham> sp = dbm.SanPhams.Where(x => x.IDPhanLoai == id).ToList().ToList();
            return View(sp);
        }
        [HttpGet]
        public ActionResult Registry()
        {
            return View();
        }       

        [HttpPost]
        public ActionResult Registry(string name, string pass, string email, string diachi )
        {
            if (Validate(pass,email,name)>0)
            {
                ViewBag.Error = Validate(pass, email, name);
                return View();
            }
            RegistNew(name, pass, email, diachi);
            //Response.Write("<script>alert('Đăng ký thành công. Mời đăng nhập');</script>");
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string name, string pass)
        {
            if (dbm.UserTBs.Any(x => x.Username == name))
            {
                var check = dbm.UserTBs.Where(x => x.Username == name).FirstOrDefault();
                if (check.Password == PassUtility.MD5Hash(pass))
                {
                    Session["UserName"] = check.Username;
                    return RedirectToAction("Index", "Home") ;
                }
            }
            TempData["Error"] = 1;
            return RedirectToAction("Index", "Home") ;
        }
        public ActionResult Logout()
        {
            Session.Remove("UserName");
            return RedirectToAction("Index", "Home");
        }


        private void RegistNew(string name, string pass, string email, string diachi)
        {
            UserTB newuser = new UserTB();
            newuser.Username = name;
            newuser.Password = PassUtility.MD5Hash(pass);
            dbm.UserTBs.Add(newuser);
            UserInfo info = new UserInfo();
            info.Username = name;
            info.Email = email;
            info.DiaChi = diachi;
            dbm.UserInfos.Add(info);
            dbm.SaveChanges();
        }
        private int Validate(string pass, string email,string name)
        {
            if (!PassUtility.ValidatePass(pass))
            {
                return 1;
            }
            if (!email.Contains("@"))
            {
                return 2;
            }
            if (dbm.UserTBs.Any(x => x.Username == name))
            {
                return 3;
            }
            return 0;

        }
        public ActionResult AddCart(int id, string kc, int mau)
        {

            List<DetailCarts> a = (List<DetailCarts>)Session["Cart"];
            if (a == null)
            {
                a = new List<DetailCarts>();
                Carts carts = new Carts(1, id, mau, kc);
                DetailCarts detail = makeCart(carts, 1);
                a.Add(detail);
            }
            else
            {

                Carts carts = new Carts(a.Count() + 1, id, mau, kc);
                var checkin = a.Where(x => x.product.Sp == id).FirstOrDefault();
                if (checkin != null)
                {
                    foreach (var x in a)
                    {
                        if (x.product.ID == checkin.product.ID)
                        {
                            x.quantity++;
                            break;
                        }
                    }

                }
                else
                {
                    DetailCarts detail = makeCart(carts, 1);
                    a.Add(detail);
                }


            }
            Session["Cart"] = a;
            return View(a);
        }

        public DetailCarts makeCart(Carts cart, int i)
        {
            DetailCarts dc = new DetailCarts();
            dc.product = cart;
            dc.quantity = 1;
            return dc;
        }
    }
}