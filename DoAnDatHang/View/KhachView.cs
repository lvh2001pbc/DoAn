using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnDatHang.View
{
    class KhachView
    {
        public static int CompareByMa(KhachView a, KhachView b)
        {
            return a.MaKhachHang - a.MaKhachHang;
        }
        public static int CompareByTen(KhachView a, KhachView b)
        {
            return a.HoTen.CompareTo(b.HoTen);
        }
        public int MaKhachHang { get; set; }
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public int CMND { get; set; }
    }
}
