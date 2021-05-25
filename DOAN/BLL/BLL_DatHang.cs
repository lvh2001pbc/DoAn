using DOAN.DTO;
using DOAN.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAN.BLL
{
    class BLL_DatHang
    {
        private QLKTX db = new QLKTX();
        private static BLL_DatHang _Instance;
        public static BLL_DatHang Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new BLL_DatHang();
                return _Instance;
            }
            set
            {

            }
        }
        public HDDatHang AddDonHang(int MaKhachHang)
        {
            HDDatHang hd = new HDDatHang();
            hd.MaKhachHang = MaKhachHang;
            hd.ThoiGian = DateTime.Now;
            db.HDDatHangs.Add(hd);
            db.SaveChanges();
            return hd;
        }
        public List<HDDatHang> getAllHD()
        {
            return db.HDDatHangs.ToList();
        }
        public List<HDDatHang> GetListHDDatHangbyKhach(int ID)
        {
            List<HDDatHang> all = getAllHD();
            List<HDDatHang> khach = new List<HDDatHang>();
            foreach(HDDatHang i in all)
            {
                if(i.MaKhachHang== ID)
                {
                    khach.Add(i);
                }
            }
            return khach;
        }
        public HDDatHang getHDbyMaHD(int MaHD)
        {
            foreach(HDDatHang i in getAllHD())
            {
                if(i.MaHDDatHang==MaHD)
                {
                    return i;
                }
            }
            return null;
        }

        public void DellHoaDon(int MaHD)
        {
            BLL_DatHangMonAn.Instance.DellHoaDonMon(MaHD);
            BLL_DatHangNuoc.Instance.DellHoaDonNuoc(MaHD);
            foreach (HDDatHang i in db.HDDatHangs)
            {
                if (i.MaHDDatHang == MaHD)
                {
                    db.HDDatHangs.Remove(i);
                }
            }
            db.SaveChanges();
        }
        public List<Hoadon> HoadonChiTiet(int MaHD)
        {
            List<Hoadon> hd = new List<Hoadon>();
            List<MonAn_HDDatHang> mon = BLL_DatHangMonAn.Instance.getListHDMonByHD(MaHD);
            List<NuocUong_HDDatHang> nuoc = BLL_DatHangNuoc.Instance.getListHDNuocByHD(MaHD);
            foreach(MonAn_HDDatHang i in mon)
            {
                hd.Add(new Hoadon
                {
                    Ten = BLL_MONAN.Instance.getMonAnbyID(i.MaMonAn).TenMonAn.ToString(),
                    Gia = Convert.ToInt32((BLL_MONAN.Instance.getMonAnbyID(i.MaMonAn).Gia)),
                    Soluong= i.SoLuong,
                    Thanhtien=Convert.ToInt32(i.ThanhTien)
                }); 
            }
            foreach(NuocUong_HDDatHang i in nuoc)
            {
                hd.Add(new Hoadon
                {
                    Ten = BLL_NUOCUONG.Instance.getNuocbyID(i.MaNuocUong).TenNuocUong.ToString(),
                    Gia = Convert.ToInt32(BLL_NUOCUONG.Instance.getNuocbyID(i.MaNuocUong).Gia),
                    Soluong = i.SoLuong,
                    Thanhtien = Convert.ToInt32(i.ThanhTien)
                });
            }
            return hd;
        }
    }
}
