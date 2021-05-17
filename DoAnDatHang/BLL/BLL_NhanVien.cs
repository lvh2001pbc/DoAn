using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnDatHang.BLL
{
    class BLL_NhanVien
    {
        private static BLL_NhanVien _Instance;
        public static BLL_NhanVien Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_NhanVien();
                }
                return _Instance;
            }
        }
        public List<NhanVien> getAllNhanVien()
        {
            var db = new DoAnEntities();
            return db.NhanViens.Select(s => s).ToList();
        }

        public NhanVien getNhanVienByID(int ID)
        {
            List<NhanVien> all = getAllNhanVien();
            foreach (NhanVien i in all)
            {
                if (i.MaNhanVien == ID)
                {
                    return i;
                }
            }
            return null;
        }
        public bool addNhanVien(NhanVien nvien)
        {
            try
            {
                using (var db = new DoAnEntities())
                {
                    db.NhanViens.Add(nvien);
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool editKhachHang(NhanVien nvien)
        {
            try
            {
                using (var db = new DoAnEntities())
                {
                    var result = db.NhanViens.Find(nvien.MaNhanVien);
                    if (result != null)
                    {
                        result.HoTen = nvien.HoTen;
                        result.SDT = nvien.SDT;
                        result.DiaChi = nvien.DiaChi;
                        result.Email = nvien.Email;
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
        public bool removeKhachHang(NhanVien nvien)
        {
            try
            {
                using (var db = new DoAnEntities())
                {
                    var result = db.NhanViens.Find(nvien.MaNhanVien);
                    if (result != null)
                    {
                        db.NhanViens.Remove(result);
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
    }
}
