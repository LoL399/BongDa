using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoccerWorld.Models;

namespace SoccerWorld.DAO
{
    public class Produc
    {
        SoccerStoreEntities dbm = new SoccerStoreEntities();
        public SanPham GetProduct(int id)
        {

            return dbm.SanPhams.Where(x => x.ID.ToString() == id.ToString()).FirstOrDefault();
        }
        public MauSac GetColor(int id)
        {

            return dbm.MauSacs.Where(x => x.IDMau.ToString() == id.ToString()).FirstOrDefault();
        }
        public Kichco GetSize(int id)
        {

            return dbm.Kichcoes.Where(x => x.IDKichCo.ToString() == id.ToString()).FirstOrDefault();
        }
        public List<SanPham> GetListSP(int id)
        {
            return dbm.SanPhams.Where(x => x.IDPhanLoai == id).ToList();
        }
        public PhanLoai GetOneCategory(int id)
        {
            return dbm.PhanLoais.FirstOrDefault(x => x.IDPhanLoai == id);
        }
        public List<PhanLoai> GetCategory()
        {
            return dbm.PhanLoais.ToList();
        }
        public List<PhanLoai> GetCategoryNotEmpty()
        {
            List<PhanLoai> phanloai = dbm.PhanLoais.ToList();
            List<PhanLoai> phanloai1 = new List<PhanLoai>();
            foreach (var x in phanloai)
            {
                if (checkAvibility(x.IDPhanLoai))
                    phanloai1.Add(x);
            }
            return phanloai1;
        }
        public List<SanPham> GetCardInfo(int id, int no)
        {
            return dbm.SanPhams.Where(x => x.IDPhanLoai == id).Take(no).ToList();
        }
        public List<Kichco> GetListSize(int id)
        {
            List<CTKichCo> lstkichco = dbm.CTKichCoes.Where(x => x.IDSP == id && x.TinhTrang == true).ToList();
            List<Kichco> kichco = new List<Kichco>();
            foreach (var x in lstkichco)
            {
                kichco.Add(dbm.Kichcoes.Where(o => o.IDKichCo == x.IDKC).FirstOrDefault());
            }
            return kichco;
        }
        public List<HinhAnh> GetImg(int id)
        {
            return dbm.HinhAnhs.Where(x => x.IDSP == id).ToList();
        }
        public List<MauSac> GetListColor(int id)
        {
            List<ChiTietMau> lstmau = dbm.ChiTietMaus.Where(x => x.IDSP == id).ToList();
            List<MauSac> mau = new List<MauSac>();
            foreach (var x in lstmau)
            {
                mau.Add(dbm.MauSacs.Where(o => o.IDMau == x.IDMau).FirstOrDefault());
            }
            return mau;
        }
        //
        public bool checkAvibility(int id)
        {
            return dbm.SanPhams.Any(x => x.IDPhanLoai == id);

        }
        

    }
}