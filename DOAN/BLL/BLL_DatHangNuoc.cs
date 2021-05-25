using DOAN.DTO;
using DOAN.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAN.BLL
{
    class BLL_DatHangNuoc
    {
        private QLKTX db = new QLKTX();
        private static BLL_DatHangNuoc _Instance;
        public static BLL_DatHangNuoc Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new BLL_DatHangNuoc();
                return _Instance;
            }
            set
            {

            }
        }
        public void AddHoaDonNuoc(List<Nuoc> nuoc, int MaHD)
        {
            foreach (Nuoc i in nuoc)
            {
                NuocUong_HDDatHang Hoadon = new NuocUong_HDDatHang
                {
                    MaHDDatHang = MaHD,
                    MaNuocUong = i.MaNuoc,
                    Gia = i.Gia,
                    SoLuong = i.Soluong,
                };
                db.NuocUong_HDDatHang.Add(Hoadon);
                db.SaveChanges();
            }
        }
        public List<NuocUong_HDDatHang> getListHDNuocByHD(int ID)
        {
            return db.NuocUong_HDDatHang.Where(p => p.MaHDDatHang == ID).Select(p => p).ToList();
        }
        public void DellHoaDonNuoc(int MaHD)
        {
            foreach (NuocUong_HDDatHang i in db.NuocUong_HDDatHang)
            {
                if (i.MaHDDatHang == MaHD)
                {
                    db.NuocUong_HDDatHang.Remove(i);
                }
            }
            db.SaveChanges();
        }
    }
}
