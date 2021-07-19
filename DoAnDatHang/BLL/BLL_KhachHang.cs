using DoAnDatHang.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnDatHang.BLL
{
    class BLL_KhachHang
    {
        private static BLL_KhachHang _Instance;
        public static BLL_KhachHang Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_KhachHang();
                }
                return _Instance;
            }
        }
        public List<KhachView> getAllKhach()
        {
            using (var db = new DoAnEntities())
            {
                return db.Khaches.Select(s => new KhachView
                {
                    HoTen = s.HoTen,
                    SDT = s.SDT,
                    CMND = s.CMND,
                    DiaChi = s.DiaChi,
                    Email = s.Email,
                    MaKhachHang = s.MaKhachHang
                }).ToList();
            }
        }

        public Khach getKhachByID(int ID)
        {
            using(var db = new DoAnEntities())
            {
                return db.Khaches.Find(ID);
            }
        }
        public bool addKhachHang(Khach khach)
        {
            try {
                using (var db = new DoAnEntities())
                {
                    db.Khaches.Add(khach);
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool editKhachHang(Khach khach)
        {
            try
            {
                using (var db = new DoAnEntities())
                {
                    var result = db.Khaches.Find(khach.MaKhachHang);
                    if (result != null)
                    {
                        result.HoTen = khach.HoTen;
                        result.SDT = khach.SDT;
                        result.DiaChi = khach.DiaChi;
                        result.Email = khach.Email;
                        db.SaveChanges();
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool removeKhachHang(Khach khach)
        {
            try
            {
                using (var db = new DoAnEntities())
                {
                    var result = db.Khaches.Find(khach.MaKhachHang);
                    if (result != null)
                    {
                        db.Khaches.Remove(result);
                        db.SaveChanges();
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        public List<KhachView> getKhach(int id = 0, string name = "")
        {
            using( var db = new DoAnEntities())
            {
                return db.Khaches.Where(s =>  (s.HoTen.Contains(name)) && (id == 0 || s.MaKhachHang == id)).Select(s => new KhachView
                {
                    HoTen = s.HoTen,
                    SDT = s.SDT,
                    CMND = s.CMND,
                    DiaChi = s.DiaChi,
                    Email = s.Email,
                    MaKhachHang = s.MaKhachHang
                }).ToList();
            }
        }
    }
}
