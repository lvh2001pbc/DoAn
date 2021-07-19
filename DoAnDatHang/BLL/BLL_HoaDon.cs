using DoAnDatHang.View;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public void removeHoaDon(int id)
        {
            using (var db = new DoAnEntities())
            {
                var hd = db.HDDatHangs.Find(id);
                db.HDDatHangs.Remove(hd);
                db.SaveChanges();
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
        public List<HoaDonView> getAllHoaDonView(bool trangthai)
        {
            using (var db = new DoAnEntities())
            {
                return db.HDDatHangs.Where(s => s.TrangThai == trangthai).Select(s => new HoaDonView {
                    MaHoaDon = s.MaHDDatHang,
                    TenKhachHang = s.Khach.HoTen,
                    ThoiGian = s.ThoiGian,
                    ThanhTien = s.ThanhTien,
                    //TrangThai = s.TrangThai
                }).ToList();

            }
        }
        public List<HoaDonView> GetHoaDonViewContainsID(int id,bool trangthai)
        {
            using (var db = new DoAnEntities())
            {
                return db.HDDatHangs.Where(s => s.MaHDDatHang.ToString().Contains(id.ToString()) && s.TrangThai == trangthai).Select(s => new HoaDonView
                {
                    MaHoaDon = s.MaHDDatHang,
                    TenKhachHang = s.Khach.HoTen,
                    ThoiGian = s.ThoiGian,
                    ThanhTien = s.ThanhTien
                }).ToList();
            }
        }
        public List<HoaDonView> getHoaDonViewByTime(DateTime from, DateTime to)
        {
            using (var db = new DoAnEntities())
            {
                DateTime to1 = to.AddDays(1);
                return db.HDDatHangs.Where(s => s.ThoiGian.CompareTo(from.Date) >= 0 && s.ThoiGian.CompareTo(to1.Date) <= 0).Select(s => new HoaDonView
                {
                    MaHoaDon = s.MaHDDatHang,
                    TenKhachHang = s.Khach.HoTen,
                    ThoiGian = s.ThoiGian,
                    ThanhTien = s.ThanhTien,
                    //TrangThai = s.TrangThai
                }).ToList();

            }
        }
        public List<DoanhThu> getAllDoanhThuByDay(DateTime from, DateTime to)
        {
            using (var db = new DoAnEntities())
            {
                List<DoanhThu> a = new List<DoanhThu>();

                for (DateTime day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
                {
                    decimal tien = 0;
                    if (db.HDDatHangs.Where(s => DbFunctions.TruncateTime(s.ThoiGian) == day.Date).Count() > 0)
                    {
                        tien = db.HDDatHangs.Where(s => DbFunctions.TruncateTime(s.ThoiGian) == day.Date).Sum(s => s.ThanhTien);
                    }
                    a.Add(new DoanhThu
                    {
                        ThanhTien = Convert.ToDecimal(tien),
                        Ngay = day.Date.ToShortDateString()
                    });
                }
                return a;
            }
        }
        public List<DoanhThu> getAllDoanhThuByMonth(DateTime from, DateTime to)
        {
            using (var db = new DoAnEntities())
            {
                List<DoanhThu> a = new List<DoanhThu>();

                for (DateTime day = from.Date; day.Date <= to.Date; day = day.AddMonths(1))
                {
                    decimal tien = 0;
                    if (db.HDDatHangs.Where(s => s.ThoiGian.Month == day.Month && s.ThoiGian.Year == day.Year).Count() > 0)
                    {
                        tien = db.HDDatHangs.Where(s => s.ThoiGian.Month == day.Month && s.ThoiGian.Year == day.Year).Sum(s => s.ThanhTien);
                    }
                    a.Add(new DoanhThu
                    {
                        ThanhTien = Convert.ToDecimal(tien),
                        Ngay = day.Date.ToShortDateString()
                    });
                }
                return a;
            }
        }
        public List<DoanhThu> getAllDoanhThuByYear(DateTime from, DateTime to)
        {
            using (var db = new DoAnEntities())
            {
                List<DoanhThu> a = new List<DoanhThu>();

                for (DateTime day = from.Date; day.Date <= to.Date; day = day.AddMonths(1))
                {
                    decimal tien = 0;
                    if (db.HDDatHangs.Where(s => s.ThoiGian.Year == day.Year).Count() > 0)
                    {
                        tien = db.HDDatHangs.Where(s => s.ThoiGian.Year == day.Year).Sum(s => s.ThanhTien);
                    }
                    a.Add(new DoanhThu
                    {
                        ThanhTien = Convert.ToDecimal(tien),
                        Ngay = day.Date.ToShortDateString()
                    });
                }
                return a;
            }
        }
    }
}
