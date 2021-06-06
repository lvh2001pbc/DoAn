using DoAnDatHang.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnDatHang.BLL
{
    class BLL_NuocUong
    {
        private static BLL_NuocUong _Instance;
        public static BLL_NuocUong Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_NuocUong();
                }
                return _Instance;
            }
        }

        public List<NuocUongView> getAllNuocView()
        {
            using (var db1 = new DoAnEntities())
            {
                List<NuocUongView> mon = new List<NuocUongView>();
                foreach (var i in db1.NuocUongs.ToList())
                {
                    mon.Add(new NuocUongView
                    {
                        MaNuocUong = i.MaNuocUong,
                        TenNuocUong = i.TenNuocUong,
                        Gia = i.Gia,
                        SoLuongCon = i.SoLuongCon
                    });
                }
                return mon;
            }
        }
        public NuocUong getNuocByID(int id)
        {
            using (var db = new DoAnEntities())
            {
                return db.NuocUongs.Find(id);
            }
        }
        public List<NuocUong_HDDatHang> getNuocUong_HDDatHang(int id)
        {
            using(var db = new DoAnEntities())
            {
                return db.NuocUong_HDDatHang.Where(s => s.MaHDDatHang == id).ToList();
            }
        }
        public bool addNuocUong_HDDatHang(List<NuocUong_HDDatHang> list)
        {
            try
            {
                using (var db = new DoAnEntities())
                {
                    db.NuocUong_HDDatHang.AddRange(list);
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
    }
}
