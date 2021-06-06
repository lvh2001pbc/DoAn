using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnDatHang.View
{
    class HoaDonView
    {
        public static int CompareByNgay(HoaDonView a, HoaDonView b)
        {
            return a.ThoiGian.CompareTo(b.ThoiGian);
        }
        public static int CompareByTongTien(HoaDonView a, HoaDonView b)
        {
            return Convert.ToInt32(a.ThanhTien - b.ThanhTien);
        }
        public int MaHoaDon { get; set; }
        public string TenKhachHang { get; set; }
        public System.DateTime ThoiGian { get; set; }
        public decimal ThanhTien { get; set; }
        //public bool TrangThai { get; set; }
    }
}
