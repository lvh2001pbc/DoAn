using DOAN.DTO;
using DOAN.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAN.BLL
{
    class BLL_DatHangMonAn
    {
        private QLKTX db = new QLKTX();
        private static BLL_DatHangMonAn _Instance;
        public static BLL_DatHangMonAn Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new BLL_DatHangMonAn();
                return _Instance;
            }
            set
            {

            }
        }
        public void AddHoaDonMon(List<Mon> mon,int MaHD)
        {
            foreach (Mon i in mon)
            {
                MonAn_HDDatHang Hoadon = new MonAn_HDDatHang
                {
                    MaMonAn = i.MaMonAn,
                    MaHDDHang = MaHD,
                    Gia = i.Gia,
                    SoLuong = i.Soluong,
                };
                db.MonAn_HDDatHang.Add(Hoadon);
                db.SaveChanges();
            }
        }
        public List<MonAn_HDDatHang> getListHDMonByHD(int ID)
        {
            return db.MonAn_HDDatHang.Where(p => p.MaHDDHang == ID).Select(p => p).ToList();
        }
        public void DellHoaDonMon(int MaHD)
        {
            foreach(MonAn_HDDatHang i in db.MonAn_HDDatHang)
            {
                if (i.MaHDDHang == MaHD)
                {
                    db.MonAn_HDDatHang.Remove(i);
                }
            }
            db.SaveChanges();
        }
    }
}
