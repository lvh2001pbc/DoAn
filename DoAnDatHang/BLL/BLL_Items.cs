using DoAnDatHang.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnDatHang.BLL
{
    class BLL_Items
    {
        private static BLL_Items _Instance;
        public static BLL_Items Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_Items();
                }
                return _Instance;
            }
        }
        public List<ItemView> getAllItemViewsbyHD(int ID)
        {
            using(var db = new DoAnEntities())
            {
                List<ItemView> monan = db.MonAn_HDDatHang.Where(s => s.MaHDDHang == ID).Select(s => new ItemView {
                    TenMonAn = s.MonAn.TenMonAn,
                    Gia = s.Gia,
                    SoLuong = s.SoLuong,
                    ThanhTien = s.Gia*s.SoLuong
                }).ToList();
                List<ItemView> nuocuong = db.NuocUong_HDDatHang.Where(s => s.MaHDDatHang == ID).Select(s => new ItemView
                {
                    TenMonAn = s.NuocUong.TenNuocUong,
                    Gia = s.Gia,
                    SoLuong = s.SoLuong,
                    ThanhTien = s.Gia * s.SoLuong
                }).ToList();
                monan.AddRange(nuocuong);
                return monan;
            }
        }
        public List<DrinkItemView> getAllDrinkViewbyHD(int ID)
        {
            using (var db = new DoAnEntities()) { }
            return null;
        }
    }
}
