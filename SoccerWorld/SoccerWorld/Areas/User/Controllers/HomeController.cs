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
using System.Net;

namespace SoccerWorld.Areas.User.Controllers
{
    public class HomeController : Controller
    {
        Produc db = new Produc();


        // GET: User/Home
        public ActionResult Index()
        {
            if (TempData["Error"] != null)
            {
                ViewBag.Error = 123;
                TempData.Remove("Error");
            }


            return View(db.GetCategoryNotEmpty());
        }
        public ActionResult GetCardInfo(int id, int no)
        {
            return PartialView(db.GetCardInfo(id, no));
        }
        public ActionResult Category()
        {

            return PartialView("_Category", db.GetCategory());
        }

        public ActionResult Product(int id)
        {
            SanPham sp = db.GetProduct(id);
            var phanloai = db.GetOneCategory(sp.IDPhanLoai);
            ViewBag.IDPL = phanloai.IDPhanLoai;
            ViewBag.PhanLoai = phanloai.TenPhanLoai.ToString();
            return View(sp);
        }
        public ActionResult GetSize(int id)
        {

            return PartialView(db.GetListSize(id));
        }
        public ActionResult GetImg(int id)
        {
            return PartialView(db.GetImg(id));
        }
        public ActionResult GetImgCard(int id)
        {
            return PartialView(db.GetImg(id));
        }


        public ActionResult GetMau(int id)
        {

            return PartialView(db.GetListColor(id));
        }

        public ActionResult ListPageCategory(int id)
        {
            return View(db.GetListSP(id));
        }
        [HttpGet]
        public ActionResult Registry()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registry(string name, string pass, string email, string diachi)
        {
            UserDB udb = new UserDB();
            int val = udb.Validate(pass, email, name);
            if (val > 0)
            {
                ViewBag.Error = val;
                return View();
            }
            udb.RegistNew(name, pass, email, diachi);
            //Response.Write("<script>alert('Đăng ký thành công. Mời đăng nhập');</script>");
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string name, string pass)
        {
            UserDB udb = new UserDB();
            if (udb.checkSignin(name, pass))
            {
                Session["UserName"] = name;
                return RedirectToAction("Index", "Home");
            }


            TempData["Error"] = 1;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            Session.Remove("UserName");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Product(int id, int kc, int mau)
        {

            List<DetailCarts> a = (List<DetailCarts>)Session["Cart"];
            var sp = db.GetProduct(id);
            var mausac = db.GetColor(mau);
            var kichco = db.GetSize(kc);
            if (a == null)
            {
                a = new List<DetailCarts>();
                Carts carts = new Carts(1, sp, mausac, kichco);
                DetailCarts detail = makeCart(carts, 1);
                a.Add(detail);
            }
            else
            {

                Carts carts = new Carts(a.Count() + 1, sp, mausac, kichco);
                var checkin = a.Where(x => x.product.Sp.ID == id && x.product.Kc.IDKichCo == kc && x.product.Ms.IDMau == mau).FirstOrDefault();
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
            ViewBag.Toast = 1;
            var phanloai = db.GetOneCategory(sp.IDPhanLoai);
            ViewBag.IDPL = phanloai.IDPhanLoai;
            ViewBag.PhanLoai = phanloai.TenPhanLoai.ToString();
            return View(sp);
        }
        public ActionResult showCart()
        {
            List<DetailCarts> a = (List<DetailCarts>)Session["Cart"];
            return PartialView(a);
        }
        public ActionResult CountCart()
        {

            if (Session["Cart"] == null)
            {
                ViewBag.count = 0;
            }
            else
            {
                List<DetailCarts> a = (List<DetailCarts>)Session["Cart"];
                ViewBag.count = a.Count();
            }
            return PartialView();
        }
        public DetailCarts makeCart(Carts cart, int i)
        {
            DetailCarts dc = new DetailCarts();
            dc.product = cart;
            dc.quantity = 1;
            return dc;
        }
        public ActionResult EditCarts() {
            List<DetailCarts> a = (List<DetailCarts>)Session["Cart"];
            return View(a);
        }
        public ActionResult RemoveItem(int id)
        {
            List<DetailCarts> a = (List<DetailCarts>)Session["Cart"];
            a.RemoveAll(x=>x.product.ID==id);
            return RedirectToAction("EditCarts");

        }
        
    }
}