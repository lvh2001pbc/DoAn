using DOAN.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAN.BLL
{
    class BLL_KH
    {
        private static BLL_KH _Instance;
        public static BLL_KH Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new BLL_KH();
                return _Instance;
            }
            set
            {

            }
        }
        public List<Khach> getAllKhach() 
        {
            var db = new QLKTX();
            return db.Khaches.ToList();
        }
        public Khach getKhachbyID(int ID) {
            List<Khach> all = getAllKhach();
            foreach(Khach i in all)
            {
                if (i.MaKhachHang == ID)
                {
                    return i;
                }
            }
            return null;
        }
        public bool UpdateUser(Khach khach)
        {
               using(var db = new QLKTX())
                {
                    var s = db.Khaches.Where(p =>p.MaKhachHang == khach.MaKhachHang).FirstOrDefault();
                if (s != null)
                {
                    s.HoTen = khach.HoTen;
                    s.SDT = khach.SDT;
                    s.DiaChi = khach.DiaChi;
                    s.Email = khach.Email;
                    db.SaveChanges();
                    return true;
                }
                    }
            return false;
                }
    }
}
