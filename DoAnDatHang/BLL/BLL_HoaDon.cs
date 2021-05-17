using DoAnDatHang.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnDatHang.BLL
{
    class BLL_HoaDon
    {

        private static BLL_HoaDon _Instance;
        public static BLL_HoaDon Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_HoaDon();
                }
                return _Instance;
            }
        }
        public int createHoaDon(HDDatHang hoadon)
        {
            using (var db = new DoAnEntities())
            {
                HDDatHang p = db.HDDatHangs.Add(hoadon);
                db.SaveChanges();
                return p.MaHDDatHang;
            }
        }
        public List<HoaDonView> getAllHoaDonView()
        {
            using (var db = new DoAnEntities())
            {
                return db.HDDatHangs.Select(s => new HoaDonView {
                    MaHoaDon = s.MaHDDatHang,
                    TenKhachHang = s.Khach.HoTen,
                    TenNhanVien = s.NhanVien.HoTen,
                    ThoiGian = s.ThoiGian,
                    ThanhTien = s.ThanhTien,
                    //TrangThai = s.TrangThai
                }).ToList();

            }
        }
        public List<HoaDonView> getHoaDonViewByTime(DateTime from, DateTime to)
        {
            using (var db = new DoAnEntities())
            {
                return db.HDDatHangs.Where(s => s.ThoiGian.CompareTo(from) >= 0 && s.ThoiGian.CompareTo(to) <= 0).Select(s => new HoaDonView
                {
                    MaHoaDon = s.MaHDDatHang,
                    TenKhachHang = s.Khach.HoTen,
                    TenNhanVien = s.NhanVien.HoTen,
                    ThoiGian = s.ThoiGian,
                    ThanhTien = s.ThanhTien,
                    //TrangThai = s.TrangThai
                }).ToList();

            }
        } 
    }
}
