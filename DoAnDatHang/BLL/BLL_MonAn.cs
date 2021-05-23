using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnDatHang.BLL
{
    class BLL_MonAn
    {
        private DoAnEntities db = new DoAnEntities();
        private static BLL_MonAn _Instance;
        public static BLL_MonAn Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_MonAn();
                }
                return _Instance;
            }
        }
        public List<MonAn> getAllMonAn()
        {
            return db.MonAns.ToList();
        }
        public MonAn getMonAnByID(int Id)
        {
            return db.MonAns.Find(Id);
        }
        public bool addMonAnHDDatHang(List<MonAn_HDDatHang> list)
        {
            try
            {
                using (var db = new DoAnEntities())
                {
                    db.MonAn_HDDatHang.AddRange(list);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public void removeMonAnHDDatHangbyID(int id)
        {
            using(var db = new DoAnEntities())
            {
                foreach (var i in GetMonAn_HDDatHang(id))
                {
                    db.MonAn_HDDatHang.Attach(i);
                    db.MonAn_HDDatHang.Remove(i);
                }
                db.SaveChanges();
            }
        }
        public bool editMonAnHDDatHang(List<MonAn_HDDatHang> list)
        {
            try
            {
                using (var db = new DoAnEntities())
                {
                    removeMonAnHDDatHangbyID(list[0].MaHDDHang);
                    db.MonAn_HDDatHang.AddRange(list);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public List<MonAn_HDDatHang> GetMonAn_HDDatHang(int id)
        {
            using (var db = new DoAnEntities())
            {
                return db.MonAn_HDDatHang.Where(s => s.MaHDDHang == id).ToList();
            }
        }
    }
}
